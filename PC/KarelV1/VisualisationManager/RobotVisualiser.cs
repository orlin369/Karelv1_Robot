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
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

using KarelV1Lib.Data;
using KarelV1.Util;

namespace KarelV1.VisualisationManager
{
    class RobotVisualiser
    {

        #region Variables

        #region Canvas

        /// <summary>
        /// Picture box to show the trajectory.
        /// </summary>
        private PictureBox pbCanvas;

        /// <summary>
        /// Canvas background image.
        /// </summary>
        private Bitmap backgroundImage;

        #endregion

        #region Trajectory

        /// <summary>
        /// Trajectory container.
        /// </summary>
        private Positions trajectory = new Positions();

        /// <summary>
        /// Trajectory pen
        /// </summary>
        private Pen trajectoryPen = new Pen(Color.Red, 2f);

        /// <summary>
        /// Single trajectory point, font.
        /// </summary>
        private Font trajectoryFont = new Font("Arial", 8);

        /// <summary>
        /// Single trajectory point pen.
        /// </summary>
        private Pen trajectoryPointsPen = new Pen(Color.Red, 2f);

        /// <summary>
        /// Single trajectory point radius.
        /// </summary>
        private int trajectoryPointsRadius = 3;

        #endregion

        #region Cursor

        /// <summary>
        /// Current cursor pen.
        /// </summary>
        private Point cursorPoint = new Point();

        /// <summary>
        /// Cursor radius.
        /// </summary>
        private int cursorRadius = 3;

        /// <summary>
        /// Cursor visibility.
        /// </summary>
        private bool cursorVisible = false;

        /// <summary>
        /// Unlocked cursor pen.
        /// </summary>
        private Pen unlockedCursorPen = new Pen(Color.Green, 1f);

        /// <summary>
        /// Locked cursor pen.
        /// </summary>
        private Pen lockedCursorPen = new Pen(Color.Orange, 1f);

        #endregion

        #region Current Selected Point

        /// <summary>
        /// Selected point pen.
        /// </summary>
        private Pen selectedPointPen = new Pen(Color.Blue, 2f);

        /// <summary>
        /// Selected point radius.
        /// </summary>
        private int selectedPointRadius = 5;

        /// <summary>
        /// Current selected point.
        /// </summary>
        private int slsectedPointIndex = -1;

        #endregion

        #region Robot Animation

        /// <summary>
        /// Actual robot position.
        /// </summary>
        private Position actualRobotPosition = new Position();

        /// <summary>
        /// Robot marker pen.
        /// </summary>
        private Pen robotPositionPen = new Pen(Color.Black, 2f);
        
        /// <summary>
        /// Left side sensor flag.
        /// </summary>
        private float robotLeftSensor = 0f;

        /// <summary>
        /// Right side sensor flag.
        /// </summary>
        private float robotRightSensor = 0f;

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Trajectory mode.
        /// </summary>
        public CaptureMode CaptureMode { get; set; }

        /// <summary>
        /// Trajectory container.
        /// </summary>
        public Positions Trajectory
        {
            get
            {
                return trajectory;
            }
            set
            {
                this.trajectory = value;
                this.SafeGraphicsRefresh();
            }
        }

        /// <summary>
        /// Lock editing mode.
        /// </summary>
        public bool LockEditing { get; set; }

        /// <summary>
        /// Steps per second.
        /// </summary>
        public double StepsPerSecond { get; set; }

        #endregion

        #region Constructor

        public RobotVisualiser()
        {
            this.CaptureMode = CaptureMode.None;
            robotPositionPen.StartCap = LineCap.ArrowAnchor;
            robotPositionPen.EndCap = LineCap.RoundAnchor;
        }

        #endregion

        #region pbCanvas

        private void pbCanvas_MouseLeave(object sender, EventArgs e)
        {
            this.cursorVisible = false;
            this.SafeGraphicsRefresh();
        }

        private void pbCanvas_MouseEnter(object sender, EventArgs e)
        {
            this.cursorVisible = true;
            this.SafeGraphicsRefresh();
        }

        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            this.cursorPoint = e.Location;
            this.SafeGraphicsRefresh();
        }

        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.LockEditing) return;

            if(e.Button == MouseButtons.Left)
            { 
                if (CaptureMode == CaptureMode.DefinePoints)
                {
                    double y = this.Map(e.Y, this.pbCanvas.Height, 0, this.pbCanvas.Height, 0);
                    double x = this.Map(e.X, this.pbCanvas.Width , 0, this.pbCanvas.Width, 0);
                    Point p = new Point((int)y, (int)x);
                    this.Trajectory.Add(p, this.StepsPerSecond);
                }
            }
            else if(e.Button == MouseButtons.Right)
            {
                this.trajectory.Clear();
            }

            this.SafeGraphicsRefresh();
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {

            #region Setup Graphics

            // e.Graphics the graphics.
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //e.Graphics.CompositingMode = CompositingMode.SourceCopy;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            //e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Clear the background.
            e.Graphics.Clear(Color.White);

            #endregion

            #region Draw background

            if (this.backgroundImage != null)
            {
                e.Graphics.DrawImage(this.backgroundImage, new PointF());
            }

            #endregion
            
            #region Draw trajectory line

            if (this.trajectory.Count > 1)
            { 
                e.Graphics.DrawLines(this.trajectoryPen, this.trajectory.GetPoints());
            }

            #endregion

            #region Draw trajectory points

            int index = 1;
            foreach (Position position in this.trajectory)
            {
                PointF p = position.ToCartesian();
                e.Graphics.DrawEllipse(trajectoryPointsPen, p.X - this.trajectoryPointsRadius, p.Y - this.trajectoryPointsRadius, this.trajectoryPointsRadius * 2, this.trajectoryPointsRadius * 2);

                PointF textLocation = new PointF(p.X + 3, p.Y);
                e.Graphics.DrawString(string.Format("{0}", index), trajectoryFont, Brushes.Green, textLocation);

                if(position.DistanceSensors != null)
                {
                    if(position.DistanceSensors.Count > 0)
                    {
                        foreach(DistanceSensors ds in position.DistanceSensors)
                        {
                            //TODO: Draw the graphics to the screen.
                        }
                    }
                }

                index++;
            }

            #endregion

            #region Draw selected position

            if (this.slsectedPointIndex > -1 && this.slsectedPointIndex < this.trajectory.Count)
            {
                PointF p = this.trajectory[this.slsectedPointIndex].ToCartesian();
                e.Graphics.DrawEllipse(this.selectedPointPen, p.X - this.selectedPointRadius, p.Y - this.selectedPointRadius, this.selectedPointRadius * 2, this.selectedPointRadius * 2);
            }

            #endregion

            #region Draw robot position
            
            PointF actualPosition = actualRobotPosition.ToCartesian();

            Rectangle contour = new Rectangle((int)actualPosition.X - this.selectedPointRadius,
                                               (int)actualPosition.Y - this.selectedPointRadius,
                                               this.selectedPointRadius * 2, this.selectedPointRadius * 2);

            if (actualRobotPosition.DistanceSensors != null)
            {
                if (actualRobotPosition.DistanceSensors.Count > 0)
                {
                    foreach (DistanceSensors ds in actualRobotPosition.DistanceSensors)
                    {
                        //TODO: Draw the graphics to the screen.
                    }
                }
            }

            //TODO: Draw left, right sensors.

            e.Graphics.DrawEllipse(this.robotPositionPen, contour);

            #endregion

            #region Draw the cursor

            if (this.cursorVisible)
            {
                if (this.LockEditing)
                {
                    e.Graphics.DrawLine(this.lockedCursorPen, new Point(this.cursorPoint.X, 0), new Point(this.cursorPoint.X, this.pbCanvas.Height));
                    e.Graphics.DrawLine(this.lockedCursorPen, new Point(0, this.cursorPoint.Y), new Point(this.pbCanvas.Width, this.cursorPoint.Y));
                    e.Graphics.DrawEllipse(this.lockedCursorPen, this.cursorPoint.X - this.cursorRadius, this.cursorPoint.Y - this.cursorRadius, this.cursorRadius * 2, this.cursorRadius * 2);
                }
                else
                {
                    e.Graphics.DrawLine(this.unlockedCursorPen, new Point(this.cursorPoint.X, 0), new Point(this.cursorPoint.X, this.pbCanvas.Height));
                    e.Graphics.DrawLine(this.unlockedCursorPen, new Point(0, this.cursorPoint.Y), new Point(this.pbCanvas.Width, this.cursorPoint.Y));
                    e.Graphics.DrawEllipse(this.unlockedCursorPen, this.cursorPoint.X - this.cursorRadius, this.cursorPoint.Y - this.cursorRadius, this.cursorRadius * 2, this.cursorRadius * 2);
                }
            }

            #endregion

        }

        #endregion

        #region Private Methods

        private double Map(double value, double inMin, double inMax, double outMin, double outMax)
        {
            return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        private void SafeGraphicsRefresh()
        {
            if (this.pbCanvas.InvokeRequired)
            {
                this.pbCanvas.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.pbCanvas.Refresh();
                });
            }
            else
            {
                this.pbCanvas.Refresh();
            }
        }

        #endregion

        #region Public Methods

        public void AddControl(PictureBox controlToDrawOn)
        {
            if (controlToDrawOn == null) return;

            this.pbCanvas = controlToDrawOn;
            this.pbCanvas.Paint += pbCanvas_Paint;
            this.pbCanvas.MouseDown += pbCanvas_MouseDown;
            this.pbCanvas.MouseMove += pbCanvas_MouseMove;
            this.pbCanvas.MouseEnter += pbCanvas_MouseEnter;
            this.pbCanvas.MouseLeave += pbCanvas_MouseLeave;

            this.SafeGraphicsRefresh();
        }

        public void SetCurrentPoint(int index)
        {
            if (index < -1 && index > trajectory.Count) return;
            this.slsectedPointIndex = index;
            this.SafeGraphicsRefresh();
        }

        public void ResetCurrentPoint()
        {
            this.slsectedPointIndex = -1;
            this.SafeGraphicsRefresh();
        }

        public void SetRobotPosition(Position position)
        {
            this.actualRobotPosition = position;

            if (this.CaptureMode == CaptureMode.RecordMotion)
            {
                this.Trajectory.Add(position);
            }

            this.SafeGraphicsRefresh();
        }

        public void SetSideSensors(Sensors sensors)
        {
            this.robotLeftSensor = sensors.Left;
            this.robotRightSensor = sensors.Right;
            this.SafeGraphicsRefresh();
        }

        public void SetBackgroundImage(Bitmap image)
        {
            if (image == null) return;
            this.backgroundImage = AppUtils.FitImage(image, this.pbCanvas.Size);
            this.SafeGraphicsRefresh();
        }

        #endregion
    }
}
