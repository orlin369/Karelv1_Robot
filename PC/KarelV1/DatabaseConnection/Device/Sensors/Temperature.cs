using DatabaseConnection.Units;
using System;

namespace DatabaseConnection.Device.Sensors
{
    [Serializable]
    public class Temperature : Sensor
    {
        public TemperatureScale Unit { get; set; }

        public Temperature()
        {
            this.Type = "Temperature";
        }
    }
}
