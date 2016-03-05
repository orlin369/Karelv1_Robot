using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KarelRobot.Events
{
    public class RobotPositionEventArgs : EventArgs
    {
        public double Alpha { get; private set; }
        public double Distance { get; private set; }

        public RobotPositionEventArgs()
        {
            this.Alpha = 0.0d;
            this.Distance = 0.0d;
        }

        public RobotPositionEventArgs(double alpha, double distance)
        {
            this.Alpha = alpha;
            this.Distance = distance;
        }
    }
}
