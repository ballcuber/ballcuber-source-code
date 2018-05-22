namespace fgSolver
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.sauvegarderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.précédentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suivantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonAdvanced = new System.Windows.Forms.ToolStripDropDownButton();
            this.fenêtreDeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugger3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stripAlarm = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnPrevious = new System.Windows.Forms.ToolStripButton();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripSeparator();
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.navPnl = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tmrAlarmBlink = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Enabled = true;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2,
            this.toolStripDropDownButtonAdvanced,
            this.stripAlarm});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1108, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sauvegarderToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(55, 22);
            this.toolStripDropDownButton1.Text = "Fichier";
            // 
            // sauvegarderToolStripMenuItem
            // 
            this.sauvegarderToolStripMenuItem.Name = "sauvegarderToolStripMenuItem";
            this.sauvegarderToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.sauvegarderToolStripMenuItem.Text = "Sauvegarder";
            this.sauvegarderToolStripMenuItem.Click += new System.EventHandler(this.sauvegarderToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.précédentToolStripMenuItem,
            this.suivantToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(57, 22);
            this.toolStripDropDownButton2.Text = "Edition";
            // 
            // précédentToolStripMenuItem
            // 
            this.précédentToolStripMenuItem.Image = global::fgSolver.Properties.Resources.Left;
            this.précédentToolStripMenuItem.Name = "précédentToolStripMenuItem";
            this.précédentToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.précédentToolStripMenuItem.Text = "Précédent";
            this.précédentToolStripMenuItem.Click += new System.EventHandler(this.précédentToolStripMenuItem_Click);
            // 
            // suivantToolStripMenuItem
            // 
            this.suivantToolStripMenuItem.Image = global::fgSolver.Properties.Resources.Right;
            this.suivantToolStripMenuItem.Name = "suivantToolStripMenuItem";
            this.suivantToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.suivantToolStripMenuItem.Text = "Suivant";
            this.suivantToolStripMenuItem.Click += new System.EventHandler(this.suivantToolStripMenuItem_Click);
            // 
            // toolStripDropDownButtonAdvanced
            // 
            this.toolStripDropDownButtonAdvanced.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButtonAdvanced.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fenêtreDeTestToolStripMenuItem,
            this.debugger3DToolStripMenuItem});
            this.toolStripDropDownButtonAdvanced.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonAdvanced.Image")));
            this.toolStripDropDownButtonAdvanced.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonAdvanced.Name = "toolStripDropDownButtonAdvanced";
            this.toolStripDropDownButtonAdvanced.Size = new System.Drawing.Size(59, 22);
            this.toolStripDropDownButtonAdvanced.Text = "Avancé";
            // 
            // fenêtreDeTestToolStripMenuItem
            // 
            this.fenêtreDeTestToolStripMenuItem.Name = "fenêtreDeTestToolStripMenuItem";
            this.fenêtreDeTestToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.fenêtreDeTestToolStripMenuItem.Text = "Fenêtre de test";
            this.fenêtreDeTestToolStripMenuItem.Click += new System.EventHandler(this.fenêtreDeTestToolStripMenuItem_Click);
            // 
            // debugger3DToolStripMenuItem
            // 
            this.debugger3DToolStripMenuItem.CheckOnClick = true;
            this.debugger3DToolStripMenuItem.Name = "debugger3DToolStripMenuItem";
            this.debugger3DToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.debugger3DToolStripMenuItem.Text = "Debugger 3D";
            this.debugger3DToolStripMenuItem.Click += new System.EventHandler(this.debugger3DToolStripMenuItem_Click);
            // 
            // stripAlarm
            // 
            this.stripAlarm.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.stripAlarm.AutoSize = false;
            this.stripAlarm.AutoToolTip = false;
            this.stripAlarm.BackColor = System.Drawing.Color.Yellow;
            this.stripAlarm.DoubleClickEnabled = true;
            this.stripAlarm.Image = global::fgSolver.Properties.Resources.Warning;
            this.stripAlarm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stripAlarm.Name = "stripAlarm";
            this.stripAlarm.Size = new System.Drawing.Size(300, 22);
            this.stripAlarm.Text = "(2) Il y a un gros soucis de m                  ...";
            this.stripAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.stripAlarm.Click += new System.EventHandler(this.stripAlarm_Click);
            this.stripAlarm.DoubleClick += new System.EventHandler(this.stripAlarm_DoubleClick);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrevious,
            this.btnNext,
            this.toolStripButton3});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1108, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnPrevious
            // 
            this.btnPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrevious.Image = global::fgSolver.Properties.Resources.Left;
            this.btnPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(23, 22);
            this.btnPrevious.Text = "Précédent";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNext.Image = global::fgSolver.Properties.Resources.Right;
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(23, 22);
            this.btnNext.Text = "Suivant";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(6, 25);
            // 
            // splitter
            // 
            this.splitter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitter.Location = new System.Drawing.Point(0, 50);
            this.splitter.Name = "splitter";
            // 
            // splitter.Panel1
            // 
            this.splitter.Panel1.Controls.Add(this.navPnl);
            this.splitter.Panel1.Controls.Add(this.lblTitle);
            this.splitter.Size = new System.Drawing.Size(1108, 493);
            this.splitter.SplitterDistance = 549;
            this.splitter.TabIndex = 4;
            // 
            // navPnl
            // 
            this.navPnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.navPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navPnl.Location = new System.Drawing.Point(0, 42);
            this.navPnl.Name = "navPnl";
            this.navPnl.Size = new System.Drawing.Size(547, 449);
            this.navPnl.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Image = global::fgSolver.Properties.Resources.FaceCube;
            this.lblTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(547, 42);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "label1";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrAlarmBlink
            // 
            this.tmrAlarmBlink.Enabled = true;
            this.tmrAlarmBlink.Interval = 1000;
            this.tmrAlarmBlink.Tick += new System.EventHandler(this.tmrAlarmeBlink_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 543);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitter.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonAdvanced;
        private System.Windows.Forms.ToolStripMenuItem fenêtreDeTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugger3DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sauvegarderToolStripMenuItem;
        private System.Windows.Forms.Panel navPnl;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ToolStripButton btnPrevious;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripSeparator toolStripButton3;
        private System.Windows.Forms.ToolStripMenuItem précédentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suivantToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton stripAlarm;
        private System.Windows.Forms.Timer tmrAlarmBlink;
    }
}