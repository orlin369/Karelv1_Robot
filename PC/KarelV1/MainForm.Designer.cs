namespace KarelV1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPorts = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiReset = new System.Windows.Forms.ToolStripMenuItem();
            this.statBar = new System.Windows.Forms.StatusStrip();
            this.lblIsConnected = new System.Windows.Forms.ToolStripStatusLabel();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblControls = new System.Windows.Forms.TableLayoutPanel();
            this.txtState = new System.Windows.Forms.TextBox();
            this.gpbControls = new System.Windows.Forms.GroupBox();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnBackward = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.tcSensorsFunctions = new System.Windows.Forms.TabControl();
            this.tbpSonar = new System.Windows.Forms.TabPage();
            this.tlpSonar = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSonar = new System.Windows.Forms.Panel();
            this.lblDegreeSensor = new System.Windows.Forms.Label();
            this.tbSensorPosition = new System.Windows.Forms.TextBox();
            this.btnGetUltrasonic = new System.Windows.Forms.Button();
            this.crtUltrasinicSensor = new System.Windows.Forms.DataVisualization.Charting.Chart();
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
            this.btnLoginTest = new System.Windows.Forms.Button();
            this.lblRobotPosition = new System.Windows.Forms.Label();
            this.btnGetRobotPos = new System.Windows.Forms.Button();
            this.prbRightSensor = new System.Windows.Forms.ProgressBar();
            this.prbLeftSensor = new System.Windows.Forms.ProgressBar();
            this.btnGetSensors = new System.Windows.Forms.Button();
            this.cbMetric = new System.Windows.Forms.ComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.talkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.statBar.SuspendLayout();
            this.tblMain.SuspendLayout();
            this.tblControls.SuspendLayout();
            this.gpbControls.SuspendLayout();
            this.tcSensorsFunctions.SuspendLayout();
            this.tbpSonar.SuspendLayout();
            this.tlpSonar.SuspendLayout();
            this.pnlSonar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crtUltrasinicSensor)).BeginInit();
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
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiConnection});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(1335, 28);
            this.mainMenu.TabIndex = 11;
            this.mainMenu.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.talkToolStripMenuItem,
            this.toolStripSeparator1,
            this.tsmiExit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(44, 24);
            this.tsmiFile.Text = "File";
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.tsmiExit.Size = new System.Drawing.Size(181, 26);
            this.tsmiExit.Text = "Exit";
            // 
            // tsmiConnection
            // 
            this.tsmiConnection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPorts,
            this.tsSeparator1,
            this.tsmiReset});
            this.tsmiConnection.Name = "tsmiConnection";
            this.tsmiConnection.Size = new System.Drawing.Size(96, 24);
            this.tsmiConnection.Text = "Connection";
            this.tsmiConnection.Click += new System.EventHandler(this.tsmiConnection_Click);
            // 
            // tsmiPorts
            // 
            this.tsmiPorts.Name = "tsmiPorts";
            this.tsmiPorts.Size = new System.Drawing.Size(120, 26);
            this.tsmiPorts.Text = "Ports";
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(117, 6);
            // 
            // tsmiReset
            // 
            this.tsmiReset.Name = "tsmiReset";
            this.tsmiReset.Size = new System.Drawing.Size(120, 26);
            this.tsmiReset.Text = "Reset";
            this.tsmiReset.Click += new System.EventHandler(this.tsmiReset_Click);
            // 
            // statBar
            // 
            this.statBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblIsConnected});
            this.statBar.Location = new System.Drawing.Point(0, 637);
            this.statBar.Name = "statBar";
            this.statBar.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statBar.Size = new System.Drawing.Size(1335, 25);
            this.statBar.TabIndex = 12;
            this.statBar.Text = "statusStrip1";
            // 
            // lblIsConnected
            // 
            this.lblIsConnected.Name = "lblIsConnected";
            this.lblIsConnected.Size = new System.Drawing.Size(119, 20);
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
            this.tblMain.Location = new System.Drawing.Point(0, 28);
            this.tblMain.Margin = new System.Windows.Forms.Padding(4);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 1;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMain.Size = new System.Drawing.Size(1335, 609);
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
            this.tblControls.Location = new System.Drawing.Point(4, 4);
            this.tblControls.Margin = new System.Windows.Forms.Padding(4);
            this.tblControls.Name = "tblControls";
            this.tblControls.RowCount = 2;
            this.tblControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.73251F));
            this.tblControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.26749F));
            this.tblControls.Size = new System.Drawing.Size(381, 601);
            this.tblControls.TabIndex = 0;
            // 
            // txtState
            // 
            this.txtState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtState.Location = new System.Drawing.Point(4, 332);
            this.txtState.Margin = new System.Windows.Forms.Padding(4);
            this.txtState.Multiline = true;
            this.txtState.Name = "txtState";
            this.txtState.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtState.Size = new System.Drawing.Size(373, 265);
            this.txtState.TabIndex = 7;
            // 
            // gpbControls
            // 
            this.gpbControls.Controls.Add(this.btnRight);
            this.gpbControls.Controls.Add(this.btnStop);
            this.gpbControls.Controls.Add(this.btnBackward);
            this.gpbControls.Controls.Add(this.btnLeft);
            this.gpbControls.Controls.Add(this.btnForward);
            this.gpbControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gpbControls.Location = new System.Drawing.Point(4, 4);
            this.gpbControls.Margin = new System.Windows.Forms.Padding(4);
            this.gpbControls.Name = "gpbControls";
            this.gpbControls.Padding = new System.Windows.Forms.Padding(4);
            this.gpbControls.Size = new System.Drawing.Size(373, 320);
            this.gpbControls.TabIndex = 0;
            this.gpbControls.TabStop = false;
            this.gpbControls.Text = "Controls";
            // 
            // btnRight
            // 
            this.btnRight.Image = global::KarelV1.Images.ArrowRight;
            this.btnRight.Location = new System.Drawing.Point(219, 121);
            this.btnRight.Margin = new System.Windows.Forms.Padding(4);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(97, 90);
            this.btnRight.TabIndex = 18;
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnStop
            // 
            this.btnStop.Image = global::KarelV1.Images.Stop;
            this.btnStop.Location = new System.Drawing.Point(113, 121);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(97, 90);
            this.btnStop.TabIndex = 17;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnBackward
            // 
            this.btnBackward.Image = global::KarelV1.Images.ArrowDown;
            this.btnBackward.Location = new System.Drawing.Point(113, 218);
            this.btnBackward.Margin = new System.Windows.Forms.Padding(4);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(97, 90);
            this.btnBackward.TabIndex = 16;
            this.btnBackward.UseVisualStyleBackColor = true;
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Image = global::KarelV1.Images.ArrowLeft;
            this.btnLeft.Location = new System.Drawing.Point(8, 121);
            this.btnLeft.Margin = new System.Windows.Forms.Padding(4);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(97, 90);
            this.btnLeft.TabIndex = 15;
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnForward
            // 
            this.btnForward.Image = global::KarelV1.Images.ArrowUp;
            this.btnForward.Location = new System.Drawing.Point(113, 23);
            this.btnForward.Margin = new System.Windows.Forms.Padding(4);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(97, 90);
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
            this.tcSensorsFunctions.Location = new System.Drawing.Point(393, 4);
            this.tcSensorsFunctions.Margin = new System.Windows.Forms.Padding(4);
            this.tcSensorsFunctions.Name = "tcSensorsFunctions";
            this.tcSensorsFunctions.SelectedIndex = 0;
            this.tcSensorsFunctions.Size = new System.Drawing.Size(938, 601);
            this.tcSensorsFunctions.TabIndex = 1;
            // 
            // tbpSonar
            // 
            this.tbpSonar.Controls.Add(this.tlpSonar);
            this.tbpSonar.Location = new System.Drawing.Point(4, 29);
            this.tbpSonar.Margin = new System.Windows.Forms.Padding(4);
            this.tbpSonar.Name = "tbpSonar";
            this.tbpSonar.Padding = new System.Windows.Forms.Padding(4);
            this.tbpSonar.Size = new System.Drawing.Size(930, 568);
            this.tbpSonar.TabIndex = 0;
            this.tbpSonar.Text = "Sonar";
            this.tbpSonar.UseVisualStyleBackColor = true;
            // 
            // tlpSonar
            // 
            this.tlpSonar.ColumnCount = 2;
            this.tlpSonar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSonar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpSonar.Controls.Add(this.pnlSonar, 1, 0);
            this.tlpSonar.Controls.Add(this.crtUltrasinicSensor, 0, 0);
            this.tlpSonar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSonar.Location = new System.Drawing.Point(4, 4);
            this.tlpSonar.Margin = new System.Windows.Forms.Padding(4);
            this.tlpSonar.Name = "tlpSonar";
            this.tlpSonar.RowCount = 1;
            this.tlpSonar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSonar.Size = new System.Drawing.Size(922, 560);
            this.tlpSonar.TabIndex = 12;
            // 
            // pnlSonar
            // 
            this.pnlSonar.Controls.Add(this.cbMetric);
            this.pnlSonar.Controls.Add(this.lblDegreeSensor);
            this.pnlSonar.Controls.Add(this.tbSensorPosition);
            this.pnlSonar.Controls.Add(this.btnGetUltrasonic);
            this.pnlSonar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSonar.Location = new System.Drawing.Point(806, 4);
            this.pnlSonar.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSonar.Name = "pnlSonar";
            this.pnlSonar.Size = new System.Drawing.Size(112, 552);
            this.pnlSonar.TabIndex = 13;
            // 
            // lblDegreeSensor
            // 
            this.lblDegreeSensor.AutoSize = true;
            this.lblDegreeSensor.Location = new System.Drawing.Point(50, 104);
            this.lblDegreeSensor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDegreeSensor.Name = "lblDegreeSensor";
            this.lblDegreeSensor.Size = new System.Drawing.Size(51, 20);
            this.lblDegreeSensor.TabIndex = 26;
            this.lblDegreeSensor.Text = "[deg]";
            // 
            // tbSensorPosition
            // 
            this.tbSensorPosition.Location = new System.Drawing.Point(4, 101);
            this.tbSensorPosition.Margin = new System.Windows.Forms.Padding(4);
            this.tbSensorPosition.Name = "tbSensorPosition";
            this.tbSensorPosition.Size = new System.Drawing.Size(38, 26);
            this.tbSensorPosition.TabIndex = 25;
            // 
            // btnGetUltrasonic
            // 
            this.btnGetUltrasonic.Image = global::KarelV1.Images.Sensor2;
            this.btnGetUltrasonic.Location = new System.Drawing.Point(4, 4);
            this.btnGetUltrasonic.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetUltrasonic.Name = "btnGetUltrasonic";
            this.btnGetUltrasonic.Size = new System.Drawing.Size(97, 90);
            this.btnGetUltrasonic.TabIndex = 24;
            this.btnGetUltrasonic.UseVisualStyleBackColor = true;
            this.btnGetUltrasonic.Click += new System.EventHandler(this.btnGetUltrasonic_Click);
            // 
            // crtUltrasinicSensor
            // 
            this.crtUltrasinicSensor.BackColor = System.Drawing.Color.Transparent;
            this.crtUltrasinicSensor.BackImageTransparentColor = System.Drawing.Color.Transparent;
            this.crtUltrasinicSensor.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea3.Area3DStyle.Rotation = 10;
            chartArea3.Name = "ChartArea1";
            this.crtUltrasinicSensor.ChartAreas.Add(chartArea3);
            this.crtUltrasinicSensor.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.crtUltrasinicSensor.Legends.Add(legend3);
            this.crtUltrasinicSensor.Location = new System.Drawing.Point(3, 3);
            this.crtUltrasinicSensor.Name = "crtUltrasinicSensor";
            series3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center;
            series3.BackSecondaryColor = System.Drawing.Color.Black;
            series3.BorderColor = System.Drawing.Color.Black;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar;
            series3.Color = System.Drawing.Color.Green;
            series3.Legend = "Legend1";
            series3.MarkerColor = System.Drawing.Color.Black;
            series3.Name = "Ultrasonic Sensor";
            this.crtUltrasinicSensor.Series.Add(series3);
            this.crtUltrasinicSensor.Size = new System.Drawing.Size(796, 554);
            this.crtUltrasinicSensor.TabIndex = 14;
            this.crtUltrasinicSensor.Text = "Sonar";
            // 
            // tbpGlyph
            // 
            this.tbpGlyph.Controls.Add(this.tblGlyph);
            this.tbpGlyph.Location = new System.Drawing.Point(4, 29);
            this.tbpGlyph.Margin = new System.Windows.Forms.Padding(4);
            this.tbpGlyph.Name = "tbpGlyph";
            this.tbpGlyph.Padding = new System.Windows.Forms.Padding(4);
            this.tbpGlyph.Size = new System.Drawing.Size(930, 568);
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
            this.tblGlyph.Location = new System.Drawing.Point(4, 4);
            this.tblGlyph.Margin = new System.Windows.Forms.Padding(4);
            this.tblGlyph.Name = "tblGlyph";
            this.tblGlyph.RowCount = 1;
            this.tblGlyph.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblGlyph.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblGlyph.Size = new System.Drawing.Size(922, 560);
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
            this.pnlGlyphControl.Location = new System.Drawing.Point(747, 4);
            this.pnlGlyphControl.Margin = new System.Windows.Forms.Padding(4);
            this.pnlGlyphControl.Name = "pnlGlyphControl";
            this.pnlGlyphControl.Size = new System.Drawing.Size(171, 552);
            this.pnlGlyphControl.TabIndex = 0;
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(8, 16);
            this.lblURL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(46, 20);
            this.lblURL.TabIndex = 24;
            this.lblURL.Text = "URL";
            // 
            // lblUTInterval
            // 
            this.lblUTInterval.AutoSize = true;
            this.lblUTInterval.Location = new System.Drawing.Point(8, 291);
            this.lblUTInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUTInterval.Name = "lblUTInterval";
            this.lblUTInterval.Size = new System.Drawing.Size(99, 20);
            this.lblUTInterval.TabIndex = 23;
            this.lblUTInterval.Text = "Time [ms]:";
            // 
            // tbCameraUpdateTime
            // 
            this.tbCameraUpdateTime.Location = new System.Drawing.Point(115, 288);
            this.tbCameraUpdateTime.Margin = new System.Windows.Forms.Padding(4);
            this.tbCameraUpdateTime.Name = "tbCameraUpdateTime";
            this.tbCameraUpdateTime.Size = new System.Drawing.Size(35, 26);
            this.tbCameraUpdateTime.TabIndex = 22;
            this.tbCameraUpdateTime.Text = "500";
            // 
            // chkContinuesCapture
            // 
            this.chkContinuesCapture.AutoSize = true;
            this.chkContinuesCapture.Location = new System.Drawing.Point(12, 325);
            this.chkContinuesCapture.Margin = new System.Windows.Forms.Padding(4);
            this.chkContinuesCapture.Name = "chkContinuesCapture";
            this.chkContinuesCapture.Size = new System.Drawing.Size(97, 24);
            this.chkContinuesCapture.TabIndex = 21;
            this.chkContinuesCapture.Text = "Capture";
            this.chkContinuesCapture.UseVisualStyleBackColor = true;
            this.chkContinuesCapture.CheckedChanged += new System.EventHandler(this.chkContinuesCapture_CheckedChanged);
            // 
            // tbCameraIP
            // 
            this.tbCameraIP.Location = new System.Drawing.Point(12, 39);
            this.tbCameraIP.Margin = new System.Windows.Forms.Padding(4);
            this.tbCameraIP.Multiline = true;
            this.tbCameraIP.Name = "tbCameraIP";
            this.tbCameraIP.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbCameraIP.Size = new System.Drawing.Size(138, 122);
            this.tbCameraIP.TabIndex = 20;
            this.tbCameraIP.Text = "https://scontent-vie1-1.xx.fbcdn.net/hphotos-xlt1/v/t1.0-9/473_10201482439540779_" +
    "3601827889724477532_n.jpg?oh=310e1862af7b481da70be9ec3bca9563&oe=5753D41A";
            // 
            // btnCapture
            // 
            this.btnCapture.Image = global::KarelV1.Images.Capture;
            this.btnCapture.Location = new System.Drawing.Point(12, 169);
            this.btnCapture.Margin = new System.Windows.Forms.Padding(4);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(97, 90);
            this.btnCapture.TabIndex = 19;
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // pbGlyph
            // 
            this.pbGlyph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbGlyph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbGlyph.Location = new System.Drawing.Point(4, 4);
            this.pbGlyph.Margin = new System.Windows.Forms.Padding(4);
            this.pbGlyph.Name = "pbGlyph";
            this.pbGlyph.Size = new System.Drawing.Size(735, 552);
            this.pbGlyph.TabIndex = 1;
            this.pbGlyph.TabStop = false;
            // 
            // tbpSensors
            // 
            this.tbpSensors.Controls.Add(this.pnlSensors);
            this.tbpSensors.Location = new System.Drawing.Point(4, 29);
            this.tbpSensors.Margin = new System.Windows.Forms.Padding(4);
            this.tbpSensors.Name = "tbpSensors";
            this.tbpSensors.Size = new System.Drawing.Size(930, 568);
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
            this.pnlSensors.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSensors.Name = "pnlSensors";
            this.pnlSensors.Size = new System.Drawing.Size(930, 568);
            this.pnlSensors.TabIndex = 0;
            // 
            // btnLoginTest
            // 
            this.btnLoginTest.Location = new System.Drawing.Point(1229, 20);
            this.btnLoginTest.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoginTest.Name = "btnLoginTest";
            this.btnLoginTest.Size = new System.Drawing.Size(97, 90);
            this.btnLoginTest.TabIndex = 25;
            this.btnLoginTest.Text = "Login Test";
            this.btnLoginTest.UseVisualStyleBackColor = true;
            this.btnLoginTest.Click += new System.EventHandler(this.btnLoginTest_Click);
            // 
            // lblRobotPosition
            // 
            this.lblRobotPosition.AutoSize = true;
            this.lblRobotPosition.Location = new System.Drawing.Point(171, 303);
            this.lblRobotPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRobotPosition.Name = "lblRobotPosition";
            this.lblRobotPosition.Size = new System.Drawing.Size(83, 20);
            this.lblRobotPosition.TabIndex = 24;
            this.lblRobotPosition.Text = "Position:";
            // 
            // btnGetRobotPos
            // 
            this.btnGetRobotPos.Image = global::KarelV1.Images.Sensor3;
            this.btnGetRobotPos.Location = new System.Drawing.Point(175, 202);
            this.btnGetRobotPos.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetRobotPos.Name = "btnGetRobotPos";
            this.btnGetRobotPos.Size = new System.Drawing.Size(97, 90);
            this.btnGetRobotPos.TabIndex = 23;
            this.btnGetRobotPos.UseVisualStyleBackColor = true;
            this.btnGetRobotPos.Click += new System.EventHandler(this.btnGetRobotPos_Click);
            // 
            // prbRightSensor
            // 
            this.prbRightSensor.Location = new System.Drawing.Point(280, 58);
            this.prbRightSensor.Margin = new System.Windows.Forms.Padding(4);
            this.prbRightSensor.Maximum = 1023;
            this.prbRightSensor.Name = "prbRightSensor";
            this.prbRightSensor.Size = new System.Drawing.Size(133, 28);
            this.prbRightSensor.TabIndex = 22;
            // 
            // prbLeftSensor
            // 
            this.prbLeftSensor.Location = new System.Drawing.Point(33, 58);
            this.prbLeftSensor.Margin = new System.Windows.Forms.Padding(4);
            this.prbLeftSensor.Maximum = 1023;
            this.prbLeftSensor.Name = "prbLeftSensor";
            this.prbLeftSensor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.prbLeftSensor.RightToLeftLayout = true;
            this.prbLeftSensor.Size = new System.Drawing.Size(133, 28);
            this.prbLeftSensor.TabIndex = 21;
            // 
            // btnGetSensors
            // 
            this.btnGetSensors.Image = global::KarelV1.Images.Sensor1;
            this.btnGetSensors.Location = new System.Drawing.Point(175, 32);
            this.btnGetSensors.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetSensors.Name = "btnGetSensors";
            this.btnGetSensors.Size = new System.Drawing.Size(97, 90);
            this.btnGetSensors.TabIndex = 20;
            this.btnGetSensors.UseVisualStyleBackColor = true;
            this.btnGetSensors.Click += new System.EventHandler(this.btnGetSensors_Click);
            // 
            // cbMetric
            // 
            this.cbMetric.FormattingEnabled = true;
            this.cbMetric.Location = new System.Drawing.Point(3, 134);
            this.cbMetric.Name = "cbMetric";
            this.cbMetric.Size = new System.Drawing.Size(98, 28);
            this.cbMetric.TabIndex = 27;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // talkToolStripMenuItem
            // 
            this.talkToolStripMenuItem.Name = "talkToolStripMenuItem";
            this.talkToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.talkToolStripMenuItem.Text = "Talk";
            this.talkToolStripMenuItem.Click += new System.EventHandler(this.talkToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1335, 662);
            this.Controls.Add(this.tblMain);
            this.Controls.Add(this.statBar);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(4);
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
            this.tlpSonar.ResumeLayout(false);
            this.pnlSonar.ResumeLayout(false);
            this.pnlSonar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crtUltrasinicSensor)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem tsmiConnection;
        private System.Windows.Forms.ToolStripMenuItem tsmiPorts;
        private System.Windows.Forms.StatusStrip statBar;
        private System.Windows.Forms.ToolStripStatusLabel lblIsConnected;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.TableLayoutPanel tblControls;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.GroupBox gpbControls;
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
        private System.Windows.Forms.TableLayoutPanel tlpSonar;
        private System.Windows.Forms.Panel pnlSonar;
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
        private System.Windows.Forms.DataVisualization.Charting.Chart crtUltrasinicSensor;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripSeparator tsSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiReset;
        private System.Windows.Forms.ComboBox cbMetric;
        private System.Windows.Forms.ToolStripMenuItem talkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

