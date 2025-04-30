using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.Annotations;
using NXOpen.BlockStyler;

public static class PmiListBuilder
{

    public static Dictionary<string, Pmi> CreatePmiMap()
    {
        // create a dictionary to hold the PMI objects
        Dictionary<string, Pmi> pmiMap = new Dictionary<string, Pmi>();
        
        NXOpen.Session theSession = NXOpen.Session.GetSession();
        NXOpen.Part workPart = theSession.Parts.Work;
        if (workPart == null)
        {
            UI.GetUI().NXMessageBox.Show("Error", NXMessageBox.DialogType.Error, "No part loaded.");
            return pmiMap;
        }

        PmiManager pmiManager = workPart.PmiManager;
        PmiCollection pmis = pmiManager.Pmis;

        // add each PMI to the dictionary
        int index = 0;
        foreach (NXOpen.Annotations.Pmi pmi in pmis)
        {
            // generate a unique key for each PMI
            string key = index++.ToString();

            pmiMap.Add(key, pmi);
        }
        return pmiMap;
    }
    public static void PopulatePmiList(ListBox listBox, Dictionary<string, Pmi> pmiMap, Dictionary<string, bool> pmiState)
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
