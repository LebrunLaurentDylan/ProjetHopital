using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopitalDll
{
    public class Medecin : IObserver
    {
        private string name;
        private Secretariat salleAttente;

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
            }
        }

        public void Dispose()
        {
            salleAttente.SortirPatient(this);
        }
    }
}

