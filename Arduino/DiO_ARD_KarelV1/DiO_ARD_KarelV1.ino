/*---------------------------------------------------------------------------------------\
|  FILE       : Karel v1 Example    |  This project is created for demonstrading,        |
|  DEVELOPER  : Orlin Dimitrov, DiO |  Karel v1 abilities.                               |
|----------------------------------------------------------------------------------------|
|                               R E Q U I R E S                                          |
|   1. Adafruit_Motorshield v2 library.                                                  |
|   https://github.com/adafruit/Adafruit_Motor_Shield_V2_Library                         |
|   2. AccelStepper with AFMotor support.                                                |
|   https://github.com/adafruit/AccelStepper                                             |
|   3. Ultrasonic sensor                                                                 |
|   http://blog.iteadstudio.com/arduino-library-for-ultrasonic-ranging-module-hc-sr04/   |
|                                                                                        |
|----------------------------------------------------------------------------------------|
|                                                         |                              |
|                  DiO_ARD_Karel_V1.ino                   | Copyright: MIT               |
|                                                         |                              |
\---------------------------------------------------------------------------------------*/

// I2C
#include <Wire.h>

// Motor driver/shield
#include <Adafruit_MotorShield.h>
#include "utility/Adafruit_PWMServoDriver.h"

// Motor regulators
#include <AccelStepper.h>

// Servo driver
#include <Servo.h>

// String and standart functions.
#include <String.h>
#include <stdlib.h>

// Ultrsconic sensor HC-SR04.
#include <Ultrasonic.h>

//////////////////////////////////////////////////////////////////////////
// Small distance sensor pin index.
//////////////////////////////////////////////////////////////////////////
#define SENSOR_LEFT  6
#define SENSOR_RIGHT 7

//////////////////////////////////////////////////////////////////////////
// Distance sensor pin index.
//////////////////////////////////////////////////////////////////////////
#define SENSOR_PIN_ECHO 5
#define SENSOR_PIN_TRIG 4

//////////////////////////////////////////////////////////////////////////
// Motor pin index.
//////////////////////////////////////////////////////////////////////////
#define LEFT_MOTOR_INDEX  1
#define RIGHT_MOTOR_INDEX 2

//////////////////////////////////////////////////////////////////////////
// Pin for the servo that cotrols the sensor position.
//////////////////////////////////////////////////////////////////////////
#define SERVO_PIN 9

//////////////////////////////////////////////////////////////////////////
// Prototypes
//////////////////////////////////////////////////////////////////////////
void ReadCommand();
boolean ValidateCommand(String command);
void ParseCommand(String command);
void RotateLeftCB();
void RotateRihtCB();
void TranslateForwardCB();
void TranslateBackwardCB();

//////////////////////////////////////////////////////////////////////////
// Motor shield address
//////////////////////////////////////////////////////////////////////////
char ShieldAddress = 0x60;
char MotorSteps = 200;

// Rotation parameters.
float MaxRotSpeed = 200.0;
float MaxRotAccel = 100.0;

// Translation parameters.
float MaxTransSpeed = 200.0;
float MaxTransAccel = 100.0;

//
boolean Echo = false;

//
char PrintArr[256];

// Ultra soic sensor: HC-SR04
Ultrasonic UltraSonic(SENSOR_PIN_TRIG, SENSOR_PIN_ECHO);

// Create driver for the motor shield.
Adafruit_MotorShield MotorShield(ShieldAddress);  
// Create two motors on chanel 1 and 2.
Adafruit_StepperMotor *MotorLeft = MotorShield.getStepper(MotorSteps, 1);
Adafruit_StepperMotor *MotorRight = MotorShield.getStepper(MotorSteps, 2);

// This is the acceleration controllers.
AccelStepper Rotation(RotateLeftCB, RotateRihtCB);
AccelStepper Translation(TranslateForwardCB, TranslateBackwardCB);

// Create servo object to control a servo.
Servo SensorServo;

//////////////////////////////////////////////////////////////////////////
// Setup function.
//////////////////////////////////////////////////////////////////////////
void setup()
{
  // Initialize the digital pin as an output.
  pinMode(SENSOR_LEFT , INPUT);
  pinMode(SENSOR_RIGHT, INPUT);
  
  // Create with the default frequency 1.6KHz.
  MotorShield.begin();
  //MotorShield.begin(1000);  // OR with a different frequency, say 1KHz
  
  // Attaches the servo on pin 9 to the servo object.
  SensorServo.attach(SERVO_PIN);
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

//////////////////////////////////////////////////////////////////////////  
// Loop function.
//////////////////////////////////////////////////////////////////////////
void loop()
{
  ReadCommand();
  Rotation.run();
  Translation.run();
}

//////////////////////////////////////////////////////////////////////////  
// Read incomming data from the serial buffer.
//////////////////////////////////////////////////////////////////////////  
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
    }
    // Print command for feedback.
    if(Echo)
    {
      Serial.print("Cmd; ");
      Serial.println(IncommingCommnad);
    }
  }
  
  // Clear the command data buffer.
  IncommingCommnad = ""; 
}

//////////////////////////////////////////////////////////////////////////  
// Validate incomming command from the serial buffer.
//////////////////////////////////////////////////////////////////////////  
boolean ValidateCommand(String command)
{
  //
  // ?R+180\n
  // ||||||||
  // ||||||\\- Termin
  // ||\\\\--- Degree value [-180 : +180].
  // ||\------ Direction index .
  // |\------- Motion type - in this case rotation.
  // \-------- Start symbol.
  //
  // ?M+999\n
  // ||||||||
  // ||||||\\- Termin
  // ||\\\\--- Degree value [-999 : +999].
  // ||\------ Direction index .
  // |\------- Motion type - in this case move.
  // \-------- Start symbol.
  //
  // Package lenght 1 + 1 + 1 + 3 + 1 = 7 bytes;
  //
  
  
  boolean state = false;
  // Number value.
  static int numValue;
  numValue = 0;
  // ?M+1800\n
  if(command[0] == '?' && command[7] == '\n')
  {
    if(command[2] == '-' || command[2] == '+')
    {
      if(command[1] == 'M')
      {
        // Convert commands from string to numbers.
        numValue = atoi(command.substring(3, 7).c_str());

        if(numValue <= 9999 && numValue >= -9999)
        {
          // If is valid.
          state = true;
        }
      }
      if(command[1] == 'R')
      {
        if(numValue <= 9999 && numValue >= -9999)
        {
          // If is valid.
          state = true;
        } 
      }
    }
  }
  // ?US180\n
  if(command[0] == '?' && command[6] == '\n')
  {
    if(command[1] == 'U' && command[2] == 'S')
    {
      // Convert commands from string to numbers.
      numValue = atoi(command.substring(3, 6).c_str());       
      if(numValue >= 0 && numValue <= 180)
      {
        // If is valid.
        state = true;
      }
    }
  }
  if(command == "?SENSORS\n")
  {
    // If is valid.
    state = true;
  }
  if(command == "?POSITION\n")
  {
    // If is valid.
    state = true;
  }
  if(command == "?STOP\n")
  {
    // If is valid.
    state = true;
  }
  if(command == "?USA\n")
  {
    // If is valid.
    state = true;
  }
  
  // If is not valid.
  return state;
}

void ParseCommand(String command)
{
  // Number value.
  static int steps;
  static long r;
  static long t;
  static float cmMsec;
  static long microsec;
  
  steps = 0;
  r = 0;
  t = 0;
  cmMsec = 0;
  microsec = 0;
  
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
    int leftSensor = digitalRead(SENSOR_LEFT);
    int rightSensor = digitalRead(SENSOR_RIGHT);
    sprintf(PrintArr, "#SENSORS;L:%d;R:%d", leftSensor, rightSensor);
    Serial.println(PrintArr);
  }
  else if(command == "?POSITION\n")
  {
    r = Rotation.currentPosition();
    t = Translation.currentPosition();
    sprintf(PrintArr, "#POSITION;D:%ld;A:%ld;", t, r);
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
    int pos = SensorServo.read();
    pos += 1000;
    delay(pos);
    
    for(int indexPos = 0; indexPos <= 180; indexPos++)
    {
        SensorServo.write(indexPos);
        delay(100);
        microsec = UltraSonic.timing();     
        cmMsec = UltraSonic.convert(microsec, Ultrasonic::CM);
        //sprintf(PrintArr, "#US;%d:%d", indexPos, cmMsec);
        //Serial.println(PrintArr);   

        Serial.print("#US;");
        Serial.print(indexPos);
        Serial.print(":");
        Serial.println(cmMsec);
    }
  }
  // ?US180\n
  else if(command[0] == '?' && command[6] == '\n')
  {
    if(command[1] == 'U' && command[2] == 'S')
    {
      // Convert commands from string to numbers.
      steps = atoi(command.substring(3, 6).c_str());
       
      if(steps >= 0 && steps <= 180)
      {
        SensorServo.write(steps);
        //delay(2000);
        microsec = UltraSonic.timing();     
        cmMsec = UltraSonic.convert(microsec, Ultrasonic::CM);

        Serial.print("#US;");
        Serial.print(steps);
        Serial.print(":");
        Serial.println(cmMsec);
      }
    }
  }
}

//////////////////////////////////////////////////////////////////////////  
// This fuctions is call backs for the acceleration motor driver.
// The first two functions is used for rotation of the robot.
////////////////////////////////////////////////////////////////////////// 
void RotateLeftCB()
{
  MotorLeft->onestep(BACKWARD, MICROSTEP);
  MotorRight->onestep(FORWARD, MICROSTEP);
}

void RotateRihtCB()
{
  MotorLeft->onestep(FORWARD, MICROSTEP);
  MotorRight->onestep(BACKWARD, MICROSTEP);
}

//////////////////////////////////////////////////////////////////////////
// The second one is used for the translation of the robot.
//////////////////////////////////////////////////////////////////////////
void TranslateForwardCB()
{
  MotorLeft->onestep(FORWARD, MICROSTEP);
  MotorRight->onestep(FORWARD, MICROSTEP);
}

void TranslateBackwardCB()
{
  MotorLeft->onestep(BACKWARD, MICROSTEP);
  MotorRight->onestep(BACKWARD, MICROSTEP);
}

