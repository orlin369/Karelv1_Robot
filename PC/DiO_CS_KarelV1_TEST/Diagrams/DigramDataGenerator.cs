using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagrams
{
    public class DigramDataGenerator
    {
        #region Variables

        /// <summary>
        /// Generate random numbers
        /// </summary>
        private Random myRandom = new Random();

        #endregion

        #region Public

        /// <summary>
        /// Generate white noice.
        /// </summary>
        /// <returns>Data buffer.</returns>
        public double[] GenerateWhiteNoise()
        {
            double[] testData = new double[180];

            // Generate random numbers
            Random myRandom = new Random();

            // Prepare data.
            for (int i = 0; i < testData.Length; i++)
            {
                double randomNum = myRandom.Next(0, 100);
                randomNum /= 100;
                testData[i] = randomNum;
            }

            return testData;
        }

        /// <summary>
        /// Generate colored noice.
        /// </summary>
        /// <param name="maxValue">Minimum value border.</param>
        /// <param name="minValue">Maximum value border.</param>
        /// <returns>Data buffer.</returns>
        public double[] GenerateColordNoice(int minValue = 0, int maxValue = 100)
        {
            double[] testData = new double[180];

            // Generate random numbers
            Random myRandom = new Random();

            // Prepare data.
            for (int i = 0; i < testData.Length; i++)
            {
                double randomNum = myRandom.Next(minValue, maxValue);
                randomNum /= maxValue;
                testData[i] = randomNum;
            }

            return testData;
        }

        /// <summary>
        /// Create animation data.
        /// </summary>
        public double[] CreateDataSet()
        {
            double[] sensorData = new double[180];
            double randomNum = 0.0d;
            int maxValue = 100;

            for (int indexData = 0; indexData < 180; indexData++)
            {
                if (indexData > 0 && indexData < 20)
                {
                    randomNum = myRandom.Next(20, 30);
                    randomNum /= maxValue;
                }
                if (indexData > 19 && indexData < 100)
                {
                    randomNum = myRandom.Next(50, 70);
                    randomNum /= maxValue;
                }
                if (indexData > 99 && indexData < 179)
                {
                    randomNum = myRandom.Next(70, 90);
                    randomNum /= maxValue;
                }

                sensorData[indexData] = randomNum;
            }

            return sensorData;
        }

        #endregion
    }
}
