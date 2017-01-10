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

/** @file BLE_Convertor.ino
 *  @brief Firmware for bluetooth comunication module for Karel v1.
 *  @author Orlin Dimnitrov (orlin369)
 */

// Examples
// https://www.adafruit.com/product/1697
// Module
// https://www.nordicsemi.com/eng/Products/Bluetooth-low-energy/nRF8001
// API
// https://github.com/adafruit/Adafruit_nRF8001

/* -- Includes -- */
/* SPI driver. */
#include <SPI.h>

/* Adafruit BLE nRF8001 driveer. */
#include "Adafruit_BLE_UART.h"


/* -- Prototypes -- */
/** @brief Reset robot procedure.
 *  @return Void.
 */
void reset_robot();

/** @brief RX callback procedure.
 *  @return Void.
 */
void rxCallback(uint8_t *buffer, uint8_t len);

/** @brief ACI callback procedure.
 *  @return Void.
 */
void aciCallback(aci_evt_opcode_t event);


/* -- Constants -- */
/** \brief Bluetooth request pin. */
const uint8_t BT_REQUEST_PIN = 10;

/** \brief Bluetooth ready pin. */
const uint8_t BT_READY_PIN = 2;

/** \brief Bluetooth reset pin. */
const uint8_t BT_RESET_PIN = 9;

/** \brief Reset robot pin. */
const uint8_t RESET_ROBOT = 3;

/** \brief Serial port boudrate. */
const uint16_t BOUDRATE = 9600;


/* -- Variables -- */
/** \brief Bluetooth module. */
Adafruit_BLE_UART g_BtModule = Adafruit_BLE_UART(BT_REQUEST_PIN, BT_READY_PIN, BT_RESET_PIN);


/** @brief Setup the peripheral hardware and variables.
 *  @return Void.
 */
void setup()
{
  // Setup pins.
  pinMode(RESET_ROBOT, OUTPUT);
  // Reset the robot.
  reset_robot();

  // Setup UART.
  Serial.begin(BOUDRATE);

  // Attach RX event.
  g_BtModule.setRXcallback(rxCallback);
  // Attach ACI event.
  g_BtModule.setACIcallback(aciCallback);
  // 7 characters max!
  g_BtModule.setDeviceName("KARELV1");
  // Run the blutooth.
  g_BtModule.begin();
}

/** @brief Main loop of the program.
 *  @return Void.
 */
void loop()
{
  // Tell the nRF8001 to do whatever it should be working on.
  g_BtModule.pollACI();

  // Send TX buffer.
  txSendBuffer();
}

/** @brief ACI callback procedure.
 *  @return Void.
 */
void aciCallback(aci_evt_opcode_t event)
{
  switch(event)
  {
    case ACI_EVT_DEVICE_STARTED:
      // Advertising started.
      break;
    case ACI_EVT_CONNECTED:
      reset_robot();
      // Connected.
      break;
    case ACI_EVT_DISCONNECTED:
      reset_robot();
      // Disconnected or advertising timed out.
      break;
    default:
      break;
  }
}

/** @brief RX callback procedure.
 *  @return Void.
 */
void rxCallback(uint8_t *buffer, uint8_t len)
{
  static String IncommingDataBuffer;

  // Clear incomming data bufer.
  IncommingDataBuffer = "";

  // Convert the buffer.
  for(int index = 0; index < len; index++)
  {
     IncommingDataBuffer += (char)buffer[index];
  }

  // Send incomming buffer.
  Serial.print(IncommingDataBuffer); 
}

void txSendBuffer()
{
  static String OutgoingDataBuffer;
  static uint8_t DataBufferLengthL;

  //
  OutgoingDataBuffer = "";
  DataBufferLengthL = 0;
  
  // Fill the command data buffer with command.
  while(Serial.available())
  {
    // Add new char.
    OutgoingDataBuffer += (char)Serial.read();
    DataBufferLengthL++;
    // Wait a while for a a new char.
    delay(5);
  }

  //
  g_BtModule.write(OutgoingDataBuffer.c_str(), DataBufferLengthL);
}

/** @brief Reset robot procedure.
 *  @return Void.
 */
void reset_robot()
{
  digitalWrite(RESET_ROBOT, LOW);
  delay(10);
  digitalWrite(RESET_ROBOT, HIGH);
}

