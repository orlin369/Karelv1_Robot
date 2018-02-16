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

#ifndef _DIFFERENTIALMODEL_h
#define _DIFFERENTIALMODEL_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "Arduino.h"
#else
	#include "WProgram.h"
#endif

#pragma region Definitions

/** @brief Motor full steps count. */
#define MOTOR_STEPS_COUNT 200.0F

/** @brief Stepper motor post scaler. */
#define POST_SCALER 1.0F

/** @brief Wheel diameter. */
#define WHEEL_DIAMETTER 53.5F

/** @brief Distance between wheels. */
#define DISTANCE_BETWEEN_WHEELS 149.5F

#pragma endregion

#pragma region Prototypes

/** @brief Convert distance to steps.
 *  @param distance, Distance [mm]
 *  @return int, Steps.
 *
 * In this case.
 * microStep = 16
 * stepToDeg = (200 * microStep) / 360[deg]
 * lengthOfWheel = (dOfWheel * PI) //53.5[mm]
 * scalar = lengthOfWheel / stepToDeg
 * inputDistance = 1000
 * outputSteps = inputDistance * scalar
 *
 */
int MmToStep(double distance);

/** @brief Convert degree to steps.
 *  @param degrees, Degrees [deg]
 *  @return int, Steps.
 */
int DegToStep(double degrees);

#pragma endregion

#endif

