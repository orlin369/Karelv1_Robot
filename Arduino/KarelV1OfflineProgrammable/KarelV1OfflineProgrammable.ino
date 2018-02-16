/*

Copyright (c) [2018] [Orlin Dimitrov]

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
*  ButtonDebounce library: https://github.com/maykon/ButtonDebounce/
*  AccelStepper library: https://github.com/adafruit/AccelStepper
*  QList library: https://github.com/SloCompTech/QList
*  Adafruit_MotorShield_g v2: https://github.com/adafruit/Adafruit_Motor_Shield_V2_Library
*/

#pragma region Headers

/** \brief Configuration of the robot. */
#include "ApplicationConfiguration.h"

/** \brief Mechanical model of the robot. */
#include "DifferentialModel.h"

/** \brief Motion state library. */
#include "MotionType.h"

/** \brief Execution state library. */
#include "ExecutionState.h"

/** \brief Button denounce library. */
#include <ButtonDebounce.h>

/** \brief Acceleration stepper motor controller library. */
#include <AccelStepper.h>

/** \brief Queue list library. */
#include <QList.h>

#if defined ADAFRUIT_MOTOR_SHIELD_V1

/** \brief Motor shield driver.
* Requires the AFMotor library (https://github.com/adafruit/Adafruit-Motor-Shield-library)
*/
#include <AFMotor.h>

#elif defined ADAFRUIT_MOTOR_SHIELD_V2

/** \brief Motor shield driver. */
#include <Adafruit_MotorShield.h>

#endif

#pragma endregion

#pragma region Prototypes

/** @brief Button forward callback.
 *  @param state, button state [LOW : HIGH]
 *  @return Void.
 */
void BtnForward_callback(int state);

/** @brief Button backward callback.
 *  @param state, button state [LOW : HIGH]
 *  @return Void.
 */
void BtnBackward_callback(int state);

/** @brief Button left callback.
 *  @param state, button state [LOW : HIGH]
 *  @return Void.
 */
void BtnLeft_callback(int state);

/** @brief Button right callback.
 *  @param state, button state [LOW : HIGH]
 *  @return Void.
 */
void BtnRight_callback(int state);

/** @brief Button go callback.
 *  @param state, button state [LOW : HIGH]
 *  @return Void.
 */
void BtnGo_callback(int state);

/** @brief Button clear callback.
 *  @param state, button state [LOW : HIGH]
 *  @return Void.
 */
void BtnClear_callback(int state);

/** @brief Button pause callback.
 *  @param state, button state [LOW : HIGH]
 *  @return Void.
 */
void BtnPause_callback(int state);

/** @brief Go trough command set.
 *  @return Void.
 */
void GoTroughCommands();

/** @brief Run the indication.
 *  @return Void.
 */
void Indication();

/** @brief This function is callback for CW motion.
 *  @return Void.
 */
void cw_callback();

/** @brief This function is callback for CCW motion.
 *  @return Void.
 */
void ccw_callback();

/** @brief This function is callback for CCW motion.
 *  @param time, Time to beep.
 *  @return Void.
 */
void beep(uint16_t time);

#pragma endregion

#pragma region Variables

/** \brief Forward button instance. */
ButtonDebounce BtnForward_g(PIN_BTN_FORWARD, BTN_DEBOUNCE_TIME);

/** \brief Backward button instance. */
ButtonDebounce BtnBackward_g(PIN_BTN_BACKWARD, BTN_DEBOUNCE_TIME);

/** \brief Left button instance. */
ButtonDebounce BtnLeft_g(PIN_BTN_LEFT, BTN_DEBOUNCE_TIME);

/** \brief Right button instance. */
ButtonDebounce BtnRight_g(PIN_BTN_RIGHT, BTN_DEBOUNCE_TIME);

/** \brief Go button instance. */
ButtonDebounce BtnGo_g(PIN_BTN_GO, BTN_DEBOUNCE_TIME);

/** \brief Clear button instance. */
ButtonDebounce BtnClear_g(PIN_BTN_CLEAR, BTN_DEBOUNCE_TIME);

/** \brief Pause button instance. */
ButtonDebounce BtnPause_g(PIN_BTN_PAUSE, BTN_DEBOUNCE_TIME);

/** \brief Motion acceleration controller. */
AccelStepper MotionController_g(cw_callback, ccw_callback);

#if defined ADAFRUIT_MOTOR_SHIELD_V1

/** \brief Left motor on channel 1. */
AF_Stepper MotorLeft(MOTORS_STEPS, LEFT_MOTOR_INDEX);

/** \brief Right motor on channel 2. */
AF_Stepper MotorRight(MOTORS_STEPS, RIGHT_MOTOR_INDEX);

#elif defined ADAFRUIT_MOTOR_SHIELD_V2
/** \brief The motor shield. */
Adafruit_MotorShield MotorShield_g(MOTOR_SHIELD_ADDRESS);

/** \brief Left motor on channel 1. */
Adafruit_StepperMotor *MotorLeft_g = MotorShield_g.getStepper(MOTOR_STEPS_COUNT, LEFT_MOTOR_INDEX);

/** \brief Right motor on channel 2. */
Adafruit_StepperMotor *MotorRight_g = MotorShield_g.getStepper(MOTOR_STEPS_COUNT, RIGHT_MOTOR_INDEX);

#endif

/** \brief motion commands holder. */
QList<char> Commands_g;

/** \brief Command iterator index. */
int CommandIndex_g = 0;

/** \brief Motion state flag. */
MotionType_t MotionType_g = MotionType_t::None;

/** \brief Execution state flag. */
ExecutionState_t ExecutionState_g = ExecutionState_t::Stop;

/** \brief Translation steps count. */
int TranslationSteps_g = 0;

/** \brief Rotation steps count. */
int RotationSteps_g = 0;

#pragma endregion

/** @brief Setup the peripheral hardware and variables.
*  @return Void.
*/
void setup()
{
#ifdef DEBUG_PRINT
	// Setup Serial port at 115200 bps.
	DEBUG_PRINT.begin(115200);
#endif // DEBUG_PRINT

	// Clear command set.
	Commands_g.clear();

	// Setup the motion configuration.
	TranslationSteps_g = MmToStep(TRANSLATION_MM);
	RotationSteps_g = DegToStep(ROTATION_DEG);

#ifdef DEBUG_PRINT
	DEBUG_PRINT.print("Translation steps: ");
	DEBUG_PRINT.println(TranslationSteps_g);
	DEBUG_PRINT.print("Rotation stepss: ");
	DEBUG_PRINT.println(RotationSteps_g);
#endif // DEBUG_PRINT


#ifdef ADAFRUIT_MOTOR_SHIELD_V2
	// Create with the default frequency 1.6KHz.
	//MotorShield_g.begin();
	//MotorShield_g.begin(1000);  // OR with a different frequency, say 1KHz
	MotorShield_g.begin(MOTORS_PWM_FREQ);

	// Turn OFF motor drivers.
	MotorLeft_g->release();
	MotorRight_g->release();
#endif

	// Set the motor regulator.
	MotionController_g.setSpeed(MAX_MOTORS_SPEED);
	MotionController_g.setAcceleration(MAX_MOTORS_ACCELERATION);

	// Set the buttons.
	BtnForward_g.setCallback(BtnForward_callback);
	BtnBackward_g.setCallback(BtnBackward_callback);
	BtnLeft_g.setCallback(BtnLeft_callback);
	BtnRight_g.setCallback(BtnRight_callback);
	BtnGo_g.setCallback(BtnGo_callback);
	BtnClear_g.setCallback(BtnClear_callback);
	BtnPause_g.setCallback(BtnPause_callback);

#ifdef DEBUG_PRINT
	//
	DEBUG_PRINT.print("Execution state: ");
	if (ExecutionState_g == ExecutionState_t::Run)
	{
		DEBUG_PRINT.println("RUN");
	}
	if (ExecutionState_g == ExecutionState_t::Pause)
	{
		DEBUG_PRINT.println("PAUSE");
	}
	if (ExecutionState_g == ExecutionState_t::Stop)
	{
		DEBUG_PRINT.println("STOP");
	}
#endif // DEBUG_PRINT

	// Configure status LED.
	pinMode(PIN_STATUS_LED, OUTPUT);

	// Turn OFF the status LED.
	digitalWrite(PIN_STATUS_LED, LOW);

	// Configure buzzer pin.
	pinMode(PIN_BUZZER, OUTPUT);

	// Turn OFF the buzzer.
	digitalWrite(PIN_STATUS_LED, LOW);

	// Set the hard coded program.
	SetHardCodedProgram();

	// Ready
	beep(READY_BEEP);
}

/** @brief Main loop of the program.
*  @return Void.
*/
void loop()
{
	// Update the buttons states.
	BtnForward_g.update();
	BtnBackward_g.update();
	BtnLeft_g.update();
	BtnRight_g.update();
	BtnGo_g.update();
	BtnClear_g.update();
	BtnPause_g.update();

	// Run the robot.
	GoTroughCommands();

	// Run the motion controller.
	MotionController_g.run();

	// Update indication.
	Indication();
}

#pragma region Funtions

/** @brief Button forward callback.
 *  @param state, button state [LOW : HIGH]
 *  @return Void.
 */
void BtnForward_callback(int state)
{
	if (state == LOW)
	{
		if (ExecutionState_g != ExecutionState_t::Run)
		{
			if (Commands_g.length() < MAX_COMMAND_LENGTH)
			{
				Commands_g.push_back(CMD_FORWARD);

#ifdef DEBUG_PRINT
				DEBUG_PRINT.println("Button: FORWARD");
#endif //DEBUG_PRINT

				// Feed back to the user.
				beep(BUTTON_BEEP);
			}
		}
	}
}

/** @brief Button backward callback.
 *  @param state, button state [LOW : HIGH]
 *  @return Void.
 */
void BtnBackward_callback(int state)
{
	if (state == LOW)
	{
		if (ExecutionState_g != ExecutionState_t::Run)
		{
			if (Commands_g.length() < MAX_COMMAND_LENGTH)
			{
				Commands_g.push_back(CMD_BACKWARD);

#ifdef DEBUG_PRINT
				DEBUG_PRINT.println("Button: BACKWARD");
#endif //DEBUG_PRINT

				// Feed back to the user.
				beep(BUTTON_BEEP);
			}
		}
	}
}

/** @brief Button left callback.
 *  @param state, button state [LOW : HIGH]
 *  @return Void.
 */
void BtnLeft_callback(int state)
{
	if (state == LOW)
	{
		if (ExecutionState_g != ExecutionState_t::Run)
		{
			if (Commands_g.length() < MAX_COMMAND_LENGTH)
			{
				Commands_g.push_back(CMD_LEFT);

#ifdef DEBUG_PRINT
				DEBUG_PRINT.println("Button: LEFT");
#endif //DEBUG_PRINT

				// Feed back to the user.
				beep(BUTTON_BEEP);
			}
		}
	}
}

/** @brief Button right callback.
 *  @param state, button state [LOW : HIGH]
 *  @return Void.
 */
void BtnRight_callback(int state)
{
	if (state == LOW)
	{
		if (ExecutionState_g != ExecutionState_t::Run)
		{
			if (Commands_g.length() < MAX_COMMAND_LENGTH)
			{
				Commands_g.push_back(CMD_RIGHT);

#ifdef DEBUG_PRINT
				DEBUG_PRINT.println("Button: RIGHT");
#endif //DEBUG_PRINT

				// Feed back to the user.
				beep(BUTTON_BEEP);
			}
		}
	}
}

/** @brief Button go callback.
 *  @param state, button state [LOW : HIGH]
 *  @return Void.
 */
void BtnGo_callback(int state)
{
	if (state == LOW)
	{
		if (ExecutionState_g != ExecutionState_t::Run)
		{
			ExecutionState_g = ExecutionState_t::Run;

#ifdef DEBUG_PRINT
			DEBUG_PRINT.println("Button: GO");
#endif //DEBUG_PRINT

			// Feed back to the user.
			beep(BUTTON_BEEP);

			// Wait before start.
			delay(300);
		}
	}
}

/** @brief Button clear callback.
 *  @param state, button state [LOW : HIGH]
 *  @return Void.
 */
void BtnClear_callback(int state)
{
	if (state == LOW)
	{
		if (ExecutionState_g != ExecutionState_t::Run)
		{
			// Change the state.
			ExecutionState_g = ExecutionState_t::Stop;

			// End the motion type.
			MotionType_g = MotionType_t::None;

			// Clear command index.
			CommandIndex_g = 0;

			// Clear the queue.
			Commands_g.clear();

			MotionController_g.setCurrentPosition(0);

#ifdef DEBUG_PRINT
			DEBUG_PRINT.println("Button: CLEAR");
#endif //DEBUG_PRINT

			// Feed back to the user.
			beep(BUTTON_BEEP);
		}
	}
}

/** @brief Button pause callback.
 *  @param state, button state [LOW : HIGH]
 *  @return Void.
 */
void BtnPause_callback(int state)
{
	if (state == LOW)
	{
		if (ExecutionState_g == ExecutionState_t::Run)
		{
			// Go to pause state.
			ExecutionState_g = ExecutionState_t::Pause;

			// Stop the motion controller.
			MotionType_g = MotionType_t::None;

#ifdef DEBUG_PRINT
			DEBUG_PRINT.println("Button: PAUSE");
#endif //DEBUG_PRINT

			// Feed back to the user.
			beep(BUTTON_BEEP);
		}
	}
}

/** @brief Go trough command set.
 *  @return Void.
 */
void GoTroughCommands()
{
	// If we run go to run sequence.
	if (ExecutionState_g == ExecutionState_t::Run)
	{
		// If motor is not running iterate trough sequence.
		if (!MotionController_g.isRunning())
		{
			// If command index is greate
			if (!(CommandIndex_g >= Commands_g.length()))
			{
				if (Commands_g[CommandIndex_g] == CMD_FORWARD)
				{
					// Enable the regulation of the motor.
					MotionType_g = MotionType_t::Translate;

					// Set the motion.
					MotionController_g.move(TranslationSteps_g);

#ifdef DEBUG_PRINT
					DEBUG_PRINT.println("Command: FORWARD");
#endif //DEBUG_PRINT
				}
				else if (Commands_g[CommandIndex_g] == CMD_BACKWARD)
				{
					// Enable the regulation of the motor.
					MotionType_g = MotionType_t::Translate;

					// Set the motion.
					MotionController_g.move(-TranslationSteps_g);

#ifdef DEBUG_PRINT
					DEBUG_PRINT.println("Command: BACKWARD");
#endif //DEBUG_PRINT
				}
				else if (Commands_g[CommandIndex_g] == CMD_LEFT)
				{
					// Enable the regulation of the motor.
					MotionType_g = MotionType_t::Rotate;

					// Set the motion.
					MotionController_g.move(-RotationSteps_g);

#ifdef DEBUG_PRINT
					DEBUG_PRINT.println("Command: LEFT");
#endif //DEBUG_PRINT
				}
				else if (Commands_g[CommandIndex_g] == CMD_RIGHT)
				{
					// Enable the regulation of the motor.
					MotionType_g = MotionType_t::Rotate;

					// Set the motion.
					MotionController_g.move(RotationSteps_g);

#ifdef DEBUG_PRINT
					DEBUG_PRINT.println("Command: RIGHT");
#endif //DEBUG_PRINT
				}

				// Increment once the next command.
				CommandIndex_g++;
			}

			// Increment only if index inside of the sequence bound.
			else
			{
				// Disable regulation of the motor.
				MotionType_g = MotionType_t::None;

				// Pause the working sequence.
				ExecutionState_g = ExecutionState_t::Pause;

				// Clear the iteration index;
				CommandIndex_g = 0;

				// Turn OFF the motor drivers.
				MotorLeft_g->release();
				MotorRight_g->release();

				// Ready beep sequence.
				beep(READY_BEEP);
				delay(300);
				beep(READY_BEEP);
				delay(300);
				beep(READY_BEEP);

#ifdef DEBUG_PRINT
				DEBUG_PRINT.println("Command: END");
#endif //DEBUG_PRINT
			}
		}
	}
}

/** @brief Run the indication.
 *  @return Void.
 */
void Indication()
{
	static unsigned long CurrentMillisTimeL = 0;
	static unsigned long PreviousMillisTimeL = 0;

	/** \brief Blink LED state flag. */
	static bool BlinkBitL = false;

	// Update time.
	CurrentMillisTimeL = millis();

	// Check and send motion state.
	if (CurrentMillisTimeL - PreviousMillisTimeL >= 250)
	{
		// save the last time you blinked the LED
		PreviousMillisTimeL = CurrentMillisTimeL;

		//
		if (ExecutionState_g == ExecutionState_t::Run)
		{
			digitalWrite(PIN_STATUS_LED, (BlinkBitL ? HIGH : LOW));
			BlinkBitL = !BlinkBitL;
		}

		//
		else if (ExecutionState_g == ExecutionState_t::Pause)
		{
			digitalWrite(PIN_STATUS_LED, LOW);
		}

		//
		else if (ExecutionState_g == ExecutionState_t::Stop)
		{
			digitalWrite(PIN_STATUS_LED, LOW);
		}
	}
}

/** @brief This function is callback for CW motion.
 *  @return Void.
 */
void cw_callback()
{
	if (MotionType_g == MotionType_t::Translate)
	{
#if defined ADAFRUIT_MOTOR_SHIELD_V1
		MotorLeft.onestep(FORWARD, MICROSTEP);
		MotorRight.onestep(FORWARD, MICROSTEP);
#elif defined ADAFRUIT_MOTOR_SHIELD_V2
		MotorLeft_g->onestep(FORWARD, SINGLE);
		MotorRight_g->onestep(FORWARD, SINGLE);
#endif
	}
	else if (MotionType_g == MotionType_t::Rotate)
	{
#if defined ADAFRUIT_MOTOR_SHIELD_V1
		MotorLeft.onestep(BACKWARD, MICROSTEP);
		MotorRight.onestep(FORWARD, MICROSTEP);
#elif defined ADAFRUIT_MOTOR_SHIELD_V2
		MotorLeft_g->onestep(BACKWARD, SINGLE);
		MotorRight_g->onestep(FORWARD, SINGLE);
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
#if defined ADAFRUIT_MOTOR_SHIELD_V1
		MotorLeft.onestep(BACKWARD, MICROSTEP);
		MotorRight.onestep(BACKWARD, MICROSTEP);
#elif defined ADAFRUIT_MOTOR_SHIELD_V2
		MotorLeft_g->onestep(BACKWARD, SINGLE);
		MotorRight_g->onestep(BACKWARD, SINGLE);
#endif
	}
	else if (MotionType_g == MotionType_t::Rotate)
	{
#if defined ADAFRUIT_MOTOR_SHIELD_V1
		MotorLeft.onestep(FORWARD, MICROSTEP);
		MotorRight.onestep(BACKWARD, MICROSTEP);
#elif defined ADAFRUIT_MOTOR_SHIELD_V2
		MotorLeft_g->onestep(FORWARD, SINGLE);
		MotorRight_g->onestep(BACKWARD, SINGLE);
#endif
	}
}

/** @brief Set the hard coded program.
 *  @return Void.
 */
void SetHardCodedProgram()
{
	//
	CommandIndex_g = 0;

	//
	Commands_g.push_back(CMD_FORWARD);
	Commands_g.push_back(CMD_LEFT);
	Commands_g.push_back(CMD_LEFT);
	Commands_g.push_back(CMD_FORWARD);
	Commands_g.push_back(CMD_RIGHT);
	Commands_g.push_back(CMD_RIGHT);
}

/** @brief This function is callback for CCW motion.
 *  @param time, Time to beep.
 *  @return Void.
 */
void beep(uint16_t time = 50U)
{
	digitalWrite(PIN_BUZZER, HIGH);
	delay(time);
	digitalWrite(PIN_BUZZER, LOW);
}

#pragma endregion