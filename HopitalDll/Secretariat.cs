using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopitalData;

namespace HopitalDll
{
    public class Secretariat : IPatient
    {
        private static Secretariat instance = null;
        private static readonly object padlock = new object();
        private static Queue<Patients> patientQueue = new Queue<Patients>();
        private List<IObserver> observers = new List<IObserver>();

        private Secretariat() { }

        public static Secretariat Instance
        {
            get
            { lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Secretariat();
                    } return instance;
                }
            }
        }

        public void AddPatient(Patients patient)
        {
            patientQueue.Enqueue(patient);
            NotifyPatient();
        }

        public Patients GetNextPatient()
        {
            if (patientQueue.Count > 0)
            {
                return patientQueue.Dequeue();
            }
            return null;
        }

        public void EntrerPatient(IObserver observer)
        {
            observers.Add(observer);
        }

        public void SortirPatient(IObserver observer)
        {
            observers.Remove(observer);
        }
        public void NotifyPatient()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }
    }
}
