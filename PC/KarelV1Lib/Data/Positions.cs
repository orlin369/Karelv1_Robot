using KarelV1Lib.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarelV1Lib.Data
{
    [Serializable]
    public class Positions : IList<Position>
    {

        #region Variables

        /// <summary>
        /// robot points container.
        /// </summary>
        private List<Position> storage = new List<Position>();

        #endregion

        #region Properties

        #endregion

        #region Implementation of IList

        public int IndexOf(Position item)
        {
            return this.storage.IndexOf(item);
        }

        public void Insert(int index, Position item)
        {
            this.storage.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.storage.RemoveAt(index);
        }

        public Position this[int index]
        {
            get
            {
                return this.storage[index];
            }
            set
            {
                this.storage[index] = value;
            }
        }
        
        public void Add(Position item)
        {
            this.storage.Add(item);
        }

        public void Clear()
        {
            this.storage.Clear();
        }

        public bool Contains(Position item)
        {
            return this.storage.Contains(item);
        }

        public void CopyTo(Position[] array, int arrayIndex)
        {
            this.storage.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return this.storage.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove(Position item)
        {
            return this.storage.Remove(item);
        }

        public IEnumerator<Position> GetEnumerator()
        {
            return this.storage.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.storage.GetEnumerator();
        }

        #endregion
        
        #region Public Methods

        public void Add(Point point, double delay)
        {
            double distance = 0.0D;
            double phase = 0.0D;

            Utils.PolarConversion.CartesianToPolar(point, out distance, out phase);

            this.storage.Add(new Position(distance, phase, delay));
        }

        public void Add(PointF point, double delay)
        {
            double distance = 0.0D;
            double phase = 0.0D;

            Utils.PolarConversion.CartesianToPolar(point, out distance, out phase);

            this.storage.Add(new Position(distance, phase, delay));
        }


        public PointF GetPoint(int index)
        {
            return this.storage[index].ToCartesian();
        }

        public PointF[] GetPointsF()
        {
            List<PointF> points = new List<PointF>();

            foreach(Position rp in this.storage)
            {
                points.Add(rp.ToCartesian());
            }

            return points.ToArray();
        }

        public Point[] GetPoints()
        {
            List<Point> points = new List<Point>();

            foreach (Position rp in this.storage)
            {
                PointF pf = rp.ToCartesian();
                points.Add(new Point((int)pf.X, (int)pf.Y));
            }

            return points.ToArray();
        }


        #endregion

    }
}
