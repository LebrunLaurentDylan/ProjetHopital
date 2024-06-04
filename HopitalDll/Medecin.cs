using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopitalData;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;

namespace HopitalDll
{
    public class Medecin : IObserver
    {
        private string name;
        private Secretariat salleAttente;
        public List<Visites> listeVisites;

        public Medecin(string name, Secretariat waitingRoom)
        {
            this.name = name;
            this.salleAttente = waitingRoom;
            this.salleAttente.EntrerPatient(this);
        }

        public void Update()
        {
            var patient = salleAttente.GetNextPatient();
            if (patient != null)
            {
                Console.WriteLine($"{name} is seeing patient {patient.Nom}");
                SaveVisite(patient); // Dylan : Sauvegarder la visite quand un patient est vu
            }
        }

        public void Dispose()
        {
            salleAttente.SortirPatient(this);
        }

        public void SaveVisite(Patients patient) // Dylan : methode d'ajout de patients à la liste
        {
            Visites visite;
            visite = new Visites(patient.IdPatient, this.name, Convert.ToString(DateTime.Now),23,2);
            listeVisites.Add(visite);
        }

        public static void SaveVisitesXml(Visites[] liste)
        {
            FileStream outStream = new FileStream($@"{FilePath()}\listeVisites.xml", FileMode.OpenOrCreate, FileAccess.Write);
            SoapFormatter binWriter = new SoapFormatter();
            binWriter.Serialize(outStream, liste);
            outStream.Close();
        }

        public static Visites[] LoadVisitesXml()
        {
            FileStream inStream = new FileStream($@"{FilePath()}\listeVisites.xml", FileMode.Open, FileAccess.Read);
            SoapFormatter binReader = new SoapFormatter();
            // Désérialiser directement en tableau de Patients
            Visites[] VisitesArray = (Visites[])binReader.Deserialize(inStream);
            inStream.Close();
            return VisitesArray;
        }

        private static string FilePath()
        {
            var enviroment = System.Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.Parent.FullName;
            return projectDirectory;
        }

    }
}

