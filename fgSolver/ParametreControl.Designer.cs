using System;

namespace fgSolver
{
    partial class ParametreControl
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
            this.pgHard1 = new System.Windows.Forms.PropertyGrid();
            this.pgHard2 = new System.Windows.Forms.PropertyGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pgGlobal = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // pgHard1
            // 
            this.pgHard1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pgHard1.Location = new System.Drawing.Point(0, 19);
            this.pgHard1.Name = "pgHard1";
            this.pgHard1.Size = new System.Drawing.Size(251, 381);
            this.pgHard1.TabIndex = 0;
            this.pgHard1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyValueChanged);
            // 
            // pgHard2
            // 
            this.pgHard2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pgHard2.Location = new System.Drawing.Point(256, 19);
            this.pgHard2.Name = "pgHard2";
            this.pgHard2.Size = new System.Drawing.Size(251, 381);
            this.pgHard2.TabIndex = 0;
            this.pgHard2.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(110, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Carte 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(355, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Carte 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(621, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Général";
            // 
            // pgGlobal
            // 
            this.pgGlobal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pgGlobal.Location = new System.Drawing.Point(511, 19);
            this.pgGlobal.Name = "pgGlobal";
            this.pgGlobal.Size = new System.Drawing.Size(251, 381);
            this.pgGlobal.TabIndex = 0;
            this.pgGlobal.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyValueChanged);
            // 
            // ParametreControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pgGlobal);
            this.Controls.Add(this.pgHard2);
            this.Controls.Add(this.pgHard1);
            this.Name = "ParametreControl";
            this.Size = new System.Drawing.Size(765, 455);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgHard1;
        private System.Windows.Forms.PropertyGrid pgHard2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PropertyGrid pgGlobal;
    }
}
