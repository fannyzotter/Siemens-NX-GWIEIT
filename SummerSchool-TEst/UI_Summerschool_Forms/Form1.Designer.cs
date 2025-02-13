namespace UI_Summerschool_Forms
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbReadPMI = new System.Windows.Forms.Button();
            this.cbClose = new System.Windows.Forms.Button();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.tbPMIs = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbReadPMI
            // 
            this.cbReadPMI.Location = new System.Drawing.Point(560, 90);
            this.cbReadPMI.Name = "cbReadPMI";
            this.cbReadPMI.Size = new System.Drawing.Size(186, 99);
            this.cbReadPMI.TabIndex = 0;
            this.cbReadPMI.Text = "Lese alle PMI\'s";
            this.cbReadPMI.UseVisualStyleBackColor = true;
            this.cbReadPMI.Click += new System.EventHandler(this.cbReadPMI_Click);
            // 
            // cbClose
            // 
            this.cbClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbClose.ForeColor = System.Drawing.Color.Blue;
            this.cbClose.Location = new System.Drawing.Point(560, 292);
            this.cbClose.Name = "cbClose";
            this.cbClose.Size = new System.Drawing.Size(186, 71);
            this.cbClose.TabIndex = 1;
            this.cbClose.Text = "Fenster schließen";
            this.cbClose.UseVisualStyleBackColor = true;
            this.cbClose.Click += new System.EventHandler(this.cbClose_Click);
            // 
            // tbPMIs
            // 
            this.tbPMIs.Location = new System.Drawing.Point(45, 76);
            this.tbPMIs.Multiline = true;
            this.tbPMIs.Name = "tbPMIs";
            this.tbPMIs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbPMIs.Size = new System.Drawing.Size(331, 273);
            this.tbPMIs.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbPMIs);
            this.Controls.Add(this.cbClose);
            this.Controls.Add(this.cbReadPMI);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cbReadPMI;
        private System.Windows.Forms.Button cbClose;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.TextBox tbPMIs;
    }
}

