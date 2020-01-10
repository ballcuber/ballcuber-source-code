namespace fgSolver
{
    partial class ResolutionSessionSupervisionControl
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup1", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("ListViewGroup2", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("0sdfgsdfhg g dfg geg erg erg egg s t h", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("1");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("2");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResolutionSessionSupervisionControl));
            this.lst = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.img = new System.Windows.Forms.ImageList(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnSetStep = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.lblState = new System.Windows.Forms.Label();
            this.chkFollow = new System.Windows.Forms.CheckBox();
            this.pnl = new System.Windows.Forms.Panel();
            this.lblTimer = new System.Windows.Forms.Label();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // lst
            // 
            this.lst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6});
            this.lst.FullRowSelect = true;
            listViewGroup1.Header = "ListViewGroup1";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "ListViewGroup2";
            listViewGroup2.Name = "listViewGroup2";
            this.lst.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.lst.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lst.HideSelection = false;
            listViewItem1.Group = listViewGroup2;
            listViewItem2.Group = listViewGroup1;
            listViewItem3.Group = listViewGroup1;
            this.lst.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.lst.LabelEdit = true;
            this.lst.Location = new System.Drawing.Point(3, 74);
            this.lst.MultiSelect = false;
            this.lst.Name = "lst";
            this.lst.Size = new System.Drawing.Size(700, 320);
            this.lst.SmallImageList = this.img;
            this.lst.TabIndex = 0;
            this.lst.UseCompatibleStateImageBehavior = false;
            this.lst.View = System.Windows.Forms.View.Details;
            this.lst.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lst_AfterLabelEdit);
            this.lst.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lst_MouseClick);
            this.lst.MouseLeave += new System.EventHandler(this.lst_MouseLeave);
            this.lst.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lst_MouseMove);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Width = 1000;
            // 
            // img
            // 
            this.img.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("img.ImageStream")));
            this.img.TransparentColor = System.Drawing.Color.Transparent;
            this.img.Images.SetKeyName(0, "Breakpoint");
            this.img.Images.SetKeyName(1, "Step");
            this.img.Images.SetKeyName(2, "BreakpointStep");
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Webdings", 12F);
            this.btnStart.Location = new System.Drawing.Point(22, 21);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "4";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Webdings", 12F);
            this.btnNext.Location = new System.Drawing.Point(103, 21);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = ":";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Font = new System.Drawing.Font("Webdings", 12F);
            this.btnAbort.Location = new System.Drawing.Point(184, 20);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 23);
            this.btnAbort.TabIndex = 1;
            this.btnAbort.Text = "<";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // btnPause
            // 
            this.btnPause.Font = new System.Drawing.Font("Webdings", 12F);
            this.btnPause.Location = new System.Drawing.Point(265, 20);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 1;
            this.btnPause.Text = ";";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnSetStep
            // 
            this.btnSetStep.Font = new System.Drawing.Font("Webdings", 12F);
            this.btnSetStep.Location = new System.Drawing.Point(346, 21);
            this.btnSetStep.Name = "btnSetStep";
            this.btnSetStep.Size = new System.Drawing.Size(75, 23);
            this.btnSetStep.TabIndex = 1;
            this.btnSetStep.Text = "8";
            this.btnSetStep.UseVisualStyleBackColor = true;
            this.btnSetStep.Click += new System.EventHandler(this.btnSetStep_Click);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(547, 19);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "TEST";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(19, 55);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(16, 13);
            this.lblState.TabIndex = 2;
            this.lblState.Text = "...";
            // 
            // chkFollow
            // 
            this.chkFollow.AutoSize = true;
            this.chkFollow.Checked = true;
            this.chkFollow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFollow.Location = new System.Drawing.Point(184, 51);
            this.chkFollow.Name = "chkFollow";
            this.chkFollow.Size = new System.Drawing.Size(142, 17);
            this.chkFollow.TabIndex = 3;
            this.chkFollow.Text = "Auto scroll to active step";
            this.chkFollow.UseVisualStyleBackColor = true;
            this.chkFollow.CheckedChanged += new System.EventHandler(this.chkFollow_CheckedChanged);
            // 
            // pnl
            // 
            this.pnl.Controls.Add(this.chkFollow);
            this.pnl.Controls.Add(this.btnTest);
            this.pnl.Controls.Add(this.btnClear);
            this.pnl.Controls.Add(this.btnSetStep);
            this.pnl.Controls.Add(this.btnPause);
            this.pnl.Controls.Add(this.btnAbort);
            this.pnl.Controls.Add(this.btnNext);
            this.pnl.Controls.Add(this.btnStart);
            this.pnl.Controls.Add(this.lst);
            this.pnl.Controls.Add(this.lblState);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(0, 140);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(709, 397);
            this.pnl.TabIndex = 7;
            // 
            // lblTimer
            // 
            this.lblTimer.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTimer.Font = new System.Drawing.Font("Open Sans", 90F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.ForeColor = System.Drawing.Color.Red;
            this.lblTimer.Location = new System.Drawing.Point(0, 0);
            this.lblTimer.Margin = new System.Windows.Forms.Padding(0);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(709, 140);
            this.lblTimer.TabIndex = 6;
            this.lblTimer.Text = "00:20:123";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Enabled = true;
            this.tmrRefresh.Interval = 50;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Font = new System.Drawing.Font("Webdings", 12F);
            this.btnClear.Location = new System.Drawing.Point(628, 19);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 25);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "r";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ResolutionSessionSupervisionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.lblTimer);
            this.Name = "ResolutionSessionSupervisionControl";
            this.Size = new System.Drawing.Size(709, 537);
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lst;
        private System.Windows.Forms.ImageList img;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnSetStep;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.CheckBox chkFollow;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.Button btnClear;
    }
}
