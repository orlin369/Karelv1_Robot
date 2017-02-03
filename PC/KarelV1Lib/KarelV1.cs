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

using System;
using KarelV1Lib.Events;
using KarelV1Lib.Adapters;

namespace KarelV1Lib
{
    public class KarelV1
    {

        #region Constants

        private const char TERMIN = '\n';

        private const string RESPONSE_SIGN = "#";

        private const string RESPONSE_DELIMITER = "\r\n";

        #endregion

        #region Variables

        private Adapter adapter;

        #endregion

        #region Properties

        /// <summary>
        /// If the board is correctly connected.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return this.adapter.IsConnected;
            }
        }

        /// <summary>
        /// Maximum timeout.
        /// </summary>
        public int MaxTimeout
        {
            get
            {
                return this.adapter.MaxTimeout;
            }
            set
            {
                this.adapter.MaxTimeout = value;
            }
        }

        #endregion

        #region Events

        public event EventHandler<SensorsEventArgs> OnSensors;

        public event EventHandler<DistanceSensorsEventArgs> OnDistanceSensors;

        public event EventHandler<EventArgs> OnStoped;

        public event EventHandler<StringEventArgs> OnGreatingsMessage;

        public event EventHandler<RobotPositionEventArgs> OnPosition;

        /// <summary>
        /// Received command message.
        /// </summary>
        public event EventHandler<StringEventArgs> OnMessage;

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">Communication port.</param>
        public KarelV1(Adapter adapter)
        {
            this.adapter = adapter;

            // max time 5 minutes
            this.adapter.MaxTimeout = 30000;

            // Attach the event handler.
            this.adapter.OnMessage += Adapter_OnMessage;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~KarelV1()
        {
            this.adapter.Dispose();
        }

        #endregion

        #region Public

        /// <summary>
        /// Connect to the Robot.
        /// </summary>
        public void Connect()
        {
            this.adapter.Connect();
            this.adapter.OnMessage += Adapter_OnMessage;
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        public void Disconnect()
        {
            this.adapter.Disconnect();
        }

        /// <summary>
        /// Reset the Robot.
        /// </summary>
        public void Reset()
        {
            this.adapter.Reset();
        }

        /// <summary>
        /// Move the robots.
        /// </summary>
        /// <param name="value">Value of the movement tenth of the [mm].</param>
        public void Move(int value)
        {
            string command = String.Format("?M{0}{1:D4}", (value > 0) ? "+" : "", value);
            this.adapter.SendRequest(command + TERMIN);
        }

        /// <summary>
        /// Rotate the robots.
        /// </summary>
        /// <param name="value">Value of the rotation tenth of the degree.</param>
        public void Rotate(int value)
        {
            string command = String.Format("?R{0}{1:D4}", (value > 0) ? "+" : "", value);
            this.adapter.SendRequest(command + TERMIN);
        }

        /// <summary>
        /// Shuts down the bridge of the motors.
        /// </summary>
        public void Stop()
        {
           string command = "?STOP";
           this.adapter.SendRequest(command + TERMIN);
        }

        /// <summary>
        /// Get sensors.
        /// </summary>
        public void GetSensors()
        {
            string command = "?SENSORS";
            this.adapter.SendRequest(command + TERMIN);
        }

        /// <summary>
        /// Get ultra sonic sensor.
        /// </summary>
        /// <param name="position">Position of the sensor.</param>
        public void GetDistanceSensors(int position)
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
            this.adapter.SendRequest(command + TERMIN);
        }

        /// <summary>
        /// Get ultra sonic sensor.
        /// </summary>
        public void GetDistanceSensors()
        {
            string command = "?USA";
            this.adapter.SendRequest(command + TERMIN);
        }

        /// <summary>
        /// Get position of the robot.
        /// </summary>
        public void GetPosition()
        {
            string command = "?POSITION";
            this.adapter.SendRequest(command + TERMIN);
        }

        #endregion

        #region Private

        private void Adapter_OnMessage(object sender, StringEventArgs e)
        {
            this.OnMessage?.Invoke(this, e);

            this.ResponseParser(e.Message);
        }

        /// <summary>
        /// Parse response message from the device.
        /// </summary>
        /// <param name="response">Response string.</param>
        private void ResponseParser(string response)
        {
            // First check if message is delimitable.
            if (response.Contains(RESPONSE_DELIMITER))
            {
                // Divide message in to tokens.
                string[] tokens = response.Trim('\0', '\n', '\r').Split(new string[] { RESPONSE_DELIMITER }, StringSplitOptions.RemoveEmptyEntries);

                // If message is divided correct then iterate trough it.
                if (tokens.Length > 0)
                {
                    foreach (string token in tokens)
                    {
                        // Exit token parsing if token does not start with '#'.
                        if(!token.StartsWith(RESPONSE_SIGN))
                        {
                            continue;
                        }

                        #region SENSORS

                        if (token.Contains("SENSORS"))
                        {
                            string[] subTokens = token.Split(new char[] { ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (subTokens[1] == "L" && subTokens[3] == "R")
                            {
                                float left = 0.0f;
                                float right = 0.0f;

                                if ((float.TryParse(subTokens[2], out left)) && (float.TryParse(subTokens[4], out right)))
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

                        if (token.Contains("STOP"))
                        {
                            if (this.OnStoped != null)
                            {
                                this.OnStoped(this, new EventArgs());
                            }
                        }

                        #endregion

                        #region ULTRA SONIC

                        if (token.Contains("US"))
                        {
                            int phase = 0;
                            int usTime = 0;
                            int irAdc = 0;

                            string tmpToken = token.Replace("#US;", "");

                            string[] subTokens = tmpToken.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                            if ((int.TryParse(subTokens[0], out phase)) && (int.TryParse(subTokens[1], out usTime)) && (int.TryParse(subTokens[2], out irAdc)))
                            {
                                this.OnDistanceSensors?.Invoke(this, new DistanceSensorsEventArgs(phase, usTime, irAdc));
                            }

                        }

                        #endregion

                        #region POSITION

                        if (token.Contains("POSITION"))
                        {
                            int position = 0;
                            int distance = 0;

                            string tmpToken = token.Replace("#POSITION;", "");

                            string[] subTokens = tmpToken.Split(new char[] { ':', ';' }, StringSplitOptions.RemoveEmptyEntries);


                            if (subTokens[0] == "T" && subTokens[2] == "R")
                            {
                                if ((int.TryParse(subTokens[1], out distance))
                                    && (int.TryParse(subTokens[3], out position)))
                                {
                                    this.OnPosition?.Invoke(this, new RobotPositionEventArgs(position, distance));
                                }
                            }

                        }

                        #endregion

                        #region GREATINGS

                        if (token.Contains("GREATINGS"))
                        {
                            string tmpToken = token.Replace("#GREATINGS;", "");
                            this.OnGreatingsMessage?.Invoke(this, new StringEventArgs(tmpToken));
                        }

                        #endregion

                    }
                }
            }
        }

        #endregion

    }
}
