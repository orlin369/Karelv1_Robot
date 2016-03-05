using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseConnection.Units
{
    public enum Scales : int
    {
        /// <summary>
        /// Milimeter
        /// </summary>
        MM = 0,

        /// <summary>
        /// Inch
        /// </summary>
        INCH = 1,

        /// <summary>
        /// Percentage
        /// </summary>
        Percentage = 2,

        /// <summary>
        /// Steps
        /// </summary>
        Steps = 3,
    }
}
