#pragma once

/**
* Marlin 3D Printer Firmware
* Copyright (C) 2016 MarlinFirmware [https://github.com/MarlinFirmware/Marlin]
*
* Based on Sprinter and grbl.
* Copyright (C) 2011 Camiel Gubbels / Erik van der Zalm
*
* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
*
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License
* along with this program.  If not, see <http://www.gnu.org/licenses/>.
*
*/

/**
* Arduino Mega with RAMPS v1.4 (or v1.3) pin assignments
*
* Applies to the following boards:
*
*  RAMPS_14_EFB (Hotend, Fan, Bed)
*  RAMPS_14_EEB (Hotend0, Hotend1, Bed)
*  RAMPS_14_EFF (Hotend, Fan0, Fan1)
*  RAMPS_14_EEF (Hotend0, Hotend1, Fan)
*  RAMPS_14_SF  (Spindle, Controller Fan)
*
*  RAMPS_13_EFB (Hotend, Fan, Bed)
*  RAMPS_13_EEB (Hotend0, Hotend1, Bed)
*  RAMPS_13_EFF (Hotend, Fan0, Fan1)
*  RAMPS_13_EEF (Hotend0, Hotend1, Fan)
*  RAMPS_13_SF  (Spindle, Controller Fan)
*
*  Other pins_MYBOARD.h files may override these defaults
*
*  Differences between
*  RAMPS_13 | RAMPS_14
*         7 | 11
*/


//
// Servos
//
#define SERVO0_PIN       11
#define SERVO1_PIN          6
#define SERVO2_PIN          5
#define SERVO3_PIN        4


//
// Limit Switches
//
#define X_MIN_PIN           3
#define X_MAX_PIN         2
#define Y_MIN_PIN          14
#define Y_MAX_PIN          15
#define Z_MIN_PIN          18
#define Z_MAX_PIN          19


//
// Steppers
//
#define X_STEP_PIN         54
#define X_DIR_PIN          55
#define X_ENABLE_PIN       38
#define X_CS_PIN           53

#define Y_STEP_PIN         60
#define Y_DIR_PIN          61
#define Y_ENABLE_PIN       56
#define Y_CS_PIN           49

#define Z_STEP_PIN         46
#define Z_DIR_PIN          48
#define Z_ENABLE_PIN       62
#define Z_CS_PIN           40

#define E0_STEP_PIN        26
#define E0_DIR_PIN         28
#define E0_ENABLE_PIN      24
#define E0_CS_PIN          42

#define E1_STEP_PIN        36
#define E1_DIR_PIN         34
#define E1_ENABLE_PIN      30
#define E1_CS_PIN          44


