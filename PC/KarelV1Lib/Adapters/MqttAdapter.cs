using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KarelV1Lib.Events;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace KarelV1Lib.Adapters
{

    public class MqttAdapter : Adapter
    {

        #region Variables

        /// <summary>
        /// MQTT client.
        /// </summary>
        private MqttClient mqttClient;

        private string address;

        private int port;

        private string inputTopic;

        private string outputTopic;

        #endregion

        #region Properties

        public override bool IsConnected
        {
            get
            {
                if (this.mqttClient == null) return false;
                return this.mqttClient.IsConnected;
            }

            protected set
            {
                
            }
        }

        public override int MaxTimeout { get; set; }

        #endregion

        #region Events

        public override event EventHandler<StringEventArgs> OnMessage;

        #endregion

        #region Constructor

        public MqttAdapter(string address, int port, string inputTopic, string outputTopic)
        {
            this.address = address;
            this.port = port;
            this.inputTopic = inputTopic;
            this.outputTopic = outputTopic;

            this.mqttClient = new MqttClient(this.address);
        }

        #endregion

        #region MQTT Events

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string message = Encoding.UTF8.GetString(e.Message);
            this.OnMessage?.Invoke(this, new StringEventArgs(message));
        }

        private void MqttClient_ConnectionClosed(object sender, EventArgs e)
        {
            this.IsConnected = false;
        }

        #endregion

        #region Public Methods

        public override void Connect()
        {
            try
            {
                // Attach events.
                this.mqttClient.ConnectionClosed += MqttClient_ConnectionClosed;
                this.mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;

                // Connect to broker.
                this.mqttClient.Connect(Guid.NewGuid().ToString());

                // Check and subscribe.
                if (this.mqttClient.IsConnected)
                {
                    if (this.inputTopic != null)
                    {
                        this.mqttClient.Subscribe(new string[] { this.inputTopic }, new byte[] { 0 });
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(String.Format("Message: {0}\r\nSourece: {1}", exception.Message, exception.Source));
            }
        }

        public override void Disconnect()
        {
            if (this.mqttClient == null || !this.mqttClient.IsConnected) return;

            try
            {
                this.mqttClient.Unsubscribe(new string[] { this.inputTopic });
                this.mqttClient.Disconnect();
                this.mqttClient = null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(String.Format("Message: {0}\r\nSourece: {1}", exception.Message, exception.Source));
            }
        }

        public override void Dispose()
        {
            this.Disconnect();
        }

        public override void SendRequest(string command)
        {
            if (this.mqttClient == null && !this.mqttClient.IsConnected) return;

            byte[] byteArray = Encoding.UTF8.GetBytes(command);
            byte[] asciiArray = Encoding.Convert(Encoding.UTF8, Encoding.ASCII, byteArray);
            this.mqttClient.Publish(this.outputTopic, asciiArray);
        }

        public override void Reset()
        {
            
        }

        #endregion

    }

}
