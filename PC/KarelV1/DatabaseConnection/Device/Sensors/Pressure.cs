using DatabaseConnection.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseConnection.Device.Sensors
{
    public class Pressure : Sensor
    {
        public Pressures Unit { get; set; }

        public Pressure()
        {
            this.Type = "Presure";
        }
    }
}
