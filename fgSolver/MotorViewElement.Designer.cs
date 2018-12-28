namespace fgSolver
{
    partial class MotorViewElement
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
            this.ledBulb1 = new Bulb.LedBulb();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ledBulb1
            // 
            this.ledBulb1.Location = new System.Drawing.Point(10, 3);
            this.ledBulb1.Name = "ledBulb1";
            this.ledBulb1.On = true;
            this.ledBulb1.Size = new System.Drawing.Size(26, 26);
            this.ledBulb1.TabIndex = 0;
            this.ledBulb1.Text = "Enabled";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "On";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 20);
            this.button1.TabIndex = 2;
            this.button1.Text = "On";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(35, 35);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 20);
            this.button2.TabIndex = 2;
            this.button2.Text = "Off";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // MotorViewElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ledBulb1);
            this.Name = "MotorViewElement";
            this.Size = new System.Drawing.Size(274, 104);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bulb.LedBulb ledBulb1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
