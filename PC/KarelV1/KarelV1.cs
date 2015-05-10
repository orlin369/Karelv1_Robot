using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;


namespace Karel
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
        public event EventHandler<StringEventArgs> Message;

        public event EventHandler<SensorsEventArgs> Sensors;

        public event EventHandler<UltraSonicSensorEventArgs> UltraSonicSensor;

        public event EventHandler<EventArgs> Stoped;

        public event EventHandler<StringEventArgs> GreatingsMessage;

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
                if (this.isConnected)
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
        /// <param name="value">Value of the movment tenth of the mm.</param>
        public void Move(int value)
        {
            string direction = "";

            lock (this.requestLock)
            {
                if (value > 0)
                {
                    direction = "+";
                }

                if (value <= 0)
                {
                    //direction = "-";
                }

                if (port.IsOpen)
                {
                    string command = String.Format("?M{0}{1:D3}", direction, value);
                    this.SendRequest(command);
                }
            }
        }

        /// <summary>
        /// Rotate the robots.
        /// </summary>
        /// <param name="value">Value of the rotation tenth of the degree.</param>
        public void Rotate(int value)
        {
            string direction = "";

            lock (this.requestLock)
            {
                if (value > 0)
                {
                    direction = "+";
                }

                if (value <= 0)
                {
                    //direction = "-";
                }

                if (port.IsOpen)
                {
                    string command = String.Format("?R{0}{1:D3}", direction, value);
                    this.SendRequest(command);
                }
            }
        }

        /// <summary>
        /// Shuts down the bridge of the motors.
        /// </summary>
        public void Stop()
        {
            lock (this.requestLock)
            {
                if (port.IsOpen)
                {
                    string command = "?STOP";
                    this.SendRequest(command);
                }
            }
        }

        /// <summary>
        /// Get sensors.
        /// </summary>
        public void GetSensors()
        {
            lock (this.requestLock)
            {
                if (port.IsOpen)
                {
                    string command = "?SENSORS";
                    this.SendRequest(command);
                }
            }
        }

        public void GetUltraSonic(int position)
        {
            //?US180\n
            lock (this.requestLock)
            {
                if (position > 180)
                {
                    throw new ArgumentException(String.Format("Position must be less then 181, actual {0}", position));
                }

                if (position < 0)
                {
                    throw new ArgumentException(String.Format("Position must be great then -1, actual {0}", position));
                }

                if (port.IsOpen)
                {
                    string command = String.Format("?US{0:D3}", position);
                    this.SendRequest(command);
                }
            }
        }

        public void GetUltraSonic()
        {
            lock (this.requestLock)
            {
                if (port.IsOpen)
                {
                    string command = "?USA";
                    this.SendRequest(command);
                }
            }
        }

        #endregion

        #region Private

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

                //TODO: Parse the incommning string from the serial port.
                // POS will be the index of the array.
                // CM will be the data in the cell.
                //string[] tokens = inData.Split(this.delimiterChars);

                //Console.WriteLine("Data: {0};\r\nTokens: {1}", indata, tokens.Length);

                //Console.WriteLine("Test: {0}", indata);

                if (this.Message != null)
                {
                    this.Message(this, new StringEventArgs(inData));
                }

                this.ResponseParser(inData);

                // Discart the duffer.
                sp.DiscardInBuffer();
            }
        }

        private void SendRequest(string request)
        {
            try
            {
                if (this.isConnected)
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

        private void ResponseParser(string response)
        {
            if (response.Contains("#") && response.Contains("\r\n"))
            {
                string tmpResponse = response.Replace("#", "").Replace("\r\n", "");

                string[] tokens = tmpResponse.Split(this.delimiterChars);

                if (tokens.Length > 0)
                {
                    if (tokens[0] == "SENSORS")
                    {
                        if (tokens[1] == "L" && tokens[3] == "R")
                        {
                            float left = 0.0f; 
                            float right = 0.0f;


                            if ((float.TryParse(tokens[2], out left)) && (float.TryParse(tokens[4], out right)))
                            {
                                ;
                            }

                            if (this.Sensors != null)
                            {
                                this.Sensors(this, new SensorsEventArgs(left, right));
                            }
                        }
                    }
                    if (tokens[0] == "STOP")
                    {
                        if (this.Stoped != null)
                        {
                            this.Stoped(this, new EventArgs());
                        }
                    }

                    if (tokens[0] == "US")
                    {
                        int position = 0;
                        double distance = 0.0d;

                        if (tokens[2].Contains("."))
                        {
                            tokens[2] = tokens[2].Replace('.', ',');
                        }

                        if((int.TryParse(tokens[1], out position)) && (double.TryParse(tokens[2], out distance)))
                        {
                            distance /= 1000;
                        }

                        if (this.UltraSonicSensor != null)
                        {
                            this.UltraSonicSensor(this, new UltraSonicSensorEventArgs(position, distance));
                        }
                    }
                    //

                    if (tokens[0].Contains("GREATINGS"))
                    {
                        if (this.GreatingsMessage != null)
                        {
                            this.GreatingsMessage(this, new StringEventArgs(tokens[1]));
                        }
                    }
                }
            }
        }

        #endregion



    }
}
