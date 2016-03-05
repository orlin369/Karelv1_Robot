using System;

namespace DatabaseConnection.Device
{
    [Serializable]
    public class Device : EventArgs
    {

        #region Properties

        public Position Position { get; set; }

        public float Value { get; set; }
        public float MaxValue { get; set; }
        public float MinValue { get; set; }
        
        public string Descripotion { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public string Type { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Device()
        {
            this.Position = new Position();
            this.DateTime = new DateTime();
            this.Descripotion = "NA";
            this.Name = "NA";
        }

        #endregion

    }
}
