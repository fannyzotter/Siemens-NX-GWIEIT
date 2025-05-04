using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.Annotations;
using NXOpen.BlockStyler;

public static class PmiListBuilder
{
    public static void createPmiLists(Dictionary<string, Pmi> pmiMap, Dictionary<Pmi, List<Face>> pmiFaceMap, Dictionary<Pmi, bool> pmiState)
    {
        UI theUI = UI.GetUI();
        NXOpen.Session theSession = NXOpen.Session.GetSession();
        NXOpen.Part workPart = theSession.Parts.Work;
        if (workPart == null)
        {
            UI.GetUI().NXMessageBox.Show("Error", NXMessageBox.DialogType.Error, "No part loaded.");
        }

        PmiManager pmiManager = workPart.PmiManager;
        PmiCollection pmis = pmiManager.Pmis;

        List<string> pmiNames = new List<string>(); 
            
        foreach (NXOpen.Annotations.Pmi pmi in pmis)
        {
            AssociatedObject assObject = pmi.GetAssociatedObject();
            NXObject[] objekt = assObject.GetObjects();
            // generate a unique key for each PMI
            string key = pmi.Index.ToString() + " " + pmi.Name.ToString();

            pmiMap.Add(key, pmi);

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
        }

        foreach (var key in pmiMap.Values)
        {
            pmiState[key] = false;
        }

    }


    public static void PopulatePmiList(ListBox listBox, Dictionary<string, Pmi> pmiMap, Dictionary<Pmi, bool> pmiState)
    {
        try
        {  
            List<string> pmiNames = new List<string>();

            // create the names of the PMIs and add them to the list
            foreach (var kvp in pmiMap)
            {
                string key = kvp.Key;
                Pmi pmi = kvp.Value;

                string prefix = pmiState[pmi] ? "[x] " : "[ ] ";

                string pmiName = pmi.Name;
                string pmiType = pmi.Type.ToString();
               
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

    public static Pmi GetSelectedPmiFromList(ListBox listBox, Dictionary<string, Pmi> pmiMap)
    {
        string[] selectedItems = listBox.GetSelectedItemStrings();
        listBox.SetSelectedItemStrings(selectedItems); // refresh highlight

        if (selectedItems == null || selectedItems.Length == 0)
        {
            return null;
        }

        // Extract key from list entry, e.g. "... - [key]"
        string[] parts = selectedItems[0].Split('-');
        string key = parts[parts.Length - 1].Trim().Trim('[', ']');

        if (pmiMap.TryGetValue(key, out var pmi))
        {
            return pmi;
        }

        return null;
    }

}
