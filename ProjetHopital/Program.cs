using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopitalDll;
using HopitalData;

namespace ProjetHopital
{
    class Program
    {
        static void Main(string[] args)
        {
            //new HopitalPatientsSqlServer().Create(new Patients(013256,"Dupont","Jean", 35, 0608054070, "50 rue du Pont 44115 Basse-Goulaine"));
            //new HopitalPatientsSqlServer().Create(new Patients(083257, "Dupont", "Jeannot", 30, 0608066070, "51 rue du Pont 44115 Basse-Goulaine"));
            foreach (Patients pat in new HopitalPatientsSqlServer().FindAll())
            {
                Console.WriteLine(pat);
                ///dssdqfdssgfds
            }
        }
    }
}
