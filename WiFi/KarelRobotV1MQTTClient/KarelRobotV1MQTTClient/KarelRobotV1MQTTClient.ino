/*

Copyright (c) [2017] [Orlin Dimitrv]

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

/** @file DiO_ARD_GreenCorp.ino
 *  @brief WiFi module (ESP8266-01) software.
 *
 * It connects to an MQTT server then:
 *  - publishes "hello world" to the topic "outTopic" every two seconds
 *  - subscribes to the topic "inTopic", printing out any messages
 *    it receives. NB - it assumes the received payloads are strings not binary
 *  - If the first character of the topic "inTopic" is an 1, switch ON the ESP Led,
 *    else switch it off
 *
 * It will reconnect to the server if the connection is lost using a blocking
 * reconnect function. See the 'mqtt_reconnect_nonblocking' example for how to
 * achieve the same result without blocking the main loop.
 *
 * To install the ESP8266 board, (using Arduino 1.6.4+):
 *  - Add the following 3rd party board manager under "File -> Preferences -> Additional Boards Manager URLs":
 *       http://arduino.esp8266.com/stable/package_esp8266com_index.json
 *  - Open the "Tools -> Board -> Board Manager" and click install for the ESP8266"
 *  - Select your ESP8266 in "Tools -> Board"
 *  @author Orlin Dimnitrov (orlin369)
 */

#pragma region Definitions

#define SERIAL_DEBUG
//#define SECURE_MQTT

#pragma endregion

#pragma region Headers

/* WiFi module driver. */
#ifdef ESP8266
/* http://arduino.esp8266.com/stable/package_esp8266com_index.json */
#include <ESP8266WiFi.h>
#endif
#ifdef ESP32 
#include <WiFi.h>
#endif

/* WiFi module modes */
#include <ESP8266WiFiType.h>

/* WEB server */
#include <ESP8266WebServer.h>

/* WEB client unsecured. */
#include <ESP8266HTTPClient.h>

/* DNS Server. */
#include <DNSServer.h>

/* https://github.com/knolleary/pubsubclient */
#include <PubSubClient.h>

#include "Memory.h"

#pragma endregion

#pragma region Functions Prototypes

/** @brief Wait 3 seconds for switch the mode.
 *  @return WiFiMode_t, radio mode.
 */
WiFiMode_t wait_soft_reset();

/** @brief MQTT reconnect procedure.
 *  @param char* topic, Topic name.
 *  @param byte* payload, Payload data.
 *  @param unsigned int length, Payload length.
 *  @return Void.
 */
void mqtt_callback(char* topic, byte* payload, unsigned int length);

/** @brief MQTT reconnect procedure.
 *  @return Void.
 */
void mqtt_connect();

/** @brief Read incoming data from the serial buffer.
 *  @return Void.
 */
void read_command();

/** @brief Read incoming data from the serial buffer.
 *  @return Void.
 */
void setup_dns();

/** @brief Serves the configuration page of the device.
 *  @return Void.
 */
void setup_web_server();

/** @brief Generate HTML option.
 *  @return String, generate HTML option.
 */
String generate_option(String option_text);

/** @brief Setup the WiFi module to access point mode.
 *  @return Void.
 */
void setup_wifi_ap();

/** @brief Setup the WiFi module to station mode.
 *  @return Void.
 */
void setup_wifi_sta();

#pragma endregion

#pragma region Constants

/** @brief DNS port. */
const byte DNS_PORT = 53;

/** @brief Reconnect try times. */
const byte RECONNECT_TIMES = 8;

/** @brief WEB server port. */
const int WEB_SERVER_PORT = 80;

/** @brief Serial port speed. */
const int g_BAUD_RATE = 115200;

/** @brief Software reset button, GPIO0 */
const int BTN_SOFT_RESET = 0;

/** @brief MQTT broker server port. */
const int g_MQTT_PORT = 1883;

/** @brief WiFi password. */
const char* AP_PASSWORD = "12345678";

/** @brief MQTT input topic. */
const char* g_InputTopic = "pt/str/i/robot/1";

/** @brief MQTT output topic. */
const char* g_OutputTopic = "pt/str/o/robot/1";

#pragma endregion

#pragma region Variables

/** @brief Domain of the setup page. */
char* g_CfgPageAddress = "www.karelv1.com";

/** @brief MQTT domain. */
String g_MqttDomain;

/** @brief WiFi module mode Access Point / Station. */
WiFiMode_t g_WiFiMode;

/** @brief Local DNS server. */
DNSServer g_DnsServer;

/** @brief IP address of the access point */
IPAddress g_APIP(192, 168, 1, 1);

/** @brief subnet mask of the access point */
IPAddress g_SubnetMask(255, 255, 255, 0);

/** @brief WEB server, to serve the configuration page. */
ESP8266WebServer g_WebServer(WEB_SERVER_PORT);

/** @brief WiFi client for the MQTT. */
WiFiClient g_WiFiClient;

/** @brief MQTT client. */
PubSubClient g_MqttClient(g_WiFiClient);

#ifdef SECURE_MQTT

char g_AuthMethod[] = "use-token-auth";

char g_AuthToken[] = "<yourDeviceToken>";

#endif // SECURE_MQTT

#pragma endregion

/** @brief Setup the peripheral hardware and variables.
 *  @return Void.
 */
void setup()
{
	// Initialize the BUILTIN_LED pin as an output
	//pinMode(BUILTIN_LED, OUTPUT);
	//D4

	// Setup IOs
	pinMode(BTN_SOFT_RESET, INPUT_PULLUP);

	// Initialize memory.
	init_memory();

	// Default SSID and password of the AP.
#error "Define your own credentials."
	set_ssid("<your-ssid>");
	set_password("<you-password>");
	set_broker("<your-broker-host>"); // example: iot.eclipse.org

	g_MqttDomain = get_broker();

	// Use Serial (port 0).
	Serial.begin(g_BAUD_RATE);

#ifdef SERIAL_DEBUG
	// Some new lines to pass the boot loader shits.
	Serial.println("\r\n\r\n");
	Serial.println("Starting ...");
	Serial.println("Waiting for software reset.");
#endif

	// 
	g_WiFiMode = WIFI_STA;

	// Give the user 1 second to press the mode button for 3 seconds.
	delay(1000);

	// Wait for software reset.
	g_WiFiMode = wait_soft_reset();

	if (g_WiFiMode == WIFI_AP)
	{
#ifdef SERIAL_DEBUG
		Serial.print("Mode:");
		Serial.println(g_WiFiMode);
		Serial.println("Access point");
#endif
		setup_web_server();
		setup_wifi_ap();
		setup_dns();
	}
	else if (g_WiFiMode == WIFI_STA)
	{
#ifdef SERIAL_DEBUG
		Serial.print("Mode:");
		Serial.println(g_WiFiMode);
		Serial.println("Station");
#endif
		setup_wifi_sta();
	}
}

/** @brief Main loop of the program.
 *  @return Void.
 */
void loop()
{
	static wl_status_t WlStatusL;

	// Update status.
	WlStatusL = WiFi.status();

	if (g_WiFiMode == WIFI_AP)
	{
		g_DnsServer.processNextRequest();
		g_WebServer.handleClient();
	}
	else if (g_WiFiMode == WIFI_STA)
	{
		// Check if the status is connected.
		if (WlStatusL == WL_CONNECTED)
		{
			// Reconnect if connection failed.
			if (!g_MqttClient.connected())
			{
				mqtt_connect();
			}
			else
			{
				// Run message transport.
				g_MqttClient.loop();

				// Read command from the serial port.
				read_command();
			}
		}
		// Check if the status is connection lost.
		else if (WlStatusL == WL_CONNECTION_LOST)
		{
			ESP.restart();
		}
		// Check if the status is connection lost.
		else if (WlStatusL == WL_CONNECT_FAILED)
		{
			ESP.restart();
		}
	}
}

#pragma region Functions

/** @brief Wait 3 seconds for switch the mode.
 *  @return WiFiMode_t, radio mode.
 */
WiFiMode_t wait_soft_reset()
{
	WiFiMode_t WiFiModeL = WIFI_STA;

	int btnSoftResetState = HIGH;
	int btnSoftResetCount = 0;

	// Search for software reset.
	for (;;)
	{
		// Read the button.
		btnSoftResetState = digitalRead(BTN_SOFT_RESET);

		// If the button is pressed increment the counter.
		if (btnSoftResetState == LOW)
		{
			btnSoftResetCount++;
		}
		// If the button is not pressed, just exit the cycle.
		else if (btnSoftResetState == HIGH)
		{
			WiFiModeL = WIFI_STA;
			break;
		}
		// If it is pressed for 3 seconds.
		if (btnSoftResetCount >= 30)
		{
			WiFiModeL = WIFI_AP;
			break;
		}

		// Wait for button to stabilize.
		delay(100);
	}

	return WiFiModeL;
}

/** @brief MQTT reconnect procedure.
 *  @param char* topic, Topic name.
 *  @param byte* payload, Payload data.
 *  @param unsigned int length, Payload length.
 *  @return Void.
 */
void mqtt_callback(char* topic, byte* payload, unsigned int length)
{
	if (strcmp(topic, g_OutputTopic) == 0)
	{
		char message[20];
		memcpy(&message, payload, length);
		message[length] = '\0';

		Serial.print(message);
	}
}

/** @brief MQTT reconnect procedure.
 *  @return Void.
 */
void mqtt_connect()
{
	// Setup random.
	randomSeed(micros());

	// Create a random client ID
	String ClientIdL = String(g_CfgPageAddress);
	ClientIdL += String(random(0xffff), HEX);

	// Set the server.
	g_MqttClient.setServer(g_MqttDomain.c_str(), g_MQTT_PORT);

#ifdef SERIAL_DEBUG
	Serial.print("Connecting to: ");
	Serial.println(g_MqttDomain);
#endif // SERIAL_DEBUG
	
	// Set callback for incoming messages.
	g_MqttClient.setCallback(mqtt_callback);
	
	// Loop until we're reconnected.
	while (!g_MqttClient.connected())
	{
		// Attempt to connect
#ifdef SECURE_MQTT
		if (g_MqttClient.connect(ClientIdL.c_str(), g_AuthMethod, g_AuthToken))
#else
		if (g_MqttClient.connect(ClientIdL.c_str()))
#endif
		{
			g_MqttClient.subscribe(g_OutputTopic);
		}
		else
		{
#ifdef SERIAL_DEBUG
			Serial.print("failed, rc=");
			Serial.print(g_MqttClient.state());
			Serial.println(" try again in 2 seconds");
#endif
			delay(2000);
		}
	}
}

/** @brief Read incoming data from the serial buffer.
 *  @return Void.
 */
void read_command()
{
	static String IncommingCommnad = "";

	// Fill the command data buffer with command.
	while (Serial.available())
	{
		// Add new char.
		IncommingCommnad += (char)Serial.read();
		// Wait a while for a a new char.
		delay(5);
	}

	// If command if not empty parse it.
	if (IncommingCommnad != "")
	{
		// Publish message.
		g_MqttClient.publish(g_InputTopic, IncommingCommnad.c_str());
		//Serial.println(IncommingCommnad);
	}

	// Clear the command data buffer.
	IncommingCommnad = "";
}

/** @brief Read incoming data from the serial buffer.
 *  @return Void.
 */
void setup_dns()
{
	// modify TTL associated    with the domain name (in seconds)
	// default is 60 seconds
	g_DnsServer.setTTL(300);
	// set which return code will be used for all other domains (e.g. sending
	// ServerFailure instead of NonExistentDomain will reduce number of queries
	// sent by clients)
	// default is DNSReplyCode::NonExistentDomain
	g_DnsServer.setErrorReplyCode(DNSReplyCode::ServerFailure);

	// start DNS server for a specific domain name
	g_DnsServer.start(DNS_PORT, "www.karelv1.com", g_APIP);
}

/** @brief Serves the configuration page of the device.
 *  @return Void.
 */
void setup_web_server()
{
	// Local variables.
	static String NetworksNamesL[50];
	static String CfgPageL = "";
	int NetowrksCountL = 0;

	// Set the WiFi module to station mode.
	WiFi.mode(WIFI_STA);

	// Disconnect to see the other networks.
	WiFi.disconnect();

	// Take the count of the networks.
	NetowrksCountL = WiFi.scanNetworks();

	// Fill the array of the networks.
	for (int index = 0; index < NetowrksCountL; ++index)
	{
		NetworksNamesL[index] = WiFi.SSID(index);
		delay(10);
	}

#ifdef SERIAL_DEBUG
	// Display the count.
	Serial.print(NetowrksCountL);
	Serial.println(" networks found");

	// Display the names and parameters.
	for (int index = 0; index < NetowrksCountL; index++)
	{
		// Print SSID and RSSI for each network found.
		Serial.print(index + 1);
		Serial.print(": ");
		Serial.print(NetworksNamesL[index]);
		Serial.print(" (");
		Serial.print(WiFi.RSSI(index));
		Serial.print(")");
		Serial.println((WiFi.encryptionType(index) == ENC_TYPE_NONE) ? " " : "*");
	}
#endif

	// Clear the page buffer.
	CfgPageL = "";

	#pragma region Configuration Page

	// Prepare the response.
	CfgPageL += "<!DOCTYPE html>";
	CfgPageL += "<html>";
	CfgPageL += "<head>";
	CfgPageL += "<meta http-equiv='content-type' content='text/html; charset=windows-1252'>";
	CfgPageL += "<style>";
	CfgPageL += "body,h1{text-align:center;color:#fff}body{font-family:Arial;background-color:#757575}[type=text]{border-radius:10px;border:2px solid #5D5B5B;padding:12px;width:calc(100% - 24px);font-size:20px}p{margin:4px 0 6px;line-height:20px}[type=submit]{border-radius:10px;border:2px solid #47704c;padding:15px 33px;background-color:#31e447;color:#fff;font-weight:700;width:100%}h1{display:inline-block;width:300px}.row{margin:10px}.dropdown{border-radius:10px;border:2px solid #5D5B5B;padding:12px;width:100%;font-size:20px}@media only screen and (min-width:768px){.dropdown,[type=submit]{width:426px}[type=text]{width:400px}.dropdown{margin-left:-4px}}";
	CfgPageL += "</style>";
	CfgPageL += "</head>";
	CfgPageL += "<body>";
	CfgPageL += "<form>";
	CfgPageL += "<h1>Robot Karel V1</h1>";
	CfgPageL += "<p>SSID:</p>";
	CfgPageL += "<select name='ssid' class='dropdown'>";
	for (int index = 0; index < NetowrksCountL; index++)
	{
		CfgPageL += generate_option(NetworksNamesL[index]);
	}
	CfgPageL += "</select>";
	CfgPageL += "<p>PASS:</p><input name='pass' type='text'>";
	CfgPageL += "<p>Broker:</p><input name='broker' type='text'>";
	CfgPageL += "<p>Input topic:</p><input name='itopic' type='text'>";
	CfgPageL += "<p>Output topic:</p><input name='otopic' type='text'>";
	CfgPageL += "<div><input value='Save' type='submit'></div>";
	CfgPageL += "</form>";
	CfgPageL += "</body>";
	CfgPageL += "</html>";

	#pragma endregion

	// Send the response to the client
	g_WebServer.on("/", []()
	{
		// Example request.
		// http://www.karelv1.com?pass=qwertyi&broker=176.33.1.5&itopic=pt%2Fi%2Fdata%2Fda&otopic=pt%2Fo%2Fdata%2Fda

		// Storage of the arguments.
		String ApSsidL = g_WebServer.arg(SSID_KEY);
		String ApPassL = g_WebServer.arg(PASS_KEY);
		String MqttBroker = g_WebServer.arg(BROKER_KEY);
		String MqttOTopic = g_WebServer.arg(OTOPIC_KEY);
		String MqttITopic = g_WebServer.arg(ITOPIC_KEY);

		// Set SSID and password.
		if ((ApSsidL != "") && (ApPassL != ""))
		{
			set_ssid(ApSsidL);
			set_password(ApPassL);  
		}

		// Set broker.
		if (MqttBroker != "")
		{
			set_broker(MqttBroker);
		}

		// Set output topic.
		if (MqttOTopic != "")
		{
#ifdef SERIAL_DEBUG
			Serial.println(MqttOTopic);
#endif
			//TODO: Debug %2F
			//set_output_topic(MqttOTopic);
		}

		// Set input topic.
		if (MqttITopic != "")
		{
#ifdef SERIAL_DEBUG
			Serial.println(MqttITopic);
#endif
			//TODO: Debug %2F
			//set_input_topic(MqttITopic);
		}

		// Send the response.
		g_WebServer.send(200, "text/html", CfgPageL);

#ifdef SERIAL_DEBUG
		Serial.println("Writed to EEPROM.");
		Serial.print("SSID: ");
		Serial.println(ApSsidL);
		Serial.print("Pass: ");
		Serial.println(ApPassL);
#endif  
	});

	// Begin the WEB server.
	g_WebServer.begin();
}

/** @brief Generate HTML option.
 *  @return String, generate HTML option.
 */
String generate_option(String option_text)
{
	return "<option value='" + option_text + "'>" + option_text + "</option>";
}

/** @brief Setup the WiFi module to access point mode.
 *  @return Void.
 */
void setup_wifi_ap()
{
	// Set the WiFi module to access point mode.
	WiFi.mode(WIFI_AP);
	WiFi.softAPConfig(g_APIP, g_APIP, g_SubnetMask);

#ifdef SERIAL_DEBUG
	// Print the IP address of the device.
	Serial.print("IP address: ");
	Serial.println(g_APIP);
#endif

	// Do a little work to get a unique name. Append the
	// last two bytes of the MAC (HEX'd) to "Thing-":
	uint8_t mac[WL_MAC_ADDR_LENGTH];
	// Read the MAC address.
	WiFi.softAPmacAddress(mac);

	// Get the last two marks.
	String macID = String(mac[WL_MAC_ADDR_LENGTH - 2], HEX) +
		String(mac[WL_MAC_ADDR_LENGTH - 1], HEX);

	// Capitalize the MAC symbols.
	macID.toUpperCase();

	// Add brand.
	String AP_NameString = String(g_CfgPageAddress);
	AP_NameString += " ";
	AP_NameString += macID;

	// Create SSID buffer.
	char AP_NameChar[AP_NameString.length() + 1];

	// Set the bytes.
	memset(AP_NameChar, 0, AP_NameString.length() + 1);

	// Copy the bytes.
	for (uint32_t indexChar = 0; indexChar < AP_NameString.length(); indexChar++)
	{
		AP_NameChar[indexChar] = AP_NameString.charAt(indexChar);
	}

	// Run the soft AP.
	WiFi.softAP(AP_NameChar, AP_PASSWORD);
}

/** @brief Setup the WiFi module to station mode.
 *  @return Void.
 */
void setup_wifi_sta()
{
	// Storage of the SSID and PASS.
	static String APSSID;
	static String APPass;

	// Read parameters.
	APSSID = get_ssid();
	APPass = get_password();

#ifdef SERIAL_DEBUG
	Serial.println("Readed from EEPROM.");
	Serial.print("SSID: ");
	Serial.println(APSSID);
	Serial.print("Pass: ");
	Serial.println(APPass);
#endif
	// Set the WiFi module to station mode.
	WiFi.mode(WIFI_STA);

	// Begin the station.
	WiFi.begin(APSSID.c_str(), APPass.c_str());

#ifdef SERIAL_DEBUG
	Serial.println("Connecting to AP.");
#endif

	int TryTimesL = 0;
	// Wait for connection.
	while (WiFi.status() != WL_CONNECTED)
	{
		delay(500);

#ifdef SERIAL_DEBUG
		Serial.print(".");
#endif

		// Increment with 1 try times.
		TryTimesL++;

		// Check if the time out has expired.
		if (TryTimesL == RECONNECT_TIMES)
		{
			delay(100);
			ESP.restart();
		}
	}

#ifdef SERIAL_DEBUG
	Serial.println();
#endif
}

#pragma endregion
