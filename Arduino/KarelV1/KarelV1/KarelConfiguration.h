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

/** @file MotionType.h
 *  @brief Firmware of the mobile robot Karel v1.
 *
 *  This project is created for demonstrading, Karel v1 abilities. 
 *
 *  @author Orlin Dimnitrov (orlin369)
 */

#ifndef _KAREL_CONFIGURATION_H_
#define _KAREL_CONFIGURATION_H_

 /** \brief Ultrasonic sensor trigger pin 4.*/
#define PIN_US_TRIG 4
/** \brief Ultrasonic sensor echo pin 5.*/
#define PIN_US_ECHO 5
/** \brief Front sensor pin 6. */
#define PIN_IRDS_FRONT 6
/** \brief Back sensor pin 7. */
#define PIN_IRDS_BACK 7
/** \brief Servo pin that cotrols the sensor position.*/
#define PIN_US_SERVO 9
/** \brief IR sensor pin.*/
#define PIN_IRDS 0

/** \brief Motor shield address. */
#define MOTOR_SHIELD_ADDRESS 0x60
/** \brief Motors max steps. */
#define MOTORS_STEPS 200
/** \brief Left motor index. */
#define  LEFT_MOTOR_INDEX 1
/** \brief Right motor index. */
#define  RIGHT_MOTOR_INDEX 2

/** \brief Maximum speed. */
#define MAX_MOTORS_SPEED 200.0
/** \brief Maximum acceleration. */
#define MAX_MOTORS_ACCELERATION 100.0
/** \brief Maximum executable steps. */
#define MAX_MOTORS_STEPS 9999

/** \brief Average filter samples count. */
#define AVG_FILTER_SAMPLES 3

/** \brief Interval at which to send motion state ti the host. (milliseconds) */
#define HART_REAT 1000

/** \brief Minimum sonar yaw value. */
#define MIN_SONAR_YAW 0
/** \brief Maximum sonar yaw value. */
#define MAX_SONAR_YAW 180
/** \brief Inverted sonar yaw. */
#define INVERTED_SONAR_YAW

#endif 