namespace fgSolver
{
    partial class ColorDefinitionControl
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
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.cornerPicture = new System.Windows.Forms.PictureBox();
            this.btnSolve = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnRand = new System.Windows.Forms.Button();
            this.cubeNets = new fgSolver.CubeNets();
            ((System.ComponentModel.ISupportInitialize)(this.cornerPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImport
            // 
            this.btnImport.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnImport.Location = new System.Drawing.Point(374, 477);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 7;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExport.Location = new System.Drawing.Point(293, 477);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "cube";
            this.dlgSave.FileName = "Initial.cube";
            this.dlgSave.Title = "Sauvegarder le cube";
            // 
            // dlgOpen
            // 
            this.dlgOpen.DefaultExt = "cube";
            // 
            // cornerPicture
            // 
            this.cornerPicture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cornerPicture.Image = global::fgSolver.Properties.Resources.FixedCornerColor;
            this.cornerPicture.Location = new System.Drawing.Point(240, 6);
            this.cornerPicture.Name = "cornerPicture";
            this.cornerPicture.Size = new System.Drawing.Size(209, 129);
            this.cornerPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cornerPicture.TabIndex = 9;
            this.cornerPicture.TabStop = false;
            // 
            // btnSolve
            // 
            this.btnSolve.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSolve.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolve.Image = global::fgSolver.Properties.Resources.Solver;
            this.btnSolve.Location = new System.Drawing.Point(10, 537);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(439, 110);
            this.btnSolve.TabIndex = 6;
            this.btnSolve.Text = "Get solution ";
            this.btnSolve.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btnReset.Image = global::fgSolver.Properties.Resources.Reset;
            this.btnReset.Location = new System.Drawing.Point(258, 362);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(160, 80);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnRand
            // 
            this.btnRand.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRand.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btnRand.Image = global::fgSolver.Properties.Resources.Random;
            this.btnRand.Location = new System.Drawing.Point(42, 362);
            this.btnRand.Name = "btnRand";
            this.btnRand.Size = new System.Drawing.Size(160, 80);
            this.btnRand.TabIndex = 4;
            this.btnRand.Text = "Random";
            this.btnRand.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRand.UseVisualStyleBackColor = true;
            this.btnRand.Click += new System.EventHandler(this.btnRand_Click);
            // 
            // cubeNets
            // 
            this.cubeNets.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cubeNets.Location = new System.Drawing.Point(69, 63);
            this.cubeNets.Name = "cubeNets";
            this.cubeNets.ShowControlButtons = false;
            this.cubeNets.Size = new System.Drawing.Size(326, 245);
            this.cubeNets.TabIndex = 5;
            // 
            // ColorDefinitionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.Controls.Add(this.cornerPicture);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.cubeNets);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnRand);
            this.Name = "ColorDefinitionControl";
            this.Size = new System.Drawing.Size(462, 658);
            ((System.ComponentModel.ISupportInitialize)(this.cornerPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnRand;
        private System.Windows.Forms.Button btnReset;
        private CubeNets cubeNets;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.PictureBox cornerPicture;
    }
}
