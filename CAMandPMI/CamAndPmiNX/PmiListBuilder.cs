using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.Annotations;
using NXOpen.BlockStyler;

public static class PmiListBuilder
{
    public static void PopulatePmiList(ListBox listBox, Dictionary<string, Pmi> pmiMap, Dictionary<Pmi, List<Face>> pmiFaceMap)
    {
        try
        {
            // Get the current session and work part
            NXOpen.Session theSession = NXOpen.Session.GetSession();
            NXOpen.Part workPart = theSession.Parts.Work;

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
                AssociatedObject assObject = pmi.GetAssociatedObject();
                NXObject[] objekt = assObject.GetObjects();

                string pmiType = pmi.Type.ToString();
                string pmiIndex = pmi.Index.ToString();
                string pmiName = pmi.Name;

                string key = $"PMI_{index++}";

                string displayText = $"{key} - {pmiType} ({pmiIndex}) \"{pmiName}\"";

                pmiNames.Add(displayText);

                List<Face> faces = new List<Face>();
                foreach (NXObject nxobj in objekt)
                {
                    if (nxobj is Face objface)
                    {
                        faces.Add(objface);
                    }
                }
                if (!pmiFaceMap.ContainsKey(pmi))
                {
                    pmiFaceMap[pmi] = faces;
                }
                pmiMap[key] = pmi;
            }
            // Clear any previous entries in the list box
            listBox.SetListItems(pmiNames.ToArray());
        }
        catch (Exception ex)
        {
            UI theUI = UI.GetUI();
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }

    public static NXOpen.Annotations.Pmi GetSelectedPmi(ListBox listBox, Dictionary<string, NXOpen.Annotations.Pmi> pmiMap)
    {
        string[] selectedItems = listBox.GetSelectedItemStrings();
        listBox.SetSelectedItemStrings(selectedItems); // Set the selected items again to ensure they are highlighted
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
