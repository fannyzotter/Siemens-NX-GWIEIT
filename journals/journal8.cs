// NX Student Edition 2412
// Journal created by fanny on Mon Apr  7 07:43:02 2025 Mitteleuropäische Sommerzeit

//
using System;
using NXOpen;

public class NXJournal
{
  public static void Main(string[] args)
  {
    NXOpen.Session theSession = NXOpen.Session.GetSession();
    NXOpen.Part workPart = theSession.Parts.Work;
    NXOpen.Part displayPart = theSession.Parts.Display;
    theSession.DeleteUndoMark((NXOpen.Session.UndoMarkId)(350), null);
    
    theSession.DeleteUndoMark((NXOpen.Session.UndoMarkId)(350), null);
    
    NXOpen.Features.EditWithRollbackManager editWithRollbackManager1 = ((NXOpen.Features.EditWithRollbackManager)workPart.Find("EditWithRollbackData {41_Machinery_Part_FBM}"));
    editWithRollbackManager1.UpdateFeature(false);
    
    editWithRollbackManager1.Stop();
    
    editWithRollbackManager1.Destroy();
    
    // ----------------------------------------------
    //   Menu: Tools->Automation->Journal->Stop Recording
    // ----------------------------------------------
    
  }
  public static int GetUnloadOption(string dummy) { return (int)NXOpen.Session.LibraryUnloadOption.Immediately; }
}
