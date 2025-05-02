using System.Collections.Generic;
using NXOpen;
using NXOpen.Annotations;
using NXOpen.UF;
using System.Collections.Generic;
using System.Linq;


public static class PmiHighlighter
{

    private static NXOpen.Annotations.Pmi highlightedPMI;
    private static List<Face> highlightedFaces = new List<Face>();
    private static UFSession ufSession = UFSession.GetUFSession();

    public static void ToggleHighlight(NXOpen.Annotations.Pmi selectedPmi)
    {
        foreach (Face obj in highlightedFaces)
        {
            ufSession.Disp.SetHighlight(obj.Tag, 0);
        }
        highlightedFaces.Clear();

        AssociatedObject assObject = selectedPmi.GetAssociatedObject();
        NXObject[] objekts = assObject.GetObjects();

        foreach (NXObject obj in objekts)
        {
            if (obj is Face face)
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


    public static void ClearPmiHighlight(Dictionary<Pmi, List<Face>> pmiFaceMap)
    {
        foreach (var kvp in pmiFaceMap)
        {
            foreach (var face in kvp.Value)
            {
                ufSession.Disp.SetHighlight(face.Tag, 0);
            }
        }
        highlightedFaces.Clear();
    }
}