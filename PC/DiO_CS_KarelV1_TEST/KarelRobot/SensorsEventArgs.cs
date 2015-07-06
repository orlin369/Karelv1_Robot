using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KarelRobot
{
    public class SensorsEventArgs : EventArgs
    {
        public float Left { get; private set; }
        public float Right { get; private set; }

        public SensorsEventArgs()
        {
            this.Left = 0.0f;
            this.Right = 0.0f;
        }

        public SensorsEventArgs(float left, float right)
        {
            this.Left = left;
            this.Right = right;
        }
    }
}