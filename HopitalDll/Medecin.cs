using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopitalData;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;
using System.Xml.Serialization;

namespace HopitalDll
{
    public class Medecin : Authentification
    {
        // attributs 
        private const int cOUTVISITE = 23;

        public static int COUTVISITE => cOUTVISITE;
        public List<Visites> listeVisites = new List<Visites>();

        public List<Visites> ListeVisites
        { get => listeVisites; set => listeVisites = value; }

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
            Patients patient = Secretariat.Instance().GetNextPatient();
            if (patient != null)
            {
                Console.WriteLine($"{Metier}.{Nom} is seeing patient {patient.Nom}");
                SaveVisite(patient); // Dylan : Sauvegarder la visite quand un patient est vu
                SaveVisitesXml();
            }
        }

        public void Dispose()
        {
            Secretariat.Instance().GetNextPatient();
        }

        public void EnvoiVisitesBDD()
        {
            ListeVisites = LoadVisitesXml();
            foreach(Visites vis in ListeVisites)
            {
                new HopitalVisitesSqlServer().Create(vis);
            }
            File.Delete($@"{FilePath()}\listeVisites.xml");
        }

        public void SaveVisite(Patients patient) // Dylan : methode d'ajout de patients à la liste
        {
            DateTime date = DateTime.Now;
            Visites visite= new Visites(patient.IdPatient, Nom, Convert.ToString(date), COUTVISITE, Salle);
            Console.WriteLine(visite.ToString());
            ListeVisites.Add(visite);
        }

        public void SaveVisitesXml()
        {
            string filePath = $@"{FilePath()}\listeVisites.xml";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Visites>));

            using (FileStream outStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                xmlSerializer.Serialize(outStream, listeVisites);
            }
        }

        public List<Visites> LoadVisitesXml()
        {
            string filePath = $@"{FilePath()}\listeVisites.xml";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Visites>));

            using (FileStream inStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return (List<Visites>)xmlSerializer.Deserialize(inStream);
            }
        }

        private static string FilePath()
        {
            var enviroment = System.Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.Parent.FullName;
            return projectDirectory;
        }
    }

    // medecin2 Mamadou
    public class Medecin2 
    {
        private int id;
        private string nom;
        private Queue<Patients> assignedPatient = new Queue<Patients>();
        public int Id { get; private set; }
        public string Nom { get; private set; }
        private Patients actuelPatient;


        public Medecin2(int id, string nom)
        {
            this.id = id;
            this.nom = nom;
        }


        public void AssignerPatient(Patients patient)
        {
            assignedPatient.Enqueue(patient);
            if (actuelPatient == null)
            {
                GetNextPatient();
            }
        }

        public void LibererActuelPatient()
        {
            actuelPatient = null;
            GetNextPatient();
        }

        public Patients GetNextPatient()
        {
            if (assignedPatient.Count > 0)
            {
                actuelPatient = assignedPatient.Dequeue();
                // Notifier soit le medecin soit le system du statut de la file
                Console.WriteLine($"Docteur {Nom} reçoit le patient {actuelPatient}");
                return actuelPatient;
            }
            return actuelPatient;
        }
        public void Update()
        {
            // Verifier si un patient est en consultation, sinon appeler le prochain
            if (actuelPatient == null && assignedPatient.Count > 0)
            {
                GetNextPatient();
            }
        }
    }
}

