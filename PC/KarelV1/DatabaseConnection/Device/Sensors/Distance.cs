using DatabaseConnection.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseConnection.Device.Sensors
{
    [Serializable]
    public class Distance : Sensor
    {
        public Scales Unit { get; set; }

        public Distance()
        {
            this.Type = "Distance";
        }
    }
}
