using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NXOpen;
using NXOpen.UF;
using NXOpen.Annotations;
using NXOpen.Utilities;

namespace Backend_SC
{
    public class PMI
    {
        public static List<Pmi> Read()
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            PmiCollection list_pmi = workPart.PmiManager.Pmis;
            Pmi[] PMIListe = list_pmi.ToArray();

            if (PMIListe.Length == 0)
            {
                Console.WriteLine("Keine PMIs gefunden");
                return new List<Pmi>();
            }

            return new List<Pmi>(PMIListe);
        }  

    }

       
    }
