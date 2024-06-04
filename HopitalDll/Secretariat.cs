using System;
using System.Collections.Generic;
using HopitalData;

namespace HopitalDll
{
    public class Secretariat
    {
        private static Secretariat instance = null;
        private static readonly object padlock = new object();
        private Queue<Patients> patientQueue = new Queue<Patients>();

        // Constructeur privé pour empêcher l'instantiation externe
        private Secretariat()
        {
            // Initialiser ici des patients par défaut si nécessaire
        }

        // Méthode pour obtenir l'instance unique du singleton
        public static Secretariat Instance()
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Secretariat();
                    }
                }
            }
            return instance;
        }

        // Méthode pour ajouter un patient à la file d'attente
        public string AddPatient(Patients patient)
        {
            lock (padlock)
            {
                patientQueue.Enqueue(patient);
                return NotifyPatient(patient);
            }
        }

        // Méthode pour obtenir le prochain patient de la file d'attente
        public Patients GetNextPatient()
        {
            lock (padlock) 
            {
                if (patientQueue.Count > 0)
                {
                    
                    return patientQueue.Dequeue();
                }
                return null;
            }
        }

        // Méthode pour retourner le contenu de la file d'attente
        public Patients[] GetAllPatients()
        {
            lock (padlock)
            {
                return patientQueue.ToArray();
            }
        }

        // Méthode pour récupérer un patient spécifique (exemple pour external method)
        public Patients PeekNextPatient()
        {
            lock (padlock)
            {
                if (patientQueue.Count > 0)
                {
                    return patientQueue.Peek();
                }
                return null;
            }
        }

        // Exemple de méthode de notification (si nécessaire)
        private string NotifyPatient(Patients patient)
        {
            int position = patientQueue.Count; // Position du patient dans la file d'attente
            return $"Patient {patient.Nom} added to the queue at position {position}.";
        }
    }
}
