namespace fgSolver
{
    partial class SolverControl
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
            this.txtMoves = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMovement = new System.Windows.Forms.Label();
            this.lblQuarter = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.imgCube2 = new System.Windows.Forms.PictureBox();
            this.imgCube1 = new System.Windows.Forms.PictureBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnSolve = new System.Windows.Forms.Button();
            this.cubeNets = new fgSolver.CubeNets();
            ((System.ComponentModel.ISupportInitialize)(this.imgCube2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCube1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMoves
            // 
            this.txtMoves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMoves.Location = new System.Drawing.Point(335, 78);
            this.txtMoves.Name = "txtMoves";
            this.txtMoves.ReadOnly = true;
            this.txtMoves.Size = new System.Drawing.Size(294, 431);
            this.txtMoves.TabIndex = 2;
            this.txtMoves.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(336, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Quarts de tour : ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(528, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mouvements :";
            // 
            // lblMovement
            // 
            this.lblMovement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMovement.AutoSize = true;
            this.lblMovement.Location = new System.Drawing.Point(599, 60);
            this.lblMovement.Name = "lblMovement";
            this.lblMovement.Size = new System.Drawing.Size(13, 13);
            this.lblMovement.TabIndex = 3;
            this.lblMovement.Text = "0";
            // 
            // lblQuarter
            // 
            this.lblQuarter.AutoSize = true;
            this.lblQuarter.Location = new System.Drawing.Point(412, 61);
            this.lblQuarter.Name = "lblQuarter";
            this.lblQuarter.Size = new System.Drawing.Size(13, 13);
            this.lblQuarter.TabIndex = 3;
            this.lblQuarter.Text = "0";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(363, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(266, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Liste des mouvements :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imgCube2
            // 
            this.imgCube2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imgCube2.BackColor = System.Drawing.Color.Transparent;
            this.imgCube2.Image = global::fgSolver.Properties.Resources.TransparentAnimatedCube;
            this.imgCube2.Location = new System.Drawing.Point(454, 220);
            this.imgCube2.Name = "imgCube2";
            this.imgCube2.Size = new System.Drawing.Size(64, 64);
            this.imgCube2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgCube2.TabIndex = 8;
            this.imgCube2.TabStop = false;
            // 
            // imgCube1
            // 
            this.imgCube1.BackColor = System.Drawing.Color.Transparent;
            this.imgCube1.Image = global::fgSolver.Properties.Resources.TransparentAnimatedCube;
            this.imgCube1.Location = new System.Drawing.Point(88, 220);
            this.imgCube1.Name = "imgCube1";
            this.imgCube1.Size = new System.Drawing.Size(64, 64);
            this.imgCube1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgCube1.TabIndex = 8;
            this.imgCube1.TabStop = false;
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Image = global::fgSolver.Properties.Resources.ResolutionSession;
            this.btnRun.Location = new System.Drawing.Point(11, 540);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(615, 107);
            this.btnRun.TabIndex = 7;
            this.btnRun.Text = "Lancer la résolution";
            this.btnRun.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnSolve
            // 
            this.btnSolve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSolve.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSolve.Image = global::fgSolver.Properties.Resources.Solver;
            this.btnSolve.Location = new System.Drawing.Point(3, 453);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(120, 56);
            this.btnSolve.TabIndex = 1;
            this.btnSolve.Text = "Obtenir solution";
            this.btnSolve.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // cubeNets
            // 
            this.cubeNets.Clickable = false;
            this.cubeNets.Location = new System.Drawing.Point(0, 132);
            this.cubeNets.Name = "cubeNets";
            this.cubeNets.ShowControlButtons = false;
            this.cubeNets.Size = new System.Drawing.Size(326, 243);
            this.cubeNets.TabIndex = 0;
            // 
            // SolverControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imgCube2);
            this.Controls.Add(this.imgCube1);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.lblMovement);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblQuarter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMoves);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.cubeNets);
            this.Name = "SolverControl";
            this.Size = new System.Drawing.Size(636, 661);
            ((System.ComponentModel.ISupportInitialize)(this.imgCube2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCube1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CubeNets cubeNets;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.RichTextBox txtMoves;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMovement;
        private System.Windows.Forms.Label lblQuarter;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox imgCube1;
        private System.Windows.Forms.PictureBox imgCube2;
    }
}
