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
    public class Medecin : Authentification
    {
        // attributs 
        public const int COUTVISITE = 23;
        // constructeur medecin (peut-être pas utile)
        public Medecin(){}
        public Medecin(string login, string password, string nom, string metier, int salle)
        {
            this.login = login;
            this.password = password;
            this.nom = nom;
            this.metier = metier;
            this.salle = salle;
        }

        // methodes du medecin
        public override void Update()
        {
            var patient = salleAttente.GetNextPatient();
            if (patient != null)
            {
                Console.WriteLine($"{nom} is seeing patient {patient.Nom}");
                SaveVisite(patient); // Dylan : Sauvegarder la visite quand un patient est vu
            }
        }

        public void Dispose()
        {
            salleAttente.GetNextPatient();
        }

        public void EnvoiVisitesBDD()
        {
            listeVisites = LoadVisitesXml();
            foreach(Visites vis in listeVisites)
            {
                new HopitalVisitesSqlServer().Create(vis);
            }
            File.Delete($@"{FilePath()}\listeVisites.xml");
        }

        public void SaveVisite(Patients patient) // Dylan : methode d'ajout de patients à la liste
        {
            Visites visite;
            visite = new Visites(patient.IdPatient, this.nom, Convert.ToString(DateTime.Now), COUTVISITE, this.salle);
            listeVisites.Add(visite);
        }

        public static void SaveVisitesXml(Visites[] liste)
        {
            FileStream outStream = new FileStream($@"{FilePath()}\listeVisites.xml", FileMode.OpenOrCreate, FileAccess.Write);
            SoapFormatter binWriter = new SoapFormatter();
            binWriter.Serialize(outStream, liste);
            outStream.Close();
        }

        public static List<Visites> LoadVisitesXml()
        {
            FileStream inStream = new FileStream($@"{FilePath()}\listeVisites.xml", FileMode.Open, FileAccess.Read);
            SoapFormatter binReader = new SoapFormatter();
            // Désérialiser directement en tableau de Patients
            List<Visites> VisitesArray = (List<Visites>)binReader.Deserialize(inStream);
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

