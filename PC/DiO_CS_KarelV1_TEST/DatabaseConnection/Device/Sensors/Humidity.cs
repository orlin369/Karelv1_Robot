using DatabaseConnection.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseConnection.Device.Sensors
{
    [Serializable]
    public class Humidity : Sensor
    {
        public Scales Unit { get; set; }

        public Humidity()
        {
            this.Type = "Humidity";
        }
    }
}
