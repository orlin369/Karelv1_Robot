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

namespace KarelV1Lib.Events
{
    /// <summary>
    /// Ultrasonic sensor event argument.
    /// </summary>
    [Serializable]
    public class DistanceSensorsEventArgs : EventArgs
    {

        #region Properties

        /// <summary>
        /// Position [DEG]
        /// </summary>
        public int Position { get; private set; }

        /// <summary>
        /// Ultrasonic distance sensor time. [us]
        /// </summary>
        public int UltrasonicTime { get; private set; }

        /// <summary>
        /// Infrared distance sensor value of ADC. [ADC 0-1023]
        /// </summary>
        public int InfraRedADCValue { get; private set; }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DistanceSensorsEventArgs()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position">Position [DEG]</param>
        /// <param name="ultrasonicTime">Time [us]</param>
        public DistanceSensorsEventArgs(int position, int ultrasonicTime, int infraRedADCValue)
        {
            this.Position = position;
            this.UltrasonicTime = ultrasonicTime;
            this.InfraRedADCValue = infraRedADCValue;
        }

        #endregion

    }
}