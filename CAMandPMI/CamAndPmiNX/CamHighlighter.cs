using System.Collections.Generic;
using NXOpen;
using NXOpen.CAM;
using NXOpen.UF;
using NXOpen.Annotations;
using NXOpen.BlockStyler;

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
        foreach (var kvp in camOperationFaceMap)
        {
            foreach (var face in kvp.Value)
            {
                ufSession.Disp.SetHighlight(face.Tag, 0);
            }
        }
        highlightedFaces.Clear();
    }



    // function that selects the strings in the listbox of all the matching operations
    public static void SelectMatchingOperations(ListBox listBox, List<NXOpen.CAM.Operation> matchingOperations, Dictionary<string, NXOpen.CAM.Operation> camMap)
    {
        string[] selectedItems = null;
        if (matchingOperations == null || matchingOperations.Count == 0)
        {
            return;
        }

        foreach (var operation in matchingOperations)
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
