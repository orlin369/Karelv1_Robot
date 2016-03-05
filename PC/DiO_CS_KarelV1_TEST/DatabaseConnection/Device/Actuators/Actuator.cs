using DatabaseConnection.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseConnection.Device.Actuators
{
    [Serializable]
    public class Actuator : Device
    {
        public float Velocity { get; set; }
        public float Acceleration { get; set; }
        public float Jerk { get; set; }
        public Scales Unit { get; set; }
    }
}
