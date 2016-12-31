/*

Copyright (c) [2016] [Orlin Dimitrov]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Diagrams
{
    /// <summary>
    /// Circular diagram draw.
    /// </summary>
    public partial class CircularDiagram : UserControl
    {

        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private Point diagramCenterPoint;

        /// <summary>
        /// 
        /// </summary>
        private Point[] diagramCurvePoints;

        /// <summary>
        /// 
        /// </summary>
        private Color backgroungColor;

        /// <summary>
        /// Grid color.
        /// </summary>
        private Pen gridPen;

        /// <summary>
        /// Create start angle of the grid.
        /// </summary>
        private float startAngle = 0.0F;

        /// <summary>
        /// Create sweep angle of the grid.
        /// </summary>
        private float sweepAngle = 180.0F;

        /// <summary>
        /// 
        /// </summary>
        private Pen diagramPen;

        /// <summary>
        /// 
        /// </summary>
        private Brush diagramBrush;
        
        /// <summary>
        /// 
        /// </summary>
        private Font diagramNameFont;

        /// <summary>
        /// 
        /// </summary>
        private Brush diagramNameFontBrush;

        /// <summary>
        /// 
        /// </summary>
        private PointF diagramNameTextPoint;

        /// <summary>
        /// 
        /// </summary>
        private int alphaBlendGraphics = 120;


        private int alphaBlendGrid = 255;


        #endregion

        #region Properties

        /// <summary>
        /// Diagram name.
        /// </summary>
        public string DiagramName { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// On drawing finished.
        /// </summary>
        public EventHandler<EventArgs> OnReadyDraw;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="size">Size of the diagram.</param>
        public CircularDiagram(Size size)
        {
            //
            if (size == null)
            {
                throw new ArgumentNullException("Size can not be null.");
            }

            //
            this.Size = size;

            //
            if (this.Size.Height <= 0 || this.Size.Width <= 0)
            {
                throw new ArgumentException("Invalid size value.");
            }

            //
            if ((this.Size.Width / 2) != this.Size.Height)
            {
                throw new FormatException("Invalid size value.");
            }

            this.backgroungColor = Color.Black;

            //
            this.diagramPen = new Pen(Color.FromArgb(alphaBlendGraphics, Color.Blue), 2);

            //
            this.diagramBrush = new SolidBrush(Color.FromArgb(alphaBlendGraphics, Color.Green));

            //
            this.gridPen = new Pen(Color.FromArgb(this.alphaBlendGrid, 0, 70, 0), 3);

            //
            this.diagramNameFontBrush = new SolidBrush(Color.FromArgb(this.alphaBlendGrid, Color.Red));

            //
            this.diagramCurvePoints = new Point[181];

            // Make center point for the graphics.
            this.diagramCenterPoint = new Point((int)(this.Size.Width / 2), 0);

            // Create font and brush.
            this.diagramNameFont = new Font("Arial", 16); //

            // Create point for upper-left corner of drawing.
            //this.diagramNameTextPoint = new PointF((int)(this.Size.Width / 2.2), (int)(this.Size.Height * 0.9));
            this.diagramNameTextPoint = new PointF(20, 20);

            // Preset the data.
            for (int index = 0; index < this.diagramCurvePoints.Length; index++ )
            {
                this.diagramCurvePoints[index] = this.diagramCenterPoint;
            }
            
            //
            InitializeComponent();


        }

        #endregion

        #region Public

        /// <summary>
        /// Draw the diagram.
        /// </summary>
        /// <param name="sensorData">Sensor data.</param>
        /// <returns>Bitmap image with the diagram.</returns>
        public void Draw(Graphics graphics)
        {
            // Setup the graphics.
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Clear with black color for background.
            graphics.Clear(this.backgroungColor);


            // Draw string to screen.
            graphics.DrawString(this.DiagramName, this.diagramNameFont, this.diagramNameFontBrush, this.diagramNameTextPoint);

            //
            graphics.ScaleTransform(1.0f, -1.0f);
            graphics.TranslateTransform(0.0f, -(float)this.Size.Height);


            // Draw the grid on the screen.
            for (double index = 0.25d; index <= 1.0d; index += 0.25d)
            {
                graphics.DrawArc(this.gridPen, this.GenerateRectangle(diagramCenterPoint, (int)(this.Size.Height * index)), startAngle, sweepAngle);
            }
            for (int index = 0; index <= 180; index += 10)
            {
                graphics.DrawLine(this.gridPen, this.PolarToDecart((int)(this.Size.Height * 0.25f), index, this.diagramCenterPoint), this.PolarToDecart(this.Size.Height, index, this.diagramCenterPoint));
            }

            // Draw the diagram
            graphics.DrawLines(this.diagramPen, this.diagramCurvePoints);
            graphics.FillPolygon(this.diagramBrush, this.diagramCurvePoints, FillMode.Winding);
            

            if (this.OnReadyDraw != null)
            {
                this.OnReadyDraw(this, null);
            }
        }

        /// <summary>
        /// Draw line on specified angle.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="angle"></param>
        public void DrawLine(Graphics graphics, int angle)
        {
            graphics.DrawLine(new Pen(Brushes.BlueViolet, 5), this.PolarToDecart((int)(this.Size.Height * 0.25f), angle, this.diagramCenterPoint), this.PolarToDecart(this.Size.Height, angle, this.diagramCenterPoint));
        }

        /// <summary>
        /// Set the data.
        /// </summary>
        /// <param name="Data">Input sensor data.</param>
        public void SetData(double[] data)
        {
            //
            if (data.Length > 181 || data.Length < 181)
            {
                return;
            }

            // Validate content.
            for (int index = 0; index < data.Length; index++)
            {
                if (data[index] > 1)
                {
                    data[index] = 1.0d;
                }

                if (data[index] < 0)
                {
                    data[index] = 0.0d;
                }
            }

            // 
            for (int i = 0; i < data.Length; i++)
            {
                this.diagramCurvePoints[i] = this.PolarToDecart(data[i] * this.Size.Height, i, this.diagramCenterPoint);
            }
        }


        /// <summary>
        /// Set data on specified position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="distance"></param>
        public void SetData(int position, double distance)
        {
            //
            if (position < 0 || position > 181)
            {
                return;
            }

            if (distance > 1)
            {
                distance = 1.0d;
            }

            if (distance < 0)
            {
                distance = 0.0d;
            }

            this.diagramCurvePoints[position] = this.PolarToDecart(distance * this.Size.Height, position, this.diagramCenterPoint);

            //this.diagramCurvePoints[position] = this.polarToDecart(distance * this.Size.Height, position, this.diagramCenterPoint);
        }

        #endregion

        #region Private

        /// <summary>
        /// Transform from polar coordinates to Decca.
        /// </summary>
        /// <param name="radius">Radius</param>
        /// <param name="angle">Angle</param>
        /// <param name="center">Center point of the vector.</param>
        /// <returns></returns>
        private Point PolarToDecart(double radius, double angle, Point center)
        {
            //
            double angleRadian = angle * 2 * Math.PI / 360;

            //
            int x = Convert.ToInt32(radius * Math.Cos(angleRadian)) + center.X;
            int y = Convert.ToInt32(radius * Math.Sin(angleRadian)) + center.Y;

            //
            return new Point(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private Rectangle GenerateRectangle(Point center, int radius)
        {
            //
            Rectangle rectangle = new Rectangle(center.X - radius, center.Y - radius, radius * 2, radius * 2);

            //
            return rectangle;    
        }

        protected override void OnResize(EventArgs e)
        {
            this.Width = this.Height * 2;
            base.OnResize(e);
        }

        #endregion

    }
}
