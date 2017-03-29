WiFi
=======================

Introduction
------------
This is a simple aplication for ESP8266-01 board.
Application directly connect the chip to the MQTT broker.
The connection between MQTT and the robot is via UART.
Basicly one topic for TX data and one topic for RX data.

### What we are using

`MQTT` - Simple MQTT client. - https://github.com/knolleary/pubsubclient
`ESP8266-01` - The chip. - http://www.microchip.ua/wireless/esp01.pdf
`Visual Studio / Arduino` - We use Visual Studio for the project and Visual micro plugin - http://www.visualmicro.com/

### HowToBasic

MQTT alows simply to excange data between PC and WiFi of the robot via the broker.

Main idea behind this project is to control/communicate with the robot via internet.
