using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagrams
{
    // TODO: Use it.
    public class HSR04
    {
        /// <summary>
        /// Maximum sensor value.
        /// Scale [cm]
        /// </summary>
        public const double MAX_DISTANCE = 300.0d;

        /// <summary>
        /// Minimum sensor value.
        /// Scale [cm]
        /// </summary>
        public const double MIN_DISTANCE = 20.0d;

        /// <summary>
        /// Rescale sensor value from 
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static double Normalyse(double distance)
        {
            distance /= (MAX_DISTANCE - MIN_DISTANCE);
            return distance;
        }

    }
}
