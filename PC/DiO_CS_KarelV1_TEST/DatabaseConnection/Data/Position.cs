using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseConnection
{
    [Serializable]
    public class Position : EventArgs
    {

        #region Properties

        /// <summary>
        /// Point
        /// </summary>
        public Point Point { get; set; }

        /// <summary>
        /// Orientation
        /// </summary>
        public Orientation Orientation { get; set; }

        /// <summary>
        /// Geo location.
        /// </summary>
        public Location Location { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Position()
        {
            this.Point = new Point();
            this.Orientation = new Orientation();
            this.Location = new Location();
        }

        #endregion

    }
}
