namespace fgSolver
{
    partial class MotorView
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grid = new System.Windows.Forms.PropertyGrid();
            this.udPosition = new System.Windows.Forms.NumericUpDown();
            this.udSpeed = new System.Windows.Forms.NumericUpDown();
            this.udAbsolute = new System.Windows.Forms.NumericUpDown();
            this.udSteps = new System.Windows.Forms.NumericUpDown();
            this.btnMoveAbsolute = new System.Windows.Forms.Button();
            this.btnSetPosition = new System.Windows.Forms.Button();
            this.btnSetSpeed = new System.Windows.Forms.Button();
            this.btnMoveSteps = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnDisable = new System.Windows.Forms.Button();
            this.btnEnable = new System.Windows.Forms.Button();
            this.lblSelected = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEnableAll = new System.Windows.Forms.Button();
            this.btnDisableAll = new System.Windows.Forms.Button();
            this.btnSetPositionAll = new System.Windows.Forms.Button();
            this.udSetPositionAll = new System.Windows.Forms.NumericUpDown();
            this.btnSetSpeedAll = new System.Windows.Forms.Button();
            this.udSetSpeedAll = new System.Windows.Forms.NumericUpDown();
            this.btnStopAll = new System.Windows.Forms.Button();
            this.ledBulb9 = new Bulb.LedBulb();
            this.ledBulb8 = new Bulb.LedBulb();
            this.ledBulb7 = new Bulb.LedBulb();
            this.ledBulb6 = new Bulb.LedBulb();
            this.ledBulb5 = new Bulb.LedBulb();
            this.ledBulb4 = new Bulb.LedBulb();
            this.ledBulb3 = new Bulb.LedBulb();
            this.ledBulb2 = new Bulb.LedBulb();
            this.ledBulb1 = new Bulb.LedBulb();
            this.ledEnabled = new Bulb.LedBulb();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAbsolute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udSetPositionAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSetSpeedAll)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            this.radioButton1.BackColor = System.Drawing.Color.Red;
            this.radioButton1.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton1.Location = new System.Drawing.Point(294, 146);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(16, 16);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.Tag = "Z2";
            this.radioButton1.UseVisualStyleBackColor = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.BackColor = System.Drawing.Color.Red;
            this.radioButton2.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton2.Location = new System.Drawing.Point(272, 160);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(16, 16);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.Tag = "Z1";
            this.radioButton2.UseVisualStyleBackColor = false;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.BackColor = System.Drawing.Color.Red;
            this.radioButton3.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton3.Location = new System.Drawing.Point(99, 135);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(16, 16);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.Tag = "Y2";
            this.radioButton3.UseVisualStyleBackColor = false;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.BackColor = System.Drawing.Color.Red;
            this.radioButton4.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton4.Location = new System.Drawing.Point(116, 169);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(16, 16);
            this.radioButton4.TabIndex = 4;
            this.radioButton4.Tag = "Y3";
            this.radioButton4.UseVisualStyleBackColor = false;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.BackColor = System.Drawing.Color.Red;
            this.radioButton5.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton5.Location = new System.Drawing.Point(140, 200);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(16, 16);
            this.radioButton5.TabIndex = 4;
            this.radioButton5.Tag = "Y1";
            this.radioButton5.UseVisualStyleBackColor = false;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioButton6
            // 
            this.radioButton6.BackColor = System.Drawing.Color.Red;
            this.radioButton6.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton6.Location = new System.Drawing.Point(536, 126);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(16, 16);
            this.radioButton6.TabIndex = 4;
            this.radioButton6.Tag = "X2";
            this.radioButton6.UseVisualStyleBackColor = false;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioButton7
            // 
            this.radioButton7.BackColor = System.Drawing.Color.Red;
            this.radioButton7.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton7.Location = new System.Drawing.Point(536, 94);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(16, 16);
            this.radioButton7.TabIndex = 4;
            this.radioButton7.Tag = "X1";
            this.radioButton7.UseVisualStyleBackColor = false;
            this.radioButton7.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioButton8
            // 
            this.radioButton8.BackColor = System.Drawing.Color.Red;
            this.radioButton8.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton8.Location = new System.Drawing.Point(473, 146);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(16, 16);
            this.radioButton8.TabIndex = 4;
            this.radioButton8.Tag = "X3";
            this.radioButton8.UseVisualStyleBackColor = false;
            this.radioButton8.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioButton9
            // 
            this.radioButton9.BackColor = System.Drawing.Color.Red;
            this.radioButton9.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton9.Location = new System.Drawing.Point(516, 250);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(16, 16);
            this.radioButton9.TabIndex = 4;
            this.radioButton9.Tag = "Z3";
            this.radioButton9.UseVisualStyleBackColor = false;
            this.radioButton9.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.txtPosition);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.grid);
            this.panel1.Controls.Add(this.udPosition);
            this.panel1.Controls.Add(this.udSpeed);
            this.panel1.Controls.Add(this.udAbsolute);
            this.panel1.Controls.Add(this.udSteps);
            this.panel1.Controls.Add(this.btnMoveAbsolute);
            this.panel1.Controls.Add(this.btnSetPosition);
            this.panel1.Controls.Add(this.btnSetSpeed);
            this.panel1.Controls.Add(this.ledEnabled);
            this.panel1.Controls.Add(this.btnMoveSteps);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnDisable);
            this.panel1.Controls.Add(this.btnEnable);
            this.panel1.Controls.Add(this.lblSelected);
            this.panel1.Location = new System.Drawing.Point(3, 322);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(675, 291);
            this.panel1.TabIndex = 5;
            // 
            // txtPosition
            // 
            this.txtPosition.Location = new System.Drawing.Point(353, 96);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.ReadOnly = true;
            this.txtPosition.Size = new System.Drawing.Size(120, 20);
            this.txtPosition.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(300, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Position :";
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grid.HelpVisible = false;
            this.grid.Location = new System.Drawing.Point(0, 29);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(278, 247);
            this.grid.TabIndex = 3;
            this.grid.ToolbarVisible = false;
            this.grid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.grid_PropertyValueChanged);
            // 
            // udPosition
            // 
            this.udPosition.Location = new System.Drawing.Point(353, 208);
            this.udPosition.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.udPosition.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.udPosition.Name = "udPosition";
            this.udPosition.Size = new System.Drawing.Size(120, 20);
            this.udPosition.TabIndex = 2;
            // 
            // udSpeed
            // 
            this.udSpeed.Location = new System.Drawing.Point(353, 179);
            this.udSpeed.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.udSpeed.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.udSpeed.Name = "udSpeed";
            this.udSpeed.Size = new System.Drawing.Size(120, 20);
            this.udSpeed.TabIndex = 2;
            this.udSpeed.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // udAbsolute
            // 
            this.udAbsolute.Location = new System.Drawing.Point(353, 124);
            this.udAbsolute.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.udAbsolute.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.udAbsolute.Name = "udAbsolute";
            this.udAbsolute.Size = new System.Drawing.Size(120, 20);
            this.udAbsolute.TabIndex = 2;
            // 
            // udSteps
            // 
            this.udSteps.Location = new System.Drawing.Point(353, 150);
            this.udSteps.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.udSteps.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.udSteps.Name = "udSteps";
            this.udSteps.Size = new System.Drawing.Size(120, 20);
            this.udSteps.TabIndex = 2;
            // 
            // btnMoveAbsolute
            // 
            this.btnMoveAbsolute.Location = new System.Drawing.Point(479, 121);
            this.btnMoveAbsolute.Name = "btnMoveAbsolute";
            this.btnMoveAbsolute.Size = new System.Drawing.Size(92, 23);
            this.btnMoveAbsolute.TabIndex = 1;
            this.btnMoveAbsolute.Text = "Move absolute";
            this.btnMoveAbsolute.UseVisualStyleBackColor = true;
            this.btnMoveAbsolute.Click += new System.EventHandler(this.btnMoveAbsolute_Click);
            // 
            // btnSetPosition
            // 
            this.btnSetPosition.Location = new System.Drawing.Point(479, 205);
            this.btnSetPosition.Name = "btnSetPosition";
            this.btnSetPosition.Size = new System.Drawing.Size(92, 23);
            this.btnSetPosition.TabIndex = 1;
            this.btnSetPosition.Text = "Set position";
            this.btnSetPosition.UseVisualStyleBackColor = true;
            this.btnSetPosition.Click += new System.EventHandler(this.btnSetPosition_Click);
            // 
            // btnSetSpeed
            // 
            this.btnSetSpeed.Location = new System.Drawing.Point(479, 176);
            this.btnSetSpeed.Name = "btnSetSpeed";
            this.btnSetSpeed.Size = new System.Drawing.Size(92, 23);
            this.btnSetSpeed.TabIndex = 1;
            this.btnSetSpeed.Text = "Set speed";
            this.btnSetSpeed.UseVisualStyleBackColor = true;
            this.btnSetSpeed.Click += new System.EventHandler(this.btnSetSpeed_Click);
            // 
            // btnMoveSteps
            // 
            this.btnMoveSteps.Location = new System.Drawing.Point(479, 147);
            this.btnMoveSteps.Name = "btnMoveSteps";
            this.btnMoveSteps.Size = new System.Drawing.Size(92, 23);
            this.btnMoveSteps.TabIndex = 1;
            this.btnMoveSteps.Text = "Move relative";
            this.btnMoveSteps.UseVisualStyleBackColor = true;
            this.btnMoveSteps.Click += new System.EventHandler(this.btnMoveSteps_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.Red;
            this.btnStop.Location = new System.Drawing.Point(450, 43);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnDisable
            // 
            this.btnDisable.Location = new System.Drawing.Point(353, 58);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(75, 23);
            this.btnDisable.TabIndex = 1;
            this.btnDisable.Text = "Disable";
            this.btnDisable.UseVisualStyleBackColor = true;
            this.btnDisable.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // btnEnable
            // 
            this.btnEnable.Location = new System.Drawing.Point(353, 29);
            this.btnEnable.Name = "btnEnable";
            this.btnEnable.Size = new System.Drawing.Size(75, 23);
            this.btnEnable.TabIndex = 1;
            this.btnEnable.Text = "Enable";
            this.btnEnable.UseVisualStyleBackColor = true;
            this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // lblSelected
            // 
            this.lblSelected.AutoSize = true;
            this.lblSelected.Location = new System.Drawing.Point(7, 8);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(35, 13);
            this.lblSelected.TabIndex = 0;
            this.lblSelected.Text = "label1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::fgSolver.Properties.Resources.View3;
            this.pictureBox2.Location = new System.Drawing.Point(420, 1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(258, 315);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::fgSolver.Properties.Resources.View2;
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(418, 315);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.btnEnableAll);
            this.groupBox1.Controls.Add(this.btnDisableAll);
            this.groupBox1.Controls.Add(this.btnSetPositionAll);
            this.groupBox1.Controls.Add(this.udSetPositionAll);
            this.groupBox1.Controls.Add(this.btnSetSpeedAll);
            this.groupBox1.Controls.Add(this.udSetSpeedAll);
            this.groupBox1.Controls.Add(this.btnStopAll);
            this.groupBox1.Location = new System.Drawing.Point(3, 619);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(675, 122);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Action groupée";
            // 
            // btnEnableAll
            // 
            this.btnEnableAll.Location = new System.Drawing.Point(10, 19);
            this.btnEnableAll.Name = "btnEnableAll";
            this.btnEnableAll.Size = new System.Drawing.Size(75, 23);
            this.btnEnableAll.TabIndex = 1;
            this.btnEnableAll.Text = "Enable all";
            this.btnEnableAll.UseVisualStyleBackColor = true;
            this.btnEnableAll.Click += new System.EventHandler(this.btnEnableAll_Click);
            // 
            // btnDisableAll
            // 
            this.btnDisableAll.Location = new System.Drawing.Point(96, 19);
            this.btnDisableAll.Name = "btnDisableAll";
            this.btnDisableAll.Size = new System.Drawing.Size(75, 23);
            this.btnDisableAll.TabIndex = 1;
            this.btnDisableAll.Text = "Disable all";
            this.btnDisableAll.UseVisualStyleBackColor = true;
            this.btnDisableAll.Click += new System.EventHandler(this.btnDisableAll_Click);
            // 
            // btnSetPositionAll
            // 
            this.btnSetPositionAll.Location = new System.Drawing.Point(137, 80);
            this.btnSetPositionAll.Name = "btnSetPositionAll";
            this.btnSetPositionAll.Size = new System.Drawing.Size(92, 23);
            this.btnSetPositionAll.TabIndex = 1;
            this.btnSetPositionAll.Text = "Set position all";
            this.btnSetPositionAll.UseVisualStyleBackColor = true;
            this.btnSetPositionAll.Click += new System.EventHandler(this.btnSetPositionAll_Click);
            // 
            // udSetPositionAll
            // 
            this.udSetPositionAll.Location = new System.Drawing.Point(11, 83);
            this.udSetPositionAll.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.udSetPositionAll.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.udSetPositionAll.Name = "udSetPositionAll";
            this.udSetPositionAll.Size = new System.Drawing.Size(120, 20);
            this.udSetPositionAll.TabIndex = 2;
            // 
            // btnSetSpeedAll
            // 
            this.btnSetSpeedAll.Location = new System.Drawing.Point(137, 51);
            this.btnSetSpeedAll.Name = "btnSetSpeedAll";
            this.btnSetSpeedAll.Size = new System.Drawing.Size(92, 23);
            this.btnSetSpeedAll.TabIndex = 1;
            this.btnSetSpeedAll.Text = "Set speed all";
            this.btnSetSpeedAll.UseVisualStyleBackColor = true;
            this.btnSetSpeedAll.Click += new System.EventHandler(this.btnSetSpeedAll_Click);
            // 
            // udSetSpeedAll
            // 
            this.udSetSpeedAll.Location = new System.Drawing.Point(11, 54);
            this.udSetSpeedAll.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.udSetSpeedAll.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.udSetSpeedAll.Name = "udSetSpeedAll";
            this.udSetSpeedAll.Size = new System.Drawing.Size(120, 20);
            this.udSetSpeedAll.TabIndex = 2;
            this.udSetSpeedAll.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // btnStopAll
            // 
            this.btnStopAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopAll.ForeColor = System.Drawing.Color.Red;
            this.btnStopAll.Location = new System.Drawing.Point(203, 19);
            this.btnStopAll.Name = "btnStopAll";
            this.btnStopAll.Size = new System.Drawing.Size(75, 23);
            this.btnStopAll.TabIndex = 1;
            this.btnStopAll.Text = "STOP ALL";
            this.btnStopAll.UseVisualStyleBackColor = true;
            this.btnStopAll.Click += new System.EventHandler(this.btnStopAll_Click);
            // 
            // ledBulb9
            // 
            this.ledBulb9.BackColor = System.Drawing.Color.White;
            this.ledBulb9.Location = new System.Drawing.Point(56, 183);
            this.ledBulb9.Name = "ledBulb9";
            this.ledBulb9.On = false;
            this.ledBulb9.Size = new System.Drawing.Size(26, 26);
            this.ledBulb9.TabIndex = 6;
            this.ledBulb9.Tag = "Y3";
            this.ledBulb9.Text = "ledBulb1";
            // 
            // ledBulb8
            // 
            this.ledBulb8.BackColor = System.Drawing.Color.White;
            this.ledBulb8.Location = new System.Drawing.Point(514, 269);
            this.ledBulb8.Name = "ledBulb8";
            this.ledBulb8.On = false;
            this.ledBulb8.Size = new System.Drawing.Size(26, 26);
            this.ledBulb8.TabIndex = 6;
            this.ledBulb8.Tag = "Z3";
            this.ledBulb8.Text = "ledBulb1";
            // 
            // ledBulb7
            // 
            this.ledBulb7.BackColor = System.Drawing.Color.Black;
            this.ledBulb7.Location = new System.Drawing.Point(453, 190);
            this.ledBulb7.Name = "ledBulb7";
            this.ledBulb7.On = false;
            this.ledBulb7.Size = new System.Drawing.Size(26, 26);
            this.ledBulb7.TabIndex = 6;
            this.ledBulb7.Tag = "X3";
            this.ledBulb7.Text = "ledBulb1";
            // 
            // ledBulb6
            // 
            this.ledBulb6.BackColor = System.Drawing.Color.DimGray;
            this.ledBulb6.Location = new System.Drawing.Point(529, 159);
            this.ledBulb6.Name = "ledBulb6";
            this.ledBulb6.On = false;
            this.ledBulb6.Size = new System.Drawing.Size(26, 26);
            this.ledBulb6.TabIndex = 6;
            this.ledBulb6.Tag = "X2";
            this.ledBulb6.Text = "ledBulb1";
            // 
            // ledBulb5
            // 
            this.ledBulb5.BackColor = System.Drawing.Color.DimGray;
            this.ledBulb5.Location = new System.Drawing.Point(529, 30);
            this.ledBulb5.Name = "ledBulb5";
            this.ledBulb5.On = false;
            this.ledBulb5.Size = new System.Drawing.Size(26, 26);
            this.ledBulb5.TabIndex = 6;
            this.ledBulb5.Tag = "X1";
            this.ledBulb5.Text = "ledBulb1";
            // 
            // ledBulb4
            // 
            this.ledBulb4.BackColor = System.Drawing.Color.Black;
            this.ledBulb4.Location = new System.Drawing.Point(343, 94);
            this.ledBulb4.Name = "ledBulb4";
            this.ledBulb4.On = false;
            this.ledBulb4.Size = new System.Drawing.Size(26, 26);
            this.ledBulb4.TabIndex = 6;
            this.ledBulb4.Tag = "Z2";
            this.ledBulb4.Text = "ledBulb1";
            // 
            // ledBulb3
            // 
            this.ledBulb3.BackColor = System.Drawing.Color.Black;
            this.ledBulb3.Location = new System.Drawing.Point(151, 222);
            this.ledBulb3.Name = "ledBulb3";
            this.ledBulb3.On = false;
            this.ledBulb3.Size = new System.Drawing.Size(26, 26);
            this.ledBulb3.TabIndex = 6;
            this.ledBulb3.Tag = "Y1";
            this.ledBulb3.Text = "ledBulb1";
            // 
            // ledBulb2
            // 
            this.ledBulb2.BackColor = System.Drawing.Color.DimGray;
            this.ledBulb2.Location = new System.Drawing.Point(206, 169);
            this.ledBulb2.Name = "ledBulb2";
            this.ledBulb2.On = false;
            this.ledBulb2.Size = new System.Drawing.Size(26, 26);
            this.ledBulb2.TabIndex = 6;
            this.ledBulb2.Tag = "Z1";
            this.ledBulb2.Text = "ledBulb1";
            // 
            // ledBulb1
            // 
            this.ledBulb1.BackColor = System.Drawing.Color.Black;
            this.ledBulb1.Location = new System.Drawing.Point(42, 64);
            this.ledBulb1.Name = "ledBulb1";
            this.ledBulb1.On = false;
            this.ledBulb1.Size = new System.Drawing.Size(26, 26);
            this.ledBulb1.TabIndex = 6;
            this.ledBulb1.Tag = "Y2";
            this.ledBulb1.Text = "ledBulb1";
            // 
            // ledEnabled
            // 
            this.ledEnabled.BackColor = System.Drawing.SystemColors.Control;
            this.ledEnabled.Location = new System.Drawing.Point(321, 43);
            this.ledEnabled.Name = "ledEnabled";
            this.ledEnabled.On = true;
            this.ledEnabled.Size = new System.Drawing.Size(26, 26);
            this.ledEnabled.TabIndex = 6;
            this.ledEnabled.Tag = "Z1";
            this.ledEnabled.Text = "ledBulb1";
            // 
            // MotorView
            // 
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ledBulb9);
            this.Controls.Add(this.ledBulb8);
            this.Controls.Add(this.ledBulb7);
            this.Controls.Add(this.ledBulb6);
            this.Controls.Add(this.ledBulb5);
            this.Controls.Add(this.ledBulb4);
            this.Controls.Add(this.ledBulb3);
            this.Controls.Add(this.ledBulb2);
            this.Controls.Add(this.ledBulb1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radioButton9);
            this.Controls.Add(this.radioButton8);
            this.Controls.Add(this.radioButton7);
            this.Controls.Add(this.radioButton6);
            this.Controls.Add(this.radioButton5);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MotorView";
            this.Size = new System.Drawing.Size(745, 744);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAbsolute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udSetPositionAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSetSpeedAll)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Button btnDisable;
        private System.Windows.Forms.Button btnEnable;
        private System.Windows.Forms.NumericUpDown udSteps;
        private System.Windows.Forms.Button btnMoveSteps;
        private System.Windows.Forms.NumericUpDown udSpeed;
        private System.Windows.Forms.Button btnSetSpeed;
        private System.Windows.Forms.PropertyGrid grid;
        private Bulb.LedBulb ledBulb1;
        private Bulb.LedBulb ledBulb2;
        private Bulb.LedBulb ledBulb3;
        private Bulb.LedBulb ledBulb4;
        private Bulb.LedBulb ledBulb5;
        private Bulb.LedBulb ledBulb6;
        private Bulb.LedBulb ledBulb7;
        private Bulb.LedBulb ledBulb8;
        private Bulb.LedBulb ledBulb9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown udAbsolute;
        private System.Windows.Forms.Button btnMoveAbsolute;
        private Bulb.LedBulb ledEnabled;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.NumericUpDown udPosition;
        private System.Windows.Forms.Button btnSetPosition;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnEnableAll;
        private System.Windows.Forms.Button btnDisableAll;
        private System.Windows.Forms.Button btnSetPositionAll;
        private System.Windows.Forms.NumericUpDown udSetPositionAll;
        private System.Windows.Forms.Button btnSetSpeedAll;
        private System.Windows.Forms.NumericUpDown udSetSpeedAll;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStopAll;
    }
}
