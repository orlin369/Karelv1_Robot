using DatabaseConnection.Units;
using System;

namespace DatabaseConnection
{
    [Serializable]
    public class Orientation
    {
        public float A { get; set; }
        public float B { get; set; }
        public float C { get; set; }
        public Radial Unit { get; set; }

        public Orientation()
        {

        }
    }
}
