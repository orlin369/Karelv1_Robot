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

Protocol of communication.
+----------------------------------------------------+----------+---------+-----------+-------+-------+-------+-----+
|                      Command                       | Preamble |  Type   | Direction | Value |  Max  |  Min  | End |
+----------------------------------------------------+----------+---------+-----------+-------+-------+-------+-----+
| Turn robot                                         | ?        | R       | +         |  9999 | +180  |  -180 | \n  |
| Move robot                                         | ?        | M       | +         |  9999 | +9999 | -9999 | \n  |
| Read Distance                                      | ?        | US      |           |   180 |       |       | \n  |
| Read Distance in All directions                    | ?        | USA     |           |       |       |       | \n  |
| Version                                            | ?        | VERSION |           |       |       |       | \n  |
| All fields are merged together to form the command |          |         |           |       |       |       |     |
+----------------------------------------------------+----------+---------+-----------+-------+-------+-------+-----+

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

/** \brief Servo driver library. */
#include <Servo.h>

/** \brief String library. */
#include <String.h>

/** \brief Standard library. */
#include <stdlib.h>

/** \brief Acceleration stepper motor controller library.
 * AccelStepper with AFMotor support
 * (https://github.com/adafruit/AccelStepper)
 */
#include <AccelStepper.h>

#ifndef FAKE_USSD

/** \brief Ultrasonic sensor HC-SR04 library. */
#include <Ultrasonic.h>

#endif

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

/** @brief Read incoming data from the serial buffer.
*  @return Void.
*/
void read_command();

/** @brief Validate the incoming commands.
*  @param command The command string.
*  @return True if successful; or False if failed.
*/
boolean validate_command(String command);

/** @brief Parse and execute the incoming commands.
*  @param command The command string.
*  @return Void.
*/
void parse_command(String command);

/** @brief This function read distance between sensor and the object.
*  @return Time that signals travel in [us].
*/
long read_distance_us();

/** @brief This function read distance between sensor and the object.
*  @return Time that signals travel in [us].
*/
long read_distance_ir();

/** @brief This function is callback for CW motion.
*  @return Void.
*/
void cw_callback();

/** @brief This function is callback for CCW motion.
*  @return Void.
*/
void ccw_callback();

/** @brief Send to the host a gratings command.
*  @return Void.
*/
void send_greetings();

/** @brief Send to the host a actual position command.
*  @return Void.
*/
void send_actual_robot_position();

#pragma endregion

#pragma region Variables

#if defined ADAFRUIT_MOTOR_SHIELD_V1

/** \brief Left motor on channel 1. */
AF_Stepper MotorLeft(MOTORS_STEPS, LEFT_MOTOR_INDEX);

/** \brief Right motor on channel 2. */
AF_Stepper MotorRight(MOTORS_STEPS, RIGHT_MOTOR_INDEX);

#elif defined ADAFRUIT_MOTOR_SHIELD_V2
/** \brief The motor shield. */
Adafruit_MotorShield MotorShield(MOTOR_SHIELD_ADDRESS);

/** \brief Left motor on channel 1. */
Adafruit_StepperMotor *MotorLeft = MotorShield.getStepper(MOTORS_STEPS, LEFT_MOTOR_INDEX);

/** \brief Right motor on channel 2. */
Adafruit_StepperMotor *MotorRight = MotorShield.getStepper(MOTORS_STEPS, RIGHT_MOTOR_INDEX);

#endif

#ifndef FAKE_USSD

/** \brief Ultrasonic sensor: HC-SR04 */
Ultrasonic UltraSonic(PIN_US_TRIG, PIN_US_ECHO);

#endif

/** \brief Motion acceleration controller. */
AccelStepper MotionController_g (cw_callback, ccw_callback);

/** \brief Ultrasonic sensor servo controller. */
Servo SensorServo_g;

/** \brief Output serial buffer. */
char ActualPositionMessage_g[256];

/** \brief Echo bit. */
boolean Echo_g = false;

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
  // Initialize the digital pin as an output.
  pinMode(PIN_IRDS_FRONT , INPUT_PULLUP);
  pinMode(PIN_IRDS_BACK, INPUT_PULLUP);

#ifdef ADAFRUIT_MOTOR_SHIELD_V2

  // Create with the default frequency 1.6KHz.
  MotorShield.begin();
  //MotorShield.begin(1000);  // OR with a different frequency, say 1KHz

#endif

  // Attaches the servo on pin 9 to the servo object.
  SensorServo_g.attach(PIN_US_SERVO);
  // Sets the servo position to the hoe position.
  SensorServo_g.write(0);
  
  // Set the motor regulator.
  MotionController_g.setSpeed(MAX_MOTORS_SPEED);
  MotionController_g.setAcceleration(MAX_MOTORS_ACCELERATION);

  // Setup Serial port at 115200 bps.
  Serial.begin(115200);

  // Send the greetings messages.
  send_greetings();

}

/** @brief Main loop of the program.
 *  @return Void.
 */
void loop()
{
  static unsigned long CurrentMillisTimeL = 0;
  static unsigned long PreviousMillisTimeL = 0;

  // Read command from the serial port.
  read_command();

  // Run the motion controller.
  if(MotionType_g != MotionType_t::None)
  {
    MotionController_g.run();
  }
 
  // Update time.
  CurrentMillisTimeL = millis();

  // Check and send motion state.
  if (CurrentMillisTimeL - PreviousMillisTimeL >= HART_REAT)
  {
    // save the last time you blinked the LED
    PreviousMillisTimeL = CurrentMillisTimeL;

	// Send the position.
    send_actual_robot_position();
  }
}

#pragma region Funtions

/** @brief Read incoming data from the serial buffer.
 *  @return Void.
 */
void read_command()
{
	static String IncommingCommnadL = "";

	// Fill the command data buffer with command.
	while (Serial.available())
	{
		// Add new char.
		IncommingCommnadL += (char)Serial.read();
		// Wait a while for a a new char.
		delay(5);
	}

	// If command if not empty parse it.
	if (IncommingCommnadL != "")
	{
		boolean IsValidL = validate_command(IncommingCommnadL);
		if (IsValidL)
		{
			parse_command(IncommingCommnadL);

			// Print command for feedback.
			if (Echo_g)
			{
				Serial.print("Command; ");
				Serial.println(IncommingCommnadL);
			}
		}
	}

	// Clear the command data buffer.
	IncommingCommnadL = "";
}

/** @brief Validate the incoming commands.
 *  @param command The command string.
 *  @return True if successful; or False if failed.
 */
boolean validate_command(String command)
{
	// Variable definitions.
	static boolean IsValidL;
	static int NumericalValueL;

	// Clear the content.
	IsValidL = false;
	NumericalValueL = 0;

	if (command[0] == '?' && command[7] == '\n')
	{
		if (command[2] == '-' || command[2] == '+')
		{
			if (command[1] == 'T')
			{
				// Convert commands from string to numbers.
				NumericalValueL = atoi(command.substring(3, 7).c_str());
				if (NumericalValueL <= MAX_MOTORS_STEPS && NumericalValueL >= -MAX_MOTORS_STEPS)
				{
					// If is valid.
					IsValidL = true;
				}
			}
			if (command[1] == 'R')
			{
				if (NumericalValueL <= MAX_MOTORS_STEPS && NumericalValueL >= -MAX_MOTORS_STEPS)
				{
					// If is valid.
					IsValidL = true;
				}
			}
		}
	}
	if (command[0] == '?' && command[6] == '\n')
	{
		if (command[1] == 'D' && command[2] == 'S')
		{
			// Convert commands from string to numbers.
			NumericalValueL = atoi(command.substring(3, 6).c_str());
			if (NumericalValueL >= MIN_SONAR_YAW && NumericalValueL <= MAX_SONAR_YAW)
			{
				// If is valid.
				IsValidL = true;
			}
		}
	}
	if (command == "?STOP\n")
	{
		// If is valid.
		IsValidL = true;
	}
	if (command == "?DSA\n")
	{
		// If is valid.
		IsValidL = true;
	}

	// If is not valid.
	return IsValidL;
}

/** @brief Parse and execute the incoming commands.
 *  @param command The command string.
 *  @return Void.
 */
void parse_command(String command)
{
	// Number value.
	static int StepsNumbersL;
	static long USTimeValueL;
	static long IRDistanceValueL;

	// Clear the content.
	StepsNumbersL = 0;
	USTimeValueL = 0;
	IRDistanceValueL = 0;

	if (command[1] == 'T')
	{
		// Convert commands from string to numbers.
		StepsNumbersL = atoi(command.substring(3, 7).c_str());

		if (command[2] == '-')
		{
			StepsNumbersL *= -1;
		}
		//else if (command[2] == '+')
		//{
		//	;
		//}

		// Enable the regulation of the motor.
		MotionType_g = MotionType_t::Translate;
		MotionController_g.move(StepsNumbersL);
	}
	else if (command[1] == 'R')
	{
		// Convert commands from string to numbers.
		StepsNumbersL = atoi(command.substring(3, 7).c_str());

		if (command[2] == '-')
		{
			StepsNumbersL *= -1;
		}
		//else if (command[2] == '+')
		//{
		//	;
		//}
		
		// Enable the regulation of the motor.
		MotionType_g = MotionType_t::Rotate;
		MotionController_g.move(StepsNumbersL);
	}
	else if (command == "?STOP\n")
	{
		// Stop the drivers.
		MotionType_g = MotionType_t::None;

#if defined ADAFRUIT_MOTOR_SHIELD_V1

		MotorLeft.release();
		MotorRight.release();

#elif defined ADAFRUIT_MOTOR_SHIELD_V2

		MotorLeft->release();
		MotorRight->release();

#endif
	}
	else if (command == "?DSA\n")
	{
#ifdef INVERTED_SONAR_YAW
		SensorServo_g.write(MAX_SONAR_YAW);
#else
		SensorServo_g.write(MIN_SONAR_YAW);
#endif // INVERTED_SONAR_YAW

		// Preventing errors in measurements.
		int position = SensorServo_g.read();
		position += 1000;
		delay(position);

#ifdef INVERTED_SONAR_YAW
		for (int indexPos = MIN_SONAR_YAW; indexPos <= MAX_SONAR_YAW; indexPos++)
#else
		for (int indexPos = MAX_SONAR_YAW; indexPos >= MIN_SONAR_YAW; indexPos--)
#endif // INVERTED_SONAR_YAW
		{
			SensorServo_g.write(indexPos);
			delay(100);
			USTimeValueL = read_distance_us();
			IRDistanceValueL = read_distance_ir();

			// Send the message.
			Serial.print("#DS;");
			Serial.print(indexPos);
			Serial.print(":");
			Serial.print(USTimeValueL);
			Serial.print(":");
			Serial.println(IRDistanceValueL);
		}
	}
	else if (command[0] == '?' && command[6] == '\n')
	{
		if (command[1] == 'D' && command[2] == 'S')
		{
			// Convert commands from string to numbers.
			StepsNumbersL = atoi(command.substring(3, 6).c_str());

			SensorServo_g.write(StepsNumbersL);
			USTimeValueL = read_distance_us();
			IRDistanceValueL = read_distance_ir();

			// Send the message.
			Serial.print("#DS;");
			Serial.print(StepsNumbersL);
			Serial.print(":");
			Serial.print(USTimeValueL);
			Serial.print(":");
			Serial.println(IRDistanceValueL);
		}
	}
}

/** @brief This function read distance between sensor and the object.
 *  @return Time that signals travel in [us].
 */
long read_distance_us()
{
	long sum = 0;

	for (int i = 0; i < AVG_FILTER_SAMPLES; i++)
	{
#if defined FAKE_USSD
		sum = FAKE_USSD;
#else
		sum += UltraSonic.timing();
#endif
		delay(1);
	}

	return sum / AVG_FILTER_SAMPLES;
}

/** @brief This function read distance between sensor and the object.
 *  @return Time that signals travel in [us].
 */
long read_distance_ir()
{
	long sum = 0;

	for (int i = 0; i < AVG_FILTER_SAMPLES; i++)
	{
#if defined FAKE_IRSD
		sum = FAKE_IRSD;
#else
		sum += analogRead(PIN_IRDS);
#endif
		delay(1);
	}

	return sum / AVG_FILTER_SAMPLES;
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

		MotorLeft.onestep(FORWARD, MICROSTEP);
		MotorRight.onestep(FORWARD, MICROSTEP);

#elif defined ADAFRUIT_MOTOR_SHIELD_V2

		MotorLeft->onestep(FORWARD, MICROSTEP);
		MotorRight->onestep(FORWARD, MICROSTEP);

#endif

	}
	else if (MotionType_g == MotionType_t::Rotate)
	{
		RotationSteps_g++;

#if defined ADAFRUIT_MOTOR_SHIELD_V1

		MotorLeft.onestep(BACKWARD, MICROSTEP);
		MotorRight.onestep(FORWARD, MICROSTEP);

#elif defined ADAFRUIT_MOTOR_SHIELD_V2

		MotorLeft->onestep(BACKWARD, MICROSTEP);
		MotorRight->onestep(FORWARD, MICROSTEP);

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

		MotorLeft.onestep(BACKWARD, MICROSTEP);
		MotorRight.onestep(BACKWARD, MICROSTEP);

#elif defined ADAFRUIT_MOTOR_SHIELD_V2

		MotorLeft->onestep(BACKWARD, MICROSTEP);
		MotorRight->onestep(BACKWARD, MICROSTEP);

#endif
	}
	else if (MotionType_g == MotionType_t::Rotate)
	{
		RotationSteps_g--;

#if defined ADAFRUIT_MOTOR_SHIELD_V1

		MotorLeft.onestep(FORWARD, MICROSTEP);
		MotorRight.onestep(BACKWARD, MICROSTEP);

#elif defined ADAFRUIT_MOTOR_SHIELD_V2

		MotorLeft->onestep(FORWARD, MICROSTEP);
		MotorRight->onestep(BACKWARD, MICROSTEP);

#endif

	}
}

/** @brief Send to the host a gratings command.
 *  @return Void.
 */
void send_greetings()
{
	Serial.println("#GREATINGS;I am Karel v1 ");
}

/** @brief Send to the host a actual position command.
 *  @return Void.
 */
void send_actual_robot_position()
{	
	// Variables
	static int FrontDistanceSensorValueL = 0;
	static int BackDistanceSensorValueL = 0;

	// Read sensors values.
	FrontDistanceSensorValueL = digitalRead(PIN_IRDS_FRONT);
	BackDistanceSensorValueL = digitalRead(PIN_IRDS_BACK);

	// Construct the message.
	sprintf(ActualPositionMessage_g, "#POSITION;T:%ld;R:%ld;F:%d;B:%d;", TranslationSteps_g, RotationSteps_g, FrontDistanceSensorValueL, BackDistanceSensorValueL);

	// Send positional data.
	Serial.println(ActualPositionMessage_g);
}

#pragma endregion