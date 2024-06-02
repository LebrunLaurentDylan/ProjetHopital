using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopitalData
{
    [Serializable]
    public class Visites
    {
        // attributs 
        private int numVisite;
        private int idPatient;
        private string nomMedecin;
        private int numSalle;
        private string dateVisite;
        public const int COUTVISITE = 23;

        // properties 
        public int IdPatient  { get => idPatient; }
        public int NumVisite { get =>numVisite; }
        public int NumSalle { get => numSalle; }
        public string NomMedecin { get => nomMedecin; }
        public string DateVisite { get => dateVisite; }

        // constructeur
        public Visites(int idPatient, string nomMedecin, int numSalle, string dateVisite)
        {
            this.idPatient = idPatient;
            this.nomMedecin = nomMedecin;
            this.numSalle = numSalle;
            this.dateVisite = dateVisite;
        }

        // methodes 
        public override string ToString()
        {
            return $"N° Visite: {numVisite}, Id Patient:{idPatient}, Nom Medecin:{nomMedecin}\nDate Visite:{dateVisite}, N° Salle:{numSalle}, Cout:{COUTVISITE} eur";
        }
    }
}
