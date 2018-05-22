namespace TestImageProcessing
{
    partial class Form1
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.barCanny1 = new System.Windows.Forms.TrackBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lblS = new System.Windows.Forms.Label();
            this.lblH = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNbFace = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.barCanny2 = new System.Windows.Forms.TrackBar();
            this.pnl = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.barCanny1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barCanny2)).BeginInit();
            this.SuspendLayout();
            // 
            // barCanny1
            // 
            this.barCanny1.Location = new System.Drawing.Point(12, 31);
            this.barCanny1.Maximum = 300;
            this.barCanny1.Name = "barCanny1";
            this.barCanny1.Size = new System.Drawing.Size(199, 45);
            this.barCanny1.TabIndex = 0;
            this.barCanny1.Value = 58;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Controls.Add(this.lblS);
            this.panel1.Controls.Add(this.lblH);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblNbFace);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.barCanny2);
            this.panel1.Controls.Add(this.barCanny1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(795, 174);
            this.panel1.TabIndex = 1;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(271, 14);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(269, 145);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // lblS
            // 
            this.lblS.AutoSize = true;
            this.lblS.Location = new System.Drawing.Point(640, 86);
            this.lblS.Name = "lblS";
            this.lblS.Size = new System.Drawing.Size(43, 13);
            this.lblS.TabIndex = 1;
            this.lblS.Text = "123456";
            // 
            // lblH
            // 
            this.lblH.AutoSize = true;
            this.lblH.Location = new System.Drawing.Point(640, 63);
            this.lblH.Name = "lblH";
            this.lblH.Size = new System.Drawing.Size(43, 13);
            this.lblH.TabIndex = 1;
            this.lblH.Text = "123456";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(591, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "S moy : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(591, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "H moy :";
            // 
            // lblNbFace
            // 
            this.lblNbFace.AutoSize = true;
            this.lblNbFace.Location = new System.Drawing.Point(591, 17);
            this.lblNbFace.Name = "lblNbFace";
            this.lblNbFace.Size = new System.Drawing.Size(43, 13);
            this.lblNbFace.TabIndex = 1;
            this.lblNbFace.Text = "123456";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Canny";
            // 
            // barCanny2
            // 
            this.barCanny2.Location = new System.Drawing.Point(3, 71);
            this.barCanny2.Maximum = 300;
            this.barCanny2.Name = "barCanny2";
            this.barCanny2.Size = new System.Drawing.Size(199, 45);
            this.barCanny2.TabIndex = 0;
            this.barCanny2.Value = 130;
            // 
            // pnl
            // 
            this.pnl.AutoScroll = true;
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(0, 174);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(795, 338);
            this.pnl.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 512);
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.barCanny1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barCanny2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar barCanny1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar barCanny2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Label lblNbFace;
        private System.Windows.Forms.Label lblS;
        private System.Windows.Forms.Label lblH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}

