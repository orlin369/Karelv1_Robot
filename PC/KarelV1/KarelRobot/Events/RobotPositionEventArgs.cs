using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KarelRobot.Events
{
    public class RobotPositionEventArgs : EventArgs
    {
        public int Alpha { get; private set; }
        public int Radius { get; private set; }

        public RobotPositionEventArgs()
        {
            this.Alpha = 0;
            this.Radius = 0;
        }

        public RobotPositionEventArgs(int radius, int distance)
        {
            this.Alpha = radius;
            this.Radius = distance;
        }
    }
}
