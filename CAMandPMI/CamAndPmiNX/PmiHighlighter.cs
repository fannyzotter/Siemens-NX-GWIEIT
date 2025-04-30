using System.Collections.Generic;
using NXOpen;
using NXOpen.Annotations;
using NXOpen.UF;


public static class PmiHighlighter
{

    private static NXOpen.Annotations.Pmi highlightedPMI;
    private static UFSession ufSession = UFSession.GetUFSession();

    private static List<NXObject> highlightedObjects = new List<NXObject>();
    

    public static void ToggleHighlight(NXOpen.Annotations.Pmi selectedPmi)
    {        
        AssociatedObject assObject = selectedPmi.GetAssociatedObject();
        NXObject highlightedObject = assObject.GetObjects()[0];

        // if the highlighted object is already highlighted, remove the highlight
        if (highlightedObjects.Contains(highlightedObject))
        {
            ufSession.Disp.SetHighlight(highlightedObject.Tag, 0);
            highlightedObjects.Remove(highlightedObject);
        }
        // if the highlighted object is not highlighted, add the highlight
        else
        {
            ufSession.Disp.SetHighlight(highlightedObject.Tag, 1);
            highlightedObjects.Add(highlightedObject);
        }
    }
}