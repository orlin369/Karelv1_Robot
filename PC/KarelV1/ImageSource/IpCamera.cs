using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Drawing;
using System.IO;

namespace ImageSource
{
    public class IpCamera : ICaptureDevice
    {
        /// <summary>
        /// Unifi
        /// </summary>
        private Uri uri;

        public Uri URI
        {
            get
            {
                return this.uri;
            }
        }

        public bool Torch
        {
            get;
            set;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uri">Address of the image source.</param>
        public IpCamera(Uri uri)
        {
            // URL Image source.
            this.uri = uri;
        }

        /// <summary>
        /// Get image from IP Camera.
        /// </summary>
        /// <returns>The bitmap image.</returns>
        public Bitmap Capture()
        {
            if (this.Torch)
            {
                this.SetTorch(true);
            }

            WebRequest request = WebRequest.Create(this.uri.AbsoluteUri);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            return new Bitmap(stream);

            if (Torch)
            {
                this.SetTorch(false);
            }
        }

        public Stream SetTorch(bool state)
        {
            string uriEnableTorch = String.Format("http://{0}:{1}/enabletorch", this.uri.Host, this.uri.Port);
            string uriDisableTorch = String.Format("http://{0}:{1}/disabletorch", this.uri.Host, this.uri.Port);

            string uriString = (state) ? uriEnableTorch : uriDisableTorch;

            WebRequest request = WebRequest.Create(new Uri(uriString));
            WebResponse response = request.GetResponse();
            return response.GetResponseStream();
        }
    }
}
