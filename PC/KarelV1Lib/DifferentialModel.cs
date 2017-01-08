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

namespace KarelV1Lib
{
    /// <summary>
    /// The differential controlling model.
    /// </summary>
    public class DifferentialModel
    {

        #region Properties

        /// <summary>
        /// Motor steps count.
        /// </summary>
        public double StepsCount { get; private set; }

        /// <summary>
        /// Additional post scaler.
        /// </summary>
        public double PostScaler { get; private set; }

        /// <summary>
        /// The diameter of the wheel.
        /// </summary>
        public double WheelDiametter { get; private set; }

        /// <summary>
        /// Distance between the wheels. [mm]
        /// </summary>
        public double DistanceBetweenWheels { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stepsCount">Motor steps count.</param>
        /// <param name="postScaler">Additional post scaler.</param>
        /// <param name="wheelDiametter">The diameter of the wheel. [mm]</param>
        /// <param name="distanceBetweenWheels">Distance between the wheels. [mm]</param>
        public DifferentialModel(double stepsCount, double postScaler, double wheelDiametter, double distanceBetweenWheels)
        {
            this.StepsCount = stepsCount;
            this.PostScaler = postScaler;
            this.WheelDiametter = wheelDiametter;
            this.DistanceBetweenWheels = distanceBetweenWheels;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Convert MM to steps.
        /// </summary>
        /// <param name="distance">Distance [mm]</param>
        /// <returns>Steps</returns>
        /// <remarks>
        /// In this case.
        /// microStep = 16
        /// stepToDeg = (200 * microStep) / 360[deg]
        /// lengthOfWheel = (dOfWheel * PI) //53.5[mm]
        /// scalar = lengthOfWheel / stepToDeg
        /// inputDistance = 1000
        /// outputSteps = inputDistance * scalar
        /// </remarks>
        public int MmToStep(double distance)
        {
            double cWheel = Math.PI * this.WheelDiametter;
            double stepToDeg = (this.StepsCount * this.PostScaler) / 360.0d;
            double scalar = cWheel / stepToDeg;
            return (int)Math.Round(distance * scalar);
        }

        /// <summary>
        /// Converts steps to [mm].
        /// </summary>
        /// <param name="steps">Steps from motor.</param>
        /// <returns>Distance [mm]</returns>
        /// <remarks>
        /// In this case.
        /// microStep = 16
        /// stepToDeg = (200 * microStep) / 360[deg]
        /// lengthOfWheel = (dOfWheel * PI) //53.5[mm]
        /// scalar = lengthOfWheel / stepToDeg
        /// inputSteps = 1000
        /// outputSteps = inputSteps / scalar
        /// </remarks>
        public double StepToMm(int steps)
        {
            double cWheel = Math.PI * this.WheelDiametter;
            double stepToDeg = (this.StepsCount * this.PostScaler) / 360.0d;
            double scalar = cWheel / stepToDeg;
            return (steps / scalar);
        }

        /// <summary>
        /// Converts degree to mm
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public int DegToStep(double degrees)
        {
            // Length of base wheel.
            double lengthOfBaseWheel = (this.DistanceBetweenWheels * Math.PI);

            // Calculate mm per degree coefficient.
            double mmPerDegree = lengthOfBaseWheel / 360;

            // Multiply mm per degree to set point.
            double rotationalDistance = mmPerDegree * degrees;

            // Calculate steps.
            return this.MmToStep(rotationalDistance); ;
        }
        

        #endregion 

    }
}
