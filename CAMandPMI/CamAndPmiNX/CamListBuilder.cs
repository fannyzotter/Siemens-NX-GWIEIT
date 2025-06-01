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
using System.Linq;
using static NXOpen.Annotations.Pmi;
using static NXOpen.CAE.AeroStructures.MatrixManip;

public static class CamListBuilder

{
    // for CollectOperationsRecursive

    public static void createCamOperationLists(CAMFeature[] camFeatures, Dictionary<string, NXOpen.CAM.Operation> camMap, Dictionary<NXOpen.CAM.Operation, bool> camState)
    {
        NXOpen.CAM.Operation[] camOperations;
        List<string> operationNames = new List<string>();

        foreach (var camFeature in camFeatures)
        {
            if (camFeature is NXOpen.CAM.CAMFeature)
            {
                camOperations = camFeature.GetOperations();
                foreach (var operation in camOperations)
                {
                    camMap[operation.Tag.ToString()] = operation;
                    operationNames.Add(operation.Name);
                }
            }
        }
        foreach (var key in camMap.Values)
        {
            camState[key] = false;
        }
    }

    public static void PopulateCamOperationList(ListBox listBox, Dictionary<string, NXOpen.CAM.Operation> camMap, Dictionary<NXOpen.CAM.Operation, bool> camState)
    {
        try
        {
            List<string> camNames = new List<string>();

            // create the names of the CAM operations and add them to the list
            foreach (var kvp in camMap)
            {
                string key = kvp.Key;
                NXOpen.CAM.Operation camOperation = kvp.Value;
                string camName = kvp.Value.Name;

                string prefix = camState[camOperation] ? "[x] " : "[ ] ";

                string displayText = prefix + camName + " - " + "[" + key + "]";

                camNames.Add(displayText);
            }

            listBox.SetListItems(camNames.ToArray());
        }
        catch (Exception ex)
        {
            /*if (ex.Message.Contains("PopulateCamList"))
            {
                return;
            }*/
            {
                UI.GetUI().NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
            }
        }
    }


    /*public static void PopulateCamOperationList(ListBox listBox, Dictionary<string, NXOpen.CAM.Operation> camMap, CAMSetup camSetup)
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
    }*/

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

                if (camMap.ContainsValue(camOperation))
                {
                    operationNames.Add(camOperation.Name);
                }
            }
            listBox.SetListItems(operationNames.ToArray());
        }
        catch (Exception ex)
        {
            UI.GetUI().NXMessageBox.Show("Exception", NXMessageBox.DialogType.Error, ex.Message);
        }
    }

    // Function to populate a dictionary with a list for all faces for each cam operation
    public static void PopulateCamWithFaces(CAMFeature[] camFeatures, Dictionary<string, NXOpen.CAM.Operation> camMap, Dictionary<NXOpen.CAM.Operation, List<Face>> camOperationFaceMap)
    {
        foreach (var camOperation in camMap)
        {
            List<Face> camFaces = new List<Face>();
            Operation currentOperation = camOperation.Value;

            foreach (CAMFeature feature in camFeatures)
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
            camOperationFaceMap[currentOperation] = camFaces;
        }
    }



    public static Operation GetSelectedCam(ListBox listBox, Dictionary<string, Operation> camMap)
    {
        string[] selectedItems = listBox.GetSelectedItemStrings();
        listBox.SetSelectedItemStrings(selectedItems); // Set the selected items again to ensure they are highlighted
        if (selectedItems == null || selectedItems.Length == 0)
            return null;


        // Extract key from list entry, e.g. "... - [key]"
        string[] parts = selectedItems[0].Split('-');
        string key = parts[parts.Length - 1].Trim().Trim('[', ']');

        if (camMap.TryGetValue(key, out var camOperation))
        {
            return camOperation;
        }

        return null;
    }

    public static void ComparePmiAndCamFaces(Dictionary<Pmi, bool> pmiState, Dictionary<Pmi, List<Face>> pmiFaceMap,
        Dictionary<NXOpen.CAM.Operation, List<Face>> camOperationFaceMap, List<Operation> connectedCam,
        Dictionary<NXOpen.CAM.Operation, bool> camState)
    {
        connectedCam.Clear();
        List<Operation> uniqueCamOps = new List<Operation>();

        foreach (var pmiEntry in pmiState)
        {
            if (!pmiEntry.Value) continue;

            var pmi = pmiFaceMap.Keys.FirstOrDefault(k => k == pmiEntry.Key);
            if (pmi == null || !pmiFaceMap.ContainsKey(pmi)) continue;

            var selectedFaces = pmiFaceMap[pmi];
            if (selectedFaces == null || selectedFaces.Count == 0) continue;

            foreach (var camEntry in camOperationFaceMap)
            {
                var camOperation = camEntry.Key;
                var camFaces = camEntry.Value;

                if (selectedFaces.Any(face => camFaces.Contains(face)))
                {
                    uniqueCamOps.Add(camOperation);
                }
            }
        }

        connectedCam.AddRange(uniqueCamOps);

        ClearCamState(camState);
        // update camstate
        foreach (var oper in connectedCam)
        {
            camState[oper] = true;
        }
    }

    // clear the list of selected cam operations
    public static void ClearCamOperationList(ListBox listBox)
    {
        listBox.SetSelectedItemStrings(new string[] { });
        listBox.SetListItems(new string[] { });
    }

    public static void ClearCamState(Dictionary<NXOpen.CAM.Operation, bool> camState)
    {
        foreach (var key in camState.Keys.ToList())
        {
            camState[key] = false;
        }
    }
}