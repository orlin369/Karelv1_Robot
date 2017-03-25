using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseConnection
{
    public class Ioly
    {
        //TODO: Evaluate to .NET 4.5.1
        //TODO: Uncomment the azure connection.

        #region Constants

        public const string ciotHubUri = "hackafe.azure-devices.net"; // ! put in value !
        public const string cdeviceId = "IolyDevice1"; // ! put in value !
        public const string cdeviceKey = "lEHj8e+jj3LoVGDb0p/voldkazX6C4qbVGy2Dgo/u1Q="; // ! put in value !

        #endregion

        #region Variables

        //private DeviceClient deviceClient;
        private string deviceId;
        private string deviceKey;

        #endregion

        #region Properties

        /// <summary>
        /// URI of the server.
        /// </summary>
        public Uri Uri
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public Ioly(Uri uri, string deviceId, string deviceKey)
        {
            this.Uri = uri;
            this.deviceId = deviceId;
            this.deviceKey = deviceKey;
        }

        #endregion

        #region Public Methods

        //public void Init()
        //{
        //    deviceClient = DeviceClient.Create(iotHubUri,
        //            AuthenticationMethodFactory.
        //                CreateAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey),
        //            TransportType.Amqp);
        //}
        
        //public async void CommitDevice(object device)
        //{
        //    // Creste JSON.
        //    string serialisedData = JsonConvert.SerializeObject(device);
        //    var message = new Message(Encoding.ASCII.GetBytes(serialisedData));
        //    await deviceClient.SendEventAsync(message);
        //}

        #endregion

    }
}
