using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backend_SC;

namespace UI_Summerschool_Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void cbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbReadPMI_Click(object sender, EventArgs e)
        {
            // PMI.Read();
            StringBuilder sb = new StringBuilder();
            sb = PMI.Read();
            if (sb == null)
                sb.AppendLine("keine PMI's gefunden ");

            tbPMIs.Text = sb.ToString();
        }
    }
}
