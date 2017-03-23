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

using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace KarelV1Lib.Data
{
    [Serializable]
    public class DistanceSensorsList : IList<DistanceSensors>
    {

        #region Variables

        /// <summary>
        /// Storage
        /// </summary>
        private List<DistanceSensors> storage = new List<DistanceSensors>();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DistanceSensorsList()
        {

        }

        #endregion

        #region Implementation of IList

        public int IndexOf(DistanceSensors item)
        {
            return this.storage.IndexOf(item);
        }

        public void Insert(int index, DistanceSensors item)
        {
            this.storage.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.storage.RemoveAt(index);
        }

        public DistanceSensors this[int index]
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

        public void Add(DistanceSensors item)
        {
            this.storage.Add(item);
        }

        public void Clear()
        {
            this.storage.Clear();
        }

        public bool Contains(DistanceSensors item)
        {
            return this.storage.Contains(item);
        }

        public void CopyTo(DistanceSensors[] array, int arrayIndex)
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

        public bool Remove(DistanceSensors item)
        {
            return this.storage.Remove(item);
        }

        public IEnumerator<DistanceSensors> GetEnumerator()
        {
            return this.storage.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.storage.GetEnumerator();
        }

        #endregion

        public double[] GetUltrasonic()
        {
            double[] data = new double[this.Count];

            for(int index = 0; index < this.Count; index++)
            {
                data[index] = this[index].UltraSonic;
            }

            return data;
        }

        public double[] GetInfrared()
        {
            double[] data = new double[this.Count];

            for (int index = 0; index < this.Count; index++)
            {
                data[index] = this[index].Infrared;
            }

            return data;
        }

        public double[] GetPositions()
        {
            double[] data = new double[this.Count];

            for (int index = 0; index < this.Count; index++)
            {
                data[index] = this[index].Position;
            }

            return data;
        }

        #region Public Static Methods

        /// <summary>
        /// Save sonars data to XML.
        /// </summary>
        /// <param name="sonarsData">Sonars data.</param>
        /// <param name="fileName">File path.</param>
        public static void SaveXML(DistanceSensorsList sonarsData, string fileName)
        {
            XmlSerializer writer = new XmlSerializer(typeof(DistanceSensorsList));
            using (StreamWriter file = new StreamWriter(fileName))
            {
                writer.Serialize(file, sonarsData);
            }
        }

        /// <summary>
        /// Save sonars data to CSV.
        /// </summary>
        /// <param name="sonarsData">Sonars data.</param>
        /// <param name="fileName">File path.</param>
        public static void SaveCSV(DistanceSensorsList sonarsData, string fileName)
        {
            using (StreamWriter writer = File.CreateText(fileName))
            {
                CsvWriter csv = new CsvWriter(writer);

                for (int degree = 0; degree < sonarsData.Count; degree++)
                {
                    csv.WriteField(degree);
                    csv.WriteField(sonarsData[degree].UltraSonic.ToString().Replace(",", "."));
                    csv.WriteField(sonarsData[degree].Infrared.ToString().Replace(",", "."));
                    csv.NextRecord();
                }
            }
        }

        #endregion

    }
}
