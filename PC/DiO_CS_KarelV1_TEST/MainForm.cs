using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Diagrams;
using KarelRobot;
using Utils;
using System.Threading;
using KarelRobot.Events;

namespace DiO_CS_KarelV1_TEST
{
    /// <summary>
    /// Main form of robot controller.
    /// </summary>
    public partial class MainForm : Form
    {

        #region Variables

        /// <summary>
        /// Robot seral port name.
        /// </summary>
        private string robotSerialPortName = "";

        /// <summary>
        /// Robot communication.
        /// </summary>
        private KarelV1 myRobot;

        /// <summary>
        /// Data generator.
        /// </summary>
        private DigramDataGenerator dataGenerator = new DigramDataGenerator();

        /// <summary>
        /// Circular diagram.
        /// </summary>
        private CircularDiagram myDiagram;

        /// <summary>
        /// Sensor data.
        /// </summary>
        private double[] sensorData = new double[181];

        /// <summary>
        /// Maximum distance index.
        /// </summary>
        private int maxDistanceIndex = 0;

        /// <summary>
        /// Maximum distance value.
        /// </summary>
        private double maxDistanceValue = -100;

        /// <summary>
        /// Update camera timer.
        /// </summary>
        private System.Windows.Forms.Timer cameraUpdateTimer;

        /// <summary>
        /// IP Camera for the glyph recogniser.
        /// </summary>
        private ImageSource.IpCamera ipCamera;

        private Bitmap capturedImage;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            this.cameraUpdateTimer = new System.Windows.Forms.Timer();
            this.cameraUpdateTimer.Stop();
            this.cameraUpdateTimer.Tick += this.cameraUpdateTimer_Tick;
        }

        #endregion

        #region Private

        /// <summary>
        /// Connect to the robot.
        /// </summary>
        private void ConnectToRobot()
        {
            try
            {
                // COM55 - Bluetooth
                // COM67 - Cabel
                // COM73 - cabel
                this.myRobot = new KarelV1(this.robotSerialPortName);
                //this.myRobot.Message += myRobot_Message;
                this.myRobot.Sensors += myRobot_Sensors;
                this.myRobot.UltraSonicSensor += myRobot_UltraSonicSensor;
                this.myRobot.Stoped += myRobot_Stoped;
                this.myRobot.GreatingsMessage += myRobot_GreatingsMessage;
                this.myRobot.MotionState += myRobot_MotionState;
                this.myRobot.Connect();
                this.myRobot.Reset();
            }
            catch (Exception exception)
            {
                this.txtState.Text = exception.Message;
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Disconnect from the robot.
        /// </summary>
        private void DisconnectFromRobot()
        {
            try
            {
                if (this.myRobot != null && this.myRobot.IsConnected)
                {
                    this.myRobot.Disconnect();
                }
            }
            catch (Exception exception)
            {
                //this.AddLogRow(exception.ToString());
            }
        }

        /// <summary>
        /// Search serial port.
        /// </summary>
        private void SearchForPorts()
        {
            this.portsToolStripMenuItem.DropDown.Items.Clear();

            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();

            if (portNames.Length == 0)
            {
                return;
            }

            foreach (string item in portNames)
            {
                //store the each retrieved available prot names into the MenuItems...
                this.portsToolStripMenuItem.DropDown.Items.Add(item);
            }

            foreach (ToolStripMenuItem item in this.portsToolStripMenuItem.DropDown.Items)
            {
                item.Click += mItPorts_Click;
                item.Enabled = true;
                item.Checked = false;
            }
        }

        /// <summary>
        /// Set the status label in the main form.
        /// </summary>
        /// <param name="status">Text to be written.</param>
        private void SetStatus(string text, Color textColor)
        {
            // + Environment.NewLine
            if (this.txtState.InvokeRequired)
            {
                this.txtState.BeginInvoke((MethodInvoker)delegate()
                {
                    this.txtState.Text = text;
                    this.txtState.BackColor = textColor;
                });
            }
            else
            {
                this.txtState.Text = text;
                this.txtState.BackColor = textColor;
            }
        }

        /// <summary>
        /// Add a status to the status list..
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textColor"></param>
        private void AddStatus(string text, Color textColor)
        {
            if (this.txtState.InvokeRequired)
            {
                this.txtState.BeginInvoke((MethodInvoker)delegate()
                {
                    this.txtState.AppendText(text);
                    this.txtState.BackColor = textColor;
                });
            }
            else
            {
                this.txtState.AppendText(text);
                this.txtState.BackColor = textColor;
            }
        }

        /// <summary>
        /// Set left sensor progres bar.
        /// </summary>
        /// <param name="value"></param>
        private void SetLeftSensor(int value)
        {
            if (this.prbRightSensor.InvokeRequired)
            {
                this.prbRightSensor.BeginInvoke((MethodInvoker)delegate()
                {
                    this.prbRightSensor.Value = value;
                });
            }
            else
            {
                this.prbRightSensor.Value = value;
            }
        }

        /// <summary>
        /// Set righr sensor progres bar.
        /// </summary>
        /// <param name="value"></param>
        private void SetRightSensor(int value)
        {
            if (this.prbRightSensor.InvokeRequired)
            {
                this.prbRightSensor.BeginInvoke((MethodInvoker)delegate()
                {
                    this.prbRightSensor.Value = value;
                });
            }
            else
            {
                this.prbRightSensor.Value = value;
            }
        }

        /// <summary>
        /// Return curret data and time.
        /// </summary>
        /// <returns></returns>
        private string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy.MM.dd/HH:mm:ss.fff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Capture the navigation video camera.
        /// </summary>
        private void CaptureCamera()
        {
            // IP Camera:    http://http://192.168.1.1/cgi-bin/image.jpg
            // Mobile phone: http://192.168.0.104:8080/photoaf.jpg

            Uri uri;
            bool isValidUri = Uri.TryCreate(this.tbCameraIP.Text, UriKind.Absolute, out uri);
            if (!isValidUri)
            {
                return;
            }

            if (this.ipCamera == null)
            {
                this.ipCamera = new ImageSource.IpCamera(uri);
                //this.ipCamera.Torch = true;
            }

            if (uri != this.ipCamera.URI)
            {
                this.ipCamera = new ImageSource.IpCamera(uri);
                //this.ipCamera.Torch = true;
            }

            if (this.capturedImage != null) this.capturedImage.Dispose();

            try
            {
                this.capturedImage = this.ipCamera.Capture();

                this.pbGlyph.Image = AppUtils.FitImage(this.capturedImage, this.pbGlyph.Size);
            }
            catch(Exception exception)
            {
                this.AddStatus(exception.ToString(), Color.White);
                return;
            }
        }

        #endregion

        #region Robot

        private void myRobot_Message(object sender, StringEventArgs e)
        {
            string infoLine = String.Format("{0} -> {1}", this.GetDateTime(), e.Message);
            this.AddStatus(infoLine + Environment.NewLine, Color.White);
        }

        private void myRobot_GreatingsMessage(object sender, StringEventArgs e)
        {
            string infoLine = String.Format("{0} -> {1}", this.GetDateTime(), e.Message);
            this.AddStatus(infoLine + Environment.NewLine, Color.White);
        }

        private void myRobot_Stoped(object sender, EventArgs e)
        {
            string infoLine = String.Format("{0} -> Stoped", this.GetDateTime());
            this.AddStatus(infoLine + Environment.NewLine, Color.White);
        }

        private void myRobot_UltraSonicSensor(object sender, UltraSonicSensorEventArgs e)
        {
            string infoLine = String.Format("{0} -> Ultrasonic Sensor: {1}[deg] {2}[mm]", this.GetDateTime(), e.Position, e.Distance);
            this.AddStatus(infoLine + Environment.NewLine, Color.White);

            if (this.pbSensorView.InvokeRequired)
            {
                this.pbSensorView.BeginInvoke((MethodInvoker)delegate()
                {
                    this.sensorData[e.Position] = e.Distance;
                    //this.myDiagram.SetData(this.sensorData);
                    this.myDiagram.SetData(e.Position, e.Distance);

                    for (int index = 0; index < this.sensorData.Length; index++)
                    {
                        if (this.maxDistanceValue < this.sensorData[index])
                        {
                            this.maxDistanceValue = this.sensorData[index];
                            this.maxDistanceIndex = index;
                        }
                    }

                    this.pbSensorView.Refresh();

                });
            }
            else
            {
                this.sensorData[e.Position] = e.Distance;
                //this.myDiagram.SetData(this.sensorData);
                this.myDiagram.SetData(e.Position, e.Distance);

                for (int index = 0; index < this.sensorData.Length; index++)
                {
                    if (this.maxDistanceValue < this.sensorData[index])
                    {
                        this.maxDistanceValue = this.sensorData[index];
                        this.maxDistanceIndex = index;
                    }
                }
                this.pbSensorView.Refresh();
            }
        }

        private void myRobot_Sensors(object sender, SensorsEventArgs e)
        {
            string infoLine = String.Format("{0} -> Sensors: {1} {2}", this.GetDateTime(), e.Left, e.Right);
            this.AddStatus(infoLine + Environment.NewLine, Color.White);
            this.SetLeftSensor((int)Math.Floor(e.Left));
            this.SetRightSensor((int)Math.Floor(e.Right));
        }

        private void myRobot_MotionState(object sender, MotionStateEventArgs e)
        {
            string infoLine = String.Format("{0} -> Position: A:{1} D:{2}", this.GetDateTime(), e.Alpha, e.Distance);
            this.AddStatus(infoLine + Environment.NewLine, Color.White);
        }

        #endregion

        #region MainForm

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Double buffer optimization.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // Font configuration.
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(Button))
                {
                    control.Font = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Bold);
                }
            }

            //
            this.myDiagram = new CircularDiagram(this.pbSensorView.Size);
            this.myDiagram.DiagramName = "Ultra Sonic Sensor";

            this.SearchForPorts();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DisconnectFromRobot();
        }

        #endregion

        #region Buttons

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null && this.myRobot.IsConnected)
            {
                // 10 mm
                this.myRobot.Move(100);
            }
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null && this.myRobot.IsConnected)
            {
                // -10 mm
                this.myRobot.Move(-100);
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null && this.myRobot.IsConnected)
            {
                // -10 deg
                this.myRobot.Rotate(-100);
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null && this.myRobot.IsConnected)
            {
                // 10 deg
                this.myRobot.Rotate(100);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null && this.myRobot.IsConnected)
            {
                this.myRobot.Stop();
            }
        }

        private void btnGetSensors_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null && this.myRobot.IsConnected)
            {
                this.myRobot.GetSensors();
            }
        }

        private void btnGetUltrasonic_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null && this.myRobot.IsConnected)
            {
                int position = 0;

                if (int.TryParse(this.tbSensorPosition.Text, out position))
                {
                    if (position > 180 || position < 0)
                    {
                        return;
                    }

                    this.myRobot.GetUltraSonic(position);
                }
                else
                {
                    for (int index = 0; index < this.sensorData.Length; index++)
                    {
                        maxDistanceValue = this.sensorData[index] = 0.0d;
                    }

                    this.maxDistanceValue = double.MinValue;
                    this.maxDistanceIndex = 0;

                    this.myRobot.GetUltraSonic();
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null && this.myRobot.IsConnected)
            {
                this.myRobot.Reset();
            }
        }

        private void tbSensorPosition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.btnGetUltrasonic_Click(sender, new EventArgs());
            }
        }

        private void btnGetRobotPos_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null && this.myRobot.IsConnected)
            {
                // -10 mm
                this.myRobot.GetPosition();
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            Thread captureThread = new Thread(
                new ThreadStart(
                    delegate()
                    {
                        this.CaptureCamera();
                    }
            ));
            captureThread.Start();
        }

        #endregion

        #region pbSensorView

        private void pbSensorView_Paint(object sender, PaintEventArgs e)
        {
            this.myDiagram.Draw(e.Graphics);
            this.myDiagram.DrawLine(e.Graphics, this.maxDistanceIndex);
        }

        #endregion

        #region Menu Item

        private void mItPorts_Click(object sender, EventArgs e)
        {
            this.DisconnectFromRobot();
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            this.robotSerialPortName = item.Text;
            this.ConnectToRobot();

            if (this.myRobot.IsConnected)
            {
                this.lblIsConnected.Text = String.Format("Connected: {0}", robotSerialPortName);
                item.Checked = true;
            }
            else
            {
                this.lblIsConnected.Text = "Not Connected";
                item.Checked = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SearchForPorts();
        }
        
        #endregion

        #region chkCameraCapture

        private void chkCameraCapture_CheckedChanged(object sender, EventArgs e)
        {
            int interval = 500;
            bool IsValidTime = int.TryParse(this.tbCameraUpdateTime.Text, out interval);
            if (!IsValidTime)
            {
                return;
            }

            this.cameraUpdateTimer.Interval = interval;

            if (this.chkCameraCapture.Checked)
            {
                this.cameraUpdateTimer.Start();
            }
            else
            {
                this.cameraUpdateTimer.Stop();
            }

            this.btnCapture.Enabled = !this.chkCameraCapture.Checked;
        }

        #endregion

        #region Camera Update Timer

        private void cameraUpdateTimer_Tick(object sender, EventArgs e)
        {
            this.CaptureCamera();
        }

        #endregion.Events

    }
}
