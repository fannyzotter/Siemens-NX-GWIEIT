using System.Collections.Generic;
using NXOpen;
using NXOpen.CAM;
using NXOpen.UF;
using NXOpen.Annotations;
using NXOpen.BlockStyler;
using System;

public static class CamHighlighter
{
    private static List<Face> highlightedFaces = new List<Face>();
    private static UFSession ufSession = UFSession.GetUFSession();

    public static void SetCamHighlight(NXOpen.CAM.Operation operation, Dictionary<NXOpen.CAM.Operation, List<Face>> camOperationFaceMap)
    {
        // Vorherige Faces deaktivieren
        foreach (var face in highlightedFaces)
        {
            ufSession.Disp.SetHighlight(face.Tag, 0);
        }
        highlightedFaces.Clear();

        if (operation == null) return;

        // Faces für die aktuelle Operation abrufen
        if (camOperationFaceMap.TryGetValue(operation, out List<Face> faces))
        {
            foreach (var face in faces)
            {
                if (highlightedFaces.Contains(face))
                {
                    ufSession.Disp.SetHighlight(face.Tag, 0);
                    highlightedFaces.Remove(face);
                }
                else
                {
                    ufSession.Disp.SetHighlight(face.Tag, 1);
                    highlightedFaces.Add(face);
                }
            }
        }
    }
    public static void ClearCamHighlight(Dictionary<NXOpen.CAM.Operation, List<Face>> camOperationFaceMap)
    {
        if (ufSession == null)
            ufSession = UFSession.GetUFSession();

        foreach (var kvp in camOperationFaceMap)
        {
            if (kvp.Value == null) continue;

            foreach (var face in kvp.Value)
            {
                if (face != null && face.Tag > 0)
                {
                    try
                    {
                        ufSession.Disp.SetHighlight(face.Tag, 0);
                    }
                    catch (Exception ex)
                    {
                        UI.GetUI().NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.Message);
                    }
                }
            }
        }
        highlightedFaces.Clear();
    }

    // function that selects the strings in the listbox of all the matching operations
    public static void SelectConnectedCam(ListBox listBox, List<NXOpen.CAM.Operation> connectedCam, Dictionary<string, NXOpen.CAM.Operation> camMap)
    {
        string[] selectedItems = null;
        if (connectedCam == null || connectedCam.Count == 0)
        {
            return;
        }

        foreach (var operation in connectedCam)
        {
            foreach (var kvp in camMap)
            {
                if (kvp.Value.Tag == operation.Tag)
                {
                    selectedItems = new string[] { kvp.Key };
                    break;
                }
            }
        }
        listBox.SetSelectedItemStrings(selectedItems);
    }

}
