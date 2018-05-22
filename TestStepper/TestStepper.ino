/*
 Name:		TestStepper.ino
 Created:	12/05/2018 21:27:49
 Author:	flore
*/

#include <StepControl.h>

Stepper motor(60,61);         // STEP pin: 2, DIR pin: 3
StepControl<> controller;    // Use default settings 

void setup() {
}

void loop()
{
	motor.setTargetRel(1000);  // Set target position to 1000 steps from current position
	controller.move(motor);    // Do the move
	delay(500);
}