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

#ifndef _IRCOMMANDS_h
#define _IRCOMMANDS_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

// Forward
#define IR_CMD_FORWARED_LONG 2080
#define IR_CMD_FORWARED_SHORT 32

// Backward
#define IR_CMD_BACWARD_LONG 2081
#define IR_CMD_BACKWARD_SHORT 33

// Left
#define IR_CMD_LEFT_LONG 2069
#define IR_CMD_LEFT_SHORT 21

// Right
#define IR_CMD_RIGHT_LONG 2070
#define IR_CMD_RIGHT_SHORT 22

// GO
#define IR_CMD_GO_LONG 2066
#define IR_CMD_GO_SHORT 18

// Clear
#define IR_CMD_CLEAR_LONG 2065
#define IR_CMD_CLEAR_SHORT 17

// Pause
#define IR_CMD_PAUSE_LONG 2064
#define IR_CMD_PAUSE_SHORT 16

#endif

