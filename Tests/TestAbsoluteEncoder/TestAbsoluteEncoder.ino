
#define STEPS_PER_MOTOR_ROTATION 3200
#define ENCODER_CLAMP_MICROS 68
#define ENCODER_PERIOD_MICROS 2174

void setup() {
	attachInterrupt(digitalPinToInterrupt(2), OnChange, CHANGE);

	Serial.begin(115200);
}

volatile long int _lastUpMicros;
volatile int _encoderSteps;
volatile int _lastAbsoluteSteps;
volatile int _turns=0;

void OnChange() {
	if (digitalRead(2)) {
		_lastUpMicros = micros();
	}
	else {
		int absoluteSteps = constrain( STEPS_PER_MOTOR_ROTATION * (micros() - _lastUpMicros - ENCODER_CLAMP_MICROS) / (ENCODER_PERIOD_MICROS - 2 * ENCODER_CLAMP_MICROS), 0, STEPS_PER_MOTOR_ROTATION);

		int diff = _lastAbsoluteSteps - absoluteSteps;
		if (abs(diff) > STEPS_PER_MOTOR_ROTATION / 4) _turns += (diff > 0) ? 1 : -1;
		_lastAbsoluteSteps = absoluteSteps;

		_encoderSteps = _turns * STEPS_PER_MOTOR_ROTATION + absoluteSteps;
	}
}


void loop() {

	Serial.print("_encoderSteps : ");
	Serial.println(_encoderSteps);
	Serial.print("_turns : ");
	Serial.println(_turns);
	Serial.print("_lastAbsoluteSteps : ");
	Serial.println(_lastAbsoluteSteps);
	Serial.print("_lastUpMicros : ");
	Serial.println(_lastUpMicros);
	delay(500);
}
