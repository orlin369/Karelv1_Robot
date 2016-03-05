using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseConnection
{
    [Serializable]
    public class Location
    {

        #region Properties

        public float Altitude { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Location()
        {

        }

        #endregion

    }
}
