using Newtonsoft.Json;
using System;

namespace DatabaseConnection
{
    public class Lora : DatabaseConnector
    {

        #region Cntructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uri">URI</param>
        public Lora(Uri uri)
            : base(uri)
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Commit data to te server.
        /// </summary>
        /// <param name="device"></param>
        public void CommitDevice(object device)
        {
            // Creste JSON.
            string serialisedData = JsonConvert.SerializeObject(device);

            Console.WriteLine(serialisedData);

            // Robot data + args.
            serialisedData = String.Format("robotData={0}", serialisedData);

            //Console.WriteLine(serialisedData);
            this.MakeRequest(serialisedData);
        }

        #endregion

    }
}
