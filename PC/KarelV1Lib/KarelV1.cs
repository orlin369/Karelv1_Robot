﻿/*

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
using System.Timers;
using System.Threading;

using KarelV1Lib.Events;
using KarelV1Lib.Adapters;
using KarelV1Lib.Utils;
using KarelV1Lib.Data;

namespace KarelV1Lib
{
    public class KarelV1
    {

        #region Constants

        /// <summary>
        /// Terminating symbol.
        /// </summary>
        private const char TERMIN = '\n';

        /// <summary>
        /// Response sign.
        /// </summary>
        private const string RESPONSE_SIGN = "#";

        /// <summary>
        /// Response delimiter.
        /// </summary>
        private const string RESPONSE_DELIMITER = "\r\n";

        /// <summary>
        /// Hart rate of the communication protocol.
        /// </summary>
        private const int HART_RATE = 1000;

        #endregion

        #region Variables

        /// <summary>
        /// Communication adapter.
        /// </summary>
        private Adapter adapter;

        /// <summary>
        /// Hart beat timer.
        /// </summary>
        private System.Timers.Timer hgartBeatTimer = new System.Timers.Timer();

        /// <summary>
        /// Communication latency.
        /// </summary>
        private int communicationLatency = 0;

        /// <summary>
        /// Previous position.
        /// </summary>
        private Position previousPosition = new Position();

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

        /// <summary>
        /// Indicates if the robot is alive.
        /// </summary>
        public bool IsAlive { get; private set; }

        /// <summary>
        /// Communication latency.
        /// It is used for adjustment of the hart beat protocol.
        /// </summary>
        public int CommunicationLatency
        {
            set
            {
                this.communicationLatency = value;
                this.hgartBeatTimer.Interval = HART_RATE + communicationLatency;
            }
            get
            {
                return this.communicationLatency;
            }
        }

        /// <summary>
        /// Indicates when the robot is moving.
        /// </summary>
        public bool IsMoving { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// Received command message.
        /// </summary>
        public event EventHandler<StringEventArgs> OnMessage;

        /// <summary>
        /// Raise gratings message is arrived.
        /// </summary>
        public event EventHandler<StringEventArgs> OnGreatingsMessage;

        /// <summary>
        /// Raise on position data arrives.
        /// </summary>
        public event EventHandler<PositionEventArgs> OnPosition;

        /// <summary>
        /// Raise on robot stops.
        /// </summary>
        public event EventHandler<EventArgs> OnStoped;

        /// <summary>
        /// Raise on robot runs.
        /// </summary>
        public event EventHandler<EventArgs> OnRuning;

        /// <summary>
        /// On sensor data arrived.
        /// </summary>
        public event EventHandler<SensorsEventArgs> OnSensors;

        /// <summary>
        /// Raise on distance sensor data arrives.
        /// </summary>
        public event EventHandler<DistanceSensorsEventArgs> OnDistanceSensors;
        
        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="adapter">Communication adapter.</param>
        /// <param name="differentialModel">Differential model.</param>
        public KarelV1(Adapter adapter)
        {
            // Set the adapter.
            this.adapter = adapter;
            this.adapter.MaxTimeout = 30000;
            this.adapter.OnMessage += Adapter_OnMessage;

            // Set the hart beat timer.
            this.hgartBeatTimer.Interval = HART_RATE;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~KarelV1()
        {
            // Detach the event handler.
            this.adapter.OnMessage -= Adapter_OnMessage;
            this.adapter.Dispose();
            this.adapter = null;

            // Stop the hart beat timer.
            this.hgartBeatTimer.Stop();
            this.hgartBeatTimer.Dispose();
            this.hgartBeatTimer = null;
        }

        #endregion

        #region Public

        /// <summary>
        /// Connect to the Robot.
        /// </summary>
        public void Connect()
        {
            this.Disconnect();
            this.adapter.OnMessage += Adapter_OnMessage;
            this.adapter.Connect();

            this.hgartBeatTimer.Elapsed += HartBeatTimer_Elapsed;
            this.hgartBeatTimer.Start();
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        public void Disconnect()
        {
            if (this.adapter == null || !this.adapter.IsConnected) return;

            this.adapter.Disconnect();

            this.hgartBeatTimer.Elapsed -= HartBeatTimer_Elapsed;
            this.hgartBeatTimer.Stop();
        }

        /// <summary>
        /// Reset the Robot.
        /// </summary>
        public void Reset()
        {
            if (this.adapter == null || !this.adapter.IsConnected) return;

            this.adapter.Reset();
        }
        
        /// <summary>
        /// Move the robots.
        /// </summary>
        /// <param name="value">Value of the movement tenth of the [mm].</param>
        public void TranslateSteps(int value)
        {
            if (this.adapter == null || !this.adapter.IsConnected) return;

            string command = String.Format("?T{0}{1:D4}", (value >= 0) ? "+" : "", value);
            this.adapter.SendRequest(command + TERMIN);
        }

        /// <summary>
        /// Rotate the robots.
        /// </summary>
        /// <param name="value">Value of the rotation tenth of the degree.</param>
        public void RotateStaps(int value)
        {
            if (this.adapter == null || !this.adapter.IsConnected) return;

            string command = String.Format("?R{0}{1:D4}", (value >= 0) ? "+" : "", value);
            this.adapter.SendRequest(command + TERMIN);
        }

        /// <summary>
        /// Shuts down the bridge of the motors.
        /// </summary>
        public void Stop()
        {
            if (this.adapter == null || !this.adapter.IsConnected) return;

            string command = "?STOP";
            this.adapter.SendRequest(command + TERMIN);
        }

        /// <summary>
        /// Get ultra sonic sensor.
        /// </summary>
        /// <param name="position">Position of the sensor.</param>
        public void GetDistanceSensors(int position)
        {
            if (this.adapter == null || !this.adapter.IsConnected) return;

            //?US180\n
            if (position > 180)
            {
                throw new ArgumentException(String.Format("Position must be less then 181, actual {0}", position));
            }

            if (position < 0)
            {
                throw new ArgumentException(String.Format("Position must be great then -1, actual {0}", position));
            }
                
            string command = String.Format("?DS{0:D3}", position);
            this.adapter.SendRequest(command + TERMIN);
        }

        /// <summary>
        /// Get ultra sonic sensor.
        /// </summary>
        public void GetDistanceSensors()
        {
            if (this.adapter == null || !this.adapter.IsConnected) return;

            string command = "?DSA";
            this.adapter.SendRequest(command + TERMIN);
        }

        #endregion

        #region Hart beat Timer

        /// <summary>
        /// Hart beat callback method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HartBeatTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.IsAlive = false;
        }

        #endregion

        #region Adapter

        /// <summary>
        /// Adapter data arrive callback method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Adapter_OnMessage(object sender, StringEventArgs e)
        {
            this.OnMessage?.Invoke(this, e);
            this.ResponseParser(e.Message);
        }

        #endregion

        #region Private
        
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

                        #region GREATINGS

                        if (token.Contains("GREATINGS"))
                        {
                            string tmpToken = token.Replace("#GREATINGS;", "");
                            this.OnGreatingsMessage?.Invoke(this, new StringEventArgs(tmpToken));
                        }

                        #endregion

                        #region POSITION

                        if (token.Contains("POSITION"))
                        {
                            int phase = 0;
                            int distance = 0;
                            int frontSensor = 0;
                            int backSensor = 0;
                            Position position = new Position();
                            position.StepsPerSecond = 0;
                            position.Sensors = new Sensors();

                            string tmpToken = token.Replace("#POSITION;", "");

                            string[] subTokens = tmpToken.Split(new char[] { ':', ';' }, StringSplitOptions.RemoveEmptyEntries);
                            
                            if (subTokens[0] == "T" && subTokens[2] == "R")
                            {
                                if ((int.TryParse(subTokens[1], out distance))
                                    && (int.TryParse(subTokens[3], out phase)))
                                {
                                    position.Distance = distance;
                                    position.Phase = phase;
                                }
                            }
                            if(subTokens[4] == "F" && subTokens[6] == "B")
                            {
                                if ((int.TryParse(subTokens[5], out frontSensor))
                                        && (int.TryParse(subTokens[7], out backSensor)))
                                {
                                    position.Sensors.Front = frontSensor;
                                    position.Sensors.Back = backSensor;
                                }
                            }
                            
                            if(position.IsDifference(previousPosition))
                            { 
                                this.IsMoving = true;
                                this.OnRuning?.Invoke(this, null);
                            }
                            else
                            {
                                this.IsMoving = false;
                                this.OnStoped?.Invoke(this, null);
                            }
                            
                            // Update previous position.
                            previousPosition = position;

                            // Keep alive.
                            this.IsAlive = true;

                            // Call event for position update.
                            this.OnPosition?.Invoke(this, new PositionEventArgs(position));

                            // Call event for sensor data.
                            this.OnSensors?.Invoke(this, new SensorsEventArgs(position.Sensors));
                        }

                        #endregion

                        #region DISTANCE SENSOR

                        if (token.Contains("DS"))
                        {
                            int phase = 0;
                            int usTime = 0;
                            int irAdc = 0;

                            string tmpToken = token.Replace("#DS;", "");

                            string[] subTokens = tmpToken.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                            if ((int.TryParse(subTokens[0], out phase)) && (int.TryParse(subTokens[1], out usTime)) && (int.TryParse(subTokens[2], out irAdc)))
                            {
                                this.OnDistanceSensors?.Invoke(this, new DistanceSensorsEventArgs(new DistanceSensors(phase, usTime, irAdc)));
                            }

                        }

                        #endregion

                    }
                }
            }
        }

        #endregion

    }
}
