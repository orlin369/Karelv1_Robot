using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Utils
{
    public static class AppUtils
    {

        #region Constants

        /// <summary>
        /// Decimal separator depending of the culture.
        /// </summary>
        public static char DecimalSeparator = Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

        #endregion

        #region Networking

        /// <summary>
        /// Get IP Address of the machine.
        /// </summary>
        /// <returns>Local IP address of the machine.</returns>
        public static IPAddress LocalIPAddress()
        {
            IPHostEntry host;
            IPAddress localIP = null;
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip;// .ToString();
                    break;
                }
            }

            return localIP;
        }

        /// <summary>
        /// Get addresses of the machine.
        /// </summary>
        /// <returns></returns>
        public static IPAddress[] GetAddresses()
        {
            List<IPAddress> addresses = new List<IPAddress>();
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    addresses.Add(ip);
                }
            }

            return addresses.ToArray();
        }

        /// <summary>
        /// Map drive on the PC.
        /// </summary>
        /// <param name="driveLetter">Drive letter.</param>
        /// <param name="address">Remote address.</param>
        /// <param name="path">Remote path.</param>
        /// <param name="credential">Credentials.</param>
        public static void MapDrive(char driveLetter, IPAddress address, string path, NetworkCredential credential)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "net.exe";
            startInfo.Arguments = String.Format(@"use {0}: \\{1}\{2} /user:{3} {4} ", driveLetter, address, path, credential.UserName, credential.Password);
            process.StartInfo = startInfo;
            process.Start();
        }


        #endregion

        #region Image

        /// <summary>
        /// Resize bitmap images.
        /// </summary>
        /// <param name="imgToResize">Source image.</param>
        /// <param name="size">Output size.</param>
        /// <returns>Resized new bitmap.</returns>
        public static Bitmap ResizeImage(Bitmap sourceImage, Size size)
        {

            Rectangle destRect = new Rectangle(0, 0, size.Width, size.Height);
            Bitmap destImage = new Bitmap(size.Width, size.Height);

            destImage.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (ImageAttributes wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(sourceImage, destRect, 0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        /// <summary>
        /// Fit image in size.
        /// </summary>
        /// <param name="image">Target image.</param>
        /// <param name="size">Output size.</param>
        /// <returns>Fited image.</returns>
        public static Bitmap FitImage(Bitmap image, Size size)
        {
            var ratioX = (double)size.Width / image.Width;
            var ratioY = (double)size.Height / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        /// <summary>
        /// Crop image by ROI (Region of Interest).
        /// </summary>
        /// <param name="image">Source image.</param>
        /// <param name="cropRectangle">Crop rectangle descriptor.</param>
        /// <param name="pading">Round pading.</param>
        /// <returns>Croped image.</returns>
        public static Bitmap CropImage(Bitmap image, Rectangle cropRectangle, int pading)
        {
            //this.selectedWorkpiece.BoundingBox
            Rectangle rectangle = new Rectangle(cropRectangle.X - pading,
                                                cropRectangle.Y - pading,
                                                cropRectangle.Width + 2 * pading,
                                                cropRectangle.Height + 2 * pading);

            Bitmap target = new Bitmap(rectangle.Width, rectangle.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(image, new Rectangle(0, 0, target.Width, target.Height), rectangle, GraphicsUnit.Pixel);
            }

            return target;
        }

        /// <summary>
        /// Rotate image once clock ways with 90deg.
        /// </summary>
        /// <param name="image">Target image.</param>
        /// <returns>Rotated image.</returns>
        public static Bitmap Rotate90CW(Bitmap image)
        {
            image.RotateFlip(RotateFlipType.Rotate270FlipNone);

            return image;
        }

        /// <summary>
        /// Convert HSV to RGB color space.
        /// </summary>
        /// <param name="h">Hue</param>
        /// <param name="s">Saturation</param>
        /// <param name="v">Value</param>
        /// <returns></returns>
        public static Color HsvToRgb(double h, double s, double v)
        {
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;

            if (v <= 0)
            {
                R = G = B = 0;
            }
            else if (s <= 0)
            {
                R = G = B = v;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = v * (1 - s);
                double qv = v * (1 - s * f);
                double tv = v * (1 - s * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = v;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = v;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = v;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = v;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = v;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = v;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = v;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = v;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = v; // Just pretend its black/white
                        break;
                }
            }

            int r = AppUtils.Clamp((int)(R * 255.0));
            int g = AppUtils.Clamp((int)(G * 255.0));
            int b = AppUtils.Clamp((int)(B * 255.0));

            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        private static int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }

        #endregion

        #region Numbers

        /// <summary>
        /// Replace no metter , or . with correct regional decimal delimiter.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CorrectDecDelimiter(string value)
        {
            value = value.Replace(',', AppUtils.DecimalSeparator);
            value = value.Replace('.', AppUtils.DecimalSeparator);

            return value;
        }

        /// <summary>
        /// Rescale value.
        /// </summary>
        /// <param name="value">Value to be scaled.</param>
        /// <param name="inMin">Minimum of the value.</param>
        /// <param name="inMax">Maximum of the value.</param>
        /// <param name="outMin">Minimum of the output scale.</param>
        /// <param name="outMax">Maximum of the output scale.</param>
        /// <returns>Rescaled Value.</returns>
        public static double Map(float value, float inMin, float inMax, float outMin, float outMax)
        {
            return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        #endregion

        #region Formating

        public static string GetValue(string value)
        {
            if (value == String.Empty)
            {
                return "";
            }

            value = value.Trim();
            string[] values = value.Split(':');

            if (values.Length != 2)
            {
                return "";
            }

            return values[1];
        }

        /// <summary>
        /// Convert ASCII string to UTF8.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>Output string.</returns>
        public static string GetAsciiAsUnicode(string str)
        {
            //Create encodings
            System.Text.Encoding ascii = System.Text.Encoding.ASCII;
            System.Text.Encoding unicode = System.Text.Encoding.Unicode;

            //Get ascii bytes
            byte[] asciiBytes = ascii.GetBytes(str);

            //Convert ascii to unicode
            byte[] unicodeBytes = System.Text.Encoding.Convert(ascii, unicode, asciiBytes);

            //Return unicode string
            return unicode.GetString(unicodeBytes);
        }

        /// <summary>
        /// Get default file as UTF8
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns></returns>
        public static Stream GetUTF8Stream(string filePath)
        {
            //Create encodings
            System.Text.Encoding def = Encoding.Default;
            System.Text.Encoding utf = System.Text.Encoding.UTF8;

            //Read file bytes
            byte[] defBytes = File.ReadAllBytes(filePath);

            //Convert default to utf
            byte[] utfBytes = System.Text.Encoding.Convert(def, utf, defBytes);

            //Create stream from utf bytes
            Stream stream = new MemoryStream(utfBytes);

            //Return stream
            return stream;
        }

        #endregion

        #region Utils

        /// <summary>
        /// Kill the current process, if another instance of this application is allready running.
        /// </summary>
        public static void KillYourSelf()
        {
            // Get all processes running on the local computer.
            Process[] localAll = Process.GetProcesses();

            // Get the current process.
            Process currentProcess = Process.GetCurrentProcess();

            foreach (Process procces in localAll)
            {
                if ((procces.ProcessName == currentProcess.ProcessName) && (procces.Id != currentProcess.Id))
                {
                    currentProcess.Kill();
                }
            }
        }

        public static List<string> PosibleDriveNames()
        {

            List<string> posibleNames = new List<string>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            foreach (DriveInfo d in allDrives)
            {
                string name = d.Name.Replace(":\\", "").ToUpper();
                letters = letters.Replace(d.Name.Replace(":\\", "").ToUpper(), "");
            }

            foreach (char letter in letters)
            {
                posibleNames.Add((letter + ":\\").ToUpper());
            }

            return posibleNames;
        }

        #endregion

    }
}
