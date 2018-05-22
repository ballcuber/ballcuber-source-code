namespace fgSolver
{
    partial class AlarmControl
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "<dfqsfqdsfqsdf",
            "fgsdfgsd"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("qsdfqsfdqsdf");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("qdsfqsdfqsdfsqfdqs");
            this.btnAcq = new System.Windows.Forms.Button();
            this.lstAlarms = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnAcq
            // 
            this.btnAcq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAcq.Image = global::fgSolver.Properties.Resources.OK;
            this.btnAcq.Location = new System.Drawing.Point(386, 376);
            this.btnAcq.Name = "btnAcq";
            this.btnAcq.Size = new System.Drawing.Size(118, 48);
            this.btnAcq.TabIndex = 0;
            this.btnAcq.Text = "Acquitter";
            this.btnAcq.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAcq.UseVisualStyleBackColor = true;
            this.btnAcq.Click += new System.EventHandler(this.btnAcq_Click);
            // 
            // lstAlarms
            // 
            this.lstAlarms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAlarms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstAlarms.FullRowSelect = true;
            this.lstAlarms.GridLines = true;
            this.lstAlarms.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.lstAlarms.Location = new System.Drawing.Point(3, 3);
            this.lstAlarms.MultiSelect = false;
            this.lstAlarms.Name = "lstAlarms";
            this.lstAlarms.Size = new System.Drawing.Size(501, 370);
            this.lstAlarms.TabIndex = 1;
            this.lstAlarms.UseCompatibleStateImageBehavior = false;
            this.lstAlarms.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Message";
            this.columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Info supplémentaires";
            this.columnHeader2.Width = 191;
            // 
            // AlarmControl
            // 
            this.Controls.Add(this.lstAlarms);
            this.Controls.Add(this.btnAcq);
            this.Name = "AlarmControl";
            this.Size = new System.Drawing.Size(507, 427);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button btnAcq;
        private System.Windows.Forms.ListView lstAlarms;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}
