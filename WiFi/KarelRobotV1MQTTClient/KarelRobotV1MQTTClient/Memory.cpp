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

#pragma region Headers

/** EEPROM driver. */
#include "Memory.h"

#pragma endregion

#pragma region Constants

/** @brief Internal EEPROM size. */
const int EEPROM_SIZE = 4000;

#pragma endregion

#pragma region Functions Prototypes

/** @brief Clear the memory space.
*  @param uint16_t address, Address of the field.
*  @param uint16_t size, Field size
*  @return Void
*/
void clear(uint16_t address, uint16_t size);

/** @brief Read from the memory space.
*  @param uint16_t address, Address of the field.
*  @param uint16_t size, Field size
*  @return String, data content of the memory.
*/
String read(uint16_t address, uint16_t size);

/** @brief Write to the memory space.
*  @param uint16_t address, Address of the field.
*  @param String data, Field data.
*  @return Void
*/
void write(uint16_t address, String data);

#pragma endregion

#pragma region Functions

/** @brief Initialize memory communication.
 *  @return Void
 */
void init_memory()
{
	// Setup EEPROM
	EEPROM.begin(EEPROM_SIZE);
}

/** @brief Get SSID text from memory.
 *  @return SSID text.
 */
String get_ssid()
{
	return read(Fields::SSID, FIELD_SIZE);
}

/** @brief Set SSID text to memory.
 *  @param String ssid, SSID text.
 *  @return Void
 */
void set_ssid(String ssid)
{
	// Clear the storage to EEPROM.
	clear(Fields::SSID, FIELD_SIZE);

	// Fill the storage to EEPROM.
	for(uint32_t indexChar = 0; indexChar < ssid.length(); indexChar++)
	{
		if(ssid[indexChar] == '+') ssid[indexChar] = ' ';
		EEPROM.write(indexChar + Fields::SSID, ssid[indexChar]);
	}

	// Save the data to EEPROM.
	EEPROM.commit();
}

/** @brief Get password text from memory.
 *  @return Password text.
 */
String get_password()
{
	return read(Fields::Password, FIELD_SIZE);
}

/** @brief Set password text to memory.
 *  @param String password, Password text.
 *  @return Void
 */
void set_password(String password)
{
	// Clear the storage to EEPROM.
	clear(Fields::Password, FIELD_SIZE);

	// Fill the storage to EEPROM.
	for(uint32_t indexChar = 0; indexChar < password.length(); indexChar++)
	{
		EEPROM.write(indexChar + Fields::Password, password[indexChar]);
	}

	// Save the data to EEPROM.
	EEPROM.commit();
}

/** @brief Get input topic from memory.
 *  @return Password text.
 */
String get_input_topic()
{
	return read(Fields::InTopic, FIELD_SIZE);
}

/** @brief Set input topic to memory.
 *  @param String topic, Input topic.
 *  @return Void
 */
void set_input_topic(String topic)
{
	// Clear the storage to EEPROM.
	clear(Fields::InTopic, FIELD_SIZE);

	// Fill the storage to EEPROM.
	for (uint32_t indexChar = 0; indexChar < topic.length(); indexChar++)
	{
		//if(APSSID[indexChar] == '+') APSSID[indexChar] = ' ';
		EEPROM.write(indexChar + Fields::InTopic, topic[indexChar]);
	}

	// Save the data to EEPROM.
	EEPROM.commit();
}

/** @brief Get output topic from memory.
 *  @return Output topic.
 */
String get_output_topic()
{
	return read(Fields::OutTopic, FIELD_SIZE);
}

/** @brief Set output topic to memory.
 *  @param String topic, Output topic.
 *  @return Void
 */
void set_output_topic(String topic)
{
	// Clear the storage to EEPROM.
	for (int indexChar = Fields::InTopic; indexChar < Fields::OutTopic; indexChar++)
	{
		EEPROM.write(indexChar, '\0');
	}

	// Fill the storage to EEPROM.
	for (uint32_t indexChar = 0; indexChar < topic.length(); indexChar++)
	{
		//if(APSSID[indexChar] == '+') APSSID[indexChar] = ' ';
		EEPROM.write(indexChar + Fields::OutTopic, topic[indexChar]);
	}

	// Save the data to EEPROM.
	EEPROM.commit();
}

/** @brief Get output topic from memory.
*  @return Output topic.
*/
String get_broker()
{
	return read(Fields::Broker, FIELD_SIZE);
}

/** @brief Set output topic to memory.
*  @param String topic, Output topic.
*  @return Void
*/
void set_broker(String broker)
{
	// Clear the storage to EEPROM.
	clear(Fields::Broker, FIELD_SIZE);

	// Fill the storage to EEPROM.
	for (uint32_t indexChar = 0; indexChar < broker.length(); indexChar++)
	{
		//if(APSSID[indexChar] == '+') APSSID[indexChar] = ' ';
		EEPROM.write(indexChar + Fields::Broker, broker[indexChar]);
	}

	// Save the data to EEPROM.
	EEPROM.commit();
}


#pragma endregion

#pragma region Private Functions

/** @brief Clear the memory space.
 *  @param uint16_t address, Address of the field.
 *  @param uint16_t size, Field size
 *  @return Void
 */
void clear(uint16_t address, uint16_t size)
{
	// Clear the storage to EEPROM.
	for (uint16_t indexChar = address; indexChar < address + size; indexChar++)
	{
		EEPROM.write(indexChar, '\0');
	}
}

/** @brief Read from the memory space.
 *  @param uint16_t address, Address of the field.
 *  @param uint16_t size, Field size
 *  @return String, data content of the memory.
 */
String read(uint16_t address, uint16_t size)
{
	char ApSsidTextL;
	String ssid;

	// Fill the storage from EEPROM.
	for (int indexChar = address; indexChar < address + size; indexChar++)
	{
		// Read the EEPROM.
		ApSsidTextL = (char)EEPROM.read(indexChar);
		// If string end, break the reading.
		if (ApSsidTextL == '\0') { break; }
		// Add chars to the SSID.
		ssid += ApSsidTextL;
	}

	return ssid;
}

/** @brief Write to the memory space.
 *  @param uint16_t address, Address of the field.
 *  @param String data, Field data.
 *  @return Void
 */
void write(uint16_t address, String data)
{
	// Fill the storage to EEPROM.
	for (uint32_t indexChar = 0; indexChar < data.length(); indexChar++)
	{
		EEPROM.write(indexChar + address, data[indexChar]);
	}

	// Save the data to EEPROM.
	EEPROM.commit();
}

#pragma endregion
