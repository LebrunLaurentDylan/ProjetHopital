using HopitalData;
using HopitalDll;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ProjetHopital
{
    public static class Operations
    {
        public static void OperationCaractere(Authentification auth)
        {
            char op = Convert.ToChar(Console.ReadLine().ToUpper());
            switch (op)
            {
                case 'A':
                    RendreLaSalle();
                    break;
                case 'B':
                    return;
                case 'C':
                    AjouterPatient(auth);
                    break;
                case 'D':
                    AfficherFileAttente(auth);
                    break;
                case 'E':
                    AfficherProchainPatient(auth);
                    break;
                case 'F':
                    AfficherProchainPatient(auth);
                    break;
                case 'G':
                    TraiterPatient(auth);
                    break;
                case 'Q':
                    Console.WriteLine("Deconnection.");
                    AfficherMenu(Login());
                    break;
                default:
                    Console.WriteLine("Option invalide. Veuillez réessayer.");
                    OperationCaractere(auth);
                    break;
            }
        }

        public static void AfficherMenu(Authentification auth)
        {
            Console.WriteLine("-------------Menu Principal --------\n");
            if (auth.Metier == "Dr")
            {
                Console.WriteLine("-------------Menu Médecin--------\n");
                Console.WriteLine(" A : Rendre la salle disponible :\n");
                Console.WriteLine(" B : Sauvegarder les visites en base :\n");
                Console.WriteLine(" F : Afficher la file d’attente :\n");
                Console.WriteLine(" G : Traiter un patient :\n");
                Console.WriteLine(" Q : Deconnection.");
                Console.WriteLine("Choisissez votre opérateur parmi les suivants : A, B, F, G, Z");
            }
            else
            {
                Console.WriteLine("-------------Menu Secrétaire--------\n");
                Console.WriteLine(" C : Ajouter un patient à la file d’attente :\n");
                Console.WriteLine(" D : Afficher la file d’attente :\n");
                Console.WriteLine(" E : Afficher le prochain patient de la file (sans le retirer) :\n");
                Console.WriteLine(" Q : Deconnection.");
                Console.WriteLine("Choisissez votre opérateur parmi les suivants : C, D, E, Z");
            }

            Console.WriteLine("Q : Quitter ce menu et revenir au menu principal :\n");

            OperationCaractere(auth);
        }

        public static void RendreLaSalle()
        {
            // Implémentation à ajouter
            Console.WriteLine("La salle a été rendue disponible.");
        }

        public static void AfficherProchainPatient(Authentification auth)
        {
            var patient = Secretariat.Instance().PeekNextPatient();
            if (patient != null)
            {
                Console.WriteLine($"Prochain patient : {patient.Nom}");
                AfficherMenu(auth);
            }
            else
            {
                Console.WriteLine("Aucun patient dans la file d'attente.");
                AfficherMenu(auth);
            }
        }

        public static void AfficherFileAttente(Authentification auth)
        {
            var patients = Secretariat.Instance().GetAllPatients();
            if (patients.Length > 0)
            {
                Console.WriteLine("Liste des patients dans la file d'attente :");
                foreach (var patient in patients)
                {
                    Console.WriteLine(patient.Nom);
                }
                AfficherMenu(auth);
            }
            else
            {
                Console.WriteLine("Aucun patient dans la file d'attente.");
                AfficherMenu(auth);
            }
        }

        public static Patients RecupererPatient(int id_Patient)
        {
            Console.WriteLine("-------------Récupérer un patient de la base--------\n");

            Patients p = new HopitalPatientsSqlServer().FindById(id_Patient);
            if (p.Nom != null && p.Nom != "")
            {
                Secretariat.Instance().AddPatient(p);
                return p;
            }
            else
                return null;
        }

        public static void AjouterPatient(Authentification auth)
        {
            Console.Write("ID patient : ");
            int id_Patient;
            if (int.TryParse(Console.ReadLine(), out id_Patient))
            {
                Patients p = RecupererPatient(id_Patient);
                if (p == null)
                {
                    Console.WriteLine("-------------Inscrire un patient en base --------\n");
                    Console.WriteLine("-------------Saisissez : \n");
                    Console.Write("Nom : ");
                    string nom = Console.ReadLine();
                    Console.Write("Prénom : ");
                    string prenom = Console.ReadLine();
                    Console.Write("Âge : ");
                    int age = int.Parse(Console.ReadLine());
                    Console.Write("Téléphone : ");
                    int telephone = int.Parse(Console.ReadLine());
                    Console.Write("Adresse : ");
                    string adresse = Console.ReadLine();

                    p = new Patients(id_Patient, nom, prenom, age, telephone, adresse);
                    new HopitalPatientsSqlServer().Create(p);
                    Secretariat.Instance().AddPatient(p);
                    Console.WriteLine($"Patient ajouté : {p}");
                }
                else
                {
                    Console.WriteLine($"Patient : {p.IdPatient}");
                }
                AfficherMenu(auth);
            }
            else
            {
                Console.WriteLine("ID patient invalide.");
                AfficherMenu(auth);
            }
        }

        public static void TraiterPatient(Authentification auth)
        {
            Medecin med = new Medecin(auth.Login, auth.Password, auth.Nom, auth.Metier, auth.Salle);
            med.Update();
        }

        public static Authentification Login()
        {
            Console.WriteLine("-------------Authentification--------\n");
            Console.Write("Login : ");
            string login = Console.ReadLine();
            Console.Write("Password : ");
            string password = Console.ReadLine();
            Authentification auth = new HopitalAuthSqlServer().Login(login, password);
            if (auth != null)
            {
                Thread.Sleep(300);
                AfficherMenu(auth);
                return auth;
            }
            else
            {
                Console.WriteLine("Authentification échouée.");
                return null;
            }
        }
    }
}
