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
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Speech.Synthesis;

using KarelV1.Util;
using KarelV1.Settings;
using KarelV1.VisualisationManager;

using KarelV1Lib;
using KarelV1Lib.Events;
using KarelV1Lib.Adapters;
using KarelV1Lib.Data;
using KarelV1Lib.Utils;

using IPWebcam;

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
        /// Differential control model.
        /// </summary>
        private DifferentialModel differentialModel;

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
        /// Sensor data.
        /// </summary>
        private DistanceSensorsList sonarsData = new DistanceSensorsList();

        /// <summary>
        /// IR sensor model.
        /// </summary>
        private GP2Y0A21YK irSensor = new GP2Y0A21YK(5, 1024);

        /// <summary>
        /// Robot visualizer.
        /// </summary>
        private RobotVisualiser visuliser;

        /// <summary>
        /// Program controller.
        /// </summary>
        private ProgramController programController = new ProgramController();

        private bool frontBit = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // The differential controlling model.
            this.differentialModel = new DifferentialModel(
                Properties.Settings.Default.SteppsCount,
                Properties.Settings.Default.StepperPostScaler,
                Properties.Settings.Default.DiameterOfWheel,
                Properties.Settings.Default.DistanceBetweenWheels);

            // Setup visualizer.
            this.visuliser = new RobotVisualiser(this.pbTerrain);
            this.visuliser.StepsPerSecond = Properties.Settings.Default.StepsPerSecond;

            // Setup program controll.
            this.programController.OnExecutionIndexCanged += ProgramController_OnExecutionIndexCanged;
            this.programController.OnFinish += ProgramController_OnFinish;
            this.programController.OnRuning += ProgramController_OnRuning;
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

            this.SetupSonarChart();

            this.SetupScaleComboBox();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DisconnectFromRobot();
        }

        #endregion

        #region Buttons

        private void btnForward_Click(object sender, EventArgs e)
        {
            this.MoveForward();
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

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

                this.robot.GetDistanceSensors(position);
            }
            else
            {
                this.robot.GetDistanceSensors();
            }
        }

        private void btnReadSonar_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            int position = 0;
            if (int.TryParse(this.tbSensorPosition.Text, out position))
            {
                if (position > 180 || position < 0)
                {
                    return;
                }

                this.robot.GetDistanceSensors(position);
            }
            else
            {
                this.robot.GetDistanceSensors();
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

        private void btnRunProgram_Click(object sender, EventArgs e)
        {
            this.RunProgram();
        }

        private void btnResumeProgram_Click(object sender, EventArgs e)
        {
            this.ResumeProgram();
        }

        private void btnStopProgram_Click(object sender, EventArgs e)
        {
            this.StopProgram();
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

        private void SetupSonarChart()
        {
            // setup the X grid
            //crtUltrasinicSensor.ChartAreas[0].AxisX.LabelStyle.Format = "F3";
            this.crtUltrasinicSensor.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            this.crtUltrasinicSensor.ChartAreas[0].AxisX.MajorGrid.Interval = 90;
            this.crtUltrasinicSensor.ChartAreas[0].AxisX.Crossing = -90;
            this.crtUltrasinicSensor.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            this.crtUltrasinicSensor.ChartAreas[0].AxisX.Minimum = 0.0f;
            this.crtUltrasinicSensor.ChartAreas[0].AxisX.Maximum = 360.0f;
            // setup the Y grid
            this.crtUltrasinicSensor.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            this.crtUltrasinicSensor.ChartAreas[0].Area3DStyle.Enable3D = false;
            this.crtUltrasinicSensor.ChartAreas[0].AxisY.ScaleView.Zoomable = true;

            // ==== Ultrasonic distance ====
            this.crtUltrasinicSensor.Series[0].ChartType = SeriesChartType.Polar;
            this.crtUltrasinicSensor.Series[0].XValueType = ChartValueType.Double;
            this.crtUltrasinicSensor.Series[0].IsXValueIndexed = true;
            this.crtUltrasinicSensor.Series[0].Name = "Ultrasonic Sensor";
            this.crtUltrasinicSensor.Series[0]["PolarDrawingStyle"] = "Line";
            this.crtUltrasinicSensor.Series[0].Color = Color.Blue;

            // ==== Infrared sensor distance ====
            this.crtUltrasinicSensor.Series[1].ChartType = SeriesChartType.Polar;
            this.crtUltrasinicSensor.Series[1].XValueType = ChartValueType.Double;
            this.crtUltrasinicSensor.Series[1].IsXValueIndexed = true;
            this.crtUltrasinicSensor.Series[1].Name = "Infrared Sensor";
            this.crtUltrasinicSensor.Series[1]["PolarDrawingStyle"] = "Line";
            this.crtUltrasinicSensor.Series[1].Color = Color.Red;

            // Clear all series.
            foreach (var series in this.crtUltrasinicSensor.Series)
            {
                series.Points.Clear();
            }

            // Clear sonar data.
            this.sonarsData.Clear();

            // Add cleared sonar data.
            for (int index = 0; index <= this.crtUltrasinicSensor.ChartAreas[0].AxisX.Maximum; index++)
            {
                this.sonarsData.Add(new DistanceSensors(index, 0.0, 0.0));
            }

            // Draw cleared data.
            this.UpdateSonarChart(this.sonarsData);
        }

        private void UpdateSonarChart(DistanceSensorsList data)
        {
            if (this.crtUltrasinicSensor.InvokeRequired)
            {
                this.crtUltrasinicSensor.BeginInvoke((MethodInvoker)delegate()
                {
                    this.crtUltrasinicSensor.Series[0].Points.DataBindXY(data.GetPositions(), data.GetUltrasonic());
                    this.crtUltrasinicSensor.Series[1].Points.DataBindXY(data.GetPositions(), data.GetInfrared());
                });
            }
            else
            {
                this.crtUltrasinicSensor.Series[0].Points.DataBindXY(data.GetPositions(), data.GetUltrasonic());
                this.crtUltrasinicSensor.Series[1].Points.DataBindXY(data.GetPositions(), data.GetInfrared());
            }
        }

        #endregion

        #region Menu Item

        private void tsmiSettings_Click(object sender, EventArgs e)
        {
            using (SettingsForm sf = new SettingsForm())
            {
                sf.ShowDialog();
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tmsiPorts_Click(object sender, EventArgs e)
        {
            this.DisconnectFromRobot();
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
            this.DisconnectFromRobot();
        }

        private void tsmiSaveProgram_Click(object sender, EventArgs e)
        {
            this.SaveProgram();
        }

        private void tsmiLoadProgram_Click(object sender, EventArgs e)
        {
            this.LoadProgram();
        }

        private void tsmiRunProgram_Click(object sender, EventArgs e)
        {
            this.RunProgram();
        }

        private void tsmiResumeProgram_Click(object sender, EventArgs e)
        {
            this.ResumeProgram();
        }

        private void tsmiStopProgram_Click(object sender, EventArgs e)
        {
            this.StopProgram();
        }


        private void tsmiClearAll_Click(object sender, EventArgs e)
        {
            this.SetupSonarChart();
        }

        private void tsmiAsCSV_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                sfd.FileName = AppUtils.GetDateTime() + ".CSV";

                if (!string.IsNullOrEmpty(sfd.FileName))
                {
                    int us = 0;
                    int ir = 1;

                    double usLen = this.crtUltrasinicSensor.Series[us].Points.Count;
                    double irLen = this.crtUltrasinicSensor.Series[ir].Points.Count;

                    DistanceSensorsList sonarData = new DistanceSensorsList();

                    if (usLen > 0 && irLen > 0 && usLen == irLen)
                    {
                        for (int angle = 0; angle < usLen; angle++)
                        {
                            sonarData.Add(
                                new DistanceSensors(
                                    angle,
                                    this.crtUltrasinicSensor.Series[us].Points[angle].YValues[0],
                                    this.crtUltrasinicSensor.Series[ir].Points[angle].YValues[0]));
                        }
                    }

                    DistanceSensorsList.SaveCSV(sonarData, sfd.FileName);
                }
            }
        }

        private void tsmiAsXML_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "XML (*.xml)|*.xml";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                sfd.FileName = AppUtils.GetDateTime() + ".XML";

                if (!string.IsNullOrEmpty(sfd.FileName))
                {
                    int us = 0;
                    int ir = 1;

                    double usLen = this.crtUltrasinicSensor.Series[us].Points.Count;
                    double irLen = this.crtUltrasinicSensor.Series[ir].Points.Count;

                    DistanceSensorsList sonarData = new DistanceSensorsList();

                    if (usLen > 0 && irLen > 0 && usLen == irLen)
                    {
                        for (int angle = 0; angle < usLen; angle++)
                        {
                            sonarData.Add(
                                new DistanceSensors(
                                    angle,
                                    this.crtUltrasinicSensor.Series[us].Points[angle].YValues[0],
                                    this.crtUltrasinicSensor.Series[ir].Points[angle].YValues[0]));
                        }
                    }

                    DistanceSensorsList.SaveXML(sonarData, sfd.FileName);
                }
            }
        }


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


        #endregion

        #region Robot

        private void ConnectToRobotViaSerial()
        {
            try
            {
                this.robot = new KarelV1Lib.KarelV1(new SerialAdapter(this.robotSerialPortName));
                this.robot.OnDistanceSensors += myRobot_OnDistanceSensors;
                this.robot.OnGreatingsMessage += myRobot_OnGreatingsMessage;
                this.robot.OnPosition += myRobot_OnPosition;
                this.robot.OnSensors += Robot_OnSensors;
                this.robot.Connect();
                this.robot.Reset();
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

                this.robot.OnDistanceSensors += myRobot_OnDistanceSensors;
                this.robot.OnGreatingsMessage += myRobot_OnGreatingsMessage;
                this.robot.OnPosition += myRobot_OnPosition;
                this.robot.OnSensors += Robot_OnSensors;
                this.robot.Connect();
                this.robot.Reset();
            }
            catch (Exception exception)
            {
                this.AddStatus(exception.ToString(), Color.White);
            }
        }

        private void DisconnectFromRobot()
        {
            try
            {
                if (this.robot != null && this.robot.IsConnected)
                {
                    this.robot.Disconnect();

                    if (this.robot.IsConnected)
                    {
                        this.lblIsConnected.Text = String.Format("Connected: {0}:{1}", Properties.Settings.Default.BrokerHost, Properties.Settings.Default.BrokerPort);
                    }
                    else
                    {
                        this.lblIsConnected.Text = "Not Connected";
                    }
                }
            }
            catch (Exception exception)
            {
                this.AddStatus(exception.ToString(), Color.White);
            }
        }

        private void MoveForward()
        {
            double distance = 0.0D;
            if(!double.TryParse(this.tbDistande.Text, out distance))
            {
                // TODO: Show message incorrect distance.
                return;
            }

            Position rp = new Position(distance, 0, Properties.Settings.Default.StepsPerSecond);

            // Move the robot.
            if (this.robot == null || !this.robot.IsConnected) return;
            int stepsD = this.differentialModel.MmToStep(rp.Distance);
            this.robot.TranslateSteps(stepsD);
        }

        private void MoveBackward()
        {
            double distance = 0.0D;
            if (!double.TryParse(this.tbDistande.Text, out distance))
            {
                // TODO: Show message incorrect distance.
                return;
            }

            Position rp = new Position(-distance, 0, Properties.Settings.Default.StepsPerSecond);

            // Move the robot.
            if (this.robot == null || !this.robot.IsConnected) return;
            int stepsD = this.differentialModel.MmToStep(rp.Distance);
            this.robot.TranslateSteps(stepsD);
        }

        private void MoveLeft()
        {
            double phase = 0.0D;
            if (!double.TryParse(this.tbPhase.Text, out phase))
            {
                // TODO: Show message incorrect distance.
                return;
            }

            Position rp = new Position(0, -phase, Properties.Settings.Default.StepsPerSecond);

            // Move the robot.
            if (this.robot == null || !this.robot.IsConnected) return;
            int stepsR = this.differentialModel.MmToStep(rp.Phase);
            this.robot.RotateStaps(stepsR);
        }

        private void MoveRight()
        {
            double phase = 0.0D;
            if (!double.TryParse(this.tbPhase.Text, out phase))
            {
                // TODO: Show message incorrect distance.
                return;
            }

            Position rp = new Position(0, phase, Properties.Settings.Default.StepsPerSecond);

            // Move the robot.
            if (this.robot == null || !this.robot.IsConnected) return;
            int stepsR = this.differentialModel.MmToStep(rp.Phase);
            this.robot.RotateStaps(stepsR);
        }

        private void GoToPosition(Position position)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            int stepsR = this.differentialModel.MmToStep(position.Phase);
            int msR = (int)((stepsR / position.StepsPerSecond) * 1000) + 100;
            this.robot.RotateStaps(stepsR);
            Thread.Sleep(msR);

            int stepsD = this.differentialModel.MmToStep(position.Distance);
            int msD = (int)((stepsD / position.StepsPerSecond) * 1000) + 100;
            this.robot.TranslateSteps(stepsD);
            Thread.Sleep(msD);
        }

        private void myRobot_OnGreatingsMessage(object sender, StringEventArgs e)
        {
            this.Talk(e.Message);
        }

        private void myRobot_OnDistanceSensors(object sender, DistanceSensorsEventArgs e)
        {
            this.AddStatus(String.Format("US Sensor: {0}[deg] {1}[us] {2}[ADC]", e.DistanceSensors.Position, e.DistanceSensors.UltraSonic, e.DistanceSensors.Infrared), Color.White);

            //MetricScale Scale = (MetricScale)this.cbMetric.SelectedItem;
            // TODO: Add scale.
            // TODO: Class HSR04.
            double us = e.DistanceSensors.UltraSonic / 58.0F;
            if (us > 330.0F) us = 330.0F;

            double ir = irSensor.Convert(e.DistanceSensors.Infrared); // AppUtils.Map(e.InfraRedADCValue, 0, 1023, 80, 10);
            if (ir > 80.0F) ir = 80.0F;
            //double ir = AppUtils.Map(e.InfraRedADCValue, 0, 1023, 80, 10);

            sonarsData[(int)e.DistanceSensors.Position] = new DistanceSensors(e.DistanceSensors.Position, us, ir);

            this.UpdateSonarChart(sonarsData);
        }

        private void myRobot_OnPosition(object sender, PositionEventArgs e)
        {
            if (this.robot.IsMoving)
            {
                // Extract and scale positional data.
                double distanceMm = this.differentialModel.StepToMm((int)e.Position.Distance);
                double alphaDeg = this.differentialModel.StepToMm((int)e.Position.Phase);

                // Recreate data.
                Position scaledPosition = new Position(distanceMm, alphaDeg, Properties.Settings.Default.StepsPerSecond);

                // Set the visualization.
                this.visuliser.SetRobotPosition(scaledPosition);

                // Add status.
                this.AddStatus(String.Format("Position: P:{0:F3} D:{1:F3}", e.Position.Phase, e.Position.Distance), Color.White);
            }
        }

        private void Robot_OnSensors(object sender, SensorsEventArgs e)
        {
            if(e.Sensors.Front == 0 && this.frontBit == false)
            {
                this.frontBit = true;
                this.Talk("The robot are seeing a wall.");
            }
            else if (e.Sensors.Front == 1)
            {
                this.frontBit = false;
            }
        }

        #endregion

        #region Program Controller

        private void ProgramController_OnExecutionIndexCanged(object sender, IntEventArgs e)
        {
            Position rp = this.programController.Commands[e.Value];
            this.visuliser.SetCurrentPoint(e.Value);
            this.GoToPosition(rp);
        }
        
        private void ProgramController_OnFinish(object sender, EventArgs e)
        {
            this.visuliser.ResetCurrentPoint();
            this.visuliser.LockEditing = false;
        }

        private void ProgramController_OnRuning(object sender, EventArgs e)
        {
            this.visuliser.LockEditing = true;
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
                //store the each retrieved available port names into the MenuItems...
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
        /// Add a status to the status list..
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textColor"></param>
        private void AddStatus(string text, Color textColor)
        {
            string infoLine = String.Format("{0} -> {1}", AppUtils.GetDateTime(), text);
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
        /// Capture the navigation video camera.
        /// </summary>
        private void CaptureCamera()
        {
            lock (this.syncLockCapture)
            {
                // IP Camera:    http://http://192.168.1.1/cgi-bin/image.jpg
                // Mobile phone: http://192.168.0.104:8080/photoaf.jpg

                Uri uri;
                bool isValidUri = Uri.TryCreate(Properties.Settings.Default.CameraUri, UriKind.Absolute, out uri);
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
                    this.ipCamera.EnableTorch = Properties.Settings.Default.CameraTorch;
                    this.capturedImage = this.ipCamera.CaptureFocused();
                    this.visuliser.SetBackgroundImage(capturedImage);
                }
                catch (Exception exception)
                {
                    this.AddStatus(exception.ToString() + Environment.NewLine, Color.White);
                }
            }
        }

        /// <summary>
        /// Setup the scale combo box.
        /// </summary>
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

        private void SaveProgram()
        {
            Positions trajectory = this.visuliser.Trajectory;
            if (trajectory == null) { return; }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML (*.xml)|*.xml";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            sfd.FileName = AppUtils.GetDateTime() + ".XML";
            //sfd.Multiselect = false;

            // Show the dialog and get result.
            DialogResult result = sfd.ShowDialog();
            
            if (result == DialogResult.OK) // Test result.
            {
                PositionsStore.Save(trajectory, sfd.FileName);
            }
        }

        private void LoadProgram()
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog ofd = new OpenFileDialog();

            // Set filter options and filter index.
            ofd.Filter = "XML Files (.xml)|*.xml|All Files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;

            // Show the dialog and get result.
            DialogResult result = ofd.ShowDialog();

            if (result == DialogResult.OK) // Test result.
            {
                string path = ofd.FileName;
                Positions trajectory = PositionsStore.Load(path);

                if (trajectory != null && trajectory.Count > 0)
                {
                    this.visuliser.Trajectory = trajectory;
                }
            }
        }

        private void RunProgram()
        {
            if (!this.programController.IsRuning)
            {
                this.programController.Commands = this.visuliser.Trajectory;
                this.programController.RunProgram();
            }
        }

        private void StopProgram()
        {
            if (this.programController.IsRuning)
            {
                this.programController.StopProgram();
            }
        }

        private void ResumeProgram()
        {
            if (!this.programController.IsRuning)
            {
                this.programController.ResumeProgram();
            }
        }
        
        /// <summary>
        /// Robot can talk via computer voices.
        /// </summary>
        /// <param name="text"></param>
        private void Talk(string text)
        {
            Thread worker = new Thread(
                new ThreadStart(
                    delegate ()
            {
                // Initialize a new instance of the SpeechSynthesizer.
                using (SpeechSynthesizer synth = new SpeechSynthesizer())
                {

                    // Output information about all of the installed voices. 
                    //Console.WriteLine("Installed voices -");

                    var voices = synth.GetInstalledVoices();
                    foreach (InstalledVoice voice in voices)
                    {
                        VoiceInfo info = voice.VoiceInfo;
                        //Console.WriteLine(" Voice Name: " + info.Name);
                    }

                    // Configure the audio output. 
                    synth.SetOutputToDefaultAudioDevice();
                    synth.SelectVoice("Microsoft Zira Desktop");

                    // Speak a string.
                    synth.Speak(text);
                }
            }));

            // Start the worker thread.
            worker.Start();
        }

        #endregion

        #region Radio Buttons

        private void rbRecord_CheckedChanged(object sender, EventArgs e)
        {
            this.visuliser.CaptureMode = CaptureMode.RecordMotion;
        }

        private void rbDefinePoints_CheckedChanged(object sender, EventArgs e)
        {
            this.visuliser.CaptureMode = CaptureMode.DefinePoints;
        }


        #endregion

    }
}
