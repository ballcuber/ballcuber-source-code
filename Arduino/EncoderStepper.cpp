// 
// 
// 

#include "EncoderStepper.h"

EncoderStepper::EncoderStepper(int id, int stepPin, int dirPin, int encoderPin, int enablePin) {
	LoadFromEEPROM();
	
	attachInterrupt(encoderPin, (void(*)()) & [this]() { EncoderInterrupt(); }, CHANGE);
	_id = id;
	_stepPin = stepPin;
	_dirPin = dirPin;
	_encoderPin = encoderPin;
	_enablePin = enablePin;

	Stepper = new AccelStepper(AccelStepper::DRIVER, stepPin, dirPin);

	Stepper->setEnablePin(enablePin);
	
	Stepper->setPinsInverted(false, false, true);	// invert enable pin (LOW or power on)
	Stepper->setMaxSpeed(100);
	Stepper->setAcceleration(140000);
	Stepper->disableOutputs();
	
	
}

void EncoderStepper::Run() {
	Stepper->run();
}

void EncoderStepper::EncoderInterrupt() {

}

void EncoderStepper::LoadFromEEPROM() {

}

void EncoderStepper::SaveToEEPROM() {

}

