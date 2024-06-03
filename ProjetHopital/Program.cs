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
            //new HopitalPatientsSqlServer().Create(new Patients(043256,"Dupont","Jean", 35, 0608054070, "50 rue du Pont 44115 Basse-Goulaine"));
            //new HopitalPatientsSqlServer().Create(new Patients(093257, "Dupont", "Jeannot", 30, 0608066070, "51 rue du Pont 44115 Basse-Goulaine"));
            //new HopitalPatientsSqlServer().Delete(43256);
            //Console.WriteLine("-----------------------------------------------------------------");

            //foreach (Patients pat in new HopitalPatientsSqlServer().FindAll())
            //{
            //    Console.WriteLine(pat.ToString());
            //}
            Authentification prop = new HopitalAuthSqlServer().Login("a1", "a1");
            Console.WriteLine(prop);
        }
    }
}
