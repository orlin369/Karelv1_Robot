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
using System.Threading;
using KarelV1Lib.Events;
using KarelV1.Utils;

namespace KarelV1Lib
{
    public class KarelV1 : Communicator, IDisposable
    {

        #region Variables



        /// <summary>
        /// Delimiting characters.
        /// </summary>
        private char[] delimiterChars = {',', ':', '\t', ';' };

        #endregion

        #region Events

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
        /// <param name="port">Communication port.</param>
        public KarelV1(string portName) : base(portName)
        {
            // max time 5 minutes
            this.MaxTimeout = 30000;

            // Attach the event handler.
            this.OnMessage += KarelV1_OnMesage;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~KarelV1()
        {
            base.Dispose();
        }

        #endregion

        #region Public

        /// <summary>
        /// Reset the MCU
        /// </summary>
        public void Reset()
        {
            if (this.IsConnected && this.SerialPort.IsOpen)
            {
                this.SerialPort.DtrEnable = true;
                Thread.Sleep(200);
                this.SerialPort.DtrEnable = false;
            }
        }

        /// <summary>
        /// Move the robots.
        /// </summary>
        /// <param name="value">Value of the movement tenth of the [mm].</param>
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
        
        /// <summary>
        /// Handler that cal response parser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KarelV1_OnMesage(object sender, StringEventArgs e)
        {
            this.ResponseParser(e.Message);
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
