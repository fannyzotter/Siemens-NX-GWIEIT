using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NXOpen;
using NXOpen.UF;
using NXOpenUI;
using System.Diagnostics;
using static NXOpen.UF.UFPath;

namespace Backend_SC
{
    public class PMI
    {
        public static StringBuilder Read()
        {
            NXOpen.Session theSession = NXOpen.Session.GetSession();
            NXOpen.Part workPart = theSession.Parts.Work;
            NXOpen.Part displayPart = theSession.Parts.Display;

            // Debugger.Launch();
            NXOpen.Annotations.PmiCollection list_pmi = workPart.PmiManager.Pmis;
            
            
            NXOpen.Annotations.Pmi[] PMIListe = list_pmi.ToArray();
            int AnzahlPMI = list_pmi.ToArray().Length;
            StringBuilder pmiStringBuilder = new StringBuilder();
            for (int i = 0; i < AnzahlPMI; i = i + 1)
            {
                String SpecificPMI = PMIListe[i].ToString();
                if (!string.IsNullOrEmpty(SpecificPMI))
                    pmiStringBuilder.AppendLine(SpecificPMI);    

            }// 1. durch liste gehen
            // 2. zu einem PMI attribute holen
            // 3. Typ, Namen und Wert ermitteln
            return pmiStringBuilder;
        }
             
    }
}
