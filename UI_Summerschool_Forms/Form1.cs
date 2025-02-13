using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backend_SC;
using NXOpen;
using NXOpen.Annotations;
using NXOpen.ShipDesign;
using NXOpen.UF;
using NXOpen.Utilities;
using Timer = System.Windows.Forms.Timer;

namespace UI_Summerschool_Forms
{
    public partial class Form1 : Form
    {
        private FormattedMessageBox formattedMessageBox;

        public Form1()
        {
            InitializeComponent();
            SetFormLanguageFromNX(); //calls method when starting the program so that the NX language is retrieved and the program starts in the same language
            cbClose.Enabled = false; //unables "Close Window" button when starting the programm

            timer_UpdatecbCloseStatus = new Timer(); // Timer to check whether all checkboxes are ticked
            timer_UpdatecbCloseStatus.Interval = 100; // Update every 100 milliseconds
            timer_UpdatecbCloseStatus.Tick += timer_UpdatecbCloseStatus_Tick; 
            timer_UpdatecbCloseStatus.Start();

            formattedMessageBox = new FormattedMessageBox(); // Form2 for information window
        }

        private void cbClose_Click(object sender, EventArgs e) // "Close Window" --> the last highlighted PMI is unhighlighted, program closes 
        {
            UnhighlightPreviousPMI();

            this.Close();
        }

        // Read all PMIs

        private void cbReadPMI_Click(object sender, EventArgs e) // "Read all PMIs" --> Retrieving the PMIs and displaying them in checkedListBox
        {
            List<Pmi> pmiList = PMI.Read(); //retrieving PMI
            checkedListBox1.DisplayMember = "Name";  
            checkedListBox1.Items.Clear();

            for (int i = 0; i < pmiList.Count; i++) // displaying the Name as an Item in the checkedListBox; adding an Item for each PMI
            {
                Pmi pmi = pmiList[i];
                string pmiName = GeneratePMIName(pmi, i + 1);  // name displayed is generated with GeneratePMIName
                checkedListBox1.Items.Add(new { Name = pmiName, Pmi = pmi });
            }
        }

        public static string GeneratePMIName(Pmi pmi, int index) // Generation of the PMI name as in NX using DimensionData, Type, Index and Name
        {
            try
            {
                string pmiType = pmi.Type.ToString();
                string pmiIndex = pmi.Index.ToString();
                string pmiName = pmi.Name.ToString();

                return $"{pmi.GetDimensionData().Type} {pmiType} ({pmiIndex}) \"{pmiName}\""; // Combines the individual pieces of information into a name
            }
            catch (Exception ex) // error handling for surface finish as they don´t have "GetDimensionData().Type"
            {

                string pmiType = pmi.Type.ToString();
                string pmiIndex = pmi.Index.ToString();
                string pmiName = pmi.Name.ToString();

                return $"{pmiType} ({pmiIndex}) \"{pmiName}\"";
            }

        }

        // retrieve PMI information to display in the information window

        public static string ReadPMIInfo(Pmi pmi) // retrieving information for the  information window with error handling in case any information can not be retrieved
        {
            if (pmi == null)
                return "PMI not found.";

            StringBuilder pmiInfo = new StringBuilder();

            try
            {
                pmiInfo.AppendLine($"PMI Type: {pmi.Type}");
            }
            catch (Exception ex)
            {
                pmiInfo.AppendLine($"Error when retrieving the PMI type: {ex.Message}");
            }

            try
            {
                pmiInfo.AppendLine($"PMI Name: {pmi.Name}");
            }
            catch (Exception ex)
            {
                pmiInfo.AppendLine($"Error when retrieving the PMI name: {ex.Message}");
            }

            try
            {
                pmiInfo.AppendLine($"PMI Journal Identifier: {pmi.JournalIdentifier}");
            }
            catch (Exception ex)
            {
                pmiInfo.AppendLine($"Error when retrieving the PMI Journal Identifiers: {ex.Message}");
            }

            try
            {
                pmiInfo.AppendLine($"PMI Index: {pmi.Index}");
            }
            catch (Exception ex)
            {
                pmiInfo.AppendLine($"Error when retrieving the PMI Index: {ex.Message}");
            }

            try
            {
                pmiInfo.AppendLine($"Tag: {pmi.Tag}");
            }
            catch (Exception ex)
            {
                pmiInfo.AppendLine($"Error when retrieving the PMI Tags: {ex.Message}");
            }

            try
            {
                pmiInfo.AppendLine($"OwningPart: {pmi.OwningPart}");
            }
            catch (Exception ex)
            {
                pmiInfo.AppendLine($"Error when retrieving the OwningParts: {ex.Message}");
            }

            try
            {
                pmiInfo.AppendLine($"Value: {pmi.GetDimensionData().DimensionValue}");
            }
            catch (Exception ex)
            {
                pmiInfo.AppendLine($"Error when retrieving the DimensionValue: {ex.Message}");
            }

            try
            {
                pmiInfo.AppendLine($"Lower Delta: {pmi.GetDimensionData().LowerDelta}");
            }
            catch (Exception ex)
            {
                pmiInfo.AppendLine($"Error when retrieving LowerDelta: {ex.Message}");
            }

            try
            {
                pmiInfo.AppendLine($"Upper Delta: {pmi.GetDimensionData().UpperDelta}");
            }
            catch (Exception ex)
            {
                pmiInfo.AppendLine($"Error when retrieving the UpperDelta: {ex.Message}");
            }

            try
            {
                var associatedObjects = pmi.GetAssociatedObject();
                if (associatedObjects != null)
                {
                    foreach (var assocObj in associatedObjects.GetObjects())
                    {
                        try
                        {
                            string assocName = assocObj.Name;
                            string assocJournalID = assocObj.JournalIdentifier;
                            Tag assocTag = assocObj.Tag;
                            pmiInfo.AppendLine($"Associated Object Name: {assocName}, JournalID: {assocJournalID}, Tag: {assocTag}");
                        }
                        catch (Exception ex)
                        {
                            pmiInfo.AppendLine($"Error when retrieving the Associated Objects: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                pmiInfo.AppendLine($"Error when retrieving the Associated Objects: {ex.Message}");
            }

            return pmiInfo.ToString();
        }

        public void checkedListBox1_MouseDoubleClick(object sender, MouseEventArgs e) // Display of Form 2 (information window) when double-clicking on an item in the checkedlistbox; pmiDetails and pmiName are changed dynamically in Form2 (depending on the selected PMI)
        {
            int index = checkedListBox1.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                var selectedItem = checkedListBox1.Items[index] as dynamic;
                Pmi selectedPmi = selectedItem.Pmi;
                string pmiDetails = ReadPMIInfo(selectedPmi);
                string pmiName = GeneratePMIName(selectedPmi, index + 1);

                ShowFormattedMessageBox(pmiDetails, pmiName); 
            }
        }

        private void ShowFormattedMessageBox(string text, string pmiName)
        {
            formattedMessageBox = new FormattedMessageBox();
            formattedMessageBox.SetTextAndFormat(text); // displaying PMI Information in information window
            formattedMessageBox.SetLabel2Text(pmiName); // so that the PMI name appears in label2

            formattedMessageBox.Show();
        }

        //"Close Window" button

        private void UpdatecbCloseStatus() //for enabling the "Close Window" Button just when all PMIs are ticked
        {
            cbClose.Enabled = CheckedListBox1_AllItemsChecked(checkedListBox1);
        }

        private bool CheckedListBox1_AllItemsChecked(CheckedListBox checkedListBox1) // method checks whether all items are ticked
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (!checkedListBox1.GetItemChecked(i))
                {
                    return false;
                }
            }
            return true;
        }

        private void timer_UpdatecbCloseStatus_Tick(object sender, EventArgs e) // Connection to the timer to check every 100 milliseconds whether all items are ticked
        {
            UpdatecbCloseStatus();
        }

        // Highlighting function

        private void checkedListBox1_Click(object sender, EventArgs e) // when clicking on an item the method "HighlightPMI" is called up
        {
            int index = checkedListBox1.SelectedIndex;
            if (index != ListBox.NoMatches)
            {
                var selectedItem = checkedListBox1.Items[index] as dynamic;
                Pmi selectedPmi = selectedItem.Pmi;

                // Highlight the selected PMI
                HighlightPMI(selectedPmi);
            }

        }

        private Pmi previouslyHighlightedPmi = null; // so that only the last PMI clicked on is highlighted
        private void HighlightPMI(Pmi pmi)
        {
            Session theSession = Session.GetSession();
            UFSession ufSession = UFSession.GetUFSession();

            try
            {
               
                if (previouslyHighlightedPmi != null)
                {
                    ufSession.Disp.SetHighlight(previouslyHighlightedPmi.Tag, 0); // Unhighlight the previously highlighted PMI; 0 for highlight off
                }
                                
                ufSession.Disp.SetHighlight(pmi.Tag, 1); // Highlight the current PMI; 1 for highlight on

                previouslyHighlightedPmi = pmi; // Store the current PMI as the previously highlighted PMI
            }
            catch (Exception e)
            {
                theSession.ListingWindow.WriteLine("An error occurred while highlighting the PMI: " + e.Message);
            }
        }

        public void UnhighlightPreviousPMI() // previously highlighted PMI is unhighlighted
        {
            if (previouslyHighlightedPmi != null)
            {
                UFSession ufSession = UFSession.GetUFSession();
                try
                {
                    ufSession.Disp.SetHighlight(previouslyHighlightedPmi.Tag, 0); // 0 for highlight off
                    previouslyHighlightedPmi = null;
                }
                catch (Exception e)
                {
                    Session.GetSession().ListingWindow.WriteLine("An error occurred while unhighlighting the PMI: " + e.Message);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) //when closing the program the last PMI clicked on isn´t highlighted anymore
        {
            UnhighlightPreviousPMI();
        }

        // language change

        private void SetFormLanguageFromNX() // retrieved language from NX; method is called up when starting the program
        {
            try
            {
                string nxLanguage = Environment.GetEnvironmentVariable("UGII_LANG");
                if (string.IsNullOrEmpty(nxLanguage))
                {
                    MessageBox.Show("Die Umgebungsvariable UGII_LANG ist nicht gesetzt.");
                }
                else
                {
                    SetLanguage(nxLanguage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Abrufen der NX Sprache: " + ex.Message);
            }
        }

        private void SetLanguage(string languageCode) // language is set to the same language NX is set to (works for English, Spanish and German), not just the form but also the CultureInfo (dates, numbers, ...)
        {
            
            switch (languageCode.ToLower())
            {
                case "english":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                    break;
                case "deutsch":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");
                    break;
                case "espanol":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");
                    break;
                default:
                    MessageBox.Show("Nicht unterstützte Sprache: " + languageCode);
                    return;
            }
            this.Controls.Clear();
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // option to manually change the language
        {
            if (comboBox1.Text == "English")
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            }
            if (comboBox1.Text == "Deutsch")
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");
            }
            if (comboBox1.Text == "Espanol")
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es");
            }
            this.Controls.Clear();
            InitializeComponent();
        }

    }
}