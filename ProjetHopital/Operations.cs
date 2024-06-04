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
                // case medecin
                case 'A':
                    if (auth.Metier == "Dr")
                        RendreLaSalle();
                    else
                    {
                        Console.WriteLine("Option Indisponible");
                        Exit(auth);
                    } 
                    break;
                case 'B':
                    if (auth.Metier == "Dr")
                        SauverDansBDD(auth);
                    else
                    {
                        Console.WriteLine("Option Indisponible");
                        Exit(auth);
                    }
                    break;
                case 'F':
                    if (auth.Metier == "Dr")
                        AfficherFileAttente(auth);
                    else
                    {
                        Console.WriteLine("Option Indisponible");
                        Exit(auth);
                    }
                    break;
                case 'G':
                    if (auth.Metier == "Dr")
                        TraiterPatient(auth);
                    else
                    {
                        Console.WriteLine("Option Indisponible");
                        Exit(auth);
                    }
                    break;
                case 'H':
                    if (auth.Metier == "Dr")
                        AfficherFichierVisites(auth);
                    else
                    {
                        Console.WriteLine("Option Indisponible");
                        Exit(auth);
                    }
                    break;
                // case secretaire
                case 'C':
                    if (auth.Metier == "secretaire")
                        AjouterPatient(auth);
                    else
                    {
                        Console.WriteLine("Option Indisponible");
                        Exit(auth);
                    }
                    break;
                case 'D':
                    if (auth.Metier == "secretaire")
                        AfficherFileAttente(auth);
                    else
                    {
                        Console.WriteLine("Option Indisponible");
                        Exit(auth);
                    }
                    break;
                case 'E':
                    if (auth.Metier == "secretaire")
                        AfficherProchainPatient(auth);
                    else
                    {
                        Console.WriteLine("Option Indisponible");
                        Exit(auth);
                    }
                    break;
                case 'I':
                    if (auth.Metier == "secretaire")
                        AfficherListeVisiteIdPatient(auth);
                    else
                    {
                        Console.WriteLine("Option Indisponible");
                        Exit(auth);
                    }
                    break;
                case 'Q':
                    Console.WriteLine("Deconnection.");
                    AfficherMenu(Login());
                    break;
                default:
                    Console.WriteLine("Option Indisponible");
                    Exit(auth);
                    break;
            }
        }

        public static void AfficherMenu(Authentification auth)
        {
            if (auth.Metier == "Dr")
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("-------------Menu Médecin--------\n");
                Console.WriteLine(" A : Rendre la salle disponible");
                Console.WriteLine(" B : Sauvegarder les visites en base");
                Console.WriteLine(" F : Afficher la file d’attente");
                Console.WriteLine(" G : Traiter un patient");
                Console.WriteLine(" H : Afficher la liste des Visites");
                Console.WriteLine(" Q : Deconnection.");
                Console.WriteLine("Choisissez votre opérateur parmi les suivants : A, B, F, G, H Q");
            }
            else if (auth.Metier == "secretaire")
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("-------------Menu Secrétaire--------\n");
                Console.WriteLine(" C : Ajouter un patient à la file d’attente");
                Console.WriteLine(" D : Afficher la file d’attente");
                Console.WriteLine(" E : Afficher le prochain patient de la file (sans le retirer)");
                Console.WriteLine(" I : Afficher liste visite par ID Patient");
                Console.WriteLine(" Q : Deconnection.");
                Console.WriteLine("Choisissez votre opérateur parmi les suivants : C, D, E, Q");
            }

            OperationCaractere(auth);
        }

        public static void RendreLaSalle()
        {
            // Implémentation à ajouter
            Console.WriteLine("La salle a été rendue disponible.");
        }
        public static void AfficherListeVisiteIdPatient(Authentification auth)
        {
            Console.Write("ID patient : ");
            int id_Patient;
            int.TryParse(Console.ReadLine(), out id_Patient);
            Console.WriteLine("------------- Liste des visites du patient --------\n");
            List<Visites> liste = new HopitalVisitesSqlServer().FindByPatient(id_Patient);
            foreach(Visites visite in liste)
            {
                Console.WriteLine(visite.ToString());
            }
            Console.Write("rentrez une touche pour sortir : ");
            Console.ReadKey();
            Exit(auth);
        }

        public static void SauverDansBDD(Authentification auth)
        {
            Medecin med = new Medecin(auth.Login, auth.Password, auth.Nom, auth.Metier, auth.Salle);
            med.EnvoiVisitesBDD();
            Exit(auth);
        }

        public static void AfficherProchainPatient(Authentification auth)
        {
            var patient = Secretariat.Instance().PeekNextPatient();
            if (patient != null)
            {
                Console.WriteLine("------------- Liste d'attente --------\n");
                Console.WriteLine($"Prochain patient : {patient.Nom}");
                Exit(auth);
            }
            else
            {
                Console.WriteLine("------------- Liste d'attente --------\n");
                Console.WriteLine("Aucun patient dans la file d'attente.");
                Exit(auth);
            }
        }

        public static void AfficherFileAttente(Authentification auth)
        {
            var patients = Secretariat.Instance().GetAllPatients();
            if (patients.Length > 0)
            {
                Console.WriteLine("------------- Liste d'attente --------\n");
                foreach (var patient in patients)
                {
                    Console.WriteLine(patient.Nom);
                }
                Exit(auth);
            }
            else
            {
                Console.WriteLine("------------- Liste d'attente --------\n");
                Console.WriteLine("Aucun patient dans la file d'attente.");
                Exit(auth);
            }
        }

        public static void AfficherFichierVisites(Authentification auth)
        {
            Medecin med = new Medecin(auth.Login, auth.Password, auth.Nom, auth.Metier, auth.Salle);
            Console.WriteLine("------------- ListeVisites.xml --------\n");
            foreach (Visites visite in med.LoadVisitesXml())
            { 
                Console.WriteLine(visite.ToString());
            }
            Exit(auth);
        }

        public static Patients RecupererPatient(int id_Patient)
        {
            Console.WriteLine("-------------Récupérer un patient de la base--------\n");

            Patients p = new HopitalPatientsSqlServer().FindById(id_Patient);
            Console.WriteLine(p.ToString());
            if (p.Nom != null && p.Nom != "")
            {
                Secretariat.Instance().AddPatient(p);
                return p;
            }
            else
            {
                return null;
            }
                
        }

        public static void AjouterPatient(Authentification auth)
        {
            Console.WriteLine("------------- Ajouter un Patient --------\n");
            Console.Write("ID patient : ");
            if (int.TryParse(Console.ReadLine(), out int id_Patient))
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
                Exit(auth);
            }
            else
            {
                Console.WriteLine("ID patient invalide.");
                Exit(auth);
            }
        }

        public static void TraiterPatient(Authentification auth)
        {
            Medecin med = new Medecin(auth.Login, auth.Password, auth.Nom, auth.Metier, auth.Salle);
            med.Update();
            Exit(auth);
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
                AfficherMenu(Login());
                return null;
            }
        }

        public static void Exit(Authentification auth)
        {
            Console.Write("rentrez une touche pour sortir...");
            Console.ReadKey();
            Console.Clear();
            AfficherMenu(auth);
        }
    }
}
