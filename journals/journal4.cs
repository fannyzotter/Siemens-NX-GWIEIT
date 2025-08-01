// NX Student Edition 2412
// Journal created by fanny on Mon Apr  7 07:38:43 2025 Mitteleuropäische Sommerzeit

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
    
    NXOpen.Features.Feature feature1 = ((NXOpen.Features.Feature)workPart.Features.FindObject("SYMBOLIC_THREAD(8)"));
    NXOpen.Features.EditWithRollbackManager editWithRollbackManager1;
    editWithRollbackManager1 = workPart.Features.StartEditWithRollbackManager(feature1, markId1);
    
    // ----------------------------------------------
    //   Dialog Begin Edit Parameters 
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId2;
    markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start");
    
    theSession.SetUndoMarkName(markId2, "Thread Dialog");
    
    // ----------------------------------------------
    //   Dialog Begin Edit Thread
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId3;
    markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start");
    
    NXOpen.MeasurePrefsBuilder measurePrefsBuilder1;
    measurePrefsBuilder1 = theSession.Preferences.CreateMeasurePrefsBuilder();
    
    measurePrefsBuilder1.InfoUnits = NXOpen.MeasurePrefsBuilder.JaMeasurePrefsInfoUnit.CustomUnit;
    
    measurePrefsBuilder1.ConsoleOutput = false;
    
    measurePrefsBuilder1.UnitsSystem = 0;
    
    measurePrefsBuilder1.AngleUnits = 0;
    
    measurePrefsBuilder1.DisableUnits = true;
    
    measurePrefsBuilder1.ShowValueOnlyToggle = false;
    
    measurePrefsBuilder1.ConsoleOutput = false;
    
    theSession.SetUndoMarkName(markId3, "Measure Dialog");
    
    workPart.MeasureManager.SetPartTransientModification();
    
    NXOpen.ScCollector scCollector1;
    scCollector1 = workPart.ScCollectors.CreateCollector();
    
    scCollector1.SetMultiComponent();
    
    workPart.MeasureManager.ClearPartTransientModification();
    
    // ----------------------------------------------
    //   Dialog Begin Measure
    // ----------------------------------------------
    measurePrefsBuilder1.UnitsSystem = 0;
    
    measurePrefsBuilder1.AngleUnits = 0;
    
    measurePrefsBuilder1.DisableUnits = false;
    
    measurePrefsBuilder1.ConsoleOutput = false;
    
    scCollector1.Destroy();
    
    workPart.MeasureManager.ClearPartTransientModification();
    
    theSession.UndoToMark(markId3, null);
    
    theSession.DeleteUndoMark(markId3, null);
    
    theSession.DeleteUndoMark(markId3, null);
    
    theSession.DeleteUndoMark(markId2, null);
    
    theSession.DeleteUndoMark(markId2, null);
    
    // ----------------------------------------------
    //   Dialog Begin Edit Parameters 
    // ----------------------------------------------
    editWithRollbackManager1.UpdateFeature(false);
    
    editWithRollbackManager1.Stop();
    
    theSession.UndoToMarkWithStatus(markId1, null);
    
    theSession.DeleteUndoMarksUpToMark(markId1, null, false);
    
    // ----------------------------------------------
    //   Menu: Tools->Automation->Journal->Stop Recording
    // ----------------------------------------------
    
  }
  public static int GetUnloadOption(string dummy) { return (int)NXOpen.Session.LibraryUnloadOption.Immediately; }
}
