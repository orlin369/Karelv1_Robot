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

        public double Distance { get; set; }

        public double Phase { get; set; }

        public double Delay { get; set; }

        public DistanceSensorsList DistanceSensors { get; set; }

        #endregion

        #region Constructor

        public Position()
        {
            
        }

        public Position(double distance, double phase, double delay)
        {
            this.Distance = distance;
            this.Phase = phase;
            this.Delay = delay;
        }

        public Position(PointF point, double delay)
        {
            double distance = 0.0D;
            double phase = 0.0D;

            Utils.PolarConversion.CartesianToPolar(point, out distance, out phase);

            this.Distance = distance;
            this.Phase = phase;
            this.Delay = delay;
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Calculate motion time.
        /// </summary>
        /// <returns>Consumed motion time.</returns>
        public double WaitTime()
        {
            return (Math.Abs(this.Phase) + Math.Abs(this.Distance) * this.Delay);
        }

        public PointF ToCartesian()
        {
            return PolarConversion.PolarToCartesian(this.Distance, this.Phase);
        }

        #endregion

    }
}
