using System.Collections.Generic;
using NXOpen;
using NXOpen.Annotations;
using NXOpen.UF;


public static class PmiHighlighter
{
    private static List<NXOpen.Annotations.Pmi> highlightedPMIs = new List<NXOpen.Annotations.Pmi>();
    private static List<NXObject> highlightedObjects = new List<NXObject>();
    private static UFSession ufSession = UFSession.GetUFSession();

    public static void ToggleHighlights(List<NXOpen.Annotations.Pmi> pmiList)
    {
        ClearHighlights();

        foreach (var pmi in pmiList)
        {
            if (pmi == null) continue;
            // highlights the PMI in the list
            ufSession.Disp.SetHighlight(pmi.Tag, 1);
            highlightedPMIs.Add(pmi);

            // highlights the associated object
            AssociatedObject associatedObject = pmi.GetAssociatedObject();
            if (associatedObject == null) continue;
            NXObject[] objects = associatedObject.GetObjects();
            if (objects == null || objects.Length == 0) continue;
            foreach (var obj in objects)
            {
                if (obj == null) continue;
                ufSession.Disp.SetHighlight(obj.Tag, 1);
                highlightedObjects.Add(obj);
            }
        }
    }
    public static void ClearHighlights()
    {
        foreach (var pmi in highlightedPMIs)
        {
            ufSession.Disp.SetHighlight(pmi.Tag, 0);
        }
        highlightedPMIs.Clear();
        foreach (var obj in highlightedObjects)
        {
            ufSession.Disp.SetHighlight(obj.Tag, 0);
        }
        highlightedObjects.Clear();
    }
}