// NX Student Edition 2412
// Journal created by fanny on Mon Apr  7 07:42:26 2025 Mitteleuropäische Sommerzeit

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
    
    NXOpen.Features.EdgeBlend edgeBlend1 = ((NXOpen.Features.EdgeBlend)workPart.Features.FindObject("BLEND(28)"));
    NXOpen.Features.EditWithRollbackManager editWithRollbackManager1;
    editWithRollbackManager1 = workPart.Features.StartEditWithRollbackManager(edgeBlend1, markId1);
    
    NXOpen.Session.UndoMarkId markId2;
    markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start");
    
    NXOpen.Features.EdgeBlendBuilder edgeBlendBuilder1;
    edgeBlendBuilder1 = workPart.Features.CreateEdgeBlendBuilder(edgeBlend1);
    
    NXOpen.ScCollector scCollector1;
    NXOpen.Expression expression1;
    bool isvalid1;
    edgeBlendBuilder1.GetChainsetAndStatus(0, out scCollector1, out expression1, out isvalid1);
    
    NXOpen.Unit unit1 = ((NXOpen.Unit)workPart.UnitCollection.FindObject("MilliMeter"));
    NXOpen.Expression expression2;
    expression2 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression3;
    expression3 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Unit nullNXOpen_Unit = null;
    NXOpen.Expression expression4;
    expression4 = workPart.Expressions.CreateSystemExpressionWithUnits("0", nullNXOpen_Unit);
    
    NXOpen.Expression expression5;
    expression5 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression6;
    expression6 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.GeometricUtilities.BlendLimitsData blendLimitsData1;
    blendLimitsData1 = edgeBlendBuilder1.LimitsListData;
    
    NXOpen.Point3d origin1 = new NXOpen.Point3d(0.0, 0.0, 0.0);
    NXOpen.Vector3d normal1 = new NXOpen.Vector3d(0.0, 0.0, 1.0);
    NXOpen.Plane plane1;
    plane1 = workPart.Planes.CreatePlane(origin1, normal1, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    NXOpen.GeometricUtilities.FacePlaneSelectionBuilder facePlaneSelectionBuilder1;
    facePlaneSelectionBuilder1 = workPart.FacePlaneSelectionBuilderData.Create();
    
    NXOpen.Expression expression7;
    expression7 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression8;
    expression8 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    theSession.SetUndoMarkName(markId2, "Edge Blend Dialog");
    
    NXOpen.Session.UndoMarkId markId3;
    markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Edge Blend");
    
    theSession.DeleteUndoMark(markId3, null);
    
    NXOpen.Session.UndoMarkId markId4;
    markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Edge Blend");
    
    edgeBlendBuilder1.RemoveEdgeChainAndPointOnEdgeChainData();
    
    edgeBlendBuilder1.Tolerance = 0.01;
    
    edgeBlendBuilder1.AllInstancesOption = false;
    
    edgeBlendBuilder1.RemoveSelfIntersection = true;
    
    edgeBlendBuilder1.PatchComplexGeometryAreas = true;
    
    edgeBlendBuilder1.LimitFailingAreas = true;
    
    edgeBlendBuilder1.ConvexConcaveY = false;
    
    edgeBlendBuilder1.RollOverSmoothEdge = true;
    
    edgeBlendBuilder1.RollOntoEdge = true;
    
    edgeBlendBuilder1.MoveSharpEdge = true;
    
    edgeBlendBuilder1.TrimmingOption = false;
    
    edgeBlendBuilder1.OverlapOption = NXOpen.Features.EdgeBlendBuilder.Overlap.AnyConvexityRollOver;
    
    edgeBlendBuilder1.BlendOrder = NXOpen.Features.EdgeBlendBuilder.OrderOfBlending.ConvexFirst;
    
    edgeBlendBuilder1.SetbackOption = NXOpen.Features.EdgeBlendBuilder.Setback.SeparateFromCorner;
    
    edgeBlendBuilder1.BlendFaceContinuity = NXOpen.Features.EdgeBlendBuilder.FaceContinuity.Tangent;
    
    int csIndex1;
    csIndex1 = edgeBlendBuilder1.AddChainset(scCollector1, "11");
    
    NXOpen.ScCollector scCollector2;
    NXOpen.Expression expression9;
    edgeBlendBuilder1.GetChainset(0, out scCollector2, out expression9);
    
    expression9.RightHandSide = "11";
    
    NXOpen.Features.Feature feature1;
    feature1 = edgeBlendBuilder1.CommitFeature();
    
    NXOpen.DisplayModification displayModification1;
    displayModification1 = theSession.DisplayManager.NewDisplayModification();
    
    displayModification1.ApplyToAllFaces = false;
    
    displayModification1.SetNewGrid(0, 0);
    
    displayModification1.PoleDisplayState = false;
    
    displayModification1.KnotDisplayState = false;
    
    NXOpen.DisplayableObject[] objects1 = new NXOpen.DisplayableObject[1];
    NXOpen.Features.BodyFeature bodyFeature1 = ((NXOpen.Features.BodyFeature)workPart.Features.FindObject("solid(1)"));
    NXOpen.Face face1 = ((NXOpen.Face)bodyFeature1.FindObject("FACE 300027 {(90,145,147.9999999999816) solid(1)}"));
    objects1[0] = face1;
    displayModification1.Apply(objects1);
    
    face1.Color = 32767;
    
    displayModification1.SetNewGrid(0, 0);
    
    displayModification1.PoleDisplayState = false;
    
    displayModification1.KnotDisplayState = false;
    
    NXOpen.DisplayableObject[] objects2 = new NXOpen.DisplayableObject[1];
    objects2[0] = face1;
    displayModification1.Apply(objects2);
    
    face1.Color = 32767;
    
    theSession.DeleteUndoMark(markId4, null);
    
    theSession.SetUndoMarkName(markId2, "Edge Blend");
    
    plane1.DestroyPlane();
    
    workPart.FacePlaneSelectionBuilderData.Destroy(facePlaneSelectionBuilder1);
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression8);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression7);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    edgeBlendBuilder1.Destroy();
    
    workPart.MeasureManager.SetPartTransientModification();
    
    workPart.Expressions.Delete(expression5);
    
    workPart.MeasureManager.ClearPartTransientModification();
    
    workPart.MeasureManager.SetPartTransientModification();
    
    workPart.Expressions.Delete(expression6);
    
    workPart.MeasureManager.ClearPartTransientModification();
    
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
