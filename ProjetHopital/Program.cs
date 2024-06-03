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
            //new HopitalPatientsSqlServer().Create(new Patients(013256,"FRANCE","Marie", 36, 0608054070, "50 rue du Pont 44115 Basse-Goulaine"));
            //new HopitalPatientsSqlServer().Create(new Patients(083257, "DUFOUR", "Jeanne", 31, 0608066070, "51 rue du Pont 44115 Basse-Goulaine"));
            //foreach (Patients pat in new HopitalPatientsSqlServer().FindAll())
            //{
            //    Console.WriteLine(pat);
            //}

            Secretariat salleAttente = Secretariat.Instance;

            Medecin med1 = new Medecin("Dr. Hugotte", salleAttente);
            Medecin med2 = new Medecin("Dr. Martin", salleAttente);

            salleAttente.AddPatient(new Patients(222211, "MARTIN", "David", 38, 0458962300, "Paris"));
            salleAttente.AddPatient(new Patients(222212, "BERTRAND", "George", 23, 0458962300, "Paris"));
            salleAttente.AddPatient(new Patients(222213, "LEPETIT", "Stephanie", 27, 0458962300, "Paris"));

            // Simulate doctors becoming available and seeing patients
            med1.Update();
            med2.Update();
            med1.Update();
            med2.Update();

            med1.Dispose();
            med2.Dispose();
        }
    }
}
