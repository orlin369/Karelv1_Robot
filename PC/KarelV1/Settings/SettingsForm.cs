using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KarelV1.Settings
{
    public partial class SettingsForm : Form
    {

        #region Constructor

        public SettingsForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Settings Form

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.LoadFields();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveFields();
        }

        #endregion

        #region Private Methods

        private void LoadFields()
        {
            this.tbSteppsCount.Text = Properties.Settings.Default.SteppsCount.ToString();
            this.tbStepperPostScaler.Text = Properties.Settings.Default.StepperPostScaler.ToString();
            this.tbDiameterOfWheel.Text = Properties.Settings.Default.DiameterOfWheel.ToString();
            this.tbDistanceBetweenWheels.Text = Properties.Settings.Default.DistanceBetweenWheels.ToString();
            this.tbStepsPerSecond.Text = Properties.Settings.Default.StepsPerSecond.ToString();
            this.tbXScale.Text = Properties.Settings.Default.XScale.ToString();
            this.tbYScale.Text = Properties.Settings.Default.YScale.ToString();

            this.tbBrokerDomain.Text = Properties.Settings.Default.BrokerHost;
            this.tbBrokerPort.Text = Properties.Settings.Default.BrokerPort.ToString();
            this.tbInputTopic.Text = Properties.Settings.Default.MqttInputTopic;
            this.tbOutputTopic.Text = Properties.Settings.Default.MqttOutputTopic;

            this.tbCameraUri.Text = Properties.Settings.Default.CameraUri;
            this.cbTorch.Checked = Properties.Settings.Default.CameraTorch;
        }

        private void SaveFields()
        {
            try
            {

                #region Mechanical Properties

                int stepsCount = 0;
                if (int.TryParse(this.tbSteppsCount.Text.Trim(), out stepsCount))
                {
                    if (stepsCount < 0 || stepsCount > 100000)
                    {
                        MessageBox.Show("Invalid steps count. [0 - 100000]", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    Properties.Settings.Default.SteppsCount = stepsCount;
                }
                else
                {
                    MessageBox.Show("Invalid steps count.", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                int stepperPostScaler = 0;
                if (int.TryParse(this.tbStepperPostScaler.Text.Trim(), out stepperPostScaler))
                {
                    if (stepperPostScaler < 0 || stepperPostScaler > 100000)
                    {
                        MessageBox.Show("Invalid stepper post scaler count. [0 - 100000]", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    Properties.Settings.Default.StepperPostScaler = stepperPostScaler;
                }
                else
                {
                    MessageBox.Show("Invalid stepper post scaler count.", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                double diameterOfWheel = 0;
                if (double.TryParse(this.tbDiameterOfWheel.Text.Trim(), out diameterOfWheel))
                {
                    if (diameterOfWheel < 0)
                    {
                        MessageBox.Show("Invalid diameter of the wheel. [x > 0]", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    Properties.Settings.Default.DiameterOfWheel = diameterOfWheel;
                }
                else
                {
                    MessageBox.Show("Invalid diameter of the wheel.", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                double distanceBetweenWheels = 0;
                if (double.TryParse(this.tbDistanceBetweenWheels.Text.Trim(), out distanceBetweenWheels))
                {
                    if (distanceBetweenWheels < 0)
                    {
                        MessageBox.Show("Invalid distance between the wheels. [x > 0]", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    Properties.Settings.Default.DistanceBetweenWheels = distanceBetweenWheels;
                }
                else
                {
                    MessageBox.Show("Invalid distance between the wheels.", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                double stepsPerSecond = 0;
                if (double.TryParse(this.tbStepsPerSecond.Text.Trim(), out stepsPerSecond))
                {
                    if (stepsPerSecond < 0)
                    {
                        MessageBox.Show("Invalid steps per second. [x > 0]", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    Properties.Settings.Default.StepsPerSecond = stepsPerSecond;
                }
                else
                {
                    MessageBox.Show("Invalid steps per second.", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                #endregion

                #region MQTT Settings

                int borkerPort;
                if (int.TryParse(this.tbBrokerPort.Text.Trim(), out borkerPort))
                {
                    if (borkerPort < 0 || borkerPort > 65535)
                    {
                        MessageBox.Show("Invalid Broker port. [0 - 65535]", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    Properties.Settings.Default.BrokerPort = borkerPort;
                }
                else
                {
                    MessageBox.Show("Invalid Broker port.", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (!string.IsNullOrEmpty(this.tbBrokerDomain.Text))
                {
                    Properties.Settings.Default.BrokerHost = this.tbBrokerDomain.Text;
                }

                if (!string.IsNullOrEmpty(this.tbInputTopic.Text))
                {
                    Properties.Settings.Default.MqttInputTopic = this.tbInputTopic.Text;
                }

                if (!string.IsNullOrEmpty(this.tbOutputTopic.Text))
                {
                    Properties.Settings.Default.MqttOutputTopic = this.tbOutputTopic.Text;
                }

                #endregion

                #region Navigation

                if (!string.IsNullOrEmpty(this.tbCameraUri.Text))
                {
                    Properties.Settings.Default.CameraUri = this.tbCameraUri.Text;
                }

                Properties.Settings.Default.CameraTorch = this.cbTorch.Checked;

                #endregion
                
                // Save settings.
                Properties.Settings.Default.Save();
            }
            catch (Exception err)
            {
                MessageBox.Show(String.Format("Message: {0}\r\nSource: {1}", err.Message, err.Source), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

    }
}
