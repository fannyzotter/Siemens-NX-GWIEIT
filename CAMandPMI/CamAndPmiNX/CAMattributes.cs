/*using System;
using NXOpen.UF;
using NXOpen;
using System.IO;


public static class CAMattributes
{
    public static void OperationNodeParams(NXOpen.CAM.Operation myop, NXOpen.Session theSession, Part workpart, NX_CAM_Standard_API.Operation_Objects SingleOperation)
    {
        try
        {
            UFSession theUFSession = UFSession.GetUFSession();
            NXOpen.CAM.CAMObject[] ncGroups = new NXOpen.CAM.CAMObject[1];
            ncGroups[0] = workpart.CAMSetup.CAMOperationCollection.FindObject(myop.Name);





            //Exception in NX2206
            //ncGroups[0].GetAttributeTitlesByType(NXObject.AttributeType.Any);



            NXOpen.Tag optag = ncGroups[0].Tag;
            NXOpen.UF.UFParam.IndexAttribute ParamAttrib;
            theUFSession.Param.AskRequiredParams(optag, out int Paramcount, out int[] Paramindices);
            Array.Sort(Paramindices);





            StreamWriter sw = new StreamWriter(@"D:\Temp\40_HPP\OutputData_posted_TJ\Param_data.txt", false);
            sw.WriteLine("Operation: " + myop.Name);
            foreach (int par in Paramindices)
            {
                double dbl_value;
                string str_value;
                int int_value;
                Tag tag_value;
                //NXOpen.Tag geom_mcs_tag;
                //IntPtr intptr_value;
                theUFSession.Param.AskParamAttributes(par, out ParamAttrib);
                string name = ParamAttrib.name;





                //Use the below log to get all params and its type to log output
                //Log.Logger.Info("new Param: >" + ParamAttrib.name + "< of " + ParamAttrib.type);





                //mywindow.WriteLine(name);
                sw.WriteLine(name);
                try
                {
                    switch (ParamAttrib.name)
                    {
                        //case "Feed Per Tooth":
                        //    theUFSession.Param.AskDoubleValue(optag, par, out dbl_value);
                        //    this.FeedPerTooth = dbl_value;
                        //    break;
                        //case "Cse Material Cutting Time":
                        //    theUFSession.Param.AskDoubleValue(optag, par, out dbl_value);
                        //    this.CseMaterialCuttingTime = dbl_value;
                        //    break;
                        //case "Cse Air Cutting Time":
                        //    theUFSession.Param.AskDoubleValue(optag, par, out dbl_value);
                        //    this.CseAirCuttingTime = dbl_value;
                        //    break;
                        //case "Cse Positioning Mixed Time":
                        //    theUFSession.Param.AskDoubleValue(optag, par, out dbl_value);
                        //    this.CsePositioningMixedTime = dbl_value;
                        //    break;
                        //case "Cse Dragging Time":
                        //    theUFSession.Param.AskDoubleValue(optag, par, out dbl_value);
                        //    this.CseDraggingTime = dbl_value;
                        //    break;
                        //case "Cse Delay Time":
                        //    theUFSession.Param.AskDoubleValue(optag, par, out dbl_value);
                        //    this.CseDelayTime = dbl_value;
                        //    break;
                        //case "Cse Wait Time":
                        //    theUFSession.Param.AskDoubleValue(optag, par, out dbl_value);
                        //    this.CseWaitTime = dbl_value;
                        //    break;
                        //case "Cse Tool Change Time":
                        //    theUFSession.Param.AskDoubleValue(optag, par, out dbl_value);
                        //    this.CseToolChangeTime = dbl_value;
                        //    break;
                        //case "Cse Positioning Linear Time":
                        //    theUFSession.Param.AskDoubleValue(optag, par, out dbl_value);
                        //    this.CsePositioningLinearTime = dbl_value;
                        //    break;
                        //case "Cse Positioning Rotary Time":
                        //    theUFSession.Param.AskDoubleValue(optag, par, out dbl_value);
                        //    this.CsePositioningRotaryTime = dbl_value;
                        //    break;
                        //case "Tool Assembly Part File":
                        //    theUFSession.Param.AskStrValue(optag, par, out str_value);
                        //    this.ToolAssemblyPartFile = str_value;
                        //    break;
                        //case "Tool Diameter":
                        //    theUFSession.Param.AskDoubleValue(optag, par, out dbl_value);
                        //    this.vcDiameterReference = dbl_value;
                        //    break;
                        //case "Part Material":
                        //    //Todo - 1 understand Part Tag and get material as string
                        //    theUFSession.Param.AskTagValue(optag, par, out tag_value);
                        //    this.PartMaterial = tag_value.ToString();
                        //    break;
                        case "Tool Number":
                            theUFSession.Param.AskIntValue(optag, par, out int_value);
                            SingleOperation.m_ToolNumber = int_value;
                            break;
                        //case "Cut Area Geometry":
                        //    theUFSession.Param.AskStrValue(optag, par, out int_value);
                        //    this.SpindleOutputModeInt = int_value;
                        //    break;
                        case "MCS":
                            theUFSession.Param.AskStrValue(optag, par, out str_value);
                            SingleOperation.m_OffsetName = str_value;
                            break;
                        case "Spindle RPM":
                            theUFSession.Param.AskDoubleValue(optag, par, out dbl_value);
                            SingleOperation.m_SpindleRPM = dbl_value;
                            break;
                        case "Surface Speed":
                            theUFSession.Param.AskDoubleValue(optag, par, out dbl_value);
                            double SMMSpeed = dbl_value;
                            break;
                        case "Spindle maximum RPM":
                            theUFSession.Param.AskDoubleValue(optag, par, out dbl_value);
                            double MaxSpindleRPM = dbl_value;
                            break;
                        case "Spindle RPM Toggle":
                            theUFSession.Param.AskIntValue(optag, par, out int_value);
                            int ToogleRPM = int_value;
                            break;
                        case "Spindle maximum RPM Toggle":
                            theUFSession.Param.AskIntValue(optag, par, out int_value);
                            int MaxRPMToogle = int_value;
                            break;
                        case "Spindle Direction Automatic":
                            theUFSession.Param.AskIntValue(optag, par, out int_value);
                            int SpindleDirectionAutomatic = int_value;
                            break;
                        case "Spindle Direction Control":
                            theUFSession.Param.AskIntValue(optag, par, out int_value);
                            int SpindleDirection = int_value;
                            break;



                    }
                    //this.FeedrateUnit = millOperationBuilder.FeedsBuilder.FeedCutBuilder.Unit.ToString();



                }
                catch (Exception)
                {





                }



            }
            sw.Close();



        }
        catch
        {
            //



        }



        ////Map Positioning Time
        //if (this.CsePositioningLinearTime != null)
        //{
        //    this.CsePositioningTime = (
        //        this.CsePositioningLinearTime
        //        + this.CsePositioningMixedTime
        //        + this.CsePositioningRotaryTime);
        //    this.CseSecondaryTime = (
        //        this.CsePositioningTime
        //        + this.CseDraggingTime
        //        + this.CseDelayTime
        //        + this.CseToolChangeTime);
        //    this.CseCuttingTime = (
        //        this.CseMaterialCuttingTime
        //        + this.CseAirCuttingTime);
        //    this.CseTotalTime = (
        //        this.CseCuttingTime
        //        + this.CseSecondaryTime);





    }
}
*/