namespace KarelV1.Settings
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcSettings = new System.Windows.Forms.TabControl();
            this.tpMechanicalProperties = new System.Windows.Forms.TabPage();
            this.tbDistanceBetweenWheels = new System.Windows.Forms.TextBox();
            this.lblDistanceBetweenWheels = new System.Windows.Forms.Label();
            this.tbDiameterOfWheel = new System.Windows.Forms.TextBox();
            this.lblDiameterOfWheel = new System.Windows.Forms.Label();
            this.tbStepperPostScaler = new System.Windows.Forms.TextBox();
            this.lblStepperPostScaler = new System.Windows.Forms.Label();
            this.tbSteppsCount = new System.Windows.Forms.TextBox();
            this.lblSteppsCount = new System.Windows.Forms.Label();
            this.tpMQTTSettings = new System.Windows.Forms.TabPage();
            this.tbBrokerPort = new System.Windows.Forms.TextBox();
            this.lblBrokerPort = new System.Windows.Forms.Label();
            this.tbBrokerDomain = new System.Windows.Forms.TextBox();
            this.lblBrokerDomain = new System.Windows.Forms.Label();
            this.tbInputTopic = new System.Windows.Forms.TextBox();
            this.tbOutputTopic = new System.Windows.Forms.TextBox();
            this.lblOutputTopic = new System.Windows.Forms.Label();
            this.lblInputTopic = new System.Windows.Forms.Label();
            this.tbXScale = new System.Windows.Forms.TextBox();
            this.lblXScale = new System.Windows.Forms.Label();
            this.tbYScale = new System.Windows.Forms.TextBox();
            this.lblYScale = new System.Windows.Forms.Label();
            this.tpNavigation = new System.Windows.Forms.TabPage();
            this.tbCameraUri = new System.Windows.Forms.TextBox();
            this.lblCameraUri = new System.Windows.Forms.Label();
            this.cbTorch = new System.Windows.Forms.CheckBox();
            this.tcSettings.SuspendLayout();
            this.tpMechanicalProperties.SuspendLayout();
            this.tpMQTTSettings.SuspendLayout();
            this.tpNavigation.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcSettings
            // 
            this.tcSettings.Controls.Add(this.tpMechanicalProperties);
            this.tcSettings.Controls.Add(this.tpMQTTSettings);
            this.tcSettings.Controls.Add(this.tpNavigation);
            this.tcSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSettings.Location = new System.Drawing.Point(0, 0);
            this.tcSettings.Name = "tcSettings";
            this.tcSettings.SelectedIndex = 0;
            this.tcSettings.Size = new System.Drawing.Size(881, 408);
            this.tcSettings.TabIndex = 1;
            // 
            // tpMechanicalProperties
            // 
            this.tpMechanicalProperties.BackColor = System.Drawing.SystemColors.Control;
            this.tpMechanicalProperties.Controls.Add(this.tbYScale);
            this.tpMechanicalProperties.Controls.Add(this.lblYScale);
            this.tpMechanicalProperties.Controls.Add(this.tbXScale);
            this.tpMechanicalProperties.Controls.Add(this.lblXScale);
            this.tpMechanicalProperties.Controls.Add(this.tbDistanceBetweenWheels);
            this.tpMechanicalProperties.Controls.Add(this.lblDistanceBetweenWheels);
            this.tpMechanicalProperties.Controls.Add(this.tbDiameterOfWheel);
            this.tpMechanicalProperties.Controls.Add(this.lblDiameterOfWheel);
            this.tpMechanicalProperties.Controls.Add(this.tbStepperPostScaler);
            this.tpMechanicalProperties.Controls.Add(this.lblStepperPostScaler);
            this.tpMechanicalProperties.Controls.Add(this.tbSteppsCount);
            this.tpMechanicalProperties.Controls.Add(this.lblSteppsCount);
            this.tpMechanicalProperties.Location = new System.Drawing.Point(4, 25);
            this.tpMechanicalProperties.Name = "tpMechanicalProperties";
            this.tpMechanicalProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tpMechanicalProperties.Size = new System.Drawing.Size(873, 379);
            this.tpMechanicalProperties.TabIndex = 0;
            this.tpMechanicalProperties.Text = "Mechanical Properties";
            // 
            // tbDistanceBetweenWheels
            // 
            this.tbDistanceBetweenWheels.Location = new System.Drawing.Point(224, 107);
            this.tbDistanceBetweenWheels.Name = "tbDistanceBetweenWheels";
            this.tbDistanceBetweenWheels.Size = new System.Drawing.Size(194, 22);
            this.tbDistanceBetweenWheels.TabIndex = 7;
            // 
            // lblDistanceBetweenWheels
            // 
            this.lblDistanceBetweenWheels.AutoSize = true;
            this.lblDistanceBetweenWheels.Location = new System.Drawing.Point(8, 110);
            this.lblDistanceBetweenWheels.Name = "lblDistanceBetweenWheels";
            this.lblDistanceBetweenWheels.Size = new System.Drawing.Size(210, 17);
            this.lblDistanceBetweenWheels.TabIndex = 6;
            this.lblDistanceBetweenWheels.Text = "Distance Between Wheels [mm]:";
            // 
            // tbDiameterOfWheel
            // 
            this.tbDiameterOfWheel.Location = new System.Drawing.Point(224, 79);
            this.tbDiameterOfWheel.Name = "tbDiameterOfWheel";
            this.tbDiameterOfWheel.Size = new System.Drawing.Size(194, 22);
            this.tbDiameterOfWheel.TabIndex = 5;
            // 
            // lblDiameterOfWheel
            // 
            this.lblDiameterOfWheel.AutoSize = true;
            this.lblDiameterOfWheel.Location = new System.Drawing.Point(8, 82);
            this.lblDiameterOfWheel.Name = "lblDiameterOfWheel";
            this.lblDiameterOfWheel.Size = new System.Drawing.Size(170, 17);
            this.lblDiameterOfWheel.TabIndex = 4;
            this.lblDiameterOfWheel.Text = "Diametter Of Wheel [mm]:";
            // 
            // tbStepperPostScaler
            // 
            this.tbStepperPostScaler.Location = new System.Drawing.Point(224, 51);
            this.tbStepperPostScaler.Name = "tbStepperPostScaler";
            this.tbStepperPostScaler.Size = new System.Drawing.Size(194, 22);
            this.tbStepperPostScaler.TabIndex = 3;
            // 
            // lblStepperPostScaler
            // 
            this.lblStepperPostScaler.AutoSize = true;
            this.lblStepperPostScaler.Location = new System.Drawing.Point(8, 54);
            this.lblStepperPostScaler.Name = "lblStepperPostScaler";
            this.lblStepperPostScaler.Size = new System.Drawing.Size(138, 17);
            this.lblStepperPostScaler.TabIndex = 2;
            this.lblStepperPostScaler.Text = "Stepper Post Scaler:";
            // 
            // tbSteppsCount
            // 
            this.tbSteppsCount.Location = new System.Drawing.Point(224, 23);
            this.tbSteppsCount.Name = "tbSteppsCount";
            this.tbSteppsCount.Size = new System.Drawing.Size(194, 22);
            this.tbSteppsCount.TabIndex = 1;
            // 
            // lblSteppsCount
            // 
            this.lblSteppsCount.AutoSize = true;
            this.lblSteppsCount.Location = new System.Drawing.Point(8, 26);
            this.lblSteppsCount.Name = "lblSteppsCount";
            this.lblSteppsCount.Size = new System.Drawing.Size(97, 17);
            this.lblSteppsCount.TabIndex = 0;
            this.lblSteppsCount.Text = "Stepps Count:";
            // 
            // tpMQTTSettings
            // 
            this.tpMQTTSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tpMQTTSettings.Controls.Add(this.tbBrokerPort);
            this.tpMQTTSettings.Controls.Add(this.lblBrokerPort);
            this.tpMQTTSettings.Controls.Add(this.tbBrokerDomain);
            this.tpMQTTSettings.Controls.Add(this.lblBrokerDomain);
            this.tpMQTTSettings.Controls.Add(this.tbInputTopic);
            this.tpMQTTSettings.Controls.Add(this.tbOutputTopic);
            this.tpMQTTSettings.Controls.Add(this.lblOutputTopic);
            this.tpMQTTSettings.Controls.Add(this.lblInputTopic);
            this.tpMQTTSettings.Location = new System.Drawing.Point(4, 25);
            this.tpMQTTSettings.Name = "tpMQTTSettings";
            this.tpMQTTSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpMQTTSettings.Size = new System.Drawing.Size(873, 379);
            this.tpMQTTSettings.TabIndex = 1;
            this.tpMQTTSettings.Text = "MQTT Settings";
            // 
            // tbBrokerPort
            // 
            this.tbBrokerPort.Location = new System.Drawing.Point(129, 40);
            this.tbBrokerPort.Name = "tbBrokerPort";
            this.tbBrokerPort.Size = new System.Drawing.Size(303, 22);
            this.tbBrokerPort.TabIndex = 18;
            // 
            // lblBrokerPort
            // 
            this.lblBrokerPort.AutoSize = true;
            this.lblBrokerPort.Location = new System.Drawing.Point(39, 43);
            this.lblBrokerPort.Name = "lblBrokerPort";
            this.lblBrokerPort.Size = new System.Drawing.Size(84, 17);
            this.lblBrokerPort.TabIndex = 17;
            this.lblBrokerPort.Text = "Broker Port:";
            // 
            // tbBrokerDomain
            // 
            this.tbBrokerDomain.Location = new System.Drawing.Point(129, 12);
            this.tbBrokerDomain.Name = "tbBrokerDomain";
            this.tbBrokerDomain.Size = new System.Drawing.Size(303, 22);
            this.tbBrokerDomain.TabIndex = 16;
            // 
            // lblBrokerDomain
            // 
            this.lblBrokerDomain.AutoSize = true;
            this.lblBrokerDomain.Location = new System.Drawing.Point(17, 15);
            this.lblBrokerDomain.Name = "lblBrokerDomain";
            this.lblBrokerDomain.Size = new System.Drawing.Size(106, 17);
            this.lblBrokerDomain.TabIndex = 15;
            this.lblBrokerDomain.Text = "Broker Domain:";
            // 
            // tbInputTopic
            // 
            this.tbInputTopic.Location = new System.Drawing.Point(129, 68);
            this.tbInputTopic.Name = "tbInputTopic";
            this.tbInputTopic.Size = new System.Drawing.Size(303, 22);
            this.tbInputTopic.TabIndex = 14;
            // 
            // tbOutputTopic
            // 
            this.tbOutputTopic.Location = new System.Drawing.Point(129, 97);
            this.tbOutputTopic.Name = "tbOutputTopic";
            this.tbOutputTopic.Size = new System.Drawing.Size(303, 22);
            this.tbOutputTopic.TabIndex = 13;
            // 
            // lblOutputTopic
            // 
            this.lblOutputTopic.AutoSize = true;
            this.lblOutputTopic.Location = new System.Drawing.Point(29, 97);
            this.lblOutputTopic.Name = "lblOutputTopic";
            this.lblOutputTopic.Size = new System.Drawing.Size(94, 17);
            this.lblOutputTopic.TabIndex = 12;
            this.lblOutputTopic.Text = "Output Topic:";
            // 
            // lblInputTopic
            // 
            this.lblInputTopic.AutoSize = true;
            this.lblInputTopic.Location = new System.Drawing.Point(41, 71);
            this.lblInputTopic.Name = "lblInputTopic";
            this.lblInputTopic.Size = new System.Drawing.Size(82, 17);
            this.lblInputTopic.TabIndex = 11;
            this.lblInputTopic.Text = "Input Topic:";
            // 
            // tbXScale
            // 
            this.tbXScale.Location = new System.Drawing.Point(224, 135);
            this.tbXScale.Name = "tbXScale";
            this.tbXScale.Size = new System.Drawing.Size(194, 22);
            this.tbXScale.TabIndex = 9;
            // 
            // lblXScale
            // 
            this.lblXScale.AutoSize = true;
            this.lblXScale.Location = new System.Drawing.Point(8, 138);
            this.lblXScale.Name = "lblXScale";
            this.lblXScale.Size = new System.Drawing.Size(60, 17);
            this.lblXScale.TabIndex = 8;
            this.lblXScale.Text = "X Scale:";
            // 
            // tbYScale
            // 
            this.tbYScale.Location = new System.Drawing.Point(224, 163);
            this.tbYScale.Name = "tbYScale";
            this.tbYScale.Size = new System.Drawing.Size(194, 22);
            this.tbYScale.TabIndex = 11;
            // 
            // lblYScale
            // 
            this.lblYScale.AutoSize = true;
            this.lblYScale.Location = new System.Drawing.Point(8, 166);
            this.lblYScale.Name = "lblYScale";
            this.lblYScale.Size = new System.Drawing.Size(60, 17);
            this.lblYScale.TabIndex = 10;
            this.lblYScale.Text = "Y Scale:";
            // 
            // tpNavigation
            // 
            this.tpNavigation.BackColor = System.Drawing.SystemColors.Control;
            this.tpNavigation.Controls.Add(this.cbTorch);
            this.tpNavigation.Controls.Add(this.tbCameraUri);
            this.tpNavigation.Controls.Add(this.lblCameraUri);
            this.tpNavigation.Location = new System.Drawing.Point(4, 25);
            this.tpNavigation.Name = "tpNavigation";
            this.tpNavigation.Size = new System.Drawing.Size(873, 379);
            this.tpNavigation.TabIndex = 2;
            this.tpNavigation.Text = "Navigation";
            // 
            // tbCameraUri
            // 
            this.tbCameraUri.Location = new System.Drawing.Point(107, 6);
            this.tbCameraUri.Name = "tbCameraUri";
            this.tbCameraUri.Size = new System.Drawing.Size(194, 22);
            this.tbCameraUri.TabIndex = 13;
            // 
            // lblCameraUri
            // 
            this.lblCameraUri.AutoSize = true;
            this.lblCameraUri.Location = new System.Drawing.Point(8, 9);
            this.lblCameraUri.Name = "lblCameraUri";
            this.lblCameraUri.Size = new System.Drawing.Size(93, 17);
            this.lblCameraUri.TabIndex = 12;
            this.lblCameraUri.Text = "Camera URL:";
            // 
            // cbTorch
            // 
            this.cbTorch.AutoSize = true;
            this.cbTorch.Location = new System.Drawing.Point(9, 40);
            this.cbTorch.Margin = new System.Windows.Forms.Padding(4);
            this.cbTorch.Name = "cbTorch";
            this.cbTorch.Size = new System.Drawing.Size(67, 21);
            this.cbTorch.TabIndex = 26;
            this.cbTorch.Text = "Torch";
            this.cbTorch.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 408);
            this.Controls.Add(this.tcSettings);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tcSettings.ResumeLayout(false);
            this.tpMechanicalProperties.ResumeLayout(false);
            this.tpMechanicalProperties.PerformLayout();
            this.tpMQTTSettings.ResumeLayout(false);
            this.tpMQTTSettings.PerformLayout();
            this.tpNavigation.ResumeLayout(false);
            this.tpNavigation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcSettings;
        private System.Windows.Forms.TabPage tpMechanicalProperties;
        private System.Windows.Forms.TabPage tpMQTTSettings;
        private System.Windows.Forms.TextBox tbBrokerPort;
        private System.Windows.Forms.Label lblBrokerPort;
        private System.Windows.Forms.TextBox tbBrokerDomain;
        private System.Windows.Forms.Label lblBrokerDomain;
        private System.Windows.Forms.TextBox tbInputTopic;
        private System.Windows.Forms.TextBox tbOutputTopic;
        private System.Windows.Forms.Label lblOutputTopic;
        private System.Windows.Forms.Label lblInputTopic;
        private System.Windows.Forms.TextBox tbSteppsCount;
        private System.Windows.Forms.Label lblSteppsCount;
        private System.Windows.Forms.TextBox tbDistanceBetweenWheels;
        private System.Windows.Forms.Label lblDistanceBetweenWheels;
        private System.Windows.Forms.TextBox tbDiameterOfWheel;
        private System.Windows.Forms.Label lblDiameterOfWheel;
        private System.Windows.Forms.TextBox tbStepperPostScaler;
        private System.Windows.Forms.Label lblStepperPostScaler;
        private System.Windows.Forms.TextBox tbYScale;
        private System.Windows.Forms.Label lblYScale;
        private System.Windows.Forms.TextBox tbXScale;
        private System.Windows.Forms.Label lblXScale;
        private System.Windows.Forms.TabPage tpNavigation;
        private System.Windows.Forms.TextBox tbCameraUri;
        private System.Windows.Forms.Label lblCameraUri;
        private System.Windows.Forms.CheckBox cbTorch;
    }
}