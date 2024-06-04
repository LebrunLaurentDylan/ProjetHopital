using HopitalDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;

namespace HopitalData
{
    public class Authentification: IObserver
    {
        // attributs
        private string login;
        private string password;
        private string nom;
        private string metier;
        private Secretariat salleAttente;
        public List<Visites> listeVisites;

        // properties
        public string Metier { get => metier; }
        public string Nom { get => nom; }
        public string Password { get => password; }
        public string Login { get => login; }
        // constructeur
        public Authentification() { }
        public Authentification(string login, string password, string nom, string metier)
        {
            this.login = login;
            this.password = password;
            this.nom = nom;
            this.metier = metier;
        }
        // methodes
        public override string ToString()
        {
            return $"You're Logged as : {metier}-{nom}\n";
        }

        public void Update()
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
            salleAttente.SortirPatient(this);
        }

        public void SaveVisite(Patients patient) // Dylan : methode d'ajout de patients à la liste
        {
            Visites visite;
            visite = new Visites(patient.IdPatient, this.nom, Convert.ToString(DateTime.Now), 23, 2);
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
