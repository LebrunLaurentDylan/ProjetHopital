using HopitalDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;

namespace HopitalData
{
    public class Authentification: IObserver
    {
        // attributs
        protected string login;
        protected string password;
        protected string nom;
        protected string metier;
        protected int salle;
        protected Secretariat salleAttente;
        

        // properties
        public string Metier { get => metier; }
        public string Nom { get => nom; }
        public string Password { get => password; }
        public string Login { get => login; }
        public int Salle { get => salle; }

        // constructeur
        public Authentification() { }
        public Authentification(string login, string password, string nom, string metier, int salle)
        {
            this.login = login;
            this.password = password;
            this.nom = nom;
            this.metier = metier;
            this.salle = salle;
        }

        // methodes
        public override string ToString()
        {
            return $"You're Logged as : {metier}-{nom}\nSalle: {salle}";
        }

        public virtual void Update()
        {
            throw new NotImplementedException();
        }
    }
}
