using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.CAM;
using NXOpen.BlockStyler;

public static class CamListBuilder
{
    public static void PopulateCamOperationList(ListBox listBox)
    {
        try
        {
            Session theSession = Session.GetSession();
            UI theUI = UI.GetUI();
            Part workPart = theSession.Parts.Work;

            if (workPart == null)
            {
                theUI.NXMessageBox.Show("Fehler", NXMessageBox.DialogType.Error, "Kein Part geladen.");
                return;
            }

            CAMSetup camSetup = workPart.CAMSetup;
            NCGroup rootGroup = camSetup.GetRoot(CAMSetup.View.ProgramOrder);

            List<string> operationNames = new List<string>();
            CollectOperationsRecursive(rootGroup, operationNames);

            listBox.SetListItems(operationNames.ToArray());
        }
        catch (Exception ex)
        {
            UI.GetUI().NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
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