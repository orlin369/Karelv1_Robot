using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KarelRobot.Events
{
    public class UltraSonicSensorEventArgs : EventArgs
    {
        public float Position { get; private set; }
        public float Distance { get; private set; }

        public UltraSonicSensorEventArgs()
        {
            this.Position = 0;
            this.Distance = 0.0f;
        }

        public UltraSonicSensorEventArgs(float position, float distance)
        {
            this.Position = position;
            this.Distance = distance;
        }
    }
}