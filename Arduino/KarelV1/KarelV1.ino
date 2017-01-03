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
 *
 *  This project is created for demonstrading, Karel v1 abilities. 
 *
 *  @author Orlin Dimnitrov (orlin369)
 */

/*
Resources.
Adafruit_Motorshield v2:   https://github.com/adafruit/Adafruit_Motor_Shield_V2_Library
AccelStepper with AFMotor: https://github.com/adafruit/AccelStepper
Ultrasonic sensor:  http://blog.iteadstudio.com/arduino-library-for-ultrasonic-ranging-module-hc-sr04/
*/

/*
Protocol of communication.
Move robot:
?R+9999\n
|||||||||
|||||||\\- Termin
||\\\\\--- Degree value [-180 : +180].
||\------- Direction index .
|\-------- Motion type - in this case rotation.
\--------- Start symbol.

?M+9999\n
|||||||||
|||||||\\- Termin
||\\\\\--- Degree value [-999 : +999].
||\------- Direction index .
|\-------- Motion type - in this case move.
\--------- Start symbol.

Read ultrasonic sensor value:
?US180\n
||||||||
||||||\\- Termin symbol.
|||\\\--- Sensor position value.
|\\------ Command marker.
\-------- Request symbol.

?USA\n
||||||
||||\\- Termin symbol.
|||\--- Sensor position value.
|\\---- Command marker.
\------ Request symbol.

Version:
?VERSION\n
||||||||||
||||||||\\- Termin symbol.
|\\\\\\\--- Command marker.
\---------- Request symbol.
*/

  //

/* -- Includes -- */
/* I2C */
#include <Wire.h>
/* Motor driver/shield */
#include <Adafruit_MotorShield.h>
/* String and standart functions. */
#include "utility/Adafruit_PWMServoDriver.h"
/* Stepper motor controller. */
#include <AccelStepper.h>
/* Servo driver */
#include <Servo.h>
/* String */
#include <String.h>
/* Standart library. */
#include <stdlib.h>
/* Ultrsconic sensor HC-SR04. */
#include <Ultrasonic.h>

/* -- Prototypes -- */
void ReadCommand();
boolean ValidateCommand(String command);
void ParseCommand(String command);
void RotateLeftCB();
void RotateRihtCB();
void TranslateForwardCB();
void TranslateBackwardCB();

/* -- Pins -- */
/** \brief Ultrasonic sensor trig pin 4.*/
const uint8_t SensorPinTrig = 4;
/** \brief Ultrasonic sensor echo pin 5.*/
const uint8_t SensorPinEcho = 5;
/** \brief Left sensor pin 6. */
const uint8_t SensorLeftEdge = 6;
/** \brief Left sensor pin 7. */
const uint8_t SensorRightEdge = 7;
/** \brief Servo pin that cotrols the sensor position.*/
const uint8_t ServoPinUltrasonicSensor = 9;

/* -- Parametters -- */
/** \brief Motor shield address. */
const uint8_t ShieldAddress = 0x60;
/** \brief Motors max steps. */
const uint8_t MotorSteps = 200;
/** \brief Maximum rotation speed. */
float MaxRotSpeed = 200.0;
/** \brief Maximum rotation acceleration. */
float MaxRotAccel = 100.0;
/** \brief Maximum translation speed. */
float MaxTransSpeed = 200.0;
/** \brief Maximum translation acceleration. */
float MaxTransAccel = 100.0;
/** \brief Left motor index. */
const uint8_t  LeftMotorIndex = 1;
/** \brief Right motor index. */
const uint8_t  RightMotorIndex = 2;
/** \brief Maximum executable steps. */
const int16_t MaxSteps = 9999;
/** \brief Echo bit. */
boolean Echo = false;

/* -- Peripheral objects. -- */
/** \brief Ultra soic sensor: HC-SR04 */
Ultrasonic UltraSonic(SensorPinTrig, SensorPinEcho);
/** \brief The motor shield. */
Adafruit_MotorShield MotorShield(ShieldAddress);  
/** \brief Left motors on chanel 1. */
Adafruit_StepperMotor *MotorLeft = MotorShield.getStepper(MotorSteps, 1);
/** \brief Right motors on chanel 2. */
Adafruit_StepperMotor *MotorRight = MotorShield.getStepper(MotorSteps, 2);
/** \brief Rotation acceleration controller. */
AccelStepper Rotation(RotateLeftCB, RotateRihtCB);
/** \brief Translation acceleration controller. */
AccelStepper Translation(TranslateForwardCB, TranslateBackwardCB);
/** \brief Ultrasonic sensor servo controller. */
Servo SensorServo;
/** \brief Output serial buffer. */
char PrintArr[256];

/* -- Functions -- */
/** @brief Setup the peripheral hardware and variables.
 *  @return Void.
 */
void setup()
{
  // Initialize the digital pin as an output.
  pinMode(SensorLeftEdge , INPUT);
  pinMode(SensorRightEdge, INPUT);
  
  // Create with the default frequency 1.6KHz.
  MotorShield.begin();
  //MotorShield.begin(1000);  // OR with a different frequency, say 1KHz
  
  // Attaches the servo on pin 9 to the servo object.
  SensorServo.attach(ServoPinUltrasonicSensor);
  // Sets the servo position to the hoe position.
  SensorServo.write(0);
   
  // Setup Serial port at 9600 bps.
  Serial.begin(9600);         
  Serial.println("#GREATINGS;I am Karel v1 ");
  
  // ...
  Rotation.setMaxSpeed(MaxRotSpeed);
  Rotation.setAcceleration(MaxRotAccel);
  // ...  
  Translation.setMaxSpeed(MaxTransSpeed);
  Translation.setAcceleration(MaxTransAccel);
}

/** @brief Main loop of the program.
 *  @return Void.
 */
void loop()
{
  ReadCommand();
  Rotation.run();
  Translation.run();
}

/** @brief Read incomming data from the serial buffer.
 *  @return Void.
 */
void ReadCommand()
{  
  static String IncommingCommnad = "";
  
  // Fill the command data buffer with command.
  while(Serial.available())
  {
    // Add new char.
    IncommingCommnad += (char)Serial.read();
    // Wait a while for a a new char.
    delay(5);
  }
  
  // If command if not empty parse it.
  if(IncommingCommnad != "")
  {
    boolean isValid = ValidateCommand(IncommingCommnad);
    if(isValid)
    {
      ParseCommand(IncommingCommnad);
          // Print command for feedback.
      if(Echo)
      {
        Serial.print("Cmd; ");
        Serial.println(IncommingCommnad);
      }
    }

  }
  
  // Clear the command data buffer.
  IncommingCommnad = ""; 
}

/** @brief Validate the incomming commands.
 *  @param command The command string.
 *  @return True if successful; or False if failed.
 */
boolean ValidateCommand(String command)
{
  // Variable definitions.
  static boolean isValid;
  static int numValue;
  
  numValue = 0;
  isValid = false;
  
  if(command[0] == '?' && command[7] == '\n')
  {
    if(command[2] == '-' || command[2] == '+')
    {
      if(command[1] == 'M')
      {
        // Convert commands from string to numbers.
        numValue = atoi(command.substring(3, 7).c_str());
        if(numValue <= MaxSteps && numValue >= -MaxSteps)
        {
          // If is valid.
          isValid = true;
        }
      }
      if(command[1] == 'R')
      {
        if(numValue <= MaxSteps && numValue >= -MaxSteps)
        {
          // If is valid.
          isValid = true;
        } 
      }
    }
  }
  if(command[0] == '?' && command[6] == '\n')
  {
    if(command[1] == 'U' && command[2] == 'S')
    {
      // Convert commands from string to numbers.
      numValue = atoi(command.substring(3, 6).c_str());       
      if(numValue >= 0 && numValue <= 180)
      {
        // If is valid.
        isValid = true;
      }
    }
  }
  if(command == "?SENSORS\n")
  {
    // If is valid.
    isValid = true;
  }
  if(command == "?POSITION\n")
  {
    // If is valid.
    isValid = true;
  }
  if(command == "?STOP\n")
  {
    // If is valid.
    isValid = true;
  }
  if(command == "?USA\n")
  {
    // If is valid.
    isValid = true;
  }
  
  // If is not valid.
  return isValid;
}

/** @brief Parse and execute the incomming commands.
 *  @param command The command string.
 *  @return Void.
 */
void ParseCommand(String command)
{
  // Number value.
  static int steps;
  static long rotationSteps;
  static long translationSteps;
  static float cmMsec;
  static long microsecond;
  
  steps = 0;
  rotationSteps = 0;
  translationSteps = 0;
  cmMsec = 0;
  microsecond = 0;
  
  // Convert commands from string to numbers.
  steps = atoi(command.substring(3, 7).c_str());
  
  if(command[1] == 'M')
  {
    if(command[2] == '-')
    {
      steps *= -1;
    }
    else if(command[2] == '+')
    {
      ;
    }
    
    Translation.move(steps);
  }
  else if(command[1] == 'R')
  {
    if(command[2] == '-')
    {
      steps *= -1;
    }
    else if(command[2] == '+')
    {
      ;
    }
    
    Rotation.move(steps);
  }
  else if(command == "?SENSORS\n")
  {
    // read the analog in value:
    int leftSensor = digitalRead(SensorLeftEdge);
    int rightSensor = digitalRead(SensorRightEdge);
    sprintf(PrintArr, "#SENSORS;L:%d;R:%d", leftSensor, rightSensor);
    Serial.println(PrintArr);
  }
  else if(command == "?POSITION\n")
  {
    rotationSteps = Rotation.currentPosition();
    translationSteps = Translation.currentPosition();
    sprintf(PrintArr, "#POSITION;T:%ld;R:%ld;", translationSteps, rotationSteps);
    Serial.println(PrintArr);   
  }
  else if(command == "?STOP\n")
  {
    MotorLeft->release();
    MotorRight->release();
    Serial.println("#STOP");
  }
  else if(command == "?USA\n")
  {
    // Preventing errors in mesurments.
    SensorServo.write(0);
    int position = SensorServo.read();
    position += 1000;
    delay(position);
    
    for(int indexPos = 0; indexPos <= 180; indexPos++)
    {
        SensorServo.write(indexPos);
        delay(100);
        microsecond = ReadDistance();     

        Serial.print("#US;");
        Serial.print(indexPos);
        Serial.print(":");
        Serial.println(microsecond);
    }
  }
  else if(command[0] == '?' && command[6] == '\n')
  {
    if(command[1] == 'U' && command[2] == 'S')
    {
      // Convert commands from string to numbers.
      steps = atoi(command.substring(3, 6).c_str());
       
      if(steps >= 0 && steps <= 180)
      {
        SensorServo.write(steps);
        microsecond = ReadDistance();     

        Serial.print("#US;");
        Serial.print(steps);
        Serial.print(":");
        Serial.println(microsecond);
      }
    }
  }
}

long ReadDistance()
{

  static long sum;
  sum = 0;
  
  for(int i = 0; i < 3; i++)
  {
    sum = sum + UltraSonic.timing();
    delay(1);
  }
  
  return sum / 5; 
}

/** @brief This fuctions is callbacck for left motion.
 *  @return Void.
 */
void RotateLeftCB()
{
  MotorLeft->onestep(BACKWARD, MICROSTEP);
  MotorRight->onestep(FORWARD, MICROSTEP);
}

/** @brief This fuctions is callbacck for right motion.
 *  @return Void.
 */
void RotateRihtCB()
{
  MotorLeft->onestep(FORWARD, MICROSTEP);
  MotorRight->onestep(BACKWARD, MICROSTEP);
}

/** @brief This fuctions is callbacck for forward motion.
 *  @return Void.
 */
void TranslateForwardCB()
{
  MotorLeft->onestep(FORWARD, MICROSTEP);
  MotorRight->onestep(FORWARD, MICROSTEP);
}

/** @brief This fuctions is callbacck for backward motion.
 *  @return Void.
 */
void TranslateBackwardCB()
{
  MotorLeft->onestep(BACKWARD, MICROSTEP);
  MotorRight->onestep(BACKWARD, MICROSTEP);
}

