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

public static class CamListBuilder

{
    // for CollectOperationsRecursive
    static int index = 0;

    public static void createCamOperationLists(CAMSetup camSetup, Dictionary<string, NXOpen.CAM.Operation> camMap, Dictionary<NXOpen.CAM.Operation, bool> camState)
    {
        NCGroup programGroup = camSetup.GetRoot(CAMSetup.View.ProgramOrder);
        List<string> operationNames = new List<string>();
        CollectOperationsRecursive(programGroup, operationNames, camMap);
        foreach (var key in camMap.Values)
        {
            camState[key] = false;
        }
    }

    public static void PopulateCamOperationList(ListBox listBox, Dictionary<string, NXOpen.CAM.Operation> camMap, CAMSetup camSetup, Dictionary<NXOpen.CAM.Operation, bool> camState)
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
    public static void PopulateConnectedCamList(UI theUI, Tree tree, Dictionary<string, NXOpen.CAM.Operation> camMap, List<NXOpen.CAM.Operation> connectedCam, Dictionary<Pmi, List<NXOpen.CAM.Operation>> pmiCamOperationMap)
    {

        try
        {

            foreach (var kvp in pmiCamOperationMap)
            {
                string label = kvp.Key.Name + " " + kvp.Key.Type;
                var parent = tree.CreateNode(label);
                tree.InsertNode(parent, null, null, Tree.NodeInsertOption.Last);

                foreach (var cam in kvp.Value)
                {
                    var child = tree.CreateNode(cam.Name);
                    tree.InsertNode(child, parent, null, Tree.NodeInsertOption.Last);
                }
            }
        }
        catch (Exception ex)
        {
            theUI.NXMessageBox.Show("Warning", NXMessageBox.DialogType.Error, ex.Message);
        }
        finally
        {
            tree.Redraw(true); // tree is redrawn after all nodes are deleted and added
        }


        /*
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

            NXOpen.BlockStyler.Node parentNode = tree.CreateNode("Operationen");
            tree.InsertNode(parentNode, null, null, NXOpen.BlockStyler.Tree.NodeInsertOption.Last);

   
            foreach (string camName in operationNames)
            {
                NXOpen.BlockStyler.Node childNode = tree.CreateNode(camName);
                tree.InsertNode(childNode, parentNode, null, NXOpen.BlockStyler.Tree.NodeInsertOption.Last);
            }

            //listBox.SetListItems(operationNames.ToArray());
        }
        catch (Exception ex)
        {
            UI.GetUI().NXMessageBox.Show("Exception", NXMessageBox.DialogType.Error, ex.Message);
        }*/
    }

    private static void CollectOperationsRecursive(NXOpen.CAM.NCGroup group, System.Collections.Generic.List<string> operationNames, Dictionary<string, NXOpen.CAM.Operation> camMap)
    {
        foreach (TaggedObject obj in group.GetMembers())
        {
            if (obj is NXOpen.CAM.Operation operation)
            {
                index++;
                camMap[index.ToString()] = operation;
                //camMap.Add(operation.Name, operation);
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
        Dictionary<NXOpen.CAM.Operation, bool> camState,
        Dictionary<Pmi, List<NXOpen.CAM.Operation>> pmiCamOperationMap)
    {
        connectedCam.Clear();
        // clear pmicamoperationmap
        foreach (var key in pmiCamOperationMap.Keys.ToList())
        {
            pmiCamOperationMap[key].Clear();
        }
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
                    // add the cam operation to the pmicamoperationmap and check if PMI is already in the list
                    if (pmiCamOperationMap.ContainsKey(pmi))
                    {
                        if (!pmiCamOperationMap[pmi].Contains(camOperation))
                        {
                            pmiCamOperationMap[pmi].Add(camOperation);
                        }
                    }
                    else
                    {
                        pmiCamOperationMap[pmi] = new List<NXOpen.CAM.Operation> { camOperation };
                    }
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
        // clear tree 

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
    
    public static Tree ClearTree(Tree tree)
    {
        try
        {
            tree.Redraw(false); // prevents the tree from being redrawn while deleting nodes
            NXOpen.BlockStyler.Node current = tree.RootNode;
            while (current != null)
            {
                NXOpen.BlockStyler.Node next;
                try
                {
                    next = current.NextNode;
                }
                catch
                {
                    break; // node is not valid anymore
                }
                try
                {
                    tree.DeleteNode(current);
                }
                catch
                {
                    break; // access to dead node
                }
                current = next;
            }
        }
        catch (Exception ex)
        {
            UI.GetUI().NXMessageBox.Show("Warning", NXMessageBox.DialogType.Error, ex.Message);
        }
        finally
        {
            tree.Redraw(true); // tree is redrawn after all nodes are deleted and added
        }
        return tree;
    }
}