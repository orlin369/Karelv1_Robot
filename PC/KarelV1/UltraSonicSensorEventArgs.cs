using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karel
{
    public class UltraSonicSensorEventArgs : EventArgs
    {
        public int Position { get; private set; }
        public double Distance { get; private set; }

        public UltraSonicSensorEventArgs()
        {
            this.Position = 0;
            this.Distance = 0.0f;
        }

        public UltraSonicSensorEventArgs(int position, double distance)
        {
            this.Position = position;
            this.Distance = distance;
        }
    }
}