using System;
using System.Collections.Generic;
using System.IO;
using NXOpen;
using NXOpen.CAM;
using NXOpen.BlockStyler;
using NXOpen.UF;
using NXOpen.CAE;
using Operation = NXOpen.CAM.Operation;
using NXOpen.Annotations;

public static class CamListBuilder
{


    public static void PopulateCamOperationList(ListBox listBox, Dictionary<string, NXOpen.CAM.Operation> camMap, CAMSetup camSetup)
    {
        try
        {
            NCGroup programGroup = camSetup.GetRoot(CAMSetup.View.ProgramOrder);

            List<string> operationNames = new List<string>();
            CollectOperationsRecursive(programGroup, operationNames, camMap);

            listBox.SetListItems(operationNames.ToArray());
        }
        catch (Exception ex)
        {
            UI.GetUI().NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }

        return;
    }

    // populate the listbox with selected cam operations
    public static void PopulateConnectedCamList(ListBox listBox, Dictionary<string, NXOpen.CAM.Operation> camMap, List<NXOpen.CAM.Operation> connectedCam)
    {
        List<string> operationNames = new List<string>();
        string debugText = "Connected CAM Ops:\n";

        try
        {
            if (connectedCam == null) return;

            foreach (var camOperation in connectedCam)
            {
                debugText += $"- {camOperation.Name}\n";

                if (camMap.TryGetValue(camOperation.Name, out var operation))
                {
                    operationNames.Add(operation.Name);
                }
            }
            listBox.SetListItems(operationNames.ToArray());
        }
        catch (Exception ex)
        {
            UI.GetUI().NXMessageBox.Show("Exception", NXMessageBox.DialogType.Error, ex.Message);
        }
    }

    private static void CollectOperationsRecursive(NXOpen.CAM.NCGroup group, System.Collections.Generic.List<string> operationNames, Dictionary<string, NXOpen.CAM.Operation> camMap)
    {
        foreach (TaggedObject obj in group.GetMembers())
        {
            if (obj is NXOpen.CAM.Operation operation)
            {
                camMap[operation.Name] = operation;
                operationNames.Add(operation.Name);
            }
            else if (obj is NXOpen.CAM.NCGroup subGroup)
            {
                CollectOperationsRecursive(subGroup, operationNames, camMap);
            }
        }
    }

    // Function to populate a dictionary with a list for all faces for each cam operation
    public static void PopulateCamWithFaces(CAMSetup camSetup, Dictionary<string, NXOpen.CAM.Operation> camMap, Dictionary<NXOpen.CAM.Operation, List<Face>> camOperationFaceMap)
    {
        NCGroup camGroup = camSetup.GetRoot(CAMSetup.View.Geometry);

        foreach (var camOperation in camMap)
        {
            List<Face> camFaces = new List<Face>();
            Operation currentOperation = camOperation.Value;

            CollectGeometriesRecursive(camGroup, camFaces, currentOperation);
            camOperationFaceMap[currentOperation] = camFaces;
        }
    }

    private static void CollectGeometriesRecursive(NXOpen.CAM.NCGroup group,  System.Collections.Generic.List<Face> camFaces, Operation currentOperation)
    {
        foreach (TaggedObject obj in group.GetMembers())
        {
            if (obj is NXOpen.CAM.FeatureGeometryGroup geometry)
            {
                CAMFeature[] features = geometry.GetFeatures();
                foreach (CAMFeature feature in features)
                {
                    NXOpen.CAM.Operation[] featureOps = feature.GetOperations();
                    foreach (NXOpen.CAM.Operation op in featureOps)
                    {
                        if (op.Tag == currentOperation.Tag)
                        {
                            camFaces.AddRange(feature.GetFaces());
                            break;
                        }
                    }
                }
            }
            else if (obj is NXOpen.CAM.NCGroup subGroup)
            {
                CollectGeometriesRecursive(subGroup, camFaces, currentOperation);
            }
        }
    }


    public static Operation GetSelectedCam(ListBox listBox, Dictionary<string, Operation> camMap)
    {
        string[] selectedItems = listBox.GetSelectedItemStrings();
        listBox.SetSelectedItemStrings(selectedItems); // Set the selected items again to ensure they are highlighted
        if (selectedItems == null || selectedItems.Length == 0)
            return null;

        string selectedText = selectedItems[0]; // z. B. "PMI_2 - ..."
        string key = selectedText;

        if (camMap.TryGetValue(key, out var camOperation))
        {
            return camOperation;
        }

        return null;
    }

    // Function that compares PMI and Cam faces and returns a list of Cam operations that are associated with the same faces
    public static void ComparePmiAndCamFaces(Pmi selectedPmi, Dictionary<Pmi, List<Face>> pmiFaceMap, Dictionary<NXOpen.CAM.Operation, List<Face>> camOperationFaceMap, List<Operation> connectedCam)
    {
        connectedCam.Clear();

        List<Face> selectedFaces = new List<Face>();

        if (selectedPmi == null || !pmiFaceMap.ContainsKey(selectedPmi))
        {
            return;
        }

        selectedFaces = pmiFaceMap[selectedPmi];
        if (selectedFaces == null || selectedFaces.Count == 0)
        {
            return;
        }

        foreach (var camEntry in camOperationFaceMap)
        {
            NXOpen.CAM.Operation camOperation = camEntry.Key;
            List<Face> camFaces = camEntry.Value;
            // Check if any of the faces in the selected PMI match with the faces in the CAM operation
            foreach (var face in selectedFaces)
            {
                if (camFaces.Contains(face))
                {
                    connectedCam.Add(camOperation);
                    break; // No need to check further for this operation
                }
            }
        }
        return;
    }

}