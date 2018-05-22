#include <MultiStepper.h>
#include <AccelStepper.h>
#include <Sharer.h>
#include "RAMPS.h"



#define STEPPER_COUNT  5



AccelStepper E0Stepper(AccelStepper::DRIVER, E0_STEP_PIN, E0_STEP_PIN);
AccelStepper E1Stepper(AccelStepper::DRIVER, E1_STEP_PIN, E1_STEP_PIN);
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

float speed(int iStepper) {
	GENERIC_ONE_STEPPER(speed);
}

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
	GENERIC_ALL_STEPPER(disableOutputs);
}

void enableOutputs(int mask) {
	GENERIC_ALL_STEPPER(enableOutputs);
}

bool isRunning(int iStepper) {
	GENERIC_ONE_STEPPER(isRunning);
}

void setMinPulseWidth(int mask, unsigned int minWidth) {
	GENERIC_ALL_STEPPER(setMinPulseWidth, minWidth);
}


bool blockingMove(byte m1, long r1, byte m2, long r2, byte m3, long r3) {
	bool useM1 = m1 < STEPPER_COUNT;
	bool useM2 = m2 < STEPPER_COUNT;
	bool useM3 = m3 < STEPPER_COUNT;

	if (useM1) Steppers[m1]->move(r1);

	if (useM2) Steppers[m2]->move(r2);

	if (useM3) Steppers[m3]->move(r3);

	bool moving = true;
	while (moving) {
		moving = useM1 && Steppers[m1]->run();
		moving |= useM2 && Steppers[m2]->run();
		moving |= useM3 && Steppers[m3]->run();
	}

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



void setup() {

	Sharer.init(115200);

	E0Stepper.setEnablePin(E0_ENABLE_PIN);
	E1Stepper.setEnablePin(E1_ENABLE_PIN);
	XStepper.setEnablePin(X_ENABLE_PIN);
	YStepper.setEnablePin(Y_ENABLE_PIN);
	ZStepper.setEnablePin(Z_ENABLE_PIN);

	
	for (int i = 0; i < STEPPER_COUNT; i++) {
		Steppers[i]->setPinsInverted(false, false, true);	// invert enable pin (LOW or power on)
		Steppers[i]->setMaxSpeed(14000);
		Steppers[i]->setAcceleration(1400000);
		Steppers[i]->enableOutputs();
	}

	
	Sharer_ShareVoid(blockingMove2, long, r1);
	Sharer_ShareVoid(stop, int, mask);
	Sharer_ShareFunction(bool, blockingMove, byte, m1, long, r1, byte, m2, long, r2, byte, m3, long, r3);
	Sharer_ShareVoid(setMaxSpeed, int, mask, float, speed);
	Sharer_ShareVoid(setAcceleration, int, mask, float, accel);
	Sharer_ShareVoid(moveTo, int, mask, long, absolute);
	Sharer_ShareVoid(move, int, mask, long, relative);
	Sharer_ShareFunction(float, maxSpeed, int, iStepper);
	Sharer_ShareVoid(setSpeed, int, mask, float, speed);
	Sharer_ShareFunction(float, speed, int, iStepper);
	Sharer_ShareFunction(long, distanceToGo, int, iStepper);
	Sharer_ShareFunction(long, targetPosition, int, iStepper);
	Sharer_ShareFunction(long, currentPosition, int, iStepper);
	Sharer_ShareVoid(setCurrentPosition, int, mask, long, position);
	Sharer_ShareVoid(disableOutputs, int, mask);
	Sharer_ShareVoid(enableOutputs, int, mask);
	Sharer_ShareFunction(bool, isRunning, int, iStepper);
	Sharer_ShareVoid(setMinPulseWidth, int, mask, uint16_t, minWidth);

	/*
	for (int i = 0; i < Sharer.functionList.count; i++) {
		Serial.print(strlen_P(Sharer.functionList.functions[i].name));
		Serial.print(" : ");
		for (int c = 0; c < strlen_P(Sharer.functionList.functions[i].name);c++) {
			char myChar = pgm_read_byte_near((int)(Sharer.functionList.functions[i].name) + c);
			Serial.print(myChar);
		}

		Serial.println();

		for (int j = 0; j < Sharer.functionList.functions[i].argumentCount; j++) {
			Serial.print("- ");
			Serial.print(strlen_P(Sharer.functionList.functions[i].Arguments[j].name));
			Serial.print(" : ");
			for (int c = 0; c < strlen_P(Sharer.functionList.functions[i].Arguments[j].name);c++) {
				char myChar = pgm_read_byte_near((int)(Sharer.functionList.functions[i].Arguments[j].name) + c);
				Serial.print(myChar);
			}
			Serial.println();

		}
	}
	*/


	/*
	for (int j = 0; j < 100; j++) {

		for (int i = 0; i < 32000; i++) {
			delayMicroseconds(100);
			digitalWrite(Z_STEP_PIN, HIGH);
			delayMicroseconds(1);
			digitalWrite(Z_STEP_PIN, LOW);
		}

		delay(100);
	}

	*/

}

void loop() {
	Sharer.run();
	for (int i = 0; i < STEPPER_COUNT; i++) {
		//Serial.println(i);
		Steppers[i]->run();

	}

}
