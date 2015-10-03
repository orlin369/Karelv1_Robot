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
            this.btnGetSensors = new System.Windows.Forms.Button();
            this.txtState = new System.Windows.Forms.TextBox();
            this.btnGetUltrasonic = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbSensorView = new System.Windows.Forms.PictureBox();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnBackward = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSensorView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetSensors
            // 
            this.btnGetSensors.Location = new System.Drawing.Point(257, 44);
            this.btnGetSensors.Name = "btnGetSensors";
            this.btnGetSensors.Size = new System.Drawing.Size(60, 60);
            this.btnGetSensors.TabIndex = 5;
            this.btnGetSensors.Text = "L/R Sens";
            this.btnGetSensors.UseVisualStyleBackColor = true;
            this.btnGetSensors.Click += new System.EventHandler(this.btnGetSensors_Click);
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(12, 450);
            this.txtState.Multiline = true;
            this.txtState.Name = "txtState";
            this.txtState.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtState.Size = new System.Drawing.Size(1111, 160);
            this.txtState.TabIndex = 6;
            // 
            // btnGetUltrasonic
            // 
            this.btnGetUltrasonic.Location = new System.Drawing.Point(257, 110);
            this.btnGetUltrasonic.Name = "btnGetUltrasonic";
            this.btnGetUltrasonic.Size = new System.Drawing.Size(60, 60);
            this.btnGetUltrasonic.TabIndex = 7;
            this.btnGetUltrasonic.Text = "Ultra Sonic";
            this.btnGetUltrasonic.UseVisualStyleBackColor = true;
            this.btnGetUltrasonic.Click += new System.EventHandler(this.btnGetUltrasonic_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(59, 44);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(60, 60);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(257, 176);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(60, 20);
            this.textBox1.TabIndex = 9;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1127, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
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
            this.pbSensorView.Location = new System.Drawing.Point(323, 44);
            this.pbSensorView.Name = "pbSensorView";
            this.pbSensorView.Size = new System.Drawing.Size(800, 400);
            this.pbSensorView.TabIndex = 10;
            this.pbSensorView.TabStop = false;
            this.pbSensorView.Paint += new System.Windows.Forms.PaintEventHandler(this.pbSensorView_Paint);
            // 
            // btnRight
            // 
            this.btnRight.Image = global::DiO_CS_KarelV1_TEST.ButtonsImages.ArrowRight;
            this.btnRight.Location = new System.Drawing.Point(191, 110);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(60, 60);
            this.btnRight.TabIndex = 4;
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnStop
            // 
            this.btnStop.Image = global::DiO_CS_KarelV1_TEST.ButtonsImages.Stop;
            this.btnStop.Location = new System.Drawing.Point(125, 110);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(60, 60);
            this.btnStop.TabIndex = 3;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnBackward
            // 
            this.btnBackward.Image = global::DiO_CS_KarelV1_TEST.ButtonsImages.ArrowDown;
            this.btnBackward.Location = new System.Drawing.Point(125, 176);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(60, 60);
            this.btnBackward.TabIndex = 2;
            this.btnBackward.UseVisualStyleBackColor = true;
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Image = global::DiO_CS_KarelV1_TEST.ButtonsImages.ArrowLeft;
            this.btnLeft.Location = new System.Drawing.Point(59, 110);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(60, 60);
            this.btnLeft.TabIndex = 1;
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnForward
            // 
            this.btnForward.Image = global::DiO_CS_KarelV1_TEST.ButtonsImages.ArrowUp;
            this.btnForward.Location = new System.Drawing.Point(125, 44);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(60, 60);
            this.btnForward.TabIndex = 0;
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1127, 638);
            this.Controls.Add(this.pbSensorView);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnGetUltrasonic);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.btnGetSensors);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnBackward);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "KarelV1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSensorView)).EndInit();
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pbSensorView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portsToolStripMenuItem;
    }
}

