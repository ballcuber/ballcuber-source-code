namespace fgSolver
{
    partial class BoardsControl
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.boardControl1 = new fgSolver.BoardControl();
            this.boardControl2 = new fgSolver.BoardControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.boardControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.boardControl2);
            this.splitContainer1.Size = new System.Drawing.Size(827, 520);
            this.splitContainer1.SplitterDistance = 425;
            this.splitContainer1.TabIndex = 0;
            // 
            // boardControl1
            // 
            this.boardControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boardControl1.Index = 1;
            this.boardControl1.Location = new System.Drawing.Point(0, 0);
            this.boardControl1.Name = "boardControl1";
            this.boardControl1.Size = new System.Drawing.Size(425, 520);
            this.boardControl1.TabIndex = 0;
            // 
            // boardControl2
            // 
            this.boardControl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boardControl2.Index = 2;
            this.boardControl2.Location = new System.Drawing.Point(0, 0);
            this.boardControl2.Name = "boardControl2";
            this.boardControl2.Size = new System.Drawing.Size(398, 520);
            this.boardControl2.TabIndex = 0;
            // 
            // BoardsControl
            // 
            this.Controls.Add(this.splitContainer1);
            this.Name = "BoardsControl";
            this.Size = new System.Drawing.Size(827, 520);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.RichTextBox txtMoves;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private BoardControl boardControl1;
        private BoardControl boardControl2;
    }
}
