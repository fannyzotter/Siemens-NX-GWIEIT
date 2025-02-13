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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cbReadPMI = new System.Windows.Forms.Button();
            this.cbClose = new System.Windows.Forms.Button();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.timer_UpdatecbCloseStatus = new System.Windows.Forms.Timer(this.components);
            this.labelInformation = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbReadPMI
            // 
            resources.ApplyResources(this.cbReadPMI, "cbReadPMI");
            this.cbReadPMI.Name = "cbReadPMI";
            this.cbReadPMI.UseVisualStyleBackColor = true;
            this.cbReadPMI.Click += new System.EventHandler(this.cbReadPMI_Click);
            // 
            // cbClose
            // 
            resources.ApplyResources(this.cbClose, "cbClose");
            this.cbClose.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbClose.Name = "cbClose";
            this.cbClose.UseVisualStyleBackColor = true;
            this.cbClose.Click += new System.EventHandler(this.cbClose_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.AllowDrop = true;
            resources.ApplyResources(this.checkedListBox1, "checkedListBox1");
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Click += new System.EventHandler(this.checkedListBox1_Click);
            this.checkedListBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.checkedListBox1_MouseDoubleClick);
            // 
            // timer_UpdatecbCloseStatus
            // 
            this.timer_UpdatecbCloseStatus.Tick += new System.EventHandler(this.timer_UpdatecbCloseStatus_Tick);
            // 
            // labelInformation
            // 
            resources.ApplyResources(this.labelInformation, "labelInformation");
            this.labelInformation.Name = "labelInformation";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // comboBox1
            // 
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            resources.GetString("comboBox1.Items"),
            resources.GetString("comboBox1.Items1"),
            resources.GetString("comboBox1.Items2")});
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelInformation);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.cbClose);
            this.Controls.Add(this.cbReadPMI);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cbReadPMI;
        private System.Windows.Forms.Button cbClose;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Timer timer_UpdatecbCloseStatus;
        private System.Windows.Forms.Label labelInformation;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox comboBox1;
    }
}

