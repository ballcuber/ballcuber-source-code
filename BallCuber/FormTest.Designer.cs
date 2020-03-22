namespace Ballcuber
{
    partial class FormTest
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtColors = new System.Windows.Forms.TextBox();
            this.txtMoves = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.udMoves = new System.Windows.Forms.NumericUpDown();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pnlViewer = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMoveCount = new System.Windows.Forms.Label();
            this.txtCubeMoves = new System.Windows.Forms.RichTextBox();
            this.txtAlg = new System.Windows.Forms.TextBox();
            this.btnMoves = new System.Windows.Forms.Button();
            this.txtMachineMoves = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMotorMoves = new System.Windows.Forms.RichTextBox();
            this.txtNBMotorMoves = new System.Windows.Forms.Label();
            this.textLongMove = new System.Windows.Forms.TextBox();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnDev = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udMoves)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtColors);
            this.groupBox1.Controls.Add(this.txtMoves);
            this.groupBox1.Controls.Add(this.btnGo);
            this.groupBox1.Controls.Add(this.btnGenerate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.udMoves);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 196);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ballcuber";
            // 
            // txtColors
            // 
            this.txtColors.Location = new System.Drawing.Point(9, 100);
            this.txtColors.Name = "txtColors";
            this.txtColors.Size = new System.Drawing.Size(256, 20);
            this.txtColors.TabIndex = 3;
            // 
            // txtMoves
            // 
            this.txtMoves.Location = new System.Drawing.Point(9, 74);
            this.txtMoves.Name = "txtMoves";
            this.txtMoves.Size = new System.Drawing.Size(256, 20);
            this.txtMoves.TabIndex = 3;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(80, 131);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(80, 45);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Généré";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mouvements";
            // 
            // udMoves
            // 
            this.udMoves.Location = new System.Drawing.Point(80, 19);
            this.udMoves.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.udMoves.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udMoves.Name = "udMoves";
            this.udMoves.Size = new System.Drawing.Size(120, 20);
            this.udMoves.TabIndex = 0;
            this.udMoves.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // pnlViewer
            // 
            this.pnlViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlViewer.Location = new System.Drawing.Point(-4, 249);
            this.pnlViewer.Name = "pnlViewer";
            this.pnlViewer.Size = new System.Drawing.Size(1239, 432);
            this.pnlViewer.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Moves :";
            // 
            // lblMoveCount
            // 
            this.lblMoveCount.AutoSize = true;
            this.lblMoveCount.Location = new System.Drawing.Point(80, 222);
            this.lblMoveCount.Name = "lblMoveCount";
            this.lblMoveCount.Size = new System.Drawing.Size(13, 13);
            this.lblMoveCount.TabIndex = 3;
            this.lblMoveCount.Text = "0";
            // 
            // txtCubeMoves
            // 
            this.txtCubeMoves.Location = new System.Drawing.Point(356, 68);
            this.txtCubeMoves.Name = "txtCubeMoves";
            this.txtCubeMoves.Size = new System.Drawing.Size(244, 147);
            this.txtCubeMoves.TabIndex = 4;
            this.txtCubeMoves.Text = "";
            // 
            // txtAlg
            // 
            this.txtAlg.Location = new System.Drawing.Point(356, 3);
            this.txtAlg.Name = "txtAlg";
            this.txtAlg.Size = new System.Drawing.Size(521, 20);
            this.txtAlg.TabIndex = 3;
            // 
            // btnMoves
            // 
            this.btnMoves.Location = new System.Drawing.Point(600, 29);
            this.btnMoves.Name = "btnMoves";
            this.btnMoves.Size = new System.Drawing.Size(126, 23);
            this.btnMoves.TabIndex = 2;
            this.btnMoves.Text = "Motor Moves";
            this.btnMoves.UseVisualStyleBackColor = true;
            this.btnMoves.Click += new System.EventHandler(this.btnMoves_Click);
            // 
            // txtMachineMoves
            // 
            this.txtMachineMoves.Location = new System.Drawing.Point(624, 68);
            this.txtMachineMoves.Name = "txtMachineMoves";
            this.txtMachineMoves.Size = new System.Drawing.Size(253, 147);
            this.txtMachineMoves.TabIndex = 4;
            this.txtMachineMoves.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(404, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Référenciel cube :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(732, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Référenciel machine :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(956, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Movements moteurs";
            // 
            // txtMotorMoves
            // 
            this.txtMotorMoves.Location = new System.Drawing.Point(901, 68);
            this.txtMotorMoves.Name = "txtMotorMoves";
            this.txtMotorMoves.Size = new System.Drawing.Size(253, 104);
            this.txtMotorMoves.TabIndex = 4;
            this.txtMotorMoves.Text = "";
            // 
            // txtNBMotorMoves
            // 
            this.txtNBMotorMoves.AutoSize = true;
            this.txtNBMotorMoves.Location = new System.Drawing.Point(1089, 52);
            this.txtNBMotorMoves.Name = "txtNBMotorMoves";
            this.txtNBMotorMoves.Size = new System.Drawing.Size(13, 13);
            this.txtNBMotorMoves.TabIndex = 2;
            this.txtNBMotorMoves.Text = "0";
            // 
            // textLongMove
            // 
            this.textLongMove.Location = new System.Drawing.Point(356, 223);
            this.textLongMove.Name = "textLongMove";
            this.textLongMove.Size = new System.Drawing.Size(521, 20);
            this.textLongMove.TabIndex = 3;
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(901, 217);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(75, 23);
            this.btnForward.TabIndex = 5;
            this.btnForward.Text = "Forward >>";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnDev
            // 
            this.btnDev.Location = new System.Drawing.Point(901, 192);
            this.btnDev.Name = "btnDev";
            this.btnDev.Size = new System.Drawing.Size(75, 23);
            this.btnDev.TabIndex = 5;
            this.btnDev.Text = "DevTools";
            this.btnDev.UseVisualStyleBackColor = true;
            this.btnDev.Click += new System.EventHandler(this.btnDev_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 681);
            this.Controls.Add(this.btnDev);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.textLongMove);
            this.Controls.Add(this.txtAlg);
            this.Controls.Add(this.txtMotorMoves);
            this.Controls.Add(this.txtMachineMoves);
            this.Controls.Add(this.txtCubeMoves);
            this.Controls.Add(this.btnMoves);
            this.Controls.Add(this.txtNBMotorMoves);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblMoveCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlViewer);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udMoves)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMoves;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown udMoves;
        private System.Windows.Forms.TextBox txtColors;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel pnlViewer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMoveCount;
        private System.Windows.Forms.RichTextBox txtCubeMoves;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.TextBox txtAlg;
        private System.Windows.Forms.Button btnMoves;
        private System.Windows.Forms.RichTextBox txtMachineMoves;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox txtMotorMoves;
        private System.Windows.Forms.Label txtNBMotorMoves;
        private System.Windows.Forms.TextBox textLongMove;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnDev;
    }
}

