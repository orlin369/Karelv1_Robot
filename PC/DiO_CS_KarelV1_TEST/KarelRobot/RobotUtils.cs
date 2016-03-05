using System;

namespace KarelRobot.Utils
{
    public class RobotUtils
    {
        /// <summary>
        /// Decimal separator depending of the culture.
        /// </summary>
        private static char DecimalSeparator = Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

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
    }
}
