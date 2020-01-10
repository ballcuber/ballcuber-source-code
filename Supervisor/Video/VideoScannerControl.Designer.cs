namespace fgSolver.Video
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnl1 = new System.Windows.Forms.Panel();
            this.pnl2 = new System.Windows.Forms.Panel();
            this.pnl3 = new System.Windows.Forms.Panel();
            this.btnSommet1 = new System.Windows.Forms.Button();
            this.btnSommet2 = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.PropertyGrid();
            this.btnParam = new System.Windows.Forms.Button();
            this.btnValidateCalib = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(640, 480);
            this.pnlMain.TabIndex = 0;
            // 
            // pnl1
            // 
            this.pnl1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pnl1.Location = new System.Drawing.Point(0, 491);
            this.pnl1.Name = "pnl1";
            this.pnl1.Size = new System.Drawing.Size(100, 100);
            this.pnl1.TabIndex = 1;
            // 
            // pnl2
            // 
            this.pnl2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pnl2.Location = new System.Drawing.Point(106, 491);
            this.pnl2.Name = "pnl2";
            this.pnl2.Size = new System.Drawing.Size(100, 100);
            this.pnl2.TabIndex = 1;
            // 
            // pnl3
            // 
            this.pnl3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pnl3.Location = new System.Drawing.Point(212, 491);
            this.pnl3.Name = "pnl3";
            this.pnl3.Size = new System.Drawing.Size(100, 100);
            this.pnl3.TabIndex = 1;
            // 
            // btnSommet1
            // 
            this.btnSommet1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSommet1.Image = global::fgSolver.Properties.Resources.Camera;
            this.btnSommet1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSommet1.Location = new System.Drawing.Point(322, 491);
            this.btnSommet1.Name = "btnSommet1";
            this.btnSommet1.Size = new System.Drawing.Size(92, 46);
            this.btnSommet1.TabIndex = 2;
            this.btnSommet1.Text = "Sommet 1";
            this.btnSommet1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSommet1.UseVisualStyleBackColor = true;
            this.btnSommet1.Click += new System.EventHandler(this.btnSommet1_Click);
            // 
            // btnSommet2
            // 
            this.btnSommet2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSommet2.Image = global::fgSolver.Properties.Resources.Camera;
            this.btnSommet2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSommet2.Location = new System.Drawing.Point(322, 545);
            this.btnSommet2.Name = "btnSommet2";
            this.btnSommet2.Size = new System.Drawing.Size(92, 46);
            this.btnSommet2.TabIndex = 2;
            this.btnSommet2.Text = "Sommet 2";
            this.btnSommet2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSommet2.UseVisualStyleBackColor = true;
            this.btnSommet2.Click += new System.EventHandler(this.btnSommet2_Click);
            // 
            // grid
            // 
            this.grid.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.grid.HelpVisible = false;
            this.grid.Location = new System.Drawing.Point(433, 335);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(177, 176);
            this.grid.TabIndex = 3;
            this.grid.ToolbarVisible = false;
            this.grid.Visible = false;
            this.grid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.grid_PropertyValueChanged);
            this.grid.VisibleChanged += new System.EventHandler(this.grid_VisibleChanged);
            // 
            // btnParam
            // 
            this.btnParam.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnParam.BackgroundImage = global::fgSolver.Properties.Resources.Parameters;
            this.btnParam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnParam.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnParam.Location = new System.Drawing.Point(616, 493);
            this.btnParam.Name = "btnParam";
            this.btnParam.Size = new System.Drawing.Size(24, 24);
            this.btnParam.TabIndex = 2;
            this.btnParam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnParam.UseVisualStyleBackColor = true;
            this.btnParam.Click += new System.EventHandler(this.btnParam_Click);
            // 
            // btnValidateCalib
            // 
            this.btnValidateCalib.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnValidateCalib.BackgroundImage = global::fgSolver.Properties.Resources.OK;
            this.btnValidateCalib.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnValidateCalib.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnValidateCalib.Location = new System.Drawing.Point(616, 523);
            this.btnValidateCalib.Name = "btnValidateCalib";
            this.btnValidateCalib.Size = new System.Drawing.Size(24, 24);
            this.btnValidateCalib.TabIndex = 2;
            this.btnValidateCalib.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnValidateCalib.UseVisualStyleBackColor = true;
            this.btnValidateCalib.Visible = false;
            this.btnValidateCalib.Click += new System.EventHandler(this.btnValidateCalib_Click);
            // 
            // btnInit
            // 
            this.btnInit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnInit.Image = global::fgSolver.Properties.Resources.FaceCube;
            this.btnInit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInit.Location = new System.Drawing.Point(419, 517);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(122, 46);
            this.btnInit.TabIndex = 2;
            this.btnInit.Text = "Initialisation !";
            this.btnInit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(494, 571);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(47, 20);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "cube";
            this.dlgSave.FileName = "export.csv";
            this.dlgSave.Title = "Save colors";
            // 
            // VideoScannerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grid);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.btnSommet2);
            this.Controls.Add(this.btnValidateCalib);
            this.Controls.Add(this.btnParam);
            this.Controls.Add(this.btnSommet1);
            this.Controls.Add(this.pnl3);
            this.Controls.Add(this.pnl2);
            this.Controls.Add(this.pnl1);
            this.Controls.Add(this.pnlMain);
            this.Name = "VideoScannerControl";
            this.Size = new System.Drawing.Size(645, 603);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnl1;
        private System.Windows.Forms.Panel pnl2;
        private System.Windows.Forms.Panel pnl3;
        private System.Windows.Forms.Button btnSommet1;
        private System.Windows.Forms.Button btnSommet2;
        private System.Windows.Forms.PropertyGrid grid;
        private System.Windows.Forms.Button btnParam;
        private System.Windows.Forms.Button btnValidateCalib;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog dlgSave;
    }
}
