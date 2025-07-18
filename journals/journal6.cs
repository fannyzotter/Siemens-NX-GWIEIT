// NX Student Edition 2412
// Journal created by fanny on Mon Apr  7 07:41:52 2025 Mitteleuropäische Sommerzeit

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
    NXOpen.Session.UndoMarkId markId1;
    markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Redefine Feature");
    
    NXOpen.Features.OffsetFace offsetFace1 = ((NXOpen.Features.OffsetFace)workPart.Features.FindObject("OFFSET(30)"));
    NXOpen.Features.EditWithRollbackManager editWithRollbackManager1;
    editWithRollbackManager1 = workPart.Features.StartEditWithRollbackManager(offsetFace1, markId1);
    
    NXOpen.Session.UndoMarkId markId2;
    markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start");
    
    NXOpen.Features.OffsetFaceBuilder offsetFaceBuilder1;
    offsetFaceBuilder1 = workPart.Features.CreateOffsetFaceBuilder(offsetFace1);
    
    theSession.SetUndoMarkName(markId2, "Offset Face Dialog");
    
    offsetFaceBuilder1.Distance.SetFormula("1");
    
    NXOpen.Session.UndoMarkId markId3;
    markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Offset Face");
    
    theSession.DeleteUndoMark(markId3, null);
    
    NXOpen.Session.UndoMarkId markId4;
    markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Offset Face");
    
    NXOpen.NXObject nXObject1;
    nXObject1 = offsetFaceBuilder1.Commit();
    
    theSession.DeleteUndoMark(markId4, null);
    
    theSession.SetUndoMarkName(markId2, "Offset Face");
    
    NXOpen.Expression expression1 = offsetFaceBuilder1.Distance;
    offsetFaceBuilder1.Destroy();
    
    theSession.DeleteUndoMark(markId2, null);
    
    editWithRollbackManager1.UpdateFeature(false);
    
    editWithRollbackManager1.Stop();
    
    editWithRollbackManager1.Destroy();
    
    // ----------------------------------------------
    //   Menu: Tools->Automation->Journal->Stop Recording
    // ----------------------------------------------
    
  }
  public static int GetUnloadOption(string dummy) { return (int)NXOpen.Session.LibraryUnloadOption.Immediately; }
}
