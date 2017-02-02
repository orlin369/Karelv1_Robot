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

using DatabaseConnection;
using DatabaseConnection.Device.Actuators;
using DatabaseConnection.Device.Sensors;
using DatabaseConnection.Units;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using KarelV1.Util;
using KarelV1.Properties;

using KarelV1Lib;
using KarelV1Lib.Events;

using IPWebcam;
using System.Windows.Forms.DataVisualization.Charting;
using System.Speech.Synthesis;
using KarelV1Lib.Adapters;
using KarelV1.Settings;

// 
// Robot tasks ...
// TODO: Add Tweeter publisher.
// TODO: Create WEB application that listen for the tweets and show it on the screen.
// TODO: Create glyph recognition module.
// TODO: Add message when robot is moving and when it is not.
// TODO: Add sensors.
//

namespace KarelV1
{
    /// <summary>
    /// Main form of robot controller.
    /// </summary>
    public partial class MainForm : Form
    {

        #region Variables

        /// <summary>
        /// Robot serial port name.
        /// </summary>
        private string robotSerialPortName = "";

        /// <summary>
        /// Robot communication.
        /// </summary>
        private KarelV1Lib.KarelV1 robot;

        /// <summary>
        /// The differential control model.
        /// </summary>
        private DifferentialModel differentialModel;

        /// <summary>
        /// Update camera timer.
        /// </summary>
        private System.Windows.Forms.Timer cameraUpdateTimer;

        /// <summary>
        /// IP Camera for the glyph recognizer.
        /// </summary>
        private IpCamera ipCamera;

        /// <summary>
        /// Captured image from the IP camera/stream.
        /// </summary>
        private Bitmap capturedImage;

        /// <summary>
        /// Sync object.
        /// </summary>
        private object syncLockCapture = new object();

        /// <summary>
        /// X axis of the chart.
        /// </summary>
        private double[] xValues = new double[361];

        /// <summary>
        /// Ultrasonic distance sensor measurements values.
        /// </summary>
        private double[] ultrasonicTime = new double[361];

        /// <summary>
        /// Infrared distance sensor measurements values.
        /// </summary>
        private double[] infraredADC = new double[361];

        private GP2Y0A21YK irSensor = new GP2Y0A21YK(5, 1024);

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

            // The differential controlling model.
            this.differentialModel = new DifferentialModel(
                Properties.Settings.Default.SteppsCount,
                Properties.Settings.Default.StepperPostScaler,
                Properties.Settings.Default.DiametterOfWheel,
                Properties.Settings.Default.DistanceBetweenWheels);
        }

        #endregion

        #region Main Form

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

            this.SearchForPorts();

            this.SetupUltrasonicChart();

            this.SetupScaleComboBox();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DisconnectFromRobotViaSerial();
            this.DisconnectFromRobotViaMqtt();
        }

        #endregion

        #region Buttons

        private void btnForward_Click(object sender, EventArgs e)
        {
            this.MoveForward();
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            this.MoveBackward();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            this.MoveLeft();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            this.MoveRight();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Stop();
        }

        private void btnGetSensors_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.GetSensors();
        }

        private void btnGetUltrasonic_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            int position = 0;
            if (int.TryParse(this.tbSensorPosition.Text, out position))
            {
                if (position > 180 || position < 0)
                {
                    return;
                }

                this.robot.GetUltraSonic(position);
            }
            else
            {
                this.robot.GetUltraSonic();
            }
        }

        private void btnGetRobotPos_Click(object sender, EventArgs e)
        {
            if (this.robot != null && this.robot.IsConnected)
            {
                this.robot.GetPosition();
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

        #region Text Box

        private void tbSensorPosition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.btnGetUltrasonic_Click(sender, new EventArgs());
            }
        }

        #endregion

        #region Sensor View

        private void SetupUltrasonicChart()
        {

            //
            // setup the X grid
            //crtUltrasinicSensor.ChartAreas[0].AxisX.LabelStyle.Format = "F3";
            crtUltrasinicSensor.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            crtUltrasinicSensor.ChartAreas[0].AxisX.MajorGrid.Interval = 90;
            crtUltrasinicSensor.ChartAreas[0].AxisX.Crossing = -90;
            crtUltrasinicSensor.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            crtUltrasinicSensor.ChartAreas[0].AxisX.Minimum = 0.0f;
            crtUltrasinicSensor.ChartAreas[0].AxisX.Maximum = 360.0f;
            // setupthe Y grid
            crtUltrasinicSensor.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            crtUltrasinicSensor.ChartAreas[0].Area3DStyle.Enable3D = false;
            crtUltrasinicSensor.ChartAreas[0].AxisY.ScaleView.Zoomable = true;

            // ==== Ultrasonic distance ====
            crtUltrasinicSensor.Series[0].ChartType = SeriesChartType.Polar;
            crtUltrasinicSensor.Series[0].XValueType = ChartValueType.Double;
            crtUltrasinicSensor.Series[0].IsXValueIndexed = true;
            crtUltrasinicSensor.Series[0].Name = "Ultrasonic Sensor";
            crtUltrasinicSensor.Series[0]["PolarDrawingStyle"] = "Line";
            crtUltrasinicSensor.Series[0].Color = Color.Blue;

            // ==== Infrared sensor distance ====
            crtUltrasinicSensor.Series[1].ChartType = SeriesChartType.Polar;
            crtUltrasinicSensor.Series[1].XValueType = ChartValueType.Double;
            crtUltrasinicSensor.Series[1].IsXValueIndexed = true;
            crtUltrasinicSensor.Series[1].Name = "Infrared Sensor";
            crtUltrasinicSensor.Series[1]["PolarDrawingStyle"] = "Line";
            crtUltrasinicSensor.Series[1].Color = Color.Red;


            for (int index = 0; index < ultrasonicTime.Length; index++)
            {
                ultrasonicTime[index] = 0.0F;
                infraredADC[index] = 0.0F;
                xValues[index] = index;
            }

            this.crtUltrasinicSensor.Series[0].Points.DataBindXY(this.xValues, this.ultrasonicTime);
            this.crtUltrasinicSensor.Series[1].Points.DataBindXY(this.xValues, this.infraredADC);
        }

        private void UpdateDiagram()
        {
            if (this.crtUltrasinicSensor.InvokeRequired)
            {
                this.crtUltrasinicSensor.BeginInvoke((MethodInvoker)delegate()
                {
                    this.crtUltrasinicSensor.Series[0].Points.DataBindXY(this.xValues, this.ultrasonicTime);
                    this.crtUltrasinicSensor.Series[1].Points.DataBindXY(this.xValues, this.infraredADC);
                });
            }
            else
            {
                this.crtUltrasinicSensor.Series[0].Points.DataBindXY(this.xValues, this.ultrasonicTime);
                this.crtUltrasinicSensor.Series[1].Points.DataBindXY(this.xValues, this.infraredADC);
            }
        }

        private void SetupScaleComboBox()
        {
            Array items = Enum.GetValues(typeof(MetricScale));
            foreach (MetricScale item in items)
            {
                this.cbMetric.Items.Add(item);
            }
            if (items != null && items.Length > 0)
            {
                this.cbMetric.Text = items.GetValue(0).ToString();
            }
        }

        #endregion

        #region Menu Item

        private void tmsiPorts_Click(object sender, EventArgs e)
        {
            this.DisconnectFromRobotViaSerial();
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            this.robotSerialPortName = item.Text;
            this.ConnectToRobotViaSerial();

            if (this.robot.IsConnected)
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

        private void tsmiConnection_Click(object sender, EventArgs e)
        {
            this.SearchForPorts();
        }

        private void tsmiReset_Click(object sender, EventArgs e)
        {
            if (this.robot != null && this.robot.IsConnected)
            {
                this.robot.Reset();
            }
        }

        private void tsmiSettings_Click(object sender, EventArgs e)
        {
            using (SettingsForm sf = new SettingsForm())
            {
                sf.ShowDialog();
            }
        }

        private void tsmiConnectToMqtt_Click(object sender, EventArgs e)
        {
            this.ConnectToRobotViaMqtt();

            if (this.robot.IsConnected)
            {
                this.lblIsConnected.Text = String.Format("Connected: {0}:{1}", Properties.Settings.Default.BrokerHost, Properties.Settings.Default.BrokerPort);
            }
            else
            {
                this.lblIsConnected.Text = "Not Connected";
            }
        }

        private void tsmiDisconnectFromMqtt_Click(object sender, EventArgs e)
        {
            this.DisconnectFromRobotViaMqtt();

            if (this.robot.IsConnected)
            {
                this.lblIsConnected.Text = String.Format("Connected: {0}:{1}", Properties.Settings.Default.BrokerHost, Properties.Settings.Default.BrokerPort);
            }
            else
            {
                this.lblIsConnected.Text = "Not Connected";
            }

        }

        #endregion

        #region Continues Capture Check

        private void chkContinuesCapture_CheckedChanged(object sender, EventArgs e)
        {
            int interval = 500;
            bool IsValidTime = int.TryParse(this.tbCameraUpdateTime.Text, out interval);
            if (!IsValidTime)
            {
                return;
            }

            this.cameraUpdateTimer.Interval = interval;

            if (this.chkContinuesCapture.Checked)
            {
                this.cameraUpdateTimer.Start();
            }
            else
            {
                this.cameraUpdateTimer.Stop();
            }

            this.btnCapture.Enabled = !this.chkContinuesCapture.Checked;
        }

        #endregion

        #region Camera Update Timer

        private void cameraUpdateTimer_Tick(object sender, EventArgs e)
        {
            this.CaptureCamera();
        }

        #endregion.Events

        #region Robot

        private void ConnectToRobotViaSerial()
        {
            try
            {
                this.robot = new KarelV1Lib.KarelV1(new SerialAdapter(this.robotSerialPortName));
                this.robot.OnMessage += myRobot_OnMessage;
                this.robot.OnSensors += myRobot_OnSensors;
                this.robot.OnDistanceSensors += myRobot_OnDistanceSensors;
                this.robot.OnGreatingsMessage += myRobot_OnGreatingsMessage;
                this.robot.OnStoped += myRobot_OnStoped;
                this.robot.OnPosition += myRobot_OnPosition;
                this.robot.Connect();
                this.robot.Reset();
            }
            catch (Exception exception)
            {
                this.AddStatus(exception.ToString(), Color.White);
            }
        }

        private void DisconnectFromRobotViaSerial()
        {
            try
            {
                if (this.robot != null && this.robot.IsConnected)
                {
                    this.robot.Disconnect();
                }
            }
            catch (Exception exception)
            {
                this.AddStatus(exception.ToString(), Color.White);
            }
        }
        
        private void ConnectToRobotViaMqtt()
        {
            try
            {
                this.robot = new KarelV1Lib.KarelV1(new MqttAdapter(
                    Properties.Settings.Default.BrokerHost,
                    Properties.Settings.Default.BrokerPort,
                    Properties.Settings.Default.MqttInputTopic,
                    Properties.Settings.Default.MqttOutputTopic));

                this.robot.OnMessage += myRobot_OnMessage;
                this.robot.OnSensors += myRobot_OnSensors;
                this.robot.OnDistanceSensors += myRobot_OnDistanceSensors;
                this.robot.OnGreatingsMessage += myRobot_OnGreatingsMessage;
                this.robot.OnStoped += myRobot_OnStoped;
                this.robot.OnPosition += myRobot_OnPosition;
                this.robot.Connect();
                this.robot.Reset();
            }
            catch (Exception exception)
            {
                this.AddStatus(exception.ToString(), Color.White);
            }
        }

        private void DisconnectFromRobotViaMqtt()
        {
            try
            {
                if (this.robot != null && this.robot.IsConnected)
                {
                    this.robot.Disconnect();
                }
            }
            catch (Exception exception)
            {
                this.AddStatus(exception.ToString(), Color.White);
            }
        }

        private void MoveForward()
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            int steps = this.differentialModel.MmToStep(10.0d);
            this.robot.Move(steps);
        }

        private void MoveBackward()
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            int steps = this.differentialModel.MmToStep(-10.0d);
            this.robot.Move(steps);
        }

        private void MoveLeft()
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            int steps = this.differentialModel.DegToStep(-90.0D);
            this.robot.Rotate(steps);
        }

        private void MoveRight()
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            int steps = this.differentialModel.DegToStep(90.0D);
            this.robot.Rotate(steps);
        }

        private void myRobot_OnMessage(object sender, StringEventArgs e)
        {
            this.AddStatus(e.Message, Color.White);
        }

        private void myRobot_OnGreatingsMessage(object sender, StringEventArgs e)
        {
            this.AddStatus(e.Message, Color.White);
        }

        private void myRobot_OnStoped(object sender, EventArgs e)
        {
            this.AddStatus("Stopped", Color.White);
        }

        private void myRobot_OnDistanceSensors(object sender, DistanceSensorsEventArgs e)
        {
            this.AddStatus(String.Format("US Sensor: {0}[deg] {1}[us] {2}[ADC]", e.Position, e.UltrasonicTime, e.InfraRedADCValue), Color.White);

            // TODO: Class HSR04
            float distance = e.UltrasonicTime / 58.0F;
            if (distance > 330.0F) distance = 330.0F;
            this.ultrasonicTime[e.Position] = distance;

            //this.infraredADC[e.Position] = irSensor.Convert(e.InfraRedADCValue); // AppUtils.Map(e.InfraRedADCValue, 0, 1023, 80, 10);
            this.infraredADC[e.Position] = AppUtils.Map(e.InfraRedADCValue, 0, 1023, 80, 10);

            this.UpdateDiagram();
        }

        private void myRobot_OnSensors(object sender, SensorsEventArgs e)
        {
            this.AddStatus(String.Format("Sensors: {0} {1}", e.Left, e.Right), Color.White);

            this.SetLeftSensor((int)Math.Floor(e.Left));
            this.SetRightSensor((int)Math.Floor(e.Right));
        }

        private void myRobot_OnPosition(object sender, RobotPositionEventArgs e)
        {
            double mm = this.differentialModel.StepToMm(e.Radius);

            this.SetRobotPos((float)mm, (float)e.Alpha);

            this.SendRobotPos((float)e.Radius, (float)e.Alpha);

            this.AddStatus(String.Format("Position: A:{0:F3} D:{1:F3}", e.Alpha, mm), Color.White);

        }

        #endregion

        #region Database

        /// <summary>
        /// Commit robot position to DB.
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="alpha"></param>
        private void SendRobotPos(float radius, float alpha)
        {
            // Calculate position.
            PointF point = PolarConversion.PolarToCartesian(radius, alpha);

            // Create position.
            Position decartRobotPose = new Position();
            decartRobotPose.Point.Unit = Scales.Steps;
            decartRobotPose.Point.X = point.X;
            decartRobotPose.Point.Y = point.Y;
            decartRobotPose.Orientation.Unit = Radial.Degree;
            decartRobotPose.Orientation.C = alpha;

            // Create sensor.
            RobotPosition sensor = new RobotPosition();
            sensor.Position = decartRobotPose;
            sensor.Name = "Robot position";
            sensor.Descripotion = "Actual robot position.";
        }

        private void SendRobotUltrasonicSensor(float radius, float alpha)
        {

        }

        #endregion

        #region Private

        /// <summary>
        /// Search serial port.
        /// </summary>
        private void SearchForPorts()
        {
            this.tsmiPorts.DropDown.Items.Clear();

            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();

            if (portNames.Length == 0)
            {
                return;
            }

            foreach (string item in portNames)
            {
                //store the each retrieved available prot names into the MenuItems...
                this.tsmiPorts.DropDown.Items.Add(item);
            }

            foreach (ToolStripMenuItem item in this.tsmiPorts.DropDown.Items)
            {
                item.Click += tmsiPorts_Click;
                item.Enabled = true;
                item.Checked = false;
            }
        }

        /// <summary>
        /// Set robot position.
        /// </summary>
        /// <param name="text">Position</param>
        private void SetRobotPos(float distance, float alpha)
        {
            string text = String.Format("Position\r\nDistance: {0:F3}\r\nAlpha: {1:F3}", distance, alpha);

            // + Environment.NewLine
            if (this.lblRobotPosition.InvokeRequired)
            {
                this.lblRobotPosition.BeginInvoke((MethodInvoker)delegate()
                {
                    this.lblRobotPosition.Text = text;
                });
            }
            else
            {
                this.lblRobotPosition.Text = text;
            }
        }

        /// <summary>
        /// Set left sensor progres bar.
        /// </summary>
        /// <param name="value"></param>
        private void SetLeftSensor(int value)
        {
            if (this.prbLeftSensor.InvokeRequired)
            {
                this.prbLeftSensor.BeginInvoke((MethodInvoker)delegate()
                {
                    this.prbLeftSensor.Value = value;
                });
            }
            else
            {
                this.prbLeftSensor.Value = value;
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
        /// Add a status to the status list..
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textColor"></param>
        private void AddStatus(string text, Color textColor)
        {
            string infoLine = String.Format("{0} -> {1}", this.GetDateTime(), text);
            infoLine += Environment.NewLine;

            if (this.txtState.InvokeRequired)
            {
                this.txtState.BeginInvoke((MethodInvoker)delegate()
                {
                    this.txtState.AppendText(infoLine);
                    this.txtState.BackColor = textColor;
                });
            }
            else
            {
                this.txtState.AppendText(infoLine);
                this.txtState.BackColor = textColor;
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
            lock (this.syncLockCapture)
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
                    this.ipCamera = new IpCamera(uri);
                    //this.ipCamera.Torch = true;
                }

                if (uri != this.ipCamera.URI)
                {
                    this.ipCamera = new IpCamera(uri);
                    //this.ipCamera.Torch = true;
                }

                if (this.capturedImage != null) this.capturedImage.Dispose();

                try
                {
                    this.capturedImage = this.ipCamera.Capture();
                    this.pbGlyph.Image = AppUtils.FitImage(this.capturedImage, this.pbGlyph.Size);
                }
                catch (Exception exception)
                {
                    this.AddStatus(exception.ToString() + Environment.NewLine, Color.White);
                }
            }
        }

        /// <summary>
        /// Unit test of LORA data base.
        /// </summary>
        private void TastDatabase()
        {
            try
            {
                // Database test.

                #region Distance

                // Create sensor.
                Distance dstSens = new Distance();

                // Date and time of mesurements.
                dstSens.DateTime = DateTime.Now;

                // Name of the sensor.
                dstSens.Name = "Sensor 1";

                // Description.
                dstSens.Descripotion = "Ultrasonic sensor on the top of the robot.";

                // Positional data.
                dstSens.Position.Point.Unit = Scales.MM;
                dstSens.Position.Point.X = 1.0f;
                dstSens.Position.Point.Y = 2.0f;
                dstSens.Position.Point.Z = 3.0f;
                dstSens.Position.Orientation.Unit = Radial.Degree;
                dstSens.Position.Orientation.A = 4.0f;
                dstSens.Position.Orientation.B = 5.0f;
                dstSens.Position.Orientation.C = 6.0f;

                // Mesurements units.
                dstSens.Unit = Scales.MM;
                dstSens.Value = 100.0f;
                dstSens.MaxValue = 50.0f;
                dstSens.MinValue = 2000.0f;

                #endregion

                #region Humidity

                // Create sensor.
                Humidity humSens = new Humidity();

                // Date and time of mesurements.
                humSens.DateTime = DateTime.Now;

                // Name of the sensor.
                humSens.Name = "Sensor 2";

                // Description.
                humSens.Descripotion = "Humidite sensor on the back of the robot.";

                // Positional data.
                humSens.Position.Point.Unit = Scales.MM;
                humSens.Position.Point.X = 1.0f;
                humSens.Position.Point.Y = 2.0f;
                humSens.Position.Point.Z = 3.0f;
                humSens.Position.Orientation.Unit = Radial.Degree;
                humSens.Position.Orientation.A = 4.0f;
                humSens.Position.Orientation.B = 5.0f;
                humSens.Position.Orientation.C = 6.0f;

                // Mesurements units.
                humSens.Unit = Scales.MM;
                humSens.Value = 50.0f;
                humSens.MaxValue = 100.0f;
                humSens.MinValue = 0.0f;

                #endregion

                #region Presure

                // Create sensor.
                Pressure presSens = new Pressure();

                // Date and time of mesurements.
                presSens.DateTime = DateTime.Now;

                // Name of the sensor.
                presSens.Name = "Sensor 3";

                // Description.
                presSens.Descripotion = "Air tank sensor.";

                // Positional data.
                presSens.Position.Point.Unit = Scales.MM;
                presSens.Position.Point.X = 1.0f;
                presSens.Position.Point.Y = 2.0f;
                presSens.Position.Point.Z = 3.0f;
                presSens.Position.Orientation.Unit = Radial.Degree;
                presSens.Position.Orientation.A = 4.0f;
                presSens.Position.Orientation.B = 5.0f;
                presSens.Position.Orientation.C = 6.0f;

                // Mesurements units.
                presSens.Unit = Pressures.Bar;
                presSens.Value = 5.0f;
                presSens.MaxValue = 10.0f;
                presSens.MinValue = -1.0f;

                #endregion

                #region Temperature

                // Create sensor.
                Temperature tempSens = new Temperature();

                // Date and time of mesurements.
                tempSens.DateTime = DateTime.Now;

                // Name of the sensor.
                tempSens.Name = "Sensor 4";

                // Description.
                tempSens.Descripotion = "Left motor temperature sensor.";

                // Positional data.
                tempSens.Position.Point.Unit = Scales.MM;
                tempSens.Position.Point.X = 1.0f;
                tempSens.Position.Point.Y = 2.0f;
                tempSens.Position.Point.Z = 3.0f;
                tempSens.Position.Orientation.Unit = Radial.Degree;
                tempSens.Position.Orientation.A = 4.0f;
                tempSens.Position.Orientation.B = 5.0f;
                tempSens.Position.Orientation.C = 6.0f;

                // Mesurements units.
                tempSens.Unit = TemperatureScale.Celsius;
                tempSens.Value = 20.0f;
                tempSens.MaxValue = 85.0f;
                tempSens.MinValue = -20.0f;

                #endregion

                #region RobotPosition

                // Create sensor.
                RobotPosition robotPos = new RobotPosition();

                // Date and time of mesurements.
                humSens.DateTime = DateTime.Now;

                // Name of the sensor.
                humSens.Name = "Robot Position";

                // Description.
                humSens.Descripotion = "Actual robot position.";

                // Positional data.
                humSens.Position.Point.Unit = Scales.MM;
                humSens.Position.Point.X = 1.0f;
                humSens.Position.Point.Y = 2.0f;
                humSens.Position.Point.Z = 3.0f;
                humSens.Position.Orientation.Unit = Radial.Degree;
                humSens.Position.Orientation.A = 4.0f;
                humSens.Position.Orientation.B = 5.0f;
                humSens.Position.Orientation.C = 6.0f;

                // Mesurements units.
                humSens.Unit = Scales.MM;
                humSens.Value = 50.0f;
                humSens.MaxValue = 100.0f;
                humSens.MinValue = 0.0f;

                #endregion

                #region Left Stepper motor

                // Create sensor.
                StepperMotor lStepAct = new StepperMotor();

                // Date and time of mesurements.
                lStepAct.DateTime = DateTime.Now;

                // Name of the sensor.
                lStepAct.Name = "Left motor";

                // Description.
                lStepAct.Descripotion = "Left of the robot motor.";

                // Positional data.
                lStepAct.Position.Point.Unit = Scales.MM;
                lStepAct.Position.Point.X = 1.0f;
                lStepAct.Position.Point.Y = 2.0f;
                lStepAct.Position.Point.Z = 3.0f;
                lStepAct.Position.Orientation.Unit = Radial.Degree;
                lStepAct.Position.Orientation.A = 4.0f;
                lStepAct.Position.Orientation.B = 5.0f;
                lStepAct.Position.Orientation.C = 6.0f;

                // Mesurements units.
                lStepAct.Unit = Scales.Steps;
                lStepAct.Value = 20.0f;
                lStepAct.MaxValue = 200.0f;
                lStepAct.MinValue = 0.0f;
                lStepAct.Acceleration = 50;
                lStepAct.Jerk = 70;

                #endregion

                #region Right Stepper motor

                // Create sensor.
                StepperMotor rStepAct = new StepperMotor();

                // Date and time of mesurements.
                rStepAct.DateTime = DateTime.Now;

                // Name of the sensor.
                rStepAct.Name = "Left motor";

                // Description.
                rStepAct.Descripotion = "Left of the robot motor.";

                // Positional data.
                rStepAct.Position.Point.Unit = Scales.MM;
                rStepAct.Position.Point.X = 1.0f;
                rStepAct.Position.Point.Y = 2.0f;
                rStepAct.Position.Point.Z = 3.0f;
                rStepAct.Position.Orientation.Unit = Radial.Degree;
                rStepAct.Position.Orientation.A = 4.0f;
                rStepAct.Position.Orientation.B = 5.0f;
                rStepAct.Position.Orientation.C = 6.0f;

                // Mesurements units.
                rStepAct.Unit = Scales.Steps;
                rStepAct.Value = 20.0f;
                rStepAct.MaxValue = 200.0f;
                rStepAct.MinValue = 0.0f;
                rStepAct.Acceleration = 50;
                rStepAct.Jerk = 70;

                #endregion

                // Send data to the DB.
                //this.database.CommitDevice(dstSens);
                //this.database.CommitDevice(humSens);
                //this.database.CommitDevice(presSens);
                //this.database.CommitDevice(tempSens);
                //this.database.CommitDevice(robotPos);
                //this.database.CommitDevice(lStepAct);
                //this.database.CommitDevice(rStepAct);
            }
            catch (Exception exception)
            {
                this.AddStatus(exception.ToString(), Color.White);
            }
        }
        
        #endregion

        private void talkToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Initialize a new instance of the SpeechSynthesizer.
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {

                // Output information about all of the installed voices. 
                Console.WriteLine("Installed voices -");
                var voices = synth.GetInstalledVoices();
                foreach (InstalledVoice voice in voices)
                {
                    VoiceInfo info = voice.VoiceInfo;
                    Console.WriteLine(" Voice Name: " + info.Name);
                }

                // Configure the audio output. 
                synth.SetOutputToDefaultAudioDevice();
                synth.SelectVoice("Microsoft Zira Desktop");

                // Speak a string.
                synth.Speak("The robot are seeing a wall.");
            }
        }
    }
}
