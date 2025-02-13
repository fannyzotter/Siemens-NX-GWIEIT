using System;
using NXOpen;
using NXOpen.UF;
using NXOpenUI;
using System.Windows.Interop;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;
using UI_Summerschool_Forms;
using System.Windows.Forms;

public class Program
{
    // class members
    private static Session theSession;
    private static UFSession theUfSession;
    public static Program theProgram;
    public static bool isDisposeCalled;

    //------------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------------
    public Program()

    {
        try
        {
            theSession = Session.GetSession();
            theUfSession = UFSession.GetUFSession();
            isDisposeCalled = false;
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----
            // UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
        }
    }

    //------------------------------------------------------------------------------
    //  Explicit Activation
    //      This entry point is used to activate the application explicitly
    //------------------------------------------------------------------------------
    public static int Main(string[] args)
    {
        int retValue = 0;
        try
        {
            theProgram = new Program();
            //TODO: Add your application code here
            //Debugger.Launch();
            try
            {

                //Initialize Frontend
                UI_SummerSchool.MainWindow mymainwindow = new UI_SummerSchool.MainWindow();
                UI_Summerschool_Forms.Form1 myForm = new UI_Summerschool_Forms.Form1();
                
                WindowInteropHelper helper = new WindowInteropHelper(mymainwindow);
                // WindowInteropHelper helper = new WindowInteropHelper(myForm);
                helper.Owner = NXOpenUI.FormUtilities.GetDefaultParentWindowHandle();
                //Show Main Window
               // mymainwindow.Show();
                myForm.Show();

                theProgram.Dispose();
            }
            catch (Exception ex)
            {
                UI.GetUI().NXMessageBox.Show("Error", NXMessageBox.DialogType.Error, ex.ToString());
                theProgram.Dispose();
            }
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----

        }
        return retValue;
    }

    //------------------------------------------------------------------------------
    // Following method disposes all the class members
    //------------------------------------------------------------------------------
    public void Dispose()
    {
        try
        {
            if (isDisposeCalled == false)
            {
                //TODO: Add your application code here 
            }
            isDisposeCalled = true;
        }
        catch (NXOpen.NXException ex)
        {
            // ---- Enter your exception handling code here -----

        }
    }

    public static int GetUnloadOption(string arg)
    {
        //Unloads the image explicitly, via an unload dialog
        return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);

        //Unloads the image immediately after execution within NX
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);

        //Unloads the image when the NX session terminates
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
    }
}
