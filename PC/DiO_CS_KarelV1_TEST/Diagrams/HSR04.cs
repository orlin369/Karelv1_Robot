using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagrams
{
    public class HSR04
    {
        /// <summary>
        /// Maximum sensor value.
        /// Scale [mm]
        /// </summary>
        private const double MAX_DISTANCE = 3000.0d;

        /// <summary>
        /// Minimum sensor value.
        /// Scale [mm]
        /// </summary>
        private const double MIN_DISTANCE = 200.0d;

        /// <summary>
        /// Rescale sensor value from 
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static double Normalyse(double distance)
        {
            distance /= MAX_DISTANCE;
            return distance;
        }

    }
}
