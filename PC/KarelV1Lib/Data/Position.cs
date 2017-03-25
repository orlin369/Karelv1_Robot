using KarelV1Lib.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarelV1Lib.Data
{
    [Serializable]
    public class Position
    {
        public double Distance { get; set; }

        public double Alpha { get; set; }

        public double Delay { get; set; }

        public Position()
        {
            
        }

        public Position(double distance, double alpha, double delay)
        {
            this.Distance = distance;
            this.Alpha = alpha;
            this.Delay = delay;
        }

        public Position(PointF point, double delay)
        {
            double distance = 0.0D;
            double alpha = 0.0D;

            Utils.PolarConversion.CartesianToPolar(point, out distance, out alpha);

            this.Distance = distance;
            this.Alpha = alpha;
            this.Delay = delay;
        }

        /// <summary>
        /// Calculate motion time.
        /// </summary>
        /// <returns>Consumed motion time.</returns>
        public double WaitTime()
        {
            return (Math.Abs(this.Alpha) + Math.Abs(this.Distance) * this.Delay);
        }

        public PointF ToCartesian()
        {
            return PolarConversion.PolarToCartesian(this.Distance, this.Alpha);
        }
    }
}
