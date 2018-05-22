namespace fgSolver
{
    partial class CubeViewer
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
            this.components = new System.ComponentModel.Container();
            this.pnlDebug = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAxe = new System.Windows.Forms.ComboBox();
            this.udSpeed = new System.Windows.Forms.NumericUpDown();
            this.udCourronneMidMin = new System.Windows.Forms.NumericUpDown();
            this.udAmount = new System.Windows.Forms.NumericUpDown();
            this.udCourronneMidMax = new System.Windows.Forms.NumericUpDown();
            this.udEndLayer = new System.Windows.Forms.NumericUpDown();
            this.udCourronneMax = new System.Windows.Forms.NumericUpDown();
            this.udStartLayer = new System.Windows.Forms.NumericUpDown();
            this.txtMove = new System.Windows.Forms.TextBox();
            this.btnSolve = new System.Windows.Forms.Button();
            this.btnTestMotorMoves = new System.Windows.Forms.Button();
            this.btnTestMoves = new System.Windows.Forms.Button();
            this.btnDebug = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlDebug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCourronneMidMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCourronneMidMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udEndLayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCourronneMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udStartLayer)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDebug
            // 
            this.pnlDebug.Controls.Add(this.label1);
            this.pnlDebug.Controls.Add(this.cbAxe);
            this.pnlDebug.Controls.Add(this.udSpeed);
            this.pnlDebug.Controls.Add(this.udCourronneMidMin);
            this.pnlDebug.Controls.Add(this.udAmount);
            this.pnlDebug.Controls.Add(this.udCourronneMidMax);
            this.pnlDebug.Controls.Add(this.udEndLayer);
            this.pnlDebug.Controls.Add(this.udCourronneMax);
            this.pnlDebug.Controls.Add(this.udStartLayer);
            this.pnlDebug.Controls.Add(this.txtMove);
            this.pnlDebug.Controls.Add(this.btnSolve);
            this.pnlDebug.Controls.Add(this.btnTestMotorMoves);
            this.pnlDebug.Controls.Add(this.btnTestMoves);
            this.pnlDebug.Controls.Add(this.btnDebug);
            this.pnlDebug.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDebug.Location = new System.Drawing.Point(0, 326);
            this.pnlDebug.Name = "pnlDebug";
            this.pnlDebug.Size = new System.Drawing.Size(498, 67);
            this.pnlDebug.TabIndex = 0;
            this.pnlDebug.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(374, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Vitesse:";
            // 
            // cbAxe
            // 
            this.cbAxe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAxe.FormattingEnabled = true;
            this.cbAxe.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.cbAxe.Location = new System.Drawing.Point(101, 32);
            this.cbAxe.Name = "cbAxe";
            this.cbAxe.Size = new System.Drawing.Size(33, 21);
            this.cbAxe.TabIndex = 3;
            // 
            // udSpeed
            // 
            this.udSpeed.Location = new System.Drawing.Point(418, 6);
            this.udSpeed.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.udSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udSpeed.Name = "udSpeed";
            this.udSpeed.Size = new System.Drawing.Size(34, 20);
            this.udSpeed.TabIndex = 2;
            this.toolTip.SetToolTip(this.udSpeed, "Vitesse");
            this.udSpeed.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.udSpeed.ValueChanged += new System.EventHandler(this.udSpeed_ValueChanged);
            // 
            // udCourronneMidMin
            // 
            this.udCourronneMidMin.Location = new System.Drawing.Point(240, 33);
            this.udCourronneMidMin.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.udCourronneMidMin.Name = "udCourronneMidMin";
            this.udCourronneMidMin.Size = new System.Drawing.Size(44, 20);
            this.udCourronneMidMin.TabIndex = 2;
            this.toolTip.SetToolTip(this.udCourronneMidMin, "courrone 2");
            this.udCourronneMidMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // udAmount
            // 
            this.udAmount.Location = new System.Drawing.Point(240, 4);
            this.udAmount.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.udAmount.Name = "udAmount";
            this.udAmount.Size = new System.Drawing.Size(44, 20);
            this.udAmount.TabIndex = 2;
            this.toolTip.SetToolTip(this.udAmount, "Quantité");
            this.udAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // udCourronneMidMax
            // 
            this.udCourronneMidMax.Location = new System.Drawing.Point(190, 33);
            this.udCourronneMidMax.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.udCourronneMidMax.Name = "udCourronneMidMax";
            this.udCourronneMidMax.Size = new System.Drawing.Size(44, 20);
            this.udCourronneMidMax.TabIndex = 2;
            this.toolTip.SetToolTip(this.udCourronneMidMax, "courrone 3");
            this.udCourronneMidMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // udEndLayer
            // 
            this.udEndLayer.Location = new System.Drawing.Point(190, 4);
            this.udEndLayer.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.udEndLayer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udEndLayer.Name = "udEndLayer";
            this.udEndLayer.Size = new System.Drawing.Size(44, 20);
            this.udEndLayer.TabIndex = 2;
            this.toolTip.SetToolTip(this.udEndLayer, "End layer");
            this.udEndLayer.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // udCourronneMax
            // 
            this.udCourronneMax.Location = new System.Drawing.Point(140, 33);
            this.udCourronneMax.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.udCourronneMax.Name = "udCourronneMax";
            this.udCourronneMax.Size = new System.Drawing.Size(44, 20);
            this.udCourronneMax.TabIndex = 2;
            this.toolTip.SetToolTip(this.udCourronneMax, "courrone 4");
            this.udCourronneMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // udStartLayer
            // 
            this.udStartLayer.Location = new System.Drawing.Point(140, 4);
            this.udStartLayer.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.udStartLayer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udStartLayer.Name = "udStartLayer";
            this.udStartLayer.Size = new System.Drawing.Size(44, 20);
            this.udStartLayer.TabIndex = 2;
            this.toolTip.SetToolTip(this.udStartLayer, "Start layer");
            this.udStartLayer.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtMove
            // 
            this.txtMove.Location = new System.Drawing.Point(101, 4);
            this.txtMove.Name = "txtMove";
            this.txtMove.Size = new System.Drawing.Size(33, 20);
            this.txtMove.TabIndex = 1;
            this.txtMove.Text = "u";
            this.toolTip.SetToolTip(this.txtMove, "base");
            // 
            // btnSolve
            // 
            this.btnSolve.Location = new System.Drawing.Point(3, 33);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(75, 23);
            this.btnSolve.TabIndex = 0;
            this.btnSolve.Text = "Résoudre";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // btnTestMotorMoves
            // 
            this.btnTestMotorMoves.Location = new System.Drawing.Point(290, 32);
            this.btnTestMotorMoves.Name = "btnTestMotorMoves";
            this.btnTestMotorMoves.Size = new System.Drawing.Size(75, 23);
            this.btnTestMotorMoves.TabIndex = 0;
            this.btnTestMotorMoves.Text = "Test";
            this.btnTestMotorMoves.UseVisualStyleBackColor = true;
            this.btnTestMotorMoves.Click += new System.EventHandler(this.btnTestMotorMoves_Click);
            // 
            // btnTestMoves
            // 
            this.btnTestMoves.Location = new System.Drawing.Point(290, 3);
            this.btnTestMoves.Name = "btnTestMoves";
            this.btnTestMoves.Size = new System.Drawing.Size(75, 23);
            this.btnTestMoves.TabIndex = 0;
            this.btnTestMoves.Text = "Test";
            this.btnTestMoves.UseVisualStyleBackColor = true;
            this.btnTestMoves.Click += new System.EventHandler(this.btnTestMoves_Click);
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(3, 3);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(75, 23);
            this.btnDebug.TabIndex = 0;
            this.btnDebug.Text = "Debug tools";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // CubeViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlDebug);
            this.Name = "CubeViewer";
            this.Size = new System.Drawing.Size(498, 393);
            this.pnlDebug.ResumeLayout(false);
            this.pnlDebug.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCourronneMidMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCourronneMidMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udEndLayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCourronneMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udStartLayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDebug;
        private System.Windows.Forms.Button btnTestMoves;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.NumericUpDown udAmount;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.NumericUpDown udEndLayer;
        private System.Windows.Forms.NumericUpDown udStartLayer;
        private System.Windows.Forms.TextBox txtMove;
        private System.Windows.Forms.ComboBox cbAxe;
        private System.Windows.Forms.NumericUpDown udCourronneMidMin;
        private System.Windows.Forms.NumericUpDown udCourronneMidMax;
        private System.Windows.Forms.NumericUpDown udCourronneMax;
        private System.Windows.Forms.Button btnTestMotorMoves;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown udSpeed;
    }
}
