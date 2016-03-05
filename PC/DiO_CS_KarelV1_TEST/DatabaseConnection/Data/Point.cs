using DatabaseConnection.Units;
using System;

namespace DatabaseConnection
{
    [Serializable]
    public class Point
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public Scales Unit { get; set; }

        public Point()
        {

        }
    }
}
