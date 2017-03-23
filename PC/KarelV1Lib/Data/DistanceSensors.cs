using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
