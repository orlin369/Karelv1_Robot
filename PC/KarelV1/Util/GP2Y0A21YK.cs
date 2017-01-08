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

namespace KarelV1.Util
{
    class GP2Y0A21YK
    {

        #region Constants

        private const double SENSOR_SCALE = 27.728;

        private const double EXPONENTIAL_RATE = -1.2045;

        private const double SCALER = 1000.0d;

        #endregion

        #region Variables

        private double voltage = 0.0d;

        private double adcMaximum = 0.0d;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GP2Y0A21YK(double voltage, double adcMaximum)
        {
            this.voltage = voltage;
            this.adcMaximum = adcMaximum;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Value scaler.
        /// </summary>
        /// <param name="value">Value for scale.</param>
        /// <param name="maxIn">Input maximum.</param>
        /// <param name="minIn">Input minimum.</param>
        /// <param name="maxOut">Output maximum.</param>
        /// <param name="minOut">Output minimum.</param>
        /// <returns>Scaled result.</returns>
        private double Map(double value, double maxIn, double minIn, double maxOut, double minOut)
        {
            return (value - minIn) * (maxOut - minOut) / (maxIn - minIn) + minOut;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Convert ADC value to centimeters.
        /// </summary>
        /// <param name="value">ADC value.</param>
        /// <returns>Distance in [CM]</returns>
        public double Convert(double value)
        {
            return SENSOR_SCALE * Math.Pow(this.Map(value, 0, this.adcMaximum, 0, (this.voltage * SCALER)) / SCALER, EXPONENTIAL_RATE);
        }

        #endregion

    }
}
