using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.CAM;
using NXOpen.BlockStyler;

public static class CamListBuilder
{
    public static void PopulateCamOperationList(ListBox listBox)
    {
        // Clear the list box
        listBox.SetListItems(new string[0]);


        Session theSession = Session.GetSession();
        UI theUI = UI.GetUI();
        Part workPart = theSession.Parts.Work;

        if (workPart == null)
        {
            theUI.NXMessageBox.Show("Error", NXMessageBox.DialogType.Error, "No part loaded.");
            return;
        }

        CAMSetup camSetup = null;
        try
        {
            camSetup = workPart.CAMSetup;
        }
        catch (NXOpen.NXException)
        {
            // Kein CAM-Setup vorhanden → kein Fehler, einfach rausgehen
            return;
        }

        try
        {
            NCGroup rootGroup = camSetup.GetRoot(CAMSetup.View.ProgramOrder);

            if (rootGroup == null)
            {
                return;
            }

            List<string> operationNames = new List<string>();
            CollectOperationsRecursive(rootGroup, operationNames);

            listBox.SetListItems(operationNames.ToArray());
        }
        catch (Exception ex)
        {
            UI.GetUI().NXMessageBox.Show("Block Styler Notice", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }

    private static void CollectOperationsRecursive(NXOpen.CAM.NCGroup group, System.Collections.Generic.List<string> operationNames)
    {
        foreach (TaggedObject obj in group.GetMembers())
        {
            if (obj is NXOpen.CAM.Operation operation)
            {
                operationNames.Add(operation.Name);
            }
            else if (obj is NXOpen.CAM.NCGroup subGroup)
            {
                CollectOperationsRecursive(subGroup, operationNames);
            }
        }
    }
}