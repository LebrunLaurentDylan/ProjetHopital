﻿using System;
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

            //Secretariat salleAttente = Secretariat.Instance;

            //Medecin med1 = new Medecin("Dr. Hugotte", salleAttente);
            //Medecin med2 = new Medecin("Dr. Martin", salleAttente);

            //salleAttente.AddPatient(new Patients(222211, "MARTIN", "David", 38, 0458962300, "Paris"));
            //salleAttente.AddPatient(new Patients(222212, "BERTRAND", "George", 23, 0458962300, "Paris"));
            //salleAttente.AddPatient(new Patients(222213, "LEPETIT", "Stephanie", 27, 0458962300, "Paris"));

            //// Simulate doctors becoming available and seeing patients
            //med1.Update();
            //med2.Update();
            //med1.Update();
            //med2.Update();

            //med1.Dispose();
            //med2.Dispose();

            if (Operations.Login() != null)
            {
                Console.WriteLine(Operations.Login().ToString());
                
            }
            else
            {
                Console.WriteLine(" Error Authentification");

            }

            //Authentification auth = new HopitalAuthSqlServer().Login("bb", "bb");
            //Medecin ops = new Medecin(auth.Login, auth.Password, auth.Nom, auth.Metier, auth.Salle);
            //Console.WriteLine(ops.ToString());
            //// Ajouter des visites pour test
            //Patients pat = new HopitalPatientsSqlServer().FindById(2525);
            //ops.ListeVisites.Add(new Visites(pat.IdPatient, ops.Nom, Convert.ToString(DateTime.Now),23,ops.Salle));
            //ops.ListeVisites.Add(new Visites(pat.IdPatient, ops.Nom, Convert.ToString(DateTime.Now), 23, ops.Salle));

            //// Sauvegarder la liste des visites
            //ops.SaveVisitesXml();

            //// Charger la liste des visites
            //List<Visites> loadedVisites = ops.LoadVisitesXml();

            //// Afficher les visites chargées
            //foreach (var visite in loadedVisites)
            //{
            //    Console.WriteLine($"ID: {visite.IdPatient}, Nom: {visite.NomMedecin}, Date: {visite.DateVisite}, Cout: {visite.CoutVisite}, Salle: {visite.NumSalle}");
            //}


        }
    }
}
