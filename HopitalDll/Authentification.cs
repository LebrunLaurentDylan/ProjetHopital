using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopitalData
{
    class Authentification
    {
        // attributs 
        private string login;
        private string password;
        private string nom;
        private string metier;
        // properties
        public string Metier { get => metier; }
        public string Nom { get => nom; }
        public string Password { get => password; }
        public string Login { get => login; }
        // constructeur
        public Authentification() { }
        public Authentification(string login, string password, string nom, string metier)
        {
            this.login = login;
            this.password = password;
            this.nom = nom;
            this.metier = metier;
        }
        // methodes
        public override string ToString()
        {
            return $"You're Logged as : {metier}-{nom}\n";
        }
    }
}
