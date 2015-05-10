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
            this.btnForward = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnBackward = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnGetSensors = new System.Windows.Forms.Button();
            this.txtState = new System.Windows.Forms.TextBox();
            this.btnGetUltrasonic = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pbSensorView = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSensorView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(125, 12);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(60, 60);
            this.btnForward.TabIndex = 0;
            this.btnForward.Text = "/\\";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(59, 78);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(60, 60);
            this.btnLeft.TabIndex = 1;
            this.btnLeft.Text = "<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnBackward
            // 
            this.btnBackward.Location = new System.Drawing.Point(125, 144);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(60, 60);
            this.btnBackward.TabIndex = 2;
            this.btnBackward.Text = "\\/";
            this.btnBackward.UseVisualStyleBackColor = true;
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(125, 78);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(60, 60);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(191, 78);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(60, 60);
            this.btnRight.TabIndex = 4;
            this.btnRight.Text = ">";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnGetSensors
            // 
            this.btnGetSensors.Location = new System.Drawing.Point(257, 12);
            this.btnGetSensors.Name = "btnGetSensors";
            this.btnGetSensors.Size = new System.Drawing.Size(60, 60);
            this.btnGetSensors.TabIndex = 5;
            this.btnGetSensors.Text = "L/R Sens";
            this.btnGetSensors.UseVisualStyleBackColor = true;
            this.btnGetSensors.Click += new System.EventHandler(this.btnGetSensors_Click);
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(12, 418);
            this.txtState.Multiline = true;
            this.txtState.Name = "txtState";
            this.txtState.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtState.Size = new System.Drawing.Size(1111, 192);
            this.txtState.TabIndex = 6;
            // 
            // btnGetUltrasonic
            // 
            this.btnGetUltrasonic.Location = new System.Drawing.Point(257, 78);
            this.btnGetUltrasonic.Name = "btnGetUltrasonic";
            this.btnGetUltrasonic.Size = new System.Drawing.Size(60, 60);
            this.btnGetUltrasonic.TabIndex = 7;
            this.btnGetUltrasonic.Text = "Ultra Sonic";
            this.btnGetUltrasonic.UseVisualStyleBackColor = true;
            this.btnGetUltrasonic.Click += new System.EventHandler(this.btnGetUltrasonic_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(59, 12);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(60, 60);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(257, 144);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(60, 20);
            this.textBox1.TabIndex = 9;
            // 
            // pbSensorView
            // 
            this.pbSensorView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSensorView.Location = new System.Drawing.Point(323, 12);
            this.pbSensorView.Name = "pbSensorView";
            this.pbSensorView.Size = new System.Drawing.Size(800, 400);
            this.pbSensorView.TabIndex = 10;
            this.pbSensorView.TabStop = false;
            this.pbSensorView.Paint += new System.Windows.Forms.PaintEventHandler(this.pbSensorView_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 638);
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
            this.Name = "MainForm";
            this.Text = "KarelV1";
            this.Load += new System.EventHandler(this.MainForm_Load);
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
    }
}

