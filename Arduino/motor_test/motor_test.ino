/*

Copyright (c) [2023] [Orlin Dimitrov]

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

/** @file KarelV1.ino
 *  @brief Firmware of the mobile robot Karel v1.
 *  @author Orlin Dimnitrov (orlin369)
 *
 *  This project is created for demonstrating, Karel v1 abilities. 
 *
 */

/*

Resources:
Adafruit_Motorshield v2:   https://github.com/adafruit/Adafruit_Motor_Shield_V2_Library
AccelStepper with AFMotor: https://github.com/adafruit/AccelStepper
Ultrasonic sensor:         http://blog.iteadstudio.com/arduino-library-for-ultrasonic-ranging-module-hc-sr04/

Motor wiring table for Adafruit Motor Shield V2.X.

+-------+-------+----------+
| Motor | Color | Terminal |
+-------+-------+----------+
| Left  | BLK   | M3       |
| Left  | GRN   | M3       |
| Left  | BLU   | M4       |
| Left  | RED   | M4       |
| Right | BLK   | M2       |
| Right | GRN   | M2       |
| Right | BLU   | M1       |
| Right | RED   | M1       |
+-------+-------+----------+

*/

#pragma region Headers

/** \brief Configuration of the robot. */
#include "ApplicationConfiguration.h"

/** \brief Motion state library. */
#include "MotionType.h"

/** \brief I2C library. */
#include <Wire.h>

/** \brief Standard library. */
#include <stdlib.h>

/** \brief Acceleration stepper motor controller library.
 * AccelStepper with AFMotor support
 * (https://github.com/adafruit/AccelStepper)
 */
#include <AccelStepper.h>

#if defined ADAFRUIT_MOTOR_SHIELD_V1

/** \brief Motor shield driver. 
 * Requires the AFMotor library (https://github.com/adafruit/Adafruit-Motor-Shield-library)
 */
#include <AFMotor.h>

#elif defined ADAFRUIT_MOTOR_SHIELD_V2

/** \brief Motor shield driver.
 * Requires the Adafruit_Motorshield v2 library
 * https://github.com/adafruit/Adafruit_Motor_Shield_V2_Library
 */
#include <Adafruit_MotorShield.h>

#endif

#pragma endregion

#pragma region Prototypes

void turn(long steps);

void move(long steps);

/** @brief This function is callback for CW motion.
*  @return Void.
*/
void cw_callback();

/** @brief This function is callback for CCW motion.
*  @return Void.
*/
void ccw_callback();

#pragma endregion

#pragma region Variables

#if defined ADAFRUIT_MOTOR_SHIELD_V1

/** \brief Left motor on channel 1. */
AF_Stepper MotorLeft_g(MOTORS_STEPS, LEFT_MOTOR_INDEX);

/** \brief Right motor on channel 2. */
AF_Stepper MotorRight_g(MOTORS_STEPS, RIGHT_MOTOR_INDEX);

#elif defined ADAFRUIT_MOTOR_SHIELD_V2
/** \brief The motor shield. */
Adafruit_MotorShield MotorShield_g(MOTOR_SHIELD_ADDRESS);

/** \brief Left motor on channel 1. */
Adafruit_StepperMotor *MotorLeft_g = MotorShield_g.getStepper(MOTORS_STEPS, LEFT_MOTOR_INDEX);

/** \brief Right motor on channel 2. */
Adafruit_StepperMotor *MotorRight_g = MotorShield_g.getStepper(MOTORS_STEPS, RIGHT_MOTOR_INDEX);

#endif

/** \brief Motion acceleration controller. */
AccelStepper MotionController_g(cw_callback, ccw_callback);

/** \brief Motion state holder. */
MotionType_t MotionType_g = MotionType_t::None; 

/** \brief Translation steps. */
long TranslationSteps_g = 0;

/** \brief Rotation steps. */
long RotationSteps_g = 0;

#pragma endregion

/** @brief Setup the peripheral hardware and variables.
 *  @return Void.
 */
void setup()
{

#ifdef ADAFRUIT_MOTOR_SHIELD_V2

  // Create with the default frequency 1.6KHz.
  MotorShield_g.begin();
  //MotorShield_g.begin(1000);  // OR with a different frequency, say 1KHz

#endif
  
  // Set the motor regulator.
  MotionController_g.setSpeed(MAX_MOTORS_SPEED);
  MotionController_g.setAcceleration(MAX_MOTORS_ACCELERATION);

  // Setup Serial port at 115200 bps.
  Serial.begin(115200);
}

/** @brief Main loop of the program.
 *  @return Void.
 */
void loop()
{
	// Update the motor controller.
    MotionController_g.run();

	// If motor is done reset motion type.
	if (MotionController_g.distanceToGo() == 0)
	{
		MotionType_g = MotionType_t::None;
	}

	// If the motion type is done turn off the motors.
	// Just to save some battery.
	if(MotionType_g == MotionType_t::None)
	{
#if defined ADAFRUIT_MOTOR_SHIELD_V1
		MotorLeft_g.release();
		MotorRight_g.release();
#elif defined ADAFRUIT_MOTOR_SHIELD_V2
		MotorLeft_g->release();
		MotorRight_g->release();
#endif
	}
}

#pragma region Fuctions

void turn(long steps)
{
	// Call this combination to run left of right.
	MotionType_g = MotionType_t::Rotate;
	MotionController_g.move(steps);
}

void move(long steps)
{
	// Call this combination to run forward.
	MotionType_g = MotionType_t::Translate;
	MotionController_g.move(steps);
}

/** @brief This function is callback for CW motion.
 *  @return Void.
 */
void cw_callback()
{
	if (MotionType_g == MotionType_t::Translate)
	{
		TranslationSteps_g++;

#if defined ADAFRUIT_MOTOR_SHIELD_V1

		MotorLeft_g.onestep(FORWARD, MICROSTEP);
		MotorRight_g.onestep(FORWARD, MICROSTEP);

#elif defined ADAFRUIT_MOTOR_SHIELD_V2

		MotorLeft_g->onestep(FORWARD, MICROSTEP);
		MotorRight_g->onestep(FORWARD, MICROSTEP);

#endif

	}
	else if (MotionType_g == MotionType_t::Rotate)
	{
		RotationSteps_g++;

#if defined ADAFRUIT_MOTOR_SHIELD_V1

		MotorLeft_g.onestep(BACKWARD, MICROSTEP);
		MotorRight_g.onestep(FORWARD, MICROSTEP);

#elif defined ADAFRUIT_MOTOR_SHIELD_V2

		MotorLeft_g->onestep(BACKWARD, MICROSTEP);
		MotorRight_g->onestep(FORWARD, MICROSTEP);

#endif

	}
}

/** @brief This function is callback for CCW motion.
 *  @return Void.
 */
void ccw_callback()
{
	if (MotionType_g == MotionType_t::Translate)
	{
		TranslationSteps_g--;

#if defined ADAFRUIT_MOTOR_SHIELD_V1

		MotorLeft_g.onestep(BACKWARD, MICROSTEP);
		MotorRight_g.onestep(BACKWARD, MICROSTEP);

#elif defined ADAFRUIT_MOTOR_SHIELD_V2

		MotorLeft_g->onestep(BACKWARD, MICROSTEP);
		MotorRight_g->onestep(BACKWARD, MICROSTEP);

#endif
	}
	else if (MotionType_g == MotionType_t::Rotate)
	{
		RotationSteps_g--;

#if defined ADAFRUIT_MOTOR_SHIELD_V1

		MotorLeft_g.onestep(FORWARD, MICROSTEP);
		MotorRight_g.onestep(BACKWARD, MICROSTEP);

#elif defined ADAFRUIT_MOTOR_SHIELD_V2

		MotorLeft_g->onestep(FORWARD, MICROSTEP);
		MotorRight_g->onestep(BACKWARD, MICROSTEP);

#endif

	}
}

#pragma endregion
