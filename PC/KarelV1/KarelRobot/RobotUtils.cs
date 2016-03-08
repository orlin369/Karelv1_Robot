using System;
using System.Drawing;

namespace KarelRobot.Utils
{
    public class RobotUtils
    {

        #region Variables

        /// <summary>
        /// Decimal separator depending of the culture.
        /// </summary>
        private static char DecimalSeparator = Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

        #endregion

        #region Formating

        /// <summary>
        /// Replace no metter , or . with correct regional decimal delimiter.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static string CorrectDecDelimiter(string value)
        {
            value = value.Replace(',', RobotUtils.DecimalSeparator);
            value = value.Replace('.', RobotUtils.DecimalSeparator);

            return value;
        }

        #endregion

        #region Geometry

        /// <summary>
        /// Convert cartesian to polar coordinates.
        /// </summary>
        /// <param name="point">Cartesian 2D point</param>
        /// <param name="magnitude">Output magnitude.</param>
        /// <param name="phase">Output phase.</param>
        public static void CartesianToPolar(PointF point, out double magnitude, out double phase)
        {
            magnitude = Math.Sqrt((point.X * point.X) + (point.Y * point.Y));
            phase = Math.Atan(point.X / point.Y);
            // Convert to degree.
            phase *= (180 / Math.PI);
        }

        /// <summary>
        /// Convert polar to cartesian coordinates.
        /// </summary>
        /// <param name="magnitude">Magnitude</param>
        /// <param name="phase">Phase</param>
        /// <returns>Cartesian 2D point</returns>
        public static PointF PolarToCartesian(double magnitude, double phase)
        {
            double rad = phase * Math.PI / 180;
            
            double sin = Math.Sin(rad);
            double cos = Math.Cos(rad);

            float x = (float)Math.Round(magnitude * cos);
            float y = (float)Math.Round(magnitude * sin);

            return new PointF(x, y);
        }

        /// <summary>
        /// Convert MM to steps.
        /// </summary>
        /// <param name="mm">Distance in milimeters.</param>
        /// <param name="stepperCount">Stepper steps count.</param>
        /// <param name="postScaler">Post scaler of driver.</param>
        /// <param name="dWheel">Diameter of wheel.</param>
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
        public static int MmToStep(
            double mm,
            double stepperCount,
            double postScaler,
            double diameterOfWheel)
        {
            double cWheel = Math.PI * diameterOfWheel;
            double stepToDeg = (stepperCount * postScaler) / 360.0d;
            double scalar = cWheel / stepToDeg;
            return (int)Math.Round(mm * scalar);
        }

        /// <summary>
        /// Converts steps to MM.
        /// </summary>
        /// <param name="steps">Steps from motor.</param>
        /// <param name="stepperCount">Stepper steps count.</param>
        /// <param name="postScaler">Post scaler of driver.</param>
        /// <param name="diameterOfWheel">Diameter of wheel.</param>
        /// <remarks>
        /// In this case.
        /// microStep = 16
        /// stepToDeg = (200 * microStep) / 360[deg]
        /// lengthOfWheel = (dOfWheel * PI) //53.5[mm]
        /// scalar = lengthOfWheel / stepToDeg
        /// inputSteps = 1000
        /// outputSteps = inputSteps / scalar
        /// </remarks>
        /// <returns>MM</returns>
        public static double StepToMm(
            int steps,
            double stepperCount,
            double postScaler,
            double diameterOfWheel)
        {
            double cWheel = Math.PI * diameterOfWheel;
            double stepToDeg = (stepperCount * postScaler) / 360.0d;
            double scalar = cWheel / stepToDeg;
            return (steps / scalar);
        }

        public static int DegToStep(double deg, double stepsCount, double postScaler, double dWheel)
        {
            /*
             * microStep = 16
             * stepToDeg = (200 * microStep) / 360[deg]
             * lengthOfWheel = (dOfWheel * PI) //53.5[mm]
             * lengthOfBWheel = (148 * PI)
             * scalar = (lengthOfBWheel/lengthOfWheel) / stepToDeg
             * inputDistance = 1000
             * outputSteps = inputDistance * scalar
             * 
             */

            double cWheel = Math.PI * dWheel;
            double cBaseWheel = Math.PI * 148; // Diameter of base wheel.

            double stepToDeg = (stepsCount * postScaler) / 360.0d;

            double scalar = (dWheel * 148) / stepToDeg;

            return (int)Math.Round(deg * scalar);
        }

        #endregion

    }
}