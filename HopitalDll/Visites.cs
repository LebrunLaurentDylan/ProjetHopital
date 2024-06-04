using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * int num_visite 
 * int id_patient
 * str nom_medecin
 * str date_visite
 * int cout_visite
 * int num_salle
 */
namespace HopitalData
{
    [Serializable]
    public class Visites
    {
        // attributs 
        
        private int idPatient;
        private string nomMedecin;
        private int numSalle;
        private string dateVisite;
        private int coutVisite = 23;

        [NonSerialized]
        private int numVisite;

        // properties 
        public int IdPatient  { get => idPatient; }
        public int NumVisite { set =>numVisite= value; }
        public int NumSalle { get => numSalle; }
        public string NomMedecin { get => nomMedecin; }
        public string DateVisite { get => dateVisite; }
        public int CoutVisite { get => coutVisite; }

        // constructeur
        public Visites()
        {

        }
        public Visites(int idPatient, string nomMedecin, int numSalle, string dateVisite)
        {
            this.idPatient = idPatient;
            this.nomMedecin = nomMedecin;
            this.numSalle = numSalle;
            this.dateVisite = dateVisite;
        }

        public Visites(int idPatient, string nomMedecin, string dateVisite, int coutVisite, int numSalle)
        {
            //this.numVisite = numVisite;
            this.idPatient = idPatient;
            this.nomMedecin = nomMedecin;
            this.dateVisite = dateVisite;
            this.coutVisite = coutVisite;
            this.numSalle = numSalle;
        }

        public Visites(int numVisite, int idPatient, string nomMedecin, string dateVisite, int coutVisite, int numSalle)
        {
            this.numVisite = numVisite;
            this.idPatient = idPatient;
            this.nomMedecin = nomMedecin;
            this.dateVisite = dateVisite;
            this.coutVisite = coutVisite;
            this.numSalle = numSalle;
        }

        // methodes 
        public override string ToString()
        {
            return $"N° Visite: {numVisite}, Id Patient:{idPatient}, Nom Medecin:{nomMedecin}\nDate Visite:{dateVisite}, N° Salle:{numSalle}, Cout:{coutVisite} eur";
        }
    }
}
