/*

Copyright (c) [2016] [Orlin Dimitrov]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

/** @file ApplicationConfiguration.h
*  @brief Configuration of the mobile robot Karel v1.
*  @author Orlin Dimnitrov (orlin369)
*
*  The purpose of this file it to hold the configuration of the robot.
*
*/

#ifndef _APPLICATIONCONFIGURATION_h
#define _APPLICATIONCONFIGURATION_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "Arduino.h"
#else
	#include "WProgram.h"
#endif


//#define ADAFRUIT_MOTOR_SHIELD_V1
#define ADAFRUIT_MOTOR_SHIELD_V2

/** \brief Motor shield address. */
#define DEBUG_PRINT Serial  // Comment this out to disable prints and save space


/** \brief IR receiver pin. */
#define PIN_IR_RECV 10

/** \brief IR receiver debounce time. */
#define IR_RECV_DEBOUNCE_TIME 500


/** \brief Status LED pin. */
#define PIN_STATUS_LED 13


/** \brief Pause button pin */
#define PIN_BUZZER 9

/** \brief Buttons beep time. */
#define BUTTON_BEEP 30

/** \brief Ready beep time. */
#define READY_BEEP 30


/** \brief Forward button pin */
#define PIN_BTN_FORWARD 2

/** \brief Backward button pin */
#define PIN_BTN_BACKWARD 3

/** \brief Left button pin */
#define PIN_BTN_LEFT 4

/** \brief Right button pin */
#define PIN_BTN_RIGHT 5

/** \brief Go button pin */
#define PIN_BTN_GO 6

/** \brief Clear button pin */
#define PIN_BTN_CLEAR 7

/** \brief Pause button pin */
#define PIN_BTN_PAUSE 8

/** \brief Buttons denounce time. */
#define BTN_DEBOUNCE_TIME 200


/** \brief Command forward. */
#define CMD_FORWARD 'F'

/** \brief Command backward. */
#define CMD_BACKWARD 'B'

/** \brief Command left. */
#define CMD_LEFT 'L'

/** \brief Command right. */
#define CMD_RIGHT 'R'

/** \brief Maximum commands length. */
#define MAX_COMMAND_LENGTH 100


#ifdef ADAFRUIT_MOTOR_SHIELD_V2

/** \brief Motor shield address. */
#define MOTOR_SHIELD_ADDRESS 0x60

/** \brief Motor PWM frequency. */
#define MOTORS_PWM_FREQ 2000

#endif // ADAFRUIT_MOTOR_SHIELD_V2

/** \brief Left motor index. */
#define  LEFT_MOTOR_INDEX 1

/** \brief Right motor index. */
#define  RIGHT_MOTOR_INDEX 2

/** \brief Maximum speed. */
#define MAX_MOTORS_SPEED 300.0F

/** \brief Maximum acceleration. */
#define MAX_MOTORS_ACCELERATION 200.0F

/** \brief Motor shield address. */
#define TRANSLATION_MM  445.0F //335 // = (335mm * (scale = 18.918))

/** \brief Motor shield address. */
#define ROTATION_DEG 90.0F

#endif

