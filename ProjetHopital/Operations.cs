using HopitalData;
using HopitalDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetHopital
{
    public static class Operations
    {

        public static void OperationCaractere(string metier)
        {
            char op = Convert.ToChar(Console.ReadLine());
            switch (op)
            {
                case 'A':
                    RendreLaSalle(); break;
                case 'B':
                    return;
                case 'C':
                    AjouterPatient(); break;
                case 'D':
                    AfficherFileAttente(); break;
                case 'E':
                    AfficherProchainPatient(); break;
                case 'F':
                    AfficherProchainPatient(); break;
                case 'Q':
                    AficherMenu(metier); break;


            }

        }
        public static void AficherMenu(string metier)
        {
            Console.WriteLine("-------------Menu Principale --------\n");
            if (metier == "Medecin")
            {
                Console.WriteLine("-------------Menu Médecin--------\n");
                Console.WriteLine(" A : le médecin décide de rendre la salle dispo :\n");
                Console.WriteLine(" B :le médecin  décide de sauvegarder en base les visites:\n");
                Console.WriteLine(" F : Afficher la file d’attente:\n");

                Console.WriteLine("Choisissez votre opérateur parmis les suivants: A , B ,F ");


            }
            else
            {
                Console.WriteLine("-------------Menu secrétaire--------\n");
                Console.WriteLine(" C : Rajouter a la file d’attente un patient :\n");
                Console.WriteLine(" D : Afficher la file d’attente:\n");
                Console.WriteLine(" E : Afficher le prochain patient de la file (sans le retirer):\n");
                Console.WriteLine("Choisissez votre opérateur parmis les suivants: A , B ");

            }


            Console.WriteLine("Q : Sortir de ce menu et revenir au menu principal:\n");

            OperationCaractere(metier);






        }
        public static void RendreLaSalle()
        {

        }
        public static void AfficherProchainPatient()
        {
            Secretariat.Instance.GetNextPatient();
        }
        public static void AfficherFileAttente()
        {
            Secretariat.Instance.NotifyPatient();
        }

        public static Patients RecupererPatient()
        {
            Console.WriteLine("-------------récupérer  un patient de la base--------\n");
            Console.WriteLine("-------------Saisissez : \n");
            Console.Write("id patient: ");
            int id_Patient = Int32.Parse((Console.ReadLine()));
            Patients p = new HopitalPatientsSqlServer().FindById(id_Patient);
            Secretariat.Instance.AddPatient(p);
            return p;
        }

        public static void AjouterPatient()

        {
            if (RecupererPatient() == null)
            {
                Console.WriteLine("-------------Inscrire un patient en base --------\n");
                Console.WriteLine("-------------Saisissez : \n");
                Console.Write("id patient: ");
                int id_Patient = Int32.Parse((Console.ReadLine()));
                Console.Write("Nom: ");
                string nom = (Console.ReadLine());
                Console.Write("Prenom: ");
                string prenom = (Console.ReadLine());
                Console.Write("Age: ");
                int age = Int32.Parse((Console.ReadLine()));
                Console.Write("Téléphone: ");
                int telephone = Int32.Parse((Console.ReadLine()));
                Console.Write("Adresse: ");
                string adresse = (Console.ReadLine());
                Patients p = new Patients(id_Patient, nom, prenom, age, telephone, adresse);
                new HopitalPatientsSqlServer().Create(p);
                Secretariat.Instance.AddPatient(p);
            }
            else
            {
                RecupererPatient();
            }

        }

        public static Authentification Login()
        {

            Console.WriteLine("-------------Authentification--------\n");
            Console.Write("Login: ");
            string login = (Console.ReadLine());
            Console.Write("Password: ");
            string passWord = (Console.ReadLine());
            Authentification auth = new HopitalAuthSqlServer().Login(login, passWord);
            if (auth != null)
            {
                Thread.Sleep(300);
                AficherMenu(auth.Metier);
                return auth;

            }
            else
            {
                return null;
            }

        }
    }
}