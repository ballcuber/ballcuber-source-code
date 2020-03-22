#include <GPIO.h>
#include "EncoderStepper.h"
#include <Servo.h>
#include <MultiStepper.h>
#include <AccelStepper.h>
#include <Sharer.h>
#include "RAMPS.h"
#include <Wire.h>

#define AMS5600_I2C_ADDRESS 0x36

#define STEPPER_COUNT	5


// définitions des steppers
AccelStepper E0Stepper(AccelStepper::DRIVER, E0_STEP_PIN, E0_DIR_PIN);
AccelStepper E1Stepper(AccelStepper::DRIVER, E1_STEP_PIN, E1_DIR_PIN);
AccelStepper XStepper(AccelStepper::DRIVER, X_STEP_PIN, X_DIR_PIN);
AccelStepper YStepper(AccelStepper::DRIVER, Y_STEP_PIN, Y_DIR_PIN);
AccelStepper ZStepper(AccelStepper::DRIVER, Z_STEP_PIN, Z_DIR_PIN);

AccelStepper * Steppers[STEPPER_COUNT] = { &E0Stepper,&E1Stepper,&XStepper,&YStepper,&ZStepper };


#define GENERIC_ALL_STEPPER(name, ...)				\
	for (int i = 0; i < STEPPER_COUNT; i++) {		\
		if (((1<<i) & mask)) {						\
			Steppers[i]->name(__VA_ARGS__);			\
		}											\
	}												\


#define GENERIC_ONE_STEPPER(name, ...)				\
if ((iStepper < 0) || (iStepper >= STEPPER_COUNT)) {	\
	return 0;										\
}													\
else {												\
	return Steppers[iStepper]->name(__VA_ARGS__);	\
}													\


void stop(int mask) {
	GENERIC_ALL_STEPPER(stop)
}

void setMaxSpeed(int mask, float speed) {
	GENERIC_ALL_STEPPER(setMaxSpeed, speed)
}

void setAcceleration(int mask, float accel) {
	GENERIC_ALL_STEPPER(setAcceleration, accel)
}

void moveTo(int mask, long absolute) {
	GENERIC_ALL_STEPPER(moveTo, absolute)
}

void move(int mask, long relative) {
	GENERIC_ALL_STEPPER(move, relative)
}

float maxSpeed(int iStepper) {
	GENERIC_ONE_STEPPER(maxSpeed);
}

void setSpeed(int mask, float speed) {
	GENERIC_ALL_STEPPER(setSpeed, speed);
}

void setCurrentPositionAndContinue(int mask, long newPosition) {
	GENERIC_ALL_STEPPER(setCurrentPosition, newPosition, false);
}

/*
float speed(int iStepper) {
	GENERIC_ONE_STEPPER(speed);
}
*/

long distanceToGo(int iStepper) {
	GENERIC_ONE_STEPPER(distanceToGo);
}

long targetPosition(int iStepper) {
	GENERIC_ONE_STEPPER(targetPosition);
}

long currentPosition(int iStepper) {
	GENERIC_ONE_STEPPER(currentPosition);
}

void setCurrentPosition(int mask, long position) {
	GENERIC_ALL_STEPPER(setCurrentPosition, position);
}

void disableOutputs(int mask) {
	stop(mask);
	GENERIC_ALL_STEPPER(disableOutputs);
}

#define MIN   0
#define MAX  3200

#define TH_MIN	(MIN+MAX)/3

#define TH_MAX	2*TH_MIN

volatile int long _encoderOffset = 0;

volatile long nbTurn = 0;
volatile long EncoderPositionInTurn;

volatile int errors = 0;

volatile int A5Value;
volatile int A5ValueErr;

int A5ValueS[3];
int idx = 0;

void enableOutputs(int mask) {
	GENERIC_ALL_STEPPER(enableOutputs);

	/*
	delay(500);

	if (mask && (1 << 4)) {
		_encoderOffset = Steppers[4]->currentPosition() - EncoderPositionInTurn;
		nbTurn = 0;
	}
	*/
}

bool isRunning(int iStepper) {
	GENERIC_ONE_STEPPER(isRunning);
}

void setMinPulseWidth(int mask, unsigned int minWidth) {
	GENERIC_ALL_STEPPER(setMinPulseWidth, minWidth);
}


// Registre 0x08 recoit 160 (PWM 460Hz)
// Registre 0xFF recoit 0x40 pour le burn des settings
// les autres registres sont à 0
// reboot pour prise en compte des valeurs
int readAMSRegister(byte reg) {
	Wire.beginTransmission(AMS5600_I2C_ADDRESS);
	Wire.write(reg);
	Wire.endTransmission();

	Wire.requestFrom(AMS5600_I2C_ADDRESS, 1);

//if (Wire.available() <= 1) {
		return Wire.read();
//	}

	if (Wire.available() == 0) {
		return -1;
	}
	else {
		return Wire.read();
	}
}

uint8_t writeAMSRegister(byte reg, byte val) {
	Wire.beginTransmission(AMS5600_I2C_ADDRESS);
	Wire.write(reg);
	Wire.write(val);
	return Wire.endTransmission();
}


bool blockingMove(byte m1, long r1, byte m2, long r2) {
	bool useM1 = m1 < STEPPER_COUNT;
	bool useM2 = m2 < STEPPER_COUNT;

	if (useM1) Steppers[m1]->move(r1);

	if (useM2) Steppers[m2]->move(r2);

	while ((useM1 && Steppers[m1]->run()) | (useM2 && Steppers[m2]->run()));

	return true;
}

void blockingMove2(long r1) {
	Steppers[3]->move(r1);

	Steppers[4]->move(r1);

	bool moving = true;
	while (moving) {
		moving = Steppers[3]->run();
		moving |= Steppers[4]->run();
	}
}

volatile float speed[STEPPER_COUNT];
volatile long position[STEPPER_COUNT];
volatile bool enabled[STEPPER_COUNT];

int state;
long ellapsedMillis;
/*

volatile unsigned long _lastUpMicros;
volatile int _cnt;

volatile long EncoderPosition;

float AEncoder = 1.556;
int BEncoder = 0; // 143;

volatile long _prevEncoderPosition;
*/


void setup() {
	Sharer.init(115200);

	E0Stepper.setEnablePin(E0_ENABLE_PIN);
	E1Stepper.setEnablePin(E1_ENABLE_PIN);
	XStepper.setEnablePin(X_ENABLE_PIN);
	YStepper.setEnablePin(Y_ENABLE_PIN);
	ZStepper.setEnablePin(Z_ENABLE_PIN);

	
	for (int i = 0; i < STEPPER_COUNT; i++) {
		Steppers[i]->setPinsInverted(false, false, true);	// invert enable pin (LOW or power on)
		Steppers[i]->setMaxSpeed(100);
		Steppers[i]->setAcceleration(140000);
		Steppers[i]->disableOutputs();
	}

	
	Sharer_ShareVoid(blockingMove2, long, r1);
	Sharer_ShareVoid(stop, int, mask);
	Sharer_ShareFunction(bool, blockingMove, byte, m1, long, r1, byte, m2, long, r2);
	Sharer_ShareVoid(setMaxSpeed, int, mask, float, speed);
	Sharer_ShareVoid(setAcceleration, int, mask, float, accel);
	Sharer_ShareVoid(moveTo, int, mask, long, absolute);
	Sharer_ShareVoid(move, int, mask, long, relative);
	Sharer_ShareFunction(float, maxSpeed, int, iStepper);
	Sharer_ShareVoid(setSpeed, int, mask, float, speed);
	//Sharer_ShareFunction(float, speed, int, iStepper);
	Sharer_ShareFunction(long, distanceToGo, int, iStepper);
	Sharer_ShareFunction(long, targetPosition, int, iStepper);
	Sharer_ShareFunction(long, currentPosition, int, iStepper);
	Sharer_ShareVoid(setCurrentPosition, int, mask, long, position);
	Sharer_ShareVoid(disableOutputs, int, mask);
	Sharer_ShareVoid(enableOutputs, int, mask);
	Sharer_ShareFunction(bool, isRunning, int, iStepper);
	Sharer_ShareVoid(setMinPulseWidth, int, mask, uint16_t, minWidth);

	Sharer_ShareFunction(int, readAMSRegister, byte, reg);
	Sharer_ShareFunction(int, writeAMSRegister, byte, reg, byte, val);

	Sharer_ShareVoid(setCurrentPositionAndContinue, int, mask, long, newPosition);

	/*
	Sharer.variableList.variables[Sharer.variableList.count].name = PSTR("pos1");

	Sharer.variableList.variables[Sharer.variableList.count].value.pointer = (void*)&pos1;
	Sharer.variableList.variables[Sharer.variableList.count].value.size = 1;
	Sharer.variableList.variables[Sharer.variableList.count].value.type = SharerClass::_SharerFunctionArgType::Typelong;
	Sharer.variableList.count++;

	Sharer.variableList.variables[Sharer.variableList.count].name = PSTR("speed1");

	Sharer.variableList.variables[Sharer.variableList.count].value.pointer = (void*)&speed1;
	Sharer.variableList.variables[Sharer.variableList.count].value.size = 1;
	Sharer.variableList.variables[Sharer.variableList.count].value.type = SharerClass::_SharerFunctionArgType::Typefloat;
	Sharer.variableList.count++;

	Sharer.variableList.variables[Sharer.variableList.count].name = PSTR("test");

	Sharer.variableList.variables[Sharer.variableList.count].value.pointer = (void*)&test;
	Sharer.variableList.variables[Sharer.variableList.count].value.size = 1;
	Sharer.variableList.variables[Sharer.variableList.count].value.type = SharerClass::_SharerFunctionArgType::Typefloat;
	Sharer.variableList.count++;

	Sharer.variableList.variables[Sharer.variableList.count].name = PSTR("test2");

	Sharer.variableList.variables[Sharer.variableList.count].value.pointer = (void*)&test2;
	Sharer.variableList.variables[Sharer.variableList.count].value.size = 1;
	Sharer.variableList.variables[Sharer.variableList.count].value.type = SharerClass::_SharerFunctionArgType::Typefloat;
	Sharer.variableList.count++;

	Sharer.variableList.variables[Sharer.variableList.count].name = PSTR("testSum");

	Sharer.variableList.variables[Sharer.variableList.count].value.pointer = (void*)&testSum;
	Sharer.variableList.variables[Sharer.variableList.count].value.size = 1;
	Sharer.variableList.variables[Sharer.variableList.count].value.type = SharerClass::_SharerFunctionArgType::Typefloat;
	Sharer.variableList.count++;
	*/
	
		
	Sharer_ShareVariable(float, speed[0]);
	Sharer_ShareVariable(float, speed[1]);
	Sharer_ShareVariable(float, speed[2]);
	Sharer_ShareVariable(float, speed[3]);
	Sharer_ShareVariable(float, speed[4]);

	Sharer_ShareVariable(long, position[0]);
	Sharer_ShareVariable(long, position[1]);
	Sharer_ShareVariable(long, position[2]);
	Sharer_ShareVariable(long, position[3]);
	Sharer_ShareVariable(long, position[4]);

	Sharer_ShareVariable(bool, enabled[0]);
	Sharer_ShareVariable(bool, enabled[1]);
	Sharer_ShareVariable(bool, enabled[2]);
	Sharer_ShareVariable(bool, enabled[3]);
	Sharer_ShareVariable(bool, enabled[4]);
	/*
	Sharer_ShareVariable(int, A5Value);


	//Sharer_ShareVariable(int, _currentEncoderPose);

	Sharer_ShareVariable(long, EncoderPosition);
	Sharer_ShareVariable(long, EncoderPositionInTurn);
	Sharer_ShareVariable(long, nbTurn);
	Sharer_ShareVariable(int, errors);
	Sharer_ShareVariable(long, _prevEncoderPosition);
	Sharer_ShareVariable(int, A5ValueErr);
	*/

	
	//Sharer_ShareVariable(int, MIN);
	//Sharer_ShareVariable(int, MAX);

	//Sharer_ShareVariable(float, AEncoder);
	//Sharer_ShareVariable(int, BEncoder);
	
	pinMode(13, OUTPUT);

	state = false;
	ellapsedMillis = millis();

	//pinMode(18, INPUT_PULLUP);

	/*
	attachInterrupt(digitalPinToInterrupt(2), OnChange2, CHANGE);
	attachInterrupt(digitalPinToInterrupt(3), OnChange3, CHANGE);
	attachInterrupt(digitalPinToInterrupt(18), OnChange18, CHANGE);
	attachInterrupt(digitalPinToInterrupt(19), OnChange19, CHANGE);
	attachInterrupt(digitalPinToInterrupt(20), OnChange20, CHANGE);
	attachInterrupt(digitalPinToInterrupt(21), OnChange21, CHANGE);

	Wire.begin();
	*/
}



volatile int _encoderSteps;
volatile int _lastAbsoluteSteps;
volatile int _turns = 0;

#define STEPS_PER_MOTOR_ROTATION 3200

/*
#define ENCODER_CLAMP_MICROS 68
#define ENCODER_PERIOD_MICROS 2174

void OnChange2() {
	OnChange(2);
}

void OnChange3() {
	OnChange(3);
}

GPIO<BOARD::D18> sng;


void OnChange18() {
	if (sng) {
		_lastUpMicros = micros();
	}
	else {
		_currentEncoderPose = micros() - _lastUpMicros;
	}
	//OnChange(18);
}

void OnChange19() {
	OnChange(19);
}

void OnChange20() {
	OnChange(20);
}

void OnChange21() {
	OnChange(21);
}



void OnChange(int pin) {

	/*
	if (digitalRead(pin)) {
		_lastUpMicros = micros();
	}
	else {
		int absoluteSteps = constrain(STEPS_PER_MOTOR_ROTATION * (micros() - _lastUpMicros - ENCODER_CLAMP_MICROS) / (ENCODER_PERIOD_MICROS - 2 * ENCODER_CLAMP_MICROS), 0, STEPS_PER_MOTOR_ROTATION);

		int diff = _lastAbsoluteSteps - absoluteSteps;
		if (abs(diff) > STEPS_PER_MOTOR_ROTATION / 4) _turns += (diff > 0) ? 1 : -1;
		_lastAbsoluteSteps = absoluteSteps;

		_encoderSteps = _turns * STEPS_PER_MOTOR_ROTATION + absoluteSteps;
	}
	
}
*/


#define STEPS_PER_ROTATION	(200 * 16 )

void loop() {
	for (int i = 0; i < STEPPER_COUNT; i++) {
		Steppers[i]->run();
		speed[i] = Steppers[i]->speed();
		position[i] = Steppers[i]->currentPosition();
	}

	enabled[0] = !digitalRead(E0_ENABLE_PIN);
	enabled[1] = !digitalRead(E1_ENABLE_PIN);
	enabled[2] = !digitalRead(X_ENABLE_PIN);
	enabled[3] = !digitalRead(Y_ENABLE_PIN);
	enabled[4] = !digitalRead(Z_ENABLE_PIN);

	Sharer.run();

	if ((millis() - ellapsedMillis) > 1000) {
		ellapsedMillis = millis();
		digitalWrite(13, state);
		state = !state;
	}

	/*

	A5ValueS[idx] =  analogRead(A5);

	A5Value = (A5ValueS[0] + A5ValueS[1] + A5ValueS[2]) / 3;



	//EncoderPositionInTurn = ((double)(constrain(_currentEncoderPose, MIN, MAX) - MIN) * STEPS_PER_ROTATION) / ((double)(MAX - MIN));
	EncoderPositionInTurn = (long)(3199.0F * (float)A5Value / 1023.0F);
	

	if ((_prevEncoderPosition < 340) && (A5ValueS[(idx + 1) % 3] >680)) {
		nbTurn = nbTurn -1 ;
	}
	else if ((_prevEncoderPosition > 680) && (A5ValueS[(idx + 1) % 3] < 340)) {
		nbTurn = nbTurn + 1;
	}

	EncoderPosition = nbTurn * STEPS_PER_ROTATION + EncoderPositionInTurn + _encoderOffset;
	
	if (enabled[4] /*&& Steppers[4]->speed() != 0.0f) {
		int err = position[4] - EncoderPosition;
		if (err > 3*16) {
			//Steppers[4]->setCurrentPosition(Steppers[4]->currentPosition() - 64 * (int)(err/64), false);
			errors++;
			A5ValueErr = A5Value;
		}
		else if (err < -3*16) {
			//Steppers[4]->setCurrentPosition(Steppers[4]->currentPosition() + 64 * (int)((-err) / 64), false);
			errors++;
			A5ValueErr = A5Value;
		}
	}


	_prevEncoderPosition = A5ValueS[idx];
	idx = (idx + 1) % 3;

	*/
}
