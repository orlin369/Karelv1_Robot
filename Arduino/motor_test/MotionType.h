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
 *  @author Orlin Dimnitrov (orlin369)
 *
 *  The purpose of this file to create the enumeration of the motion state of the robot.
 *
 */

#ifndef _MOTION_TYPE_H_
#define _MOTION_TYPE_H_

 /** \brief The enumeration of motion types. */
enum MotionType_t
{
	/** \brief No motion state. */
    None = 0,

	/** \brief Rotation state of the robot motion controller. */
	Rotate = 1,

	/** \brief Translation state of the robot motion controller. */
	Translate = 2
};

#endif 

