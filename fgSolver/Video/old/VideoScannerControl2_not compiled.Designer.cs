namespace fgSolver
{
    partial class VideoScannerControl2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.barProgress = new System.Windows.Forms.ProgressBar();
            this.btnDebug = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlVideo
            // 
            this.pnlVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVideo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVideo.Location = new System.Drawing.Point(0, 0);
            this.pnlVideo.Name = "pnlVideo";
            this.pnlVideo.Size = new System.Drawing.Size(801, 480);
            this.pnlVideo.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Controls.Add(this.barProgress);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(157, 486);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(520, 81);
            this.panel1.TabIndex = 2;
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Webdings", 12F);
            this.btnStop.Location = new System.Drawing.Point(3, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(30, 30);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "<";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(39, 51);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(383, 23);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Montrer un cube à la caméra pour démarrer...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Gauche            Arrière             Droite            Face            Dessous  " +
    "        Dessus";
            // 
            // barProgress
            // 
            this.barProgress.Location = new System.Drawing.Point(39, 5);
            this.barProgress.Maximum = 7;
            this.barProgress.Name = "barProgress";
            this.barProgress.Size = new System.Drawing.Size(430, 23);
            this.barProgress.TabIndex = 4;
            // 
            // btnDebug
            // 
            this.btnDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDebug.Font = new System.Drawing.Font("Webdings", 20F);
            this.btnDebug.Location = new System.Drawing.Point(738, 646);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(60, 50);
            this.btnDebug.TabIndex = 3;
            this.btnDebug.Text = "­";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // VideoScannerControl
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.pnlVideo);
            this.Name = "VideoScannerControl";
            this.Size = new System.Drawing.Size(801, 699);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CubeNets cubeNets;
        private System.Windows.Forms.Panel pnlVideo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar barProgress;
        private System.Windows.Forms.Button btnDebug;
    }
}
