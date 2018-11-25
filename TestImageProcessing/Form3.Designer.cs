namespace TestImageProcessing
{
    partial class Form3
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
            this.pnl = new System.Windows.Forms.SplitContainer();
            this.btnUse = new System.Windows.Forms.Button();
            this.chkAutoCalibration = new System.Windows.Forms.CheckBox();
            this.grid = new System.Windows.Forms.PropertyGrid();
            this.perspectivePnl = new System.Windows.Forms.Panel();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pnl)).BeginInit();
            this.pnl.Panel1.SuspendLayout();
            this.pnl.Panel2.SuspendLayout();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl
            // 
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.pnl.Location = new System.Drawing.Point(0, 0);
            this.pnl.Name = "pnl";
            // 
            // pnl.Panel1
            // 
            this.pnl.Panel1.Controls.Add(this.btnUse);
            this.pnl.Panel1.Controls.Add(this.chkAutoCalibration);
            this.pnl.Panel1.Controls.Add(this.grid);
            // 
            // pnl.Panel2
            // 
            this.pnl.Panel2.Controls.Add(this.perspectivePnl);
            this.pnl.Size = new System.Drawing.Size(696, 519);
            this.pnl.SplitterDistance = 232;
            this.pnl.TabIndex = 0;
            // 
            // btnUse
            // 
            this.btnUse.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUse.Location = new System.Drawing.Point(0, 238);
            this.btnUse.Name = "btnUse";
            this.btnUse.Size = new System.Drawing.Size(232, 23);
            this.btnUse.TabIndex = 0;
            this.btnUse.Text = "Valider calibration";
            this.btnUse.UseVisualStyleBackColor = true;
            this.btnUse.Visible = false;
            this.btnUse.Click += new System.EventHandler(this.btnUse_Click);
            // 
            // chkAutoCalibration
            // 
            this.chkAutoCalibration.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAutoCalibration.AutoSize = true;
            this.chkAutoCalibration.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkAutoCalibration.Location = new System.Drawing.Point(0, 215);
            this.chkAutoCalibration.Name = "chkAutoCalibration";
            this.chkAutoCalibration.Size = new System.Drawing.Size(232, 23);
            this.chkAutoCalibration.TabIndex = 0;
            this.chkAutoCalibration.Text = "Auto calibration";
            this.chkAutoCalibration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkAutoCalibration.UseVisualStyleBackColor = true;
            this.chkAutoCalibration.CheckedChanged += new System.EventHandler(this.chkAutoCalibration_CheckedChanged);
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Top;
            this.grid.HelpVisible = false;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Name = "grid";
            this.grid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.grid.Size = new System.Drawing.Size(232, 215);
            this.grid.TabIndex = 0;
            this.grid.ToolbarVisible = false;
            this.grid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.grid_PropertyValueChanged);
            // 
            // perspectivePnl
            // 
            this.perspectivePnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.perspectivePnl.Location = new System.Drawing.Point(0, 0);
            this.perspectivePnl.Name = "perspectivePnl";
            this.perspectivePnl.Size = new System.Drawing.Size(460, 115);
            this.perspectivePnl.TabIndex = 0;
            // 
            // tmr
            // 
            this.tmr.Enabled = true;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 519);
            this.Controls.Add(this.pnl);
            this.Name = "Form3";
            this.Text = "Form3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.pnl.Panel1.ResumeLayout(false);
            this.pnl.Panel1.PerformLayout();
            this.pnl.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnl)).EndInit();
            this.pnl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer pnl;
        private System.Windows.Forms.PropertyGrid grid;
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.CheckBox chkAutoCalibration;
        private System.Windows.Forms.Button btnUse;
        private System.Windows.Forms.Panel perspectivePnl;
    }
}