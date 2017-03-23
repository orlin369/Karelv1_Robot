using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace DatabaseConnection
{
    public class Lora
    {
        #region Variables

        private object syncLockComit = new object();

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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uri">URI of the server.</param>
        public Lora(Uri uri)
        {
            this.Uri = uri;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Commit data to the server.
        /// </summary>
        /// <param name="device"></param>
        public void CommitDevice(object device)
        {
            // Crete JSON.
            string serialisedData = JsonConvert.SerializeObject(device);

            //Console.WriteLine(serialisedData);

            // Robot data + args.
            serialisedData = String.Format("robotData={0}", serialisedData);

            //Console.WriteLine(serialisedData);
            this.MakeRequest(serialisedData);
        }

        public string Login(string username, string password)
        {
            lock (this.syncLockComit)
            {
                // Credentials
                string postData = String.Format("grant_type=client_credentials");

                //
                //string test = credentialsData ?? "NotSet";

                // Create a request using a URL that can receive a post.
                string uri = this.Uri + "/oauth"; //?grant_type=client_credentials";
                WebRequest request = WebRequest.Create(uri);
                //request.Credentials = new NetworkCredential(username, password);
                // Add encoded header.
                string encUserPass = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
                //request.Headers.Add("Authorization", "Basic " + encoded);
                // Set the Method property of the request to POST.
                request.Method = "POST";
                // Create POST data and convert it to a byte array.
                byte[] byteArray = Encoding.UTF8.GetBytes(postData + "\r\n" + encUserPass);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.testclient
                dataStream.Close();
                // Get the response.
                // TODO: Returns 404.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                
                // Display the content.
                Console.WriteLine(responseFromServer);
                
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
            }
        }

        #endregion

        #region Protected Methods

        protected void MakeRequest(string postData)
        {
            lock (this.syncLockComit)
            {
                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(this.Uri);
                // Set the Method property of the request to POST.
                request.Method = "POST";
                // Create POST data and convert it to a byte array.
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
            }
        }

        #endregion
    }
}
