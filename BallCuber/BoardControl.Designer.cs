namespace Ballcuber
{
    partial class BoardControl
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblConnect = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnEnable = new System.Windows.Forms.Button();
            this.btnDisable = new System.Windows.Forms.Button();
            this.pnlFunctions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.ledConnected = new Bulb.LedBulb();
            this.grpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(416, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Carte x";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConnect
            // 
            this.lblConnect.AutoSize = true;
            this.lblConnect.Location = new System.Drawing.Point(122, 37);
            this.lblConnect.Name = "lblConnect";
            this.lblConnect.Size = new System.Drawing.Size(53, 13);
            this.lblConnect.TabIndex = 2;
            this.lblConnect.Text = "Connecté";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(6, 32);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(84, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connexion";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnEnable
            // 
            this.btnEnable.Location = new System.Drawing.Point(7, 10);
            this.btnEnable.Name = "btnEnable";
            this.btnEnable.Size = new System.Drawing.Size(97, 23);
            this.btnEnable.TabIndex = 3;
            this.btnEnable.Text = "Enable outputs";
            this.btnEnable.UseVisualStyleBackColor = true;
            this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // btnDisable
            // 
            this.btnDisable.Location = new System.Drawing.Point(7, 37);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(97, 23);
            this.btnDisable.TabIndex = 3;
            this.btnDisable.Text = "Disable outputs";
            this.btnDisable.UseVisualStyleBackColor = true;
            this.btnDisable.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // pnlFunctions
            // 
            this.pnlFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFunctions.AutoScroll = true;
            this.pnlFunctions.AutoScrollMinSize = new System.Drawing.Size(1000, 10000);
            this.pnlFunctions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlFunctions.Location = new System.Drawing.Point(0, 106);
            this.pnlFunctions.Name = "pnlFunctions";
            this.pnlFunctions.Size = new System.Drawing.Size(418, 323);
            this.pnlFunctions.TabIndex = 6;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(7, 77);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(97, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Rafraichir";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // grpMain
            // 
            this.grpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMain.Controls.Add(this.btnEnable);
            this.grpMain.Controls.Add(this.pnlFunctions);
            this.grpMain.Controls.Add(this.btnDisable);
            this.grpMain.Controls.Add(this.btnRefresh);
            this.grpMain.Location = new System.Drawing.Point(-1, 68);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(418, 429);
            this.grpMain.TabIndex = 8;
            this.grpMain.TabStop = false;
            // 
            // ledConnected
            // 
            this.ledConnected.Color = System.Drawing.Color.Lime;
            this.ledConnected.Location = new System.Drawing.Point(96, 33);
            this.ledConnected.Name = "ledConnected";
            this.ledConnected.On = true;
            this.ledConnected.Size = new System.Drawing.Size(20, 20);
            this.ledConnected.TabIndex = 1;
            // 
            // BoardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblConnect);
            this.Controls.Add(this.ledConnected);
            this.Controls.Add(this.lblTitle);
            this.Name = "BoardControl";
            this.Size = new System.Drawing.Size(416, 496);
            this.grpMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private Bulb.LedBulb ledConnected;
        private System.Windows.Forms.Label lblConnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnEnable;
        private System.Windows.Forms.Button btnDisable;
        private System.Windows.Forms.FlowLayoutPanel pnlFunctions;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox grpMain;
    }
}
