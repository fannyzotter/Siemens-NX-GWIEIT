using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Backend_SC;
using NXOpen;
using NXOpen.Annotations;
using NXOpen.UF;

namespace UI_Summerschool_Forms
{
    public partial class FormattedMessageBox : Form
    {
        public FormattedMessageBox()
        {
            InitializeComponent();
            RichTextBoxControl.ReadOnly = true;  // Ensures that the user cannot change the text
            RichTextBoxControl.BorderStyle = BorderStyle.None;
            RichTextBoxControl.Font = new Font("Verdana", 12); // Sets the font for the entire RichTextBox
        }

        public void SetTextAndFormat(string text) //PMI information is displayed in information window
        {
            RichTextBoxControl.Text = text;
            RichTextBoxControl.Font = new Font("Verdana", 12);
        }


        public void SetLabel2Text(string pmiName) // label2 displays the name of the called up PMI
        {
            label2.Text = pmiName;
        }

        public RichTextBox RichTextBoxControl
        {
            get { return RichTextBox; }
        }

    }
}