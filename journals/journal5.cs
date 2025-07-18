// NX Student Edition 2412
// Journal created by fanny on Mon Apr  7 07:39:35 2025 Mitteleuropäische Sommerzeit

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
    
    NXOpen.Features.AdmMoveFace admMoveFace1 = ((NXOpen.Features.AdmMoveFace)workPart.Features.FindObject("MOVE_FACE(22)"));
    NXOpen.Features.EditWithRollbackManager editWithRollbackManager1;
    editWithRollbackManager1 = workPart.Features.StartEditWithRollbackManager(admMoveFace1, markId1);
    
    NXOpen.Session.UndoMarkId markId2;
    markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start");
    
    NXOpen.Features.AdmMoveFaceBuilder admMoveFaceBuilder1;
    admMoveFaceBuilder1 = workPart.Features.CreateAdmMoveFaceBuilder(admMoveFace1);
    
    admMoveFaceBuilder1.FaceToMove.RelationScope = 1023;
    
    admMoveFaceBuilder1.Motion.DistanceAngle.OrientXpress.AxisOption = NXOpen.GeometricUtilities.OrientXpressBuilder.Axis.Passive;
    
    admMoveFaceBuilder1.Motion.DistanceAngle.OrientXpress.PlaneOption = NXOpen.GeometricUtilities.OrientXpressBuilder.Plane.Passive;
    
    admMoveFaceBuilder1.Motion.AlongCurveAngle.AlongCurve.IsPercentUsed = true;
    
    admMoveFaceBuilder1.Motion.AlongCurveAngle.AlongCurve.Expression.SetFormula("0");
    
    admMoveFaceBuilder1.Motion.AlongCurveAngle.AlongCurve.Expression.SetFormula("0");
    
    admMoveFaceBuilder1.FaceToMove.CloneScope = 511;
    
    admMoveFaceBuilder1.FaceToMove.UseFindClone = true;
    
    admMoveFaceBuilder1.FaceToMove.UseFindRelated = true;
    
    admMoveFaceBuilder1.FaceToMove.UseFaceBrowse = true;
    
    admMoveFaceBuilder1.FaceToMove.RelationScope = 1023;
    
    admMoveFaceBuilder1.FaceToMove.CloneScope = 511;
    
    theSession.SetUndoMarkName(markId2, "Move Face Dialog");
    
    admMoveFaceBuilder1.FaceToMove.CoaxialEnabled = true;
    
    admMoveFaceBuilder1.FaceToMove.TangentEnabled = true;
    
    admMoveFaceBuilder1.FaceToMove.CoplanarEnabled = true;
    
    admMoveFaceBuilder1.FaceToMove.CoplanarAxesEnabled = true;
    
    admMoveFaceBuilder1.OnApplyPre();
    
    NXOpen.Session.UndoMarkId markId3;
    markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Move Face");
    
    theSession.DeleteUndoMark(markId3, null);
    
    NXOpen.Session.UndoMarkId markId4;
    markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Move Face");
    
    NXOpen.NXObject nXObject1;
    nXObject1 = admMoveFaceBuilder1.Commit();
    
    theSession.DeleteUndoMark(markId4, null);
    
    theSession.SetUndoMarkName(markId2, "Move Face");
    
    NXOpen.Expression expression1 = admMoveFaceBuilder1.Motion.DistanceValue;
    admMoveFaceBuilder1.Destroy();
    
    theSession.DeleteUndoMark(markId2, null);
    
    editWithRollbackManager1.UpdateFeature(false);
    
    editWithRollbackManager1.Stop();
    
    editWithRollbackManager1.Destroy();
    
    NXOpen.Session.UndoMarkId markId5;
    markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Redefine Feature");
    
    NXOpen.Features.OffsetFace offsetFace1 = ((NXOpen.Features.OffsetFace)workPart.Features.FindObject("OFFSET(31)"));
    NXOpen.Features.EditWithRollbackManager editWithRollbackManager2;
    editWithRollbackManager2 = workPart.Features.StartEditWithRollbackManager(offsetFace1, markId5);
    
    NXOpen.Session.UndoMarkId markId6;
    markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start");
    
    NXOpen.Features.OffsetFaceBuilder offsetFaceBuilder1;
    offsetFaceBuilder1 = workPart.Features.CreateOffsetFaceBuilder(offsetFace1);
    
    theSession.SetUndoMarkName(markId6, "Offset Face Dialog");
    
    // ----------------------------------------------
    //   Dialog Begin Offset Face
    // ----------------------------------------------
    // ----------------------------------------------
    //   Menu: Tools->Automation->Journal->Stop Recording
    // ----------------------------------------------
    
  }
  public static int GetUnloadOption(string dummy) { return (int)NXOpen.Session.LibraryUnloadOption.Immediately; }
}
