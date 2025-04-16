using NXOpen;
using NXOpen.Annotations;
using NXOpen.UF;


public static class PmiHighlighter
{
    private static NXOpen.Annotations.Pmi highlightedPMI;
    private static NXObject highlightedObject;
    private static UFSession ufSession = UFSession.GetUFSession();

    public static void ToggleHighlight(NXOpen.Annotations.Pmi selectedPmi)
    {
        if (highlightedPMI != null || highlightedPMI == selectedPmi)
        {
            ufSession.Disp.SetHighlight(highlightedPMI.Tag, 0);
            highlightedPMI = null;
        }
        else
        {
            ufSession.Disp.SetHighlight(selectedPmi.Tag, 1);
            highlightedPMI = selectedPmi;
        }

        AssociatedObject assObject = selectedPmi.GetAssociatedObject();
        NXObject objekt = assObject.GetObjects()[0];

        if (highlightedObject != null || highlightedObject == objekt)
        {
            ufSession.Disp.SetHighlight(highlightedObject.Tag, 0);
            highlightedObject = null;
        }
        else
        {
            ufSession.Disp.SetHighlight(objekt.Tag, 1);
            highlightedObject = objekt;
        }
    }
}