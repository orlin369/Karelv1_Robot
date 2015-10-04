namespace DiO_CS_KarelV1_TEST
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnGetSensors = new System.Windows.Forms.Button();
            this.txtState = new System.Windows.Forms.TextBox();
            this.btnGetUltrasonic = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.tbSensorPosition = new System.Windows.Forms.TextBox();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbSensorView = new System.Windows.Forms.PictureBox();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnBackward = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.statBar = new System.Windows.Forms.StatusStrip();
            this.lblIsConnected = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDegreeSensor = new System.Windows.Forms.Label();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSensorView)).BeginInit();
            this.statBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetSensors
            // 
            this.btnGetSensors.Location = new System.Drawing.Point(289, 27);
            this.btnGetSensors.Name = "btnGetSensors";
            this.btnGetSensors.Size = new System.Drawing.Size(73, 73);
            this.btnGetSensors.TabIndex = 5;
            this.btnGetSensors.Text = "L/R Sens";
            this.btnGetSensors.UseVisualStyleBackColor = true;
            this.btnGetSensors.Click += new System.EventHandler(this.btnGetSensors_Click);
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(12, 284);
            this.txtState.Multiline = true;
            this.txtState.Name = "txtState";
            this.txtState.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtState.Size = new System.Drawing.Size(431, 160);
            this.txtState.TabIndex = 6;
            // 
            // btnGetUltrasonic
            // 
            this.btnGetUltrasonic.Location = new System.Drawing.Point(289, 106);
            this.btnGetUltrasonic.Name = "btnGetUltrasonic";
            this.btnGetUltrasonic.Size = new System.Drawing.Size(73, 73);
            this.btnGetUltrasonic.TabIndex = 7;
            this.btnGetUltrasonic.Text = "Ultra Sonic";
            this.btnGetUltrasonic.UseVisualStyleBackColor = true;
            this.btnGetUltrasonic.Click += new System.EventHandler(this.btnGetUltrasonic_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(12, 27);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(73, 73);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // tbSensorPosition
            // 
            this.tbSensorPosition.Location = new System.Drawing.Point(289, 185);
            this.tbSensorPosition.Name = "tbSensorPosition";
            this.tbSensorPosition.Size = new System.Drawing.Size(36, 20);
            this.tbSensorPosition.TabIndex = 9;
            this.tbSensorPosition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSensorPosition_KeyPress);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1261, 24);
            this.mainMenu.TabIndex = 11;
            this.mainMenu.Text = "menuStrip1";
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.portsToolStripMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.connectionToolStripMenuItem.Text = "Connection";
            this.connectionToolStripMenuItem.Click += new System.EventHandler(this.connectionToolStripMenuItem_Click);
            // 
            // portsToolStripMenuItem
            // 
            this.portsToolStripMenuItem.Name = "portsToolStripMenuItem";
            this.portsToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.portsToolStripMenuItem.Text = "Ports";
            // 
            // pbSensorView
            // 
            this.pbSensorView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSensorView.Location = new System.Drawing.Point(449, 44);
            this.pbSensorView.Name = "pbSensorView";
            this.pbSensorView.Size = new System.Drawing.Size(800, 400);
            this.pbSensorView.TabIndex = 10;
            this.pbSensorView.TabStop = false;
            this.pbSensorView.Paint += new System.Windows.Forms.PaintEventHandler(this.pbSensorView_Paint);
            // 
            // btnRight
            // 
            this.btnRight.Image = global::DiO_CS_KarelV1_TEST.ButtonsImages.ArrowRight;
            this.btnRight.Location = new System.Drawing.Point(170, 106);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(73, 73);
            this.btnRight.TabIndex = 4;
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnStop
            // 
            this.btnStop.Image = global::DiO_CS_KarelV1_TEST.ButtonsImages.Stop;
            this.btnStop.Location = new System.Drawing.Point(91, 106);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(73, 73);
            this.btnStop.TabIndex = 3;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnBackward
            // 
            this.btnBackward.Image = global::DiO_CS_KarelV1_TEST.ButtonsImages.ArrowDown;
            this.btnBackward.Location = new System.Drawing.Point(91, 185);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(73, 73);
            this.btnBackward.TabIndex = 2;
            this.btnBackward.UseVisualStyleBackColor = true;
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Image = global::DiO_CS_KarelV1_TEST.ButtonsImages.ArrowLeft;
            this.btnLeft.Location = new System.Drawing.Point(12, 106);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(73, 73);
            this.btnLeft.TabIndex = 1;
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnForward
            // 
            this.btnForward.Image = global::DiO_CS_KarelV1_TEST.ButtonsImages.ArrowUp;
            this.btnForward.Location = new System.Drawing.Point(91, 27);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(73, 73);
            this.btnForward.TabIndex = 0;
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // statBar
            // 
            this.statBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblIsConnected});
            this.statBar.Location = new System.Drawing.Point(0, 457);
            this.statBar.Name = "statBar";
            this.statBar.Size = new System.Drawing.Size(1261, 22);
            this.statBar.TabIndex = 12;
            this.statBar.Text = "statusStrip1";
            // 
            // lblIsConnected
            // 
            this.lblIsConnected.Name = "lblIsConnected";
            this.lblIsConnected.Size = new System.Drawing.Size(97, 17);
            this.lblIsConnected.Text = "Connected: False";
            // 
            // lblDegreeSensor
            // 
            this.lblDegreeSensor.AutoSize = true;
            this.lblDegreeSensor.Location = new System.Drawing.Point(331, 188);
            this.lblDegreeSensor.Name = "lblDegreeSensor";
            this.lblDegreeSensor.Size = new System.Drawing.Size(31, 13);
            this.lblDegreeSensor.TabIndex = 13;
            this.lblDegreeSensor.Text = "[deg]";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 479);
            this.Controls.Add(this.lblDegreeSensor);
            this.Controls.Add(this.statBar);
            this.Controls.Add(this.pbSensorView);
            this.Controls.Add(this.tbSensorPosition);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnGetUltrasonic);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.btnGetSensors);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnBackward);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "KarelV1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSensorView)).EndInit();
            this.statBar.ResumeLayout(false);
            this.statBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnBackward;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnGetSensors;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Button btnGetUltrasonic;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox tbSensorPosition;
        private System.Windows.Forms.PictureBox pbSensorView;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statBar;
        private System.Windows.Forms.ToolStripStatusLabel lblIsConnected;
        private System.Windows.Forms.Label lblDegreeSensor;
    }
}

