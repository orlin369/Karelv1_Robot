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

namespace KarelV1Lib
{
    public class PolarConversion
    {

        #region Geometry

        /// <summary>
        /// Convert Cartesian to polar coordinates.
        /// </summary>
        /// <param name="point">Cartesian 2D point</param>
        /// <param name="magnitude">Output magnitude.</param>
        /// <param name="phase">Output phase.</param>
        public static void CartesianToPolar(PointF point, out double magnitude, out double phase)
        {
            // Convert to linear magnitude.
            magnitude = Math.Sqrt((point.X * point.X) + (point.Y * point.Y));
            // Convert to degree.
            phase = Math.Atan(point.X / point.Y) * (180 / Math.PI);
        }

        /// <summary>
        /// Convert polar to Cartesian coordinates.
        /// </summary>
        /// <param name="magnitude">Magnitude</param>
        /// <param name="phase">Phase</param>
        /// <returns>Cartesian 2D point.</returns>
        public static PointF PolarToCartesian(double magnitude, double phase)
        {
            double rad = phase * Math.PI / 180;

            return new PointF(
                (float)Math.Round(magnitude * Math.Cos(rad)),
                (float)Math.Round(magnitude * Math.Sin(rad))
                );
        }

        #endregion

    }
}