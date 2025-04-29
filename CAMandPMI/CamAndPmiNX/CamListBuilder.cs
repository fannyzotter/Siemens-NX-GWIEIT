using System;
using System.Collections.Generic;
using System.IO;
using NXOpen;
using NXOpen.CAM;
using NXOpen.BlockStyler;
using NXOpen.UF;

public static class CamListBuilder
{


    public static void PopulateCamOperationList(ListBox listBox)
    {
        try
        {
            Session theSession = Session.GetSession();
            UI theUI = UI.GetUI();
            Part workPart = theSession.Parts.Work;

            NXOpen.Annotations.LabelCollection workpartLabels = workPart.Labels;


            if (workPart == null)
            {
                theUI.NXMessageBox.Show("Fehler", NXMessageBox.DialogType.Error, "Kein Part geladen.");
                return;
            }

            CAMSetup camSetup = workPart.CAMSetup;
            NCGroup rootGroup = camSetup.GetRoot(CAMSetup.View.ProgramOrder);

            NCGroup rootGroupGeom = camSetup.GetRoot(CAMSetup.View.Geometry);


            List<string> operationNames = new List<string>();
            CollectOperationsRecursive(rootGroup, operationNames);

            CollectGeometriesRecursive(rootGroupGeom, operationNames);

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
                operationNames.Add(subGroup.Name);
                CollectOperationsRecursive(subGroup, operationNames);
            }
        }
    }

    private static void CollectGeometriesRecursive(NXOpen.CAM.NCGroup group, System.Collections.Generic.List<string> geomName)
    {
        foreach (TaggedObject obj in group.GetMembers())
        {
            if (obj is NXOpen.CAM.FeatureGeometryGroup geometry)
            {
                CAMFeature[] features = geometry.GetFeatures();
                foreach (CAMFeature feature in features)
                {
                    NXOpen.CAM.Operation[] operation = feature.GetOperations();
                    foreach (NXOpen.CAM.Operation op in operation)
                    {
                        geomName.Add(op.Name);
                    }
                    Face[] face = feature.GetFaces();
                    foreach (Face f in face)
                    {
                        geomName.Add(f.JournalIdentifier);
                    }
                    geomName.Add(feature.Name);
                }
                geomName.Add(geometry.Name);

            }
            else if (obj is NXOpen.CAM.Operation operation)
            {
                geomName.Add(operation.Name);
            }
            else if (obj is NXOpen.CAM.NCGroup subGroup)
            {
                geomName.Add(subGroup.Name);
                CollectGeometriesRecursive(subGroup, geomName);
            }
        }
    }
}