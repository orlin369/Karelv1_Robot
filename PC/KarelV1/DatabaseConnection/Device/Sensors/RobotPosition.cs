using DatabaseConnection.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseConnection.Device.Sensors
{
    [Serializable]
    class RobotPosition : Sensor
    {
        public Scales Unit { get; set; }

        public RobotPosition()
        {
            this.Type = "RobotPosition";
        }
    }
}
