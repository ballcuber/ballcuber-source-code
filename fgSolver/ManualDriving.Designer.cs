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
            this.SuspendLayout();
            // 
            // cubeNets
            // 
            this.cubeNets.Clickable = false;
            this.cubeNets.Location = new System.Drawing.Point(45, 40);
            this.cubeNets.Name = "cubeNets";
            this.cubeNets.Size = new System.Drawing.Size(360, 280);
            this.cubeNets.TabIndex = 0;
            this.cubeNets.MoveClick += new fgSolver.CubeNets.MoveClickEventHandler(this.cubeNets_MoveClick);
            // 
            // ManualDriving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cubeNets);
            this.Name = "ManualDriving";
            this.Size = new System.Drawing.Size(613, 484);
            this.ResumeLayout(false);

        }

        #endregion

        private CubeNets cubeNets;
    }
}
