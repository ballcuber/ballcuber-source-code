// EncoderStepper.h

#ifndef _ENCODERSTEPPER_h
#define _ENCODERSTEPPER_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

#include <AccelStepper.h>

struct EncoderParameters {
	int MinEncoder;
	int MaxEncoder;
	int EncoderOffset;
	byte inverted;
};

class EncoderStepper
{
private:
	int _id;
	int _stepPin;
	int _dirPin;
	int _encoderPin;
	int _enablePin;

 protected:
	 void EncoderInterrupt();
	 void LoadFromEEPROM();
	 void SaveToEEPROM();

 public:
	 EncoderStepper(int, int, int, int, int);

	 AccelStepper* Stepper;
	 void Run();
};

#endif

