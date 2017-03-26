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
using System.Drawing;

using KarelV1Lib.Utils;

namespace KarelV1Lib.Data
{
    [Serializable]
    public class Position
    {

        #region Properties

        /// <summary>
        /// Distance
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// Phase orientation of the robot.
        /// </summary>
        public double Phase { get; set; }

        /// <summary>
        /// Steps per second.
        /// </summary>
        public double StepsPerSecond { get; set; }

        /// <summary>
        /// Distance sensor.
        /// </summary>
        public DistanceSensorsList DistanceSensors { get; set; }

        /// <summary>
        /// Sensors
        /// </summary>
        public Sensors Sensors { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public Position()
        {
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="distance">Distance</param>
        /// <param name="phase">Orientation of the robot.</param>
        /// <param name="stepsPerSecon">Steps per second.</param>
        public Position(double distance, double phase, double stepsPerSecon)
        {
            this.Distance = distance;
            this.Phase = phase;
            this.StepsPerSecond = stepsPerSecon;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="point">Point</param>
        /// <param name="stepsPerSecon">Steps per second.</param>
        public Position(PointF point, double stepsPerSecon)
        {
            double distance = 0.0D;
            double phase = 0.0D;

            Utils.PolarConversion.CartesianToPolar(point, out distance, out phase);

            this.Distance = distance;
            this.Phase = phase;
            this.StepsPerSecond = stepsPerSecon;
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Transform polar position to Cartesian position.
        /// </summary>
        /// <returns>Cartesian point.</returns>
        public PointF ToCartesian()
        {
            return PolarConversion.PolarToCartesian(this.Distance, this.Phase);
        }

        #endregion

    }
}
