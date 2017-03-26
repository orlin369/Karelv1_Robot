/*

Copyright (c) [2016] [Orlin Dimitrov]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using System;
using System.Collections.Generic;
using System.Drawing;

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
