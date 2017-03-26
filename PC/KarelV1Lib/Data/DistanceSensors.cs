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

namespace KarelV1Lib.Data
{
    [Serializable]
    public class DistanceSensors
    {

        #region Properties

        /// <summary>
        /// Angle of the gamble.
        /// </summary>
        public double Position { get; set; }

        /// <summary>
        /// Ultrasonic distance sensor value.
        /// </summary>
        public double UltraSonic { get; set; }

        /// <summary>
        /// Infrared distance sensor value.
        /// </summary>
        public double Infrared { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Empty for serialization.
        /// </remarks>
        public DistanceSensors()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position">Angle of the gamble.</param>
        /// <param name="ultraSonic">Ultrasonic distance sensor value.</param>
        /// <param name="infraRed">Infrared distance sensor value.</param>
        public DistanceSensors(double position, double ultraSonic, double infraRed)
        {
            this.Position = position;
            this.UltraSonic = ultraSonic;
            this.Infrared = infraRed;
        }

        #endregion

    }
}
