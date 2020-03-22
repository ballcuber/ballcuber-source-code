namespace Ballcuber
{
    partial class ResolutionSessionControl
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
            this.resolutionSessionSupervisionControl = new Ballcuber.ResolutionSessionSupervisionControl();
            this.SuspendLayout();
            // 
            // resolutionSessionSupervisionControl
            // 
            this.resolutionSessionSupervisionControl.BottomVisible = true;
            this.resolutionSessionSupervisionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resolutionSessionSupervisionControl.Location = new System.Drawing.Point(0, 0);
            this.resolutionSessionSupervisionControl.Name = "resolutionSessionSupervisionControl";
            this.resolutionSessionSupervisionControl.Size = new System.Drawing.Size(723, 646);
            this.resolutionSessionSupervisionControl.TabIndex = 7;
            this.resolutionSessionSupervisionControl.TimerVisible = true;
            // 
            // ResolutionSessionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.resolutionSessionSupervisionControl);
            this.Name = "ResolutionSessionControl";
            this.Size = new System.Drawing.Size(723, 646);
            this.ResumeLayout(false);

        }

        #endregion
        private ResolutionSessionSupervisionControl resolutionSessionSupervisionControl;
    }
}
