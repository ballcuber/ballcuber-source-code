namespace fgSolver
{
    partial class ManualDriving
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
            this.cubeNets = new fgSolver.CubeNets();
            this.resolutionSessionSupervisionControl = new fgSolver.ResolutionSessionSupervisionControl();
            this.btnInit = new System.Windows.Forms.Button();
            this.btnAlign = new System.Windows.Forms.Button();
            this.lblMv = new System.Windows.Forms.Label();
            this.btnAddMv = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cubeNets
            // 
            this.cubeNets.Clickable = false;
            this.cubeNets.Location = new System.Drawing.Point(3, 28);
            this.cubeNets.Name = "cubeNets";
            this.cubeNets.Size = new System.Drawing.Size(387, 306);
            this.cubeNets.TabIndex = 0;
            this.cubeNets.MoveClick += new fgSolver.CubeNets.MoveClickEventHandler(this.cubeNets_MoveClick);
            // 
            // resolutionSessionSupervisionControl
            // 
            this.resolutionSessionSupervisionControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resolutionSessionSupervisionControl.BottomVisible = true;
            this.resolutionSessionSupervisionControl.Location = new System.Drawing.Point(0, 340);
            this.resolutionSessionSupervisionControl.Name = "resolutionSessionSupervisionControl";
            this.resolutionSessionSupervisionControl.Size = new System.Drawing.Size(907, 398);
            this.resolutionSessionSupervisionControl.TabIndex = 1;
            this.resolutionSessionSupervisionControl.TimerVisible = false;
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(520, 47);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(113, 23);
            this.btnInit.TabIndex = 2;
            this.btnInit.Text = "Ajouter init.";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btnAlign
            // 
            this.btnAlign.Location = new System.Drawing.Point(520, 76);
            this.btnAlign.Name = "btnAlign";
            this.btnAlign.Size = new System.Drawing.Size(113, 23);
            this.btnAlign.TabIndex = 2;
            this.btnAlign.Text = "Ajouter alignement";
            this.btnAlign.UseVisualStyleBackColor = true;
            this.btnAlign.Click += new System.EventHandler(this.btnAlign_Click);
            // 
            // lblMv
            // 
            this.lblMv.Location = new System.Drawing.Point(289, 225);
            this.lblMv.Name = "lblMv";
            this.lblMv.Size = new System.Drawing.Size(101, 15);
            this.lblMv.TabIndex = 3;
            this.lblMv.Text = "label1";
            this.lblMv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAddMv
            // 
            this.btnAddMv.Location = new System.Drawing.Point(289, 243);
            this.btnAddMv.Name = "btnAddMv";
            this.btnAddMv.Size = new System.Drawing.Size(101, 23);
            this.btnAddMv.TabIndex = 2;
            this.btnAddMv.Text = "Ajouter";
            this.btnAddMv.UseVisualStyleBackColor = true;
            this.btnAddMv.Click += new System.EventHandler(this.btnAddMv_Click);
            // 
            // ManualDriving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMv);
            this.Controls.Add(this.btnAddMv);
            this.Controls.Add(this.btnAlign);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.resolutionSessionSupervisionControl);
            this.Controls.Add(this.cubeNets);
            this.Name = "ManualDriving";
            this.Size = new System.Drawing.Size(907, 738);
            this.ResumeLayout(false);

        }

        #endregion

        private CubeNets cubeNets;
        private ResolutionSessionSupervisionControl resolutionSessionSupervisionControl;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Button btnAlign;
        private System.Windows.Forms.Label lblMv;
        private System.Windows.Forms.Button btnAddMv;
    }
}
