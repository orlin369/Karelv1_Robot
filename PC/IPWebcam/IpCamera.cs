using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Drawing;
using System.IO;

namespace IPWebcam
{
    public class IpCamera : ICaptureDevice
    {

        #region Variables

        /// <summary>
        /// URI of the camera.
        /// </summary>
        private Uri uri;

        #endregion

        #region Properties

        /// <summary>
        /// URI of the service.
        /// </summary>
        public Uri URI
        {
            get
            {
                return this.uri;
            }
        }

        /// <summary>
        /// Enable torch.
        /// </summary>
        public bool EnableTorch
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uri">Address of the image source.</param>
        public IpCamera(Uri uri)
        {
            // URL Image source.
            this.uri = uri;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get image from IP Camera.
        /// </summary>
        /// <returns>The bitmap image.</returns>
        public Bitmap Capture()
        {
            if (this.EnableTorch)
            {
                this.SetTorch(true);
            }

            WebRequest request = WebRequest.Create(this.uri.AbsoluteUri);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();

            if (EnableTorch)
            {
                this.SetTorch(false);
            }

            return new Bitmap(stream);

        }

        public Stream SetTorch(bool state)
        {
            string uriString = String.Format("http://{0}:{1}/{2}", this.uri.Host, this.uri.Port, (state) ? "enabletorch" : "disabletorch");

            WebRequest request = WebRequest.Create(new Uri(uriString));
            WebResponse response = request.GetResponse();
            return response.GetResponseStream();
        }

        #endregion

    }
}
