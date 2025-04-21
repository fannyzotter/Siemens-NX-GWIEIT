using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.Annotations;
using NXOpen.BlockStyler;

public static class PmiListBuilder
{
    public static void PopulatePmiList(ListBox listBox, Dictionary<string, Pmi> pmiMap)
    {

        // Clear the list box
        pmiMap.Clear();

        try
        {
            // Get the current session and work part
            NXOpen.Session theSession = NXOpen.Session.GetSession();
            NXOpen.Part workPart = theSession.Parts.Work;

            // Check if the work part is null
            if (workPart == null)
            {
                UI theUI = UI.GetUI();
                theUI.NXMessageBox.Show("Error", NXMessageBox.DialogType.Error, "No part loaded.");
                return;
            }

            // Access the PMI Manager
            PmiManager pmiManager = workPart.PmiManager;

            // Get all the PMI attributes in the part
            PmiCollection pmis = pmiManager.Pmis;

            // Initialize the list to hold PMI names
            List<string> pmiNames = new List<string>();
            int index = 0;

            // Iterate over each PMI attribute and extract the PMI name
            foreach (NXOpen.Annotations.Pmi pmi in pmis)
            {
                string pmiType = pmi.Type.ToString();
                string pmiIndex = pmi.Index.ToString();
                string pmiName = pmi.Name;

                string key = $"PMI_{index++}";

                string displayText = $"{key} - {pmiType} ({pmiIndex}) \"{pmiName}\"";

                pmiNames.Add(displayText);
                pmiMap[key] = pmi;
            }
            // Clear any previous entries in the list box
            listBox.SetListItems(pmiNames.ToArray());
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("PopulatePmiList"))
            {
                // Kein PMI = kein Problem
                return;
            }
            UI theUI = UI.GetUI();
            theUI.NXMessageBox.Show("Block Styler Hinweis", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }

    public static NXOpen.Annotations.Pmi GetSelectedPmi(ListBox listBox, Dictionary<string, NXOpen.Annotations.Pmi> pmiMap)
    {
        string[] selectedItems = listBox.GetSelectedItemStrings();
        if (selectedItems == null || selectedItems.Length == 0)
            return null;

        string selectedText = selectedItems[0]; // z. B. "PMI_2 - ..."
        string key = selectedText.Split('-')[0].Trim();

        if (pmiMap.TryGetValue(key, out var pmi))
        {
            return pmi;
        }

        return null;
    }
}
