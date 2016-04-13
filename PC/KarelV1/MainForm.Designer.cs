namespace GUI
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statBar = new System.Windows.Forms.StatusStrip();
            this.lblIsConnected = new System.Windows.Forms.ToolStripStatusLabel();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblControls = new System.Windows.Forms.TableLayoutPanel();
            this.txtState = new System.Windows.Forms.TextBox();
            this.gpbControls = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnBackward = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.tcSensorsFunctions = new System.Windows.Forms.TabControl();
            this.tbpSonar = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pbSensorView = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDegreeSensor = new System.Windows.Forms.Label();
            this.tbSensorPosition = new System.Windows.Forms.TextBox();
            this.btnGetUltrasonic = new System.Windows.Forms.Button();
            this.tbpGlyph = new System.Windows.Forms.TabPage();
            this.tblGlyph = new System.Windows.Forms.TableLayoutPanel();
            this.pnlGlyphControl = new System.Windows.Forms.Panel();
            this.lblURL = new System.Windows.Forms.Label();
            this.lblUTInterval = new System.Windows.Forms.Label();
            this.tbCameraUpdateTime = new System.Windows.Forms.TextBox();
            this.chkContinuesCapture = new System.Windows.Forms.CheckBox();
            this.tbCameraIP = new System.Windows.Forms.TextBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.pbGlyph = new System.Windows.Forms.PictureBox();
            this.tbpSensors = new System.Windows.Forms.TabPage();
            this.pnlSensors = new System.Windows.Forms.Panel();
            this.lblRobotPosition = new System.Windows.Forms.Label();
            this.btnGetRobotPos = new System.Windows.Forms.Button();
            this.prbRightSensor = new System.Windows.Forms.ProgressBar();
            this.prbLeftSensor = new System.Windows.Forms.ProgressBar();
            this.btnGetSensors = new System.Windows.Forms.Button();
            this.btnLoginTest = new System.Windows.Forms.Button();
            this.mainMenu.SuspendLayout();
            this.statBar.SuspendLayout();
            this.tblMain.SuspendLayout();
            this.tblControls.SuspendLayout();
            this.gpbControls.SuspendLayout();
            this.tcSensorsFunctions.SuspendLayout();
            this.tbpSonar.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSensorView)).BeginInit();
            this.panel1.SuspendLayout();
            this.tbpGlyph.SuspendLayout();
            this.tblGlyph.SuspendLayout();
            this.pnlGlyphControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGlyph)).BeginInit();
            this.tbpSensors.SuspendLayout();
            this.pnlSensors.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1455, 24);
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
            // statBar
            // 
            this.statBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblIsConnected});
            this.statBar.Location = new System.Drawing.Point(0, 516);
            this.statBar.Name = "statBar";
            this.statBar.Size = new System.Drawing.Size(1455, 22);
            this.statBar.TabIndex = 12;
            this.statBar.Text = "statusStrip1";
            // 
            // lblIsConnected
            // 
            this.lblIsConnected.Name = "lblIsConnected";
            this.lblIsConnected.Size = new System.Drawing.Size(97, 17);
            this.lblIsConnected.Text = "Connected: False";
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 2;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.20962F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.79037F));
            this.tblMain.Controls.Add(this.tblControls, 0, 0);
            this.tblMain.Controls.Add(this.tcSensorsFunctions, 1, 0);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 24);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 1;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMain.Size = new System.Drawing.Size(1455, 492);
            this.tblMain.TabIndex = 14;
            // 
            // tblControls
            // 
            this.tblControls.ColumnCount = 1;
            this.tblControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblControls.Controls.Add(this.txtState, 0, 1);
            this.tblControls.Controls.Add(this.gpbControls, 0, 0);
            this.tblControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tblControls.Location = new System.Drawing.Point(3, 3);
            this.tblControls.Name = "tblControls";
            this.tblControls.RowCount = 2;
            this.tblControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.73251F));
            this.tblControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.26749F));
            this.tblControls.Size = new System.Drawing.Size(419, 486);
            this.tblControls.TabIndex = 0;
            // 
            // txtState
            // 
            this.txtState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtState.Location = new System.Drawing.Point(3, 268);
            this.txtState.Multiline = true;
            this.txtState.Name = "txtState";
            this.txtState.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtState.Size = new System.Drawing.Size(413, 215);
            this.txtState.TabIndex = 7;
            // 
            // gpbControls
            // 
            this.gpbControls.Controls.Add(this.btnReset);
            this.gpbControls.Controls.Add(this.btnRight);
            this.gpbControls.Controls.Add(this.btnStop);
            this.gpbControls.Controls.Add(this.btnBackward);
            this.gpbControls.Controls.Add(this.btnLeft);
            this.gpbControls.Controls.Add(this.btnForward);
            this.gpbControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gpbControls.Location = new System.Drawing.Point(3, 3);
            this.gpbControls.Name = "gpbControls";
            this.gpbControls.Size = new System.Drawing.Size(413, 259);
            this.gpbControls.TabIndex = 0;
            this.gpbControls.TabStop = false;
            this.gpbControls.Text = "Controls";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(6, 19);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(73, 73);
            this.btnReset.TabIndex = 21;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnRight
            // 
            this.btnRight.Image = global::GUI.Images.ArrowRight;
            this.btnRight.Location = new System.Drawing.Point(164, 98);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(73, 73);
            this.btnRight.TabIndex = 18;
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnStop
            // 
            this.btnStop.Image = global::GUI.Images.Stop;
            this.btnStop.Location = new System.Drawing.Point(85, 98);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(73, 73);
            this.btnStop.TabIndex = 17;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnBackward
            // 
            this.btnBackward.Image = global::GUI.Images.ArrowDown;
            this.btnBackward.Location = new System.Drawing.Point(85, 177);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(73, 73);
            this.btnBackward.TabIndex = 16;
            this.btnBackward.UseVisualStyleBackColor = true;
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Image = global::GUI.Images.ArrowLeft;
            this.btnLeft.Location = new System.Drawing.Point(6, 98);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(73, 73);
            this.btnLeft.TabIndex = 15;
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnForward
            // 
            this.btnForward.Image = global::GUI.Images.ArrowUp;
            this.btnForward.Location = new System.Drawing.Point(85, 19);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(73, 73);
            this.btnForward.TabIndex = 14;
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // tcSensorsFunctions
            // 
            this.tcSensorsFunctions.Controls.Add(this.tbpSonar);
            this.tcSensorsFunctions.Controls.Add(this.tbpGlyph);
            this.tcSensorsFunctions.Controls.Add(this.tbpSensors);
            this.tcSensorsFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSensorsFunctions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tcSensorsFunctions.Location = new System.Drawing.Point(428, 3);
            this.tcSensorsFunctions.Name = "tcSensorsFunctions";
            this.tcSensorsFunctions.SelectedIndex = 0;
            this.tcSensorsFunctions.Size = new System.Drawing.Size(1024, 486);
            this.tcSensorsFunctions.TabIndex = 1;
            // 
            // tbpSonar
            // 
            this.tbpSonar.Controls.Add(this.tableLayoutPanel1);
            this.tbpSonar.Location = new System.Drawing.Point(4, 25);
            this.tbpSonar.Name = "tbpSonar";
            this.tbpSonar.Padding = new System.Windows.Forms.Padding(3);
            this.tbpSonar.Size = new System.Drawing.Size(1016, 457);
            this.tbpSonar.TabIndex = 0;
            this.tbpSonar.Text = "Sonar";
            this.tbpSonar.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.00989F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.990108F));
            this.tableLayoutPanel1.Controls.Add(this.pbSensorView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1010, 451);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // pbSensorView
            // 
            this.pbSensorView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSensorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbSensorView.Location = new System.Drawing.Point(3, 3);
            this.pbSensorView.Name = "pbSensorView";
            this.pbSensorView.Size = new System.Drawing.Size(903, 445);
            this.pbSensorView.TabIndex = 12;
            this.pbSensorView.TabStop = false;
            this.pbSensorView.Paint += new System.Windows.Forms.PaintEventHandler(this.pbSensorView_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDegreeSensor);
            this.panel1.Controls.Add(this.tbSensorPosition);
            this.panel1.Controls.Add(this.btnGetUltrasonic);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(912, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(95, 445);
            this.panel1.TabIndex = 13;
            // 
            // lblDegreeSensor
            // 
            this.lblDegreeSensor.AutoSize = true;
            this.lblDegreeSensor.Location = new System.Drawing.Point(45, 85);
            this.lblDegreeSensor.Name = "lblDegreeSensor";
            this.lblDegreeSensor.Size = new System.Drawing.Size(45, 16);
            this.lblDegreeSensor.TabIndex = 26;
            this.lblDegreeSensor.Text = "[deg]";
            // 
            // tbSensorPosition
            // 
            this.tbSensorPosition.Location = new System.Drawing.Point(3, 82);
            this.tbSensorPosition.Name = "tbSensorPosition";
            this.tbSensorPosition.Size = new System.Drawing.Size(36, 22);
            this.tbSensorPosition.TabIndex = 25;
            // 
            // btnGetUltrasonic
            // 
            this.btnGetUltrasonic.Image = global::GUI.Images.Sensor2;
            this.btnGetUltrasonic.Location = new System.Drawing.Point(3, 3);
            this.btnGetUltrasonic.Name = "btnGetUltrasonic";
            this.btnGetUltrasonic.Size = new System.Drawing.Size(73, 73);
            this.btnGetUltrasonic.TabIndex = 24;
            this.btnGetUltrasonic.UseVisualStyleBackColor = true;
            this.btnGetUltrasonic.Click += new System.EventHandler(this.btnGetUltrasonic_Click);
            // 
            // tbpGlyph
            // 
            this.tbpGlyph.Controls.Add(this.tblGlyph);
            this.tbpGlyph.Location = new System.Drawing.Point(4, 25);
            this.tbpGlyph.Name = "tbpGlyph";
            this.tbpGlyph.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGlyph.Size = new System.Drawing.Size(1016, 457);
            this.tbpGlyph.TabIndex = 1;
            this.tbpGlyph.Text = "Glyph";
            this.tbpGlyph.UseVisualStyleBackColor = true;
            // 
            // tblGlyph
            // 
            this.tblGlyph.ColumnCount = 2;
            this.tblGlyph.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.67318F));
            this.tblGlyph.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.32682F));
            this.tblGlyph.Controls.Add(this.pnlGlyphControl, 1, 0);
            this.tblGlyph.Controls.Add(this.pbGlyph, 0, 0);
            this.tblGlyph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblGlyph.Location = new System.Drawing.Point(3, 3);
            this.tblGlyph.Name = "tblGlyph";
            this.tblGlyph.RowCount = 1;
            this.tblGlyph.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblGlyph.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblGlyph.Size = new System.Drawing.Size(1010, 451);
            this.tblGlyph.TabIndex = 0;
            // 
            // pnlGlyphControl
            // 
            this.pnlGlyphControl.Controls.Add(this.lblURL);
            this.pnlGlyphControl.Controls.Add(this.lblUTInterval);
            this.pnlGlyphControl.Controls.Add(this.tbCameraUpdateTime);
            this.pnlGlyphControl.Controls.Add(this.chkContinuesCapture);
            this.pnlGlyphControl.Controls.Add(this.tbCameraIP);
            this.pnlGlyphControl.Controls.Add(this.btnCapture);
            this.pnlGlyphControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGlyphControl.Location = new System.Drawing.Point(817, 3);
            this.pnlGlyphControl.Name = "pnlGlyphControl";
            this.pnlGlyphControl.Size = new System.Drawing.Size(190, 445);
            this.pnlGlyphControl.TabIndex = 0;
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(6, 13);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(38, 16);
            this.lblURL.TabIndex = 24;
            this.lblURL.Text = "URL";
            // 
            // lblUTInterval
            // 
            this.lblUTInterval.AutoSize = true;
            this.lblUTInterval.Location = new System.Drawing.Point(12, 188);
            this.lblUTInterval.Name = "lblUTInterval";
            this.lblUTInterval.Size = new System.Drawing.Size(81, 16);
            this.lblUTInterval.TabIndex = 23;
            this.lblUTInterval.Text = "Time [ms]:";
            // 
            // tbCameraUpdateTime
            // 
            this.tbCameraUpdateTime.Location = new System.Drawing.Point(99, 185);
            this.tbCameraUpdateTime.Name = "tbCameraUpdateTime";
            this.tbCameraUpdateTime.Size = new System.Drawing.Size(40, 22);
            this.tbCameraUpdateTime.TabIndex = 22;
            this.tbCameraUpdateTime.Text = "500";
            // 
            // chkContinuesCapture
            // 
            this.chkContinuesCapture.AutoSize = true;
            this.chkContinuesCapture.Location = new System.Drawing.Point(12, 213);
            this.chkContinuesCapture.Name = "chkContinuesCapture";
            this.chkContinuesCapture.Size = new System.Drawing.Size(153, 20);
            this.chkContinuesCapture.TabIndex = 21;
            this.chkContinuesCapture.Text = "Continues Capture";
            this.chkContinuesCapture.UseVisualStyleBackColor = true;
            this.chkContinuesCapture.CheckedChanged += new System.EventHandler(this.chkContinuesCapture_CheckedChanged);
            // 
            // tbCameraIP
            // 
            this.tbCameraIP.Location = new System.Drawing.Point(9, 32);
            this.tbCameraIP.Multiline = true;
            this.tbCameraIP.Name = "tbCameraIP";
            this.tbCameraIP.Size = new System.Drawing.Size(161, 45);
            this.tbCameraIP.TabIndex = 20;
            this.tbCameraIP.Text = "https://scontent-vie1-1.xx.fbcdn.net/hphotos-xlt1/v/t1.0-9/473_10201482439540779_" +
    "3601827889724477532_n.jpg?oh=310e1862af7b481da70be9ec3bca9563&oe=5753D41A";
            // 
            // btnCapture
            // 
            this.btnCapture.Image = global::GUI.Images.Capture;
            this.btnCapture.Location = new System.Drawing.Point(9, 83);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(96, 96);
            this.btnCapture.TabIndex = 19;
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // pbGlyph
            // 
            this.pbGlyph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbGlyph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbGlyph.Location = new System.Drawing.Point(3, 3);
            this.pbGlyph.Name = "pbGlyph";
            this.pbGlyph.Size = new System.Drawing.Size(808, 445);
            this.pbGlyph.TabIndex = 1;
            this.pbGlyph.TabStop = false;
            // 
            // tbpSensors
            // 
            this.tbpSensors.Controls.Add(this.pnlSensors);
            this.tbpSensors.Location = new System.Drawing.Point(4, 25);
            this.tbpSensors.Name = "tbpSensors";
            this.tbpSensors.Size = new System.Drawing.Size(1016, 457);
            this.tbpSensors.TabIndex = 2;
            this.tbpSensors.Text = "Sensors";
            this.tbpSensors.UseVisualStyleBackColor = true;
            // 
            // pnlSensors
            // 
            this.pnlSensors.Controls.Add(this.btnLoginTest);
            this.pnlSensors.Controls.Add(this.lblRobotPosition);
            this.pnlSensors.Controls.Add(this.btnGetRobotPos);
            this.pnlSensors.Controls.Add(this.prbRightSensor);
            this.pnlSensors.Controls.Add(this.prbLeftSensor);
            this.pnlSensors.Controls.Add(this.btnGetSensors);
            this.pnlSensors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSensors.Location = new System.Drawing.Point(0, 0);
            this.pnlSensors.Name = "pnlSensors";
            this.pnlSensors.Size = new System.Drawing.Size(1016, 457);
            this.pnlSensors.TabIndex = 0;
            // 
            // lblRobotPosition
            // 
            this.lblRobotPosition.AutoSize = true;
            this.lblRobotPosition.Location = new System.Drawing.Point(128, 246);
            this.lblRobotPosition.Name = "lblRobotPosition";
            this.lblRobotPosition.Size = new System.Drawing.Size(68, 16);
            this.lblRobotPosition.TabIndex = 24;
            this.lblRobotPosition.Text = "Position:";
            // 
            // btnGetRobotPos
            // 
            this.btnGetRobotPos.Image = global::GUI.Images.Sensor3;
            this.btnGetRobotPos.Location = new System.Drawing.Point(131, 164);
            this.btnGetRobotPos.Name = "btnGetRobotPos";
            this.btnGetRobotPos.Size = new System.Drawing.Size(73, 73);
            this.btnGetRobotPos.TabIndex = 23;
            this.btnGetRobotPos.UseVisualStyleBackColor = true;
            this.btnGetRobotPos.Click += new System.EventHandler(this.btnGetRobotPos_Click);
            // 
            // prbRightSensor
            // 
            this.prbRightSensor.Location = new System.Drawing.Point(210, 47);
            this.prbRightSensor.Maximum = 1023;
            this.prbRightSensor.Name = "prbRightSensor";
            this.prbRightSensor.Size = new System.Drawing.Size(100, 23);
            this.prbRightSensor.TabIndex = 22;
            // 
            // prbLeftSensor
            // 
            this.prbLeftSensor.Location = new System.Drawing.Point(25, 47);
            this.prbLeftSensor.Maximum = 1023;
            this.prbLeftSensor.Name = "prbLeftSensor";
            this.prbLeftSensor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.prbLeftSensor.RightToLeftLayout = true;
            this.prbLeftSensor.Size = new System.Drawing.Size(100, 23);
            this.prbLeftSensor.TabIndex = 21;
            // 
            // btnGetSensors
            // 
            this.btnGetSensors.Image = global::GUI.Images.Sensor1;
            this.btnGetSensors.Location = new System.Drawing.Point(131, 26);
            this.btnGetSensors.Name = "btnGetSensors";
            this.btnGetSensors.Size = new System.Drawing.Size(73, 73);
            this.btnGetSensors.TabIndex = 20;
            this.btnGetSensors.UseVisualStyleBackColor = true;
            this.btnGetSensors.Click += new System.EventHandler(this.btnGetSensors_Click);
            // 
            // btnLoginTest
            // 
            this.btnLoginTest.Location = new System.Drawing.Point(922, 16);
            this.btnLoginTest.Name = "btnLoginTest";
            this.btnLoginTest.Size = new System.Drawing.Size(73, 73);
            this.btnLoginTest.TabIndex = 25;
            this.btnLoginTest.Text = "Login Test";
            this.btnLoginTest.UseVisualStyleBackColor = true;
            this.btnLoginTest.Click += new System.EventHandler(this.btnLoginTest_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1455, 538);
            this.Controls.Add(this.tblMain);
            this.Controls.Add(this.statBar);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "KarelV1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statBar.ResumeLayout(false);
            this.statBar.PerformLayout();
            this.tblMain.ResumeLayout(false);
            this.tblControls.ResumeLayout(false);
            this.tblControls.PerformLayout();
            this.gpbControls.ResumeLayout(false);
            this.tcSensorsFunctions.ResumeLayout(false);
            this.tbpSonar.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSensorView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tbpGlyph.ResumeLayout(false);
            this.tblGlyph.ResumeLayout(false);
            this.pnlGlyphControl.ResumeLayout(false);
            this.pnlGlyphControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGlyph)).EndInit();
            this.tbpSensors.ResumeLayout(false);
            this.pnlSensors.ResumeLayout(false);
            this.pnlSensors.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statBar;
        private System.Windows.Forms.ToolStripStatusLabel lblIsConnected;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.TableLayoutPanel tblControls;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.GroupBox gpbControls;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnBackward;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.TabControl tcSensorsFunctions;
        private System.Windows.Forms.TabPage tbpSonar;
        private System.Windows.Forms.TabPage tbpGlyph;
        private System.Windows.Forms.TableLayoutPanel tblGlyph;
        private System.Windows.Forms.Panel pnlGlyphControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pbSensorView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDegreeSensor;
        private System.Windows.Forms.TextBox tbSensorPosition;
        private System.Windows.Forms.Button btnGetUltrasonic;
        private System.Windows.Forms.TabPage tbpSensors;
        private System.Windows.Forms.Panel pnlSensors;
        private System.Windows.Forms.Button btnGetSensors;
        private System.Windows.Forms.PictureBox pbGlyph;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.TextBox tbCameraIP;
        private System.Windows.Forms.CheckBox chkContinuesCapture;
        private System.Windows.Forms.TextBox tbCameraUpdateTime;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.Label lblUTInterval;
        private System.Windows.Forms.ProgressBar prbRightSensor;
        private System.Windows.Forms.ProgressBar prbLeftSensor;
        private System.Windows.Forms.Button btnGetRobotPos;
        private System.Windows.Forms.Label lblRobotPosition;
        private System.Windows.Forms.Button btnLoginTest;
    }
}

