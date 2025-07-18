// NX Student Edition 2412
// Journal created by fanny on Thu Apr  3 14:47:17 2025 Mitteleuropäische Sommerzeit

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
    NXOpen.Matrix3x3 matrix1 = new NXOpen.Matrix3x3();
    matrix1.Xx = 1.0;
    matrix1.Xy = 0.0;
    matrix1.Xz = 0.0;
    matrix1.Yx = -0.0;
    matrix1.Yy = 0.0;
    matrix1.Yz = 1.0;
    matrix1.Zx = 0.0;
    matrix1.Zy = -1.0;
    matrix1.Zz = 0.0;
    workPart.ModelingViews.WorkView.Orient(matrix1);
    
    NXOpen.Session.UndoMarkId markId1;
    markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Hide");
    
    NXOpen.DisplayableObject[] objects1 = new NXOpen.DisplayableObject[1];
    NXOpen.Body body1 = ((NXOpen.Body)workPart.Bodies.FindObject("solid(1)"));
    objects1[0] = body1;
    theSession.DisplayManager.BlankObjects(objects1);
    
    workPart.ModelingViews.WorkView.FitAfterShowOrHide(NXOpen.View.ShowOrHideType.HideOnly);
    
    NXOpen.Session.UndoMarkId markId2;
    markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Show");
    
    NXOpen.DisplayableObject[] objects2 = new NXOpen.DisplayableObject[1];
    objects2[0] = body1;
    theSession.DisplayManager.ShowObjects(objects2, NXOpen.DisplayManager.LayerSetting.ChangeLayerToSelectable);
    
    workPart.ModelingViews.WorkView.FitAfterShowOrHide(NXOpen.View.ShowOrHideType.ShowOnly);
    
    // ----------------------------------------------
    //   Menu: Edit->Show and Hide->Hide...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId3;
    markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Hide");
    
    NXOpen.DisplayableObject[] objects3 = new NXOpen.DisplayableObject[1];
    NXOpen.Annotations.SurfaceFinish surfaceFinish1 = ((NXOpen.Annotations.SurfaceFinish)workPart.PmiManager.PmiAttributes.FindObject("HANDLE R-44453"));
    objects3[0] = surfaceFinish1;
    theSession.DisplayManager.BlankObjects(objects3);
    
    workPart.ModelingViews.WorkView.FitAfterShowOrHide(NXOpen.View.ShowOrHideType.HideOnly);
    
    // ----------------------------------------------
    //   Menu: Edit->Show and Hide->Hide...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId4;
    markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Hide");
    
    NXOpen.DisplayableObject[] objects4 = new NXOpen.DisplayableObject[1];
    NXOpen.Annotations.SurfaceFinish surfaceFinish2 = ((NXOpen.Annotations.SurfaceFinish)workPart.PmiManager.PmiAttributes.FindObject("HANDLE R-46728"));
    objects4[0] = surfaceFinish2;
    theSession.DisplayManager.BlankObjects(objects4);
    
    workPart.ModelingViews.WorkView.FitAfterShowOrHide(NXOpen.View.ShowOrHideType.HideOnly);
    
    // ----------------------------------------------
    //   Menu: Edit->Delete...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId5;
    markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Delete");
    
    theSession.UpdateManager.ClearErrorList();
    
    NXOpen.Session.UndoMarkId markId6;
    markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Delete");
    
    NXOpen.TaggedObject[] objects5 = new NXOpen.TaggedObject[1];
    NXOpen.Annotations.SurfaceFinish surfaceFinish3 = ((NXOpen.Annotations.SurfaceFinish)workPart.PmiManager.PmiAttributes.FindObject("HANDLE R-45888"));
    objects5[0] = surfaceFinish3;
    int nErrs1;
    nErrs1 = theSession.UpdateManager.AddObjectsToDeleteList(objects5);
    
    NXOpen.Session.UndoMarkId id1;
    id1 = theSession.NewestVisibleUndoMark;
    
    int nErrs2;
    nErrs2 = theSession.UpdateManager.DoUpdate(id1);
    
    theSession.DeleteUndoMark(markId5, null);
    
    // ----------------------------------------------
    //   Menu: Analysis->Measure...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId7;
    markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.MeasurePrefsBuilder measurePrefsBuilder1;
    measurePrefsBuilder1 = theSession.Preferences.CreateMeasurePrefsBuilder();
    
    measurePrefsBuilder1.InfoUnits = NXOpen.MeasurePrefsBuilder.JaMeasurePrefsInfoUnit.CustomUnit;
    
    measurePrefsBuilder1.ConsoleOutput = false;
    
    measurePrefsBuilder1.ShowValueOnlyToggle = false;
    
    measurePrefsBuilder1.ConsoleOutput = false;
    
    theSession.SetUndoMarkName(markId7, "Measure Dialog");
    
    workPart.MeasureManager.SetPartTransientModification();
    
    NXOpen.ScCollector scCollector1;
    scCollector1 = workPart.ScCollectors.CreateCollector();
    
    scCollector1.SetMultiComponent();
    
    workPart.MeasureManager.SetPartTransientModification();
    
    NXOpen.SelectionIntentRuleOptions selectionIntentRuleOptions1;
    selectionIntentRuleOptions1 = workPart.ScRuleFactory.CreateRuleOptions();
    
    selectionIntentRuleOptions1.SetSelectedFromInactive(false);
    
    NXOpen.Face[] faces1 = new NXOpen.Face[1];
    NXOpen.Features.BodyFeature bodyFeature1 = ((NXOpen.Features.BodyFeature)workPart.Features.FindObject("solid(1)"));
    NXOpen.Face face1 = ((NXOpen.Face)bodyFeature1.FindObject("FACE 300020 {(195.5584942187367,160,82.101289036588) solid(1)}"));
    faces1[0] = face1;
    NXOpen.FaceDumbRule faceDumbRule1;
    faceDumbRule1 = workPart.ScRuleFactory.CreateRuleFaceDumb(faces1, selectionIntentRuleOptions1);
    
    selectionIntentRuleOptions1.Dispose();
    NXOpen.SelectionIntentRule[] rules1 = new NXOpen.SelectionIntentRule[1];
    rules1[0] = faceDumbRule1;
    scCollector1.ReplaceRules(rules1, false);
    
    workPart.MeasureManager.SetPartTransientModification();
    
    NXOpen.ScCollector scCollector2;
    scCollector2 = workPart.ScCollectors.CreateCollector();
    
    scCollector2.SetMultiComponent();
    
    double faceaccuracy1;
    faceaccuracy1 = measurePrefsBuilder1.FaceAccuracy;
    
    NXOpen.Face face2 = ((NXOpen.Face)bodyFeature1.FindObject("FACE 300027 {(90,145,148) solid(1)}"));
    NXOpen.Line line1;
    line1 = workPart.Lines.CreateFaceAxis(face2, NXOpen.SmartObject.UpdateOption.AfterModeling);
    
    line1.SetVisibility(NXOpen.SmartObject.VisibilityOption.Visible);
    
    NXOpen.SelectionIntentRuleOptions selectionIntentRuleOptions2;
    selectionIntentRuleOptions2 = workPart.ScRuleFactory.CreateRuleOptions();
    
    selectionIntentRuleOptions2.SetSelectedFromInactive(false);
    
    NXOpen.Face[] faces2 = new NXOpen.Face[1];
    faces2[0] = face2;
    NXOpen.FaceDumbRule faceDumbRule2;
    faceDumbRule2 = workPart.ScRuleFactory.CreateRuleFaceDumb(faces2, selectionIntentRuleOptions2);
    
    selectionIntentRuleOptions2.Dispose();
    NXOpen.SelectionIntentRule[] rules2 = new NXOpen.SelectionIntentRule[1];
    rules2[0] = faceDumbRule2;
    scCollector2.ReplaceRules(rules2, false);
    
    workPart.MeasureManager.SetPartTransientModification();
    
    NXOpen.ScCollector scCollector3;
    scCollector3 = workPart.ScCollectors.CreateCollector();
    
    scCollector3.SetMultiComponent();
    
    NXOpen.TaggedObject[] objects6 = new NXOpen.TaggedObject[1];
    objects6[0] = line1;
    int nErrs3;
    nErrs3 = theSession.UpdateManager.AddObjectsToDeleteList(objects6);
    
    NXOpen.Session.UndoMarkId markId8;
    markId8 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Measure");
    
    NXOpen.Session.UndoMarkId markId9;
    markId9 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Measurement Apply");
    
    workPart.MeasureManager.ClearPartTransientModification();
    
    NXOpen.Session.UndoMarkId markId10;
    markId10 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Measurement Update");
    
    NXOpen.DisplayableObject[] objects1_1 = new NXOpen.DisplayableObject[1];
    objects1_1[0] = face1;
    NXOpen.DisplayableObject[] objects2_1 = new NXOpen.DisplayableObject[1];
    objects2_1[0] = face2;
    NXOpen.Point3d pt1_1;
    NXOpen.Point3d pt2_1;
    bool isapproximate1;
    double distance1;
    distance1 = theSession.Measurement.GetDistance(NXOpen.Measurement.AlternateDistance.Minimum, true, objects1_1, objects2_1, out pt1_1, out pt2_1, out isapproximate1);
    
    NXOpen.Point3d pt1_2;
    NXOpen.Point3d pt2_2;
    NXOpen.Point3d pt3_1;
    bool isapproximate2;
    double angle1;
    angle1 = theSession.Measurement.GetAngle(NXOpen.Measurement.AlternateAngle.Inner, true, face1, face2, out pt1_2, out pt2_2, out pt3_1, out isapproximate2);
    
    workPart.MeasureManager.SetPartTransientModification();
    
    theSession.DeleteUndoMark(markId9, "Measurement Apply");
    
    bool datadeleted1;
    datadeleted1 = theSession.DeleteTransientDynamicSectionCutData();
    
    theSession.DeleteUndoMark(markId8, null);
    
    theSession.SetUndoMarkName(markId7, "Measure");
    
    measurePrefsBuilder1.ConsoleOutput = false;
    
    scCollector1.Destroy();
    
    scCollector2.Destroy();
    
    scCollector3.Destroy();
    
    workPart.MeasureManager.ClearPartTransientModification();
    
    theSession.DeleteUndoMark(markId10, null);
    
    NXOpen.Session.UndoMarkId markId11;
    markId11 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.MeasurePrefsBuilder measurePrefsBuilder2;
    measurePrefsBuilder2 = theSession.Preferences.CreateMeasurePrefsBuilder();
    
    measurePrefsBuilder2.InfoUnits = NXOpen.MeasurePrefsBuilder.JaMeasurePrefsInfoUnit.CustomUnit;
    
    measurePrefsBuilder2.ShowValueOnlyToggle = false;
    
    measurePrefsBuilder2.ConsoleOutput = false;
    
    theSession.SetUndoMarkName(markId11, "Measure Dialog");
    
    workPart.MeasureManager.SetPartTransientModification();
    
    NXOpen.ScCollector scCollector4;
    scCollector4 = workPart.ScCollectors.CreateCollector();
    
    scCollector4.SetMultiComponent();
    
    workPart.MeasureManager.SetPartTransientModification();
    
    // ----------------------------------------------
    //   Dialog Begin Measure
    // ----------------------------------------------
    measurePrefsBuilder2.ConsoleOutput = false;
    
    scCollector4.Destroy();
    
    workPart.MeasureManager.ClearPartTransientModification();
    
    theSession.UndoToMark(markId11, null);
    
    theSession.DeleteUndoMark(markId11, null);
    
    // ----------------------------------------------
    //   Menu: Tools->Automation->Journal->Stop Recording
    // ----------------------------------------------
    
  }
  public static int GetUnloadOption(string dummy) { return (int)NXOpen.Session.LibraryUnloadOption.Immediately; }
}
