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

namespace DiO_CS_KarelV1_TEST
{
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

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Private

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
                this.myRobot.Connect();
                this.myRobot.Reset();
            }
            catch (Exception exception)
            {
                this.txtState.Text = exception.Message;
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            // + "\r\n"
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

        private string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy.MM.dd/HH:mm:ss.fff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        #endregion

        #region Robot

        private void myRobot_Message(object sender, StringEventArgs e)
        {

            string infoLine = String.Format("{0} -> {1}", this.GetDateTime(), e.Message);

            this.AddStatus(infoLine + "\r\n", Color.White);
        }

        private void myRobot_GreatingsMessage(object sender, StringEventArgs e)
        {

            string infoLine = String.Format("{0} -> {1}", this.GetDateTime(), e.Message);

            this.AddStatus(infoLine + "\r\n", Color.White);
        }

        private void myRobot_Stoped(object sender, EventArgs e)
        {

            string infoLine = String.Format("{0} -> Stoped", this.GetDateTime());

            this.AddStatus(infoLine + "\r\n", Color.White);
        }

        private void myRobot_UltraSonicSensor(object sender, UltraSonicSensorEventArgs e)
        {
            string infoLine = String.Format("{0} -> Ultrasonic Sensor: {1} {2}", this.GetDateTime(), e.Position, e.Distance);
            this.AddStatus(infoLine + "\r\n", Color.White);

            if (this.pbSensorView.InvokeRequired)
            {
                this.pbSensorView.BeginInvoke((MethodInvoker)delegate()
                {
                    this.sensorData[e.Position] = e.Distance;
                    //this.myDiagram.SetData(this.sensorData);
                    this.myDiagram.SetData(e.Position, e.Distance);

                    for (int index = 0; index < this.sensorData.Length; index++)
                    {
                        if (maxDistanceValue < this.sensorData[index])
                        {
                            maxDistanceValue = this.sensorData[index];
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

                    if (maxDistanceValue < this.sensorData[index])
                    {
                        maxDistanceValue = this.sensorData[index];
                        this.maxDistanceIndex = index;
                    }
                }
                this.pbSensorView.Refresh();
            }
        }

        private void myRobot_Sensors(object sender, SensorsEventArgs e)
        {
            string infoLine = String.Format("{0} -> Sensors: {1} {2}", this.GetDateTime(), e.Left, e.Right);
            this.AddStatus(infoLine + "\r\n", Color.White);
        }

        #endregion

        #region MainForm

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Double buffer optimization.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

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
            if (this.myRobot != null)
            {
                this.myRobot.Move(100);
            }
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null)
            {
                this.myRobot.Move(-100);
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null)
            {
                this.myRobot.Rotate(-100);
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null)
            {
                this.myRobot.Rotate(100);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null)
            {
                this.myRobot.Stop();
            }
        }

        private void btnGetSensors_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null)
            {
                this.myRobot.GetSensors();
            }
        }

        private void btnGetUltrasonic_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null)
            {
                int position = 0;

                if (int.TryParse(this.textBox1.Text, out position))
                {
                    this.myRobot.GetUltraSonic(position);
                }
                else
                {
                    for (int index = 0; index < this.sensorData.Length; index++)
                    {
                        maxDistanceValue = this.sensorData[index] = 0.0d;
                    }

                    this.maxDistanceValue = -100;
                    this.maxDistanceIndex = 0;

                    this.myRobot.GetUltraSonic();
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (this.myRobot != null)
            {
                this.myRobot.Reset();
            }
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
                item.Checked = true;
                //this.lblIsConnected.Text = String.Format("Connected@{0}", this.robotSerialPortName);
            }
            else
            {
                item.Checked = false;
                //this.lblIsConnected.Text = "Not Connected";
            }
        }

        private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SearchForPorts();
        }
        
        #endregion

    }
}
