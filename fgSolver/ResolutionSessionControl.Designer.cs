namespace fgSolver
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
            this.components = new System.ComponentModel.Container();
            this.tmrChrono = new System.Windows.Forms.Timer(this.components);
            this.lblTimer = new System.Windows.Forms.Label();
            this.resolutionSessionSupervisionControl = new fgSolver.ResolutionSessionSupervisionControl();
            this.SuspendLayout();
            // 
            // tmrChrono
            // 
            this.tmrChrono.Enabled = true;
            this.tmrChrono.Interval = 11;
            this.tmrChrono.Tick += new System.EventHandler(this.tmrChrono_Tick);
            // 
            // lblTimer
            // 
            this.lblTimer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTimer.Font = new System.Drawing.Font("Open Sans", 90F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.ForeColor = System.Drawing.Color.Red;
            this.lblTimer.Location = new System.Drawing.Point(33, 0);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(632, 145);
            this.lblTimer.TabIndex = 5;
            this.lblTimer.Text = "00:20:123";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // resolutionSessionSupervisionControl
            // 
            this.resolutionSessionSupervisionControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resolutionSessionSupervisionControl.Location = new System.Drawing.Point(3, 173);
            this.resolutionSessionSupervisionControl.Name = "resolutionSessionSupervisionControl";
            this.resolutionSessionSupervisionControl.Size = new System.Drawing.Size(717, 470);
            this.resolutionSessionSupervisionControl.TabIndex = 7;
            // 
            // ResolutionSessionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.resolutionSessionSupervisionControl);
            this.Controls.Add(this.lblTimer);
            this.Name = "ResolutionSessionControl";
            this.Size = new System.Drawing.Size(723, 646);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmrChrono;
        private System.Windows.Forms.Label lblTimer;
        private ResolutionSessionSupervisionControl resolutionSessionSupervisionControl;
    }
}
