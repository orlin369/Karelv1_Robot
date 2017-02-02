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
            this.tbBrokerDomain.Text = Properties.Settings.Default.BrokerHost;
            this.tbBrokerPort.Text = Properties.Settings.Default.BrokerPort.ToString();
            this.tbInputTopic.Text = Properties.Settings.Default.MqttInputTopic;
            this.tbOutputTopic.Text = Properties.Settings.Default.MqttOutputTopic;
        }

        private void SaveFields()
        {
            try
            {
                int borkerPort;

                // Validate baud rate.
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
