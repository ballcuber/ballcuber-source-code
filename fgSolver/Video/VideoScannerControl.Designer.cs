namespace fgSolver
{
    partial class VideoScannerControl
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
            this.pnlVideo = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlVideo
            // 
            this.pnlVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVideo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVideo.Location = new System.Drawing.Point(0, 0);
            this.pnlVideo.Name = "pnlVideo";
            this.pnlVideo.Size = new System.Drawing.Size(757, 480);
            this.pnlVideo.TabIndex = 0;
            // 
            // VideoScannerControl
            // 
            this.Controls.Add(this.pnlVideo);
            this.Name = "VideoScannerControl";
            this.Size = new System.Drawing.Size(757, 558);
            this.ResumeLayout(false);

        }

        #endregion

        private CubeNets cubeNets;
        private System.Windows.Forms.Panel pnlVideo;
    }
}
