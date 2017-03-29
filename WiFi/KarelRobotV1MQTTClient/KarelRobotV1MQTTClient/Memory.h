/*

Copyright (c) [2017] [GreenCorp Ltd.]

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

/**
 * @brief Memory header file.
 * @author Orlin Dimitrov <or.dimitrov@polygonteam.com>
 * @version 1.0
 */

#ifndef MEMORY_H
#define MEMORY_H

#pragma region Headers

#if ARDUINO >= 100
 #include "Arduino.h"
#else
 #include "WProgram.h"
#endif

/* EEPROM driver. */
#include <EEPROM.h>

#pragma endregion

#pragma region Constants

/** @brief Field size in the EEPROM. */
const uint16_t FIELD_SIZE = 32;

const String SSID_KEY = String("ssid");

const String PASS_KEY = String("pass");

const String ITOPIC_KEY = String("itopic");

const String OTOPIC_KEY = String("otopic");

const String BROKER_KEY = String("broker");

#pragma endregion

#pragma region Structures

enum Fields
{
	SSID     = 0 * FIELD_SIZE,
	Password = 1 * FIELD_SIZE,
	InTopic  = 2 * FIELD_SIZE,
	OutTopic = 3 * FIELD_SIZE,
	Broker   = 4 * FIELD_SIZE,
};

#pragma endregion

#pragma region Functions Protoypes

/** @brief Initialize memory communication.
 *  @return Void
 */
void init_memory();

/** @brief Get SSID text from memory.
 *  @return SSID text.
 */
String get_ssid();

/** @brief Set SSID text to memory.
 *  @param String ssid, SSID text.
 *  @return Void
 */
void set_ssid(String ssid);

/** @brief Get password text from memory.
 *  @return Password text.
 */
String get_password();

/** @brief Set password text to memory.
 *  @param String password, Password text.
 *  @return Void
 */
void set_password(String password);

String get_input_topic();

void set_input_topic(String topic);

String get_output_topic();

void set_output_topic(String topic);

String get_broker();

void set_broker(String broker);

#pragma endregion

#endif
