using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopitalData
{
    public class Patients
    {
        // attributs 
        private int idPatient;
        private string nom;
        private string prenom;
        private int age;
        private int telephone;
        private string adresse;

        // properties 
        public int IdPatient { get => idPatient; }
        public int Age { get => age; }
        public int Telephone { get => telephone; }
        public string Adresse { get => adresse; }
        public string Nom { get => nom; }
        public string Prenom { get => prenom; }

        // constructeur
        public Patients(int idPatient, string nom, string prenom, int age, int telephone, string adresse)
        {
            this.idPatient = idPatient;
            this.nom = nom;
            this.prenom = prenom;
            this.age = age;
            this.telephone = telephone;
            this.adresse = adresse;
        }

        // methodes 
        public override string ToString()
        {
            return $"Numéro Sécu: {idPatient}, Nom:{nom}, Prénom:{prenom}\nAge:{age}, N° telephone:{telephone}, Domicile:{adresse}";
        }
    }
}
