using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using KarelRobot.Events;
using KarelRobot.Utils;

namespace KarelRobot
{
    public class KarelV1 : IDisposable
    {

        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private int timeOut;

        /// <summary>
        /// Comunication port.
        /// </summary>
        private SerialPort port;

        /// <summary>
        /// Comunication lock object.
        /// </summary>
        private Object requestLock = new Object();

        /// <summary>
        /// When is connected to the robot.
        /// </summary>
        private bool isConnected = false;

        /// <summary>
        /// Name of the port.
        /// </summary>
        private string portName = String.Empty;

        /// <summary>
        /// Delimiting characters.
        /// </summary>
        private char[] delimiterChars = {',', ':', '\t', ';' };

        #endregion

        #region Properties

        /// <summary>
        /// Maximum timeout.
        /// </summary>
        public int MaxTimeout { get; set; }

        /// <summary>
        /// If the board is correctly connected.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return this.isConnected;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Rise when error occurred beteween PLC and PC.
        /// </summary>
        public event EventHandler<StringEventArgs> OnMessage;

        public event EventHandler<SensorsEventArgs> OnSensors;

        public event EventHandler<UltraSonicSensorEventArgs> OnUltraSonicSensor;

        public event EventHandler<EventArgs> OnStoped;

        public event EventHandler<StringEventArgs> OnGreatingsMessage;

        public event EventHandler<RobotPositionEventArgs> OnPosition;

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">Comunication port.</param>
        public KarelV1(string portName)
        {
            // max tiem 5 minutes
            this.MaxTimeout = 30000;

            // Save the port name.
            this.portName = portName;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~KarelV1()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            this.Disconnect();
        }

        #endregion

        #region Public

        /// <summary>
        /// Connetc to the serial port.
        /// </summary>
        public void Connect()
        {
            while (!this.isConnected)
            {
                try
                {
                    if (!this.isConnected)
                    {
                        this.port = new SerialPort(this.portName);
                        this.port.BaudRate = 9600;
                        this.port.DataBits = 8;
                        this.port.StopBits = StopBits.One;
                        this.port.Parity = Parity.None;
                        this.port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                        this.port.Open();

                        this.timeOut = 0;
                        this.isConnected = true;
                    }
                }
                catch (Exception exception)
                {
                    this.timeOut += 1000;
                    Thread.Sleep(timeOut);
                    this.isConnected = false;
                }
                finally
                {
                    if (timeOut > this.MaxTimeout)
                    {
                        throw new InvalidOperationException("Could not connect to the robot.");
                    }
                }
            }
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        public void Disconnect()
        {
            if (this.isConnected)
            {
                this.port.Close();
            }
            this.isConnected = false;
        }

        /// <summary>
        /// Reset the MCU
        /// </summary>
        public void Reset()
        {
            lock (this.requestLock)
            {
                if (this.isConnected && this.port.IsOpen)
                {
                    this.port.DtrEnable = true;
                    Thread.Sleep(200);
                    this.port.DtrEnable = false;
                }
            }
        }

        /// <summary>
        /// Move the robots.
        /// </summary>
        /// <param name="value">Value of the movment tenth of the [mm].</param>
        public void Move(int value)
        {
            string command = String.Format("?M{0}{1:D4}", (value > 0) ? "+" : "", value);
            this.SendRequest(command);
        }

        /// <summary>
        /// Rotate the robots.
        /// </summary>
        /// <param name="value">Value of the rotation tenth of the degree.</param>
        public void Rotate(int value)
        {
            string command = String.Format("?R{0}{1:D4}", (value > 0) ? "+" : "", value);
            this.SendRequest(command);
        }

        /// <summary>
        /// Shuts down the bridge of the motors.
        /// </summary>
        public void Stop()
        {
           string command = "?STOP";
           this.SendRequest(command);
        }

        /// <summary>
        /// Get sensors.
        /// </summary>
        public void GetSensors()
        {
            string command = "?SENSORS";
            this.SendRequest(command);
        }

        /// <summary>
        /// Get ultra sonic sensor.
        /// </summary>
        /// <param name="position">Position of the sensor.</param>
        public void GetUltraSonic(int position)
        {
            //?US180\n
            if (position > 180)
            {
                throw new ArgumentException(String.Format("Position must be less then 181, actual {0}", position));
            }

            if (position < 0)
            {
                throw new ArgumentException(String.Format("Position must be great then -1, actual {0}", position));
            }
                
            string command = String.Format("?US{0:D3}", position);
            this.SendRequest(command);
        }

        /// <summary>
        /// Get ultra sonic sensor.
        /// </summary>
        public void GetUltraSonic()
        {
            string command = "?USA";
            this.SendRequest(command);
        }

        /// <summary>
        /// Get position of the robot.
        /// </summary>
        public void GetPosition()
        {
            string command = "?POSITION";
            this.SendRequest(command);
        }

        #endregion

        #region Private

        private void SpeedRegulation()
        {
            const float maxSpeed = 100.0f;

            float turn = 0.0f;
            float speed = 0.0f;

            float left = speed;
            float right = speed;

            if (turn < 0)
            {
                left *= Math.Max(maxSpeed + turn, 1);
            }

            if (turn > 0)
            {
                left *= Math.Max(maxSpeed - turn, 1);
            }
        }

        /// <summary>
        /// Send request to device.
        /// </summary>
        /// <param name="request">Request string.</param>
        private void SendRequest(string request)
        {
            lock (this.requestLock)
            {
                try
                {
                    if (this.isConnected && port.IsOpen)
                    {
                        this.port.WriteLine(request);
                    }
                }
                catch (Exception exception)
                {
                    this.isConnected = false;
                    this.timeOut = 0;
                    // Reconnect.
                    this.Connect();
                }
            }
        }

        /// <summary>
        /// Data recievce event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            // Wait ...
            Thread.Sleep(50);

            if (sender != null)
            {
                // Make serial port to get data from.
                SerialPort sp = (SerialPort)sender;

                //string indata = sp.ReadLine();
                string inData = sp.ReadExisting();

                if (this.OnMessage != null)
                {
                    this.OnMessage(this, new StringEventArgs(inData));
                }

                this.ResponseParser(inData);

                // Discart the duffer.
                sp.DiscardInBuffer();
            }
        }

        /// <summary>
        /// Parse response message from the device.
        /// </summary>
        /// <param name="response">Response string.</param>
        private void ResponseParser(string response)
        {
            if (response.Contains("#") && response.Contains("\r\n"))
            {
                string tmpResponse = response.Replace("#", "").Replace("\r\n", "");

                string[] tokens = tmpResponse.Split(this.delimiterChars);

                if (tokens.Length > 0)
                {

                    #region SENSORS

                    if (tokens[0] == "SENSORS")
                    {
                        if (tokens[1] == "L" && tokens[3] == "R")
                        {
                            float left = 0.0f; 
                            float right = 0.0f;

                            if ((float.TryParse(tokens[2], out left)) && (float.TryParse(tokens[4], out right)))
                            {
                                if (this.OnSensors != null)
                                {
                                    this.OnSensors(this, new SensorsEventArgs(left, right));
                                }
                            }
                        }
                    }

                    #endregion

                    #region STOP

                    if (tokens[0] == "STOP")
                    {
                        if (this.OnStoped != null)
                        {
                            this.OnStoped(this, new EventArgs());
                        }
                    }

                    #endregion

                    #region ULTRA SONIC

                    if (tokens[0] == "US")
                    {
                        float phase = 0.0f;
                        float time = 0.0f;

                        if (tokens[2].Contains("."))
                        {
                            tokens[2] = tokens[2].Replace('.', ',');
                        }

                        if((float.TryParse(tokens[1], out phase)) && (float.TryParse(tokens[2], out time)))
                        {
                            time /= 58.0F;
                        }

                        if (this.OnUltraSonicSensor != null)
                        {
                            this.OnUltraSonicSensor(this, new UltraSonicSensorEventArgs(phase, time));
                        }
                    }

                    #endregion

                    #region POSITION

                    if (tokens[0] == "POSITION")
                    {
                        int position = 0;
                        int distance = 0;

                        if (tokens[1] == "D" && tokens[3] == "A")
                        {
                            tokens[2] = RobotUtils.CorrectDecDelimiter(tokens[2]);
                            if (!int.TryParse(tokens[2], out distance))
                            {
                                return;
                            }

                            tokens[4] = RobotUtils.CorrectDecDelimiter(tokens[4]);
                            if (!int.TryParse(tokens[4], out position))
                            {
                                return;
                            }
                        }

                        if (this.OnPosition != null)
                        {
                            this.OnPosition(this, new RobotPositionEventArgs(position, distance));
                        }
                    }

                    #endregion

                    #region GREATINGS

                    if (tokens[0].Contains("GREATINGS"))
                    {
                        if (this.OnGreatingsMessage != null)
                        {
                            this.OnGreatingsMessage(this, new StringEventArgs(tokens[1]));
                        }
                    }

                    #endregion

                }
            }
        }

        #endregion

    }
}
