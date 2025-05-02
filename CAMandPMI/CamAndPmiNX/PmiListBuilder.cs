using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.Annotations;
using NXOpen.BlockStyler;

public static class PmiListBuilder
{
    public static void PopulatePmiList(ListBox listBox, Dictionary<string, Pmi> pmiMap, Dictionary<Pmi, List<Face>> pmiFaceMap, Dictionary<string, bool> pmiState)
    {
        NXOpen.Session theSession = NXOpen.Session.GetSession();
        NXOpen.Part workPart = theSession.Parts.Work;
        if (workPart == null)
        {
            UI.GetUI().NXMessageBox.Show("Error", NXMessageBox.DialogType.Error, "No part loaded.");
        }

        PmiManager pmiManager = workPart.PmiManager;
        PmiCollection pmis = pmiManager.Pmis;

        List<string> pmiNames = new List<string>(); 
        int index = 0;
            
        foreach (NXOpen.Annotations.Pmi pmi in pmis)
        {
            AssociatedObject assObject = pmi.GetAssociatedObject();
            NXObject[] objekt = assObject.GetObjects();
            // generate a unique key for each PMI
            string key = index++.ToString();

            pmiMap.Add(key, pmi);
        }

        foreach (var key in pmiMap.Keys)
        {
            pmiState[key] = false;
        }
    }
    public static void PopulatePmiList(ListBox listBox, Dictionary<string, Pmi> pmiMap)
    {
        try
        {  
            List<string> pmiNames = new List<string>();

            // create the names of the PMIs and add them to the list
            foreach (var kvp in pmiMap)
            {
                string key = kvp.Key;
                Pmi pmi = kvp.Value;

                string prefix = pmiState[key] ? "[x] " : "[ ] ";

                string pmiName = pmi.Name;
                string pmiType = pmi.Type.ToString();
                string pmiIndex = key;
               
                string displayText = prefix + pmiName + " - " + pmiType + " - " + "[" + key + "]";

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

            // set all the PMIs in the list  
            listBox.SetListItems(pmiNames.ToArray());
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("PopulatePmiList"))
            {
                return;
            }
        }
    }
    public static string GetSelectedPmiString(ListBox listBox)
    {
        string[] selectedItems = listBox.GetSelectedItemStrings();
        listBox.SetSelectedItemStrings(selectedItems); // Set the selected items again to ensure they are highlighted
        if (selectedItems == null || selectedItems.Length == 0)
        {
            return null;
        }
        return selectedItems[0];
    }

    public static string GetPmiKey(string pmiListItem)
    {
        // extract the key from the list item string
        string[] pmiStrings = pmiListItem.Split('-');
        string key = pmiStrings[pmiStrings.Length-1].Trim().Trim('[', ']');
        return key;
    }
}
