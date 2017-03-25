using KarelV1Lib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace KarelV1.TrajectoryManagment
{
    class TrajectoryVisualiser
    {

        #region Variables

        /// <summary>
        /// Picture box to show the trajectory.
        /// </summary>
        private PictureBox pbTrajectory;
        
        /// <summary>
        /// Trajectory container.
        /// </summary>
        private Trajectory trajectory = new Trajectory();

        /// <summary>
        /// Single trajectory point pen.
        /// </summary>
        private Pen pointsPen = new Pen(Color.Red, 2f);

        /// <summary>
        /// Cursor pen.
        /// </summary>
        private Pen unlockedCursorPen = new Pen(Color.Green, 1f);

        /// <summary>
        /// 
        /// </summary>
        private Pen lockedCursorPen = new Pen(Color.Red, 1f);

        /// <summary>
        /// Trajectory pen
        /// </summary>
        private Pen trajectoryPen = new Pen(Color.Red, 2f);

        /// <summary>
        /// Selected point pen.
        /// </summary>
        private Pen selectedPointPen = new Pen(Color.Blue, 2f);
        
        /// <summary>
        /// Current cursor pen.
        /// </summary>
        private Point cursorPoint = new Point();

        /// <summary>
        /// Cursor radius.
        /// </summary>
        private int cursorRadius = 3;

        /// <summary>
        /// Single trajectory point radius.
        /// </summary>
        private int pointsRadius = 3;

        /// <summary>
        /// Selected point radius.
        /// </summary>
        private int selectedPointRadius = 5;

        /// <summary>
        /// Single trajectory point, font.
        /// </summary>
        private Font pointsFont = new Font("Arial", 8);

        /// <summary>
        /// Current selected point.
        /// </summary>
        private int slsectedPointIndex = -1;

        #endregion

        #region Properties

        /// <summary>
        /// Trajectory mode.
        /// </summary>
        public TrajectoryMode TrajectoryMode { get; set; }

        public double TrajectoryDelay { get; set; }

        public bool LockEditing { get; set; }

        #endregion

        #region Constructor

        public TrajectoryVisualiser()
        {
            this.TrajectoryMode = TrajectoryMode.None;
        }

        #endregion

        #region Private Methods

        private void DrawOn_MouseMove(object sender, MouseEventArgs e)
        {
            this.cursorPoint = e.Location;
            this.SafeGraphicsRefresh();
        }

        private void DrawOn_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.LockEditing) return;

            if(e.Button == MouseButtons.Left)
            { 
                if (TrajectoryMode == TrajectoryMode.DefinePoints)
                {
                    double y = this.Map(e.Y, this.pbTrajectory.Height, 0, this.pbTrajectory.Height, 0);
                    double x = this.Map(e.X, this.pbTrajectory.Width , 0, this.pbTrajectory.Width, 0);
                    Point p = new Point((int)y, (int)x);
                    this.trajectory.Add(p, this.TrajectoryDelay);
                }
            }
            else if(e.Button == MouseButtons.Right)
            {
                this.trajectory.Clear();
            }

            this.SafeGraphicsRefresh();
        }

        private void DrawOn_Paint(object sender, PaintEventArgs e)
        {
            // e.Graphics the graphics.
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //e.Graphics.CompositingMode = CompositingMode.SourceCopy;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            //e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            e.Graphics.Clear(Color.White);

            if (this.trajectory.Count > 1)
            { 
                e.Graphics.DrawLines(this.trajectoryPen, this.trajectory.GetPoints());
            }

            int index = 1;
            foreach (Position rp in this.trajectory)
            {
                PointF p = rp.ToCartesian();
                e.Graphics.DrawEllipse(pointsPen, p.X - this.pointsRadius, p.Y - this.pointsRadius, this.pointsRadius * 2, this.pointsRadius * 2);

                PointF textLocation = new PointF(p.X + 3, p.Y);
                e.Graphics.DrawString(string.Format("{0}", index), pointsFont, Brushes.Green, textLocation);
                index++;
            }

            if (this.slsectedPointIndex > -1 && this.slsectedPointIndex < this.trajectory.Count)
            {
                PointF p = this.trajectory[this.slsectedPointIndex].ToCartesian();

                e.Graphics.DrawEllipse(this.selectedPointPen, p.X - this.selectedPointRadius, p.Y - this.selectedPointRadius, this.selectedPointRadius * 2, this.selectedPointRadius * 2);
            }

            if(this.LockEditing)
            {
                e.Graphics.DrawLine(this.lockedCursorPen, new Point(this.cursorPoint.X, 0), new Point(this.cursorPoint.X, this.pbTrajectory.Height));
                e.Graphics.DrawLine(this.lockedCursorPen, new Point(0, this.cursorPoint.Y), new Point(this.pbTrajectory.Width, this.cursorPoint.Y));
                e.Graphics.DrawEllipse(this.lockedCursorPen, this.cursorPoint.X - this.cursorRadius, this.cursorPoint.Y - this.cursorRadius, this.cursorRadius * 2, this.cursorRadius * 2);
            }
            else
            {
                e.Graphics.DrawLine(this.unlockedCursorPen, new Point(this.cursorPoint.X, 0), new Point(this.cursorPoint.X, this.pbTrajectory.Height));
                e.Graphics.DrawLine(this.unlockedCursorPen, new Point(0, this.cursorPoint.Y), new Point(this.pbTrajectory.Width, this.cursorPoint.Y));
                e.Graphics.DrawEllipse(this.unlockedCursorPen, this.cursorPoint.X - this.cursorRadius, this.cursorPoint.Y - this.cursorRadius, this.cursorRadius * 2, this.cursorRadius * 2);
            }
        }

        private double Map(double value, double inMin, double inMax, double outMin, double outMax)
        {
            return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        private void SafeGraphicsRefresh()
        {
            if (this.pbTrajectory.InvokeRequired)
            {
                this.pbTrajectory.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.pbTrajectory.Refresh();
                });
            }
            else
            {
                this.pbTrajectory.Refresh();
            }
        }

        #endregion

        #region Public Methods

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

        public void AddControl(PictureBox controlToDrawOn)
        {
            if (controlToDrawOn == null) return;

            this.pbTrajectory = controlToDrawOn;
            this.pbTrajectory.Paint += DrawOn_Paint;
            this.pbTrajectory.MouseDown += DrawOn_MouseDown;
            this.pbTrajectory.MouseMove += DrawOn_MouseMove;
        }

        public void AddPoint(Position rp)
        {
            this.trajectory.Add(rp);
            this.SafeGraphicsRefresh();
        }

        public Trajectory GetTrajectory()
        {
            return this.trajectory;
        }

        internal void SetTrajectory(Trajectory trajectory)
        {
            if (trajectory == null) return;

            this.trajectory = trajectory;

            this.SafeGraphicsRefresh();
        }

        #endregion
    }
}
