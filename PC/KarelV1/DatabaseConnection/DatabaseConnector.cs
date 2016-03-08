using System;
using System.Text;
using System.Net;
using System.IO;

namespace DatabaseConnection
{
    public class DatabaseConnector
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
        public DatabaseConnector(Uri uri)
        {
            this.Uri = uri;
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