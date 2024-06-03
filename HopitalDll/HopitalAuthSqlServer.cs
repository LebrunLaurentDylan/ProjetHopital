using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using HopitalData;

namespace HopitalDll
{
    public class HopitalAuthSqlServer : IhopitalAuth
    {
        private string ConnectionString()
        {
            var enviroment = System.Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.Parent.FullName;
            return $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={projectDirectory}\HopitalDll\Hopital.mdf;Integrated Security=True";
        }

        public Authentification Login(string login, string password)
        {
            Authentification auth = new Authentification();
            string connectionString = ConnectionString();
            string sqlRead = "SELECT * FROM Authentification WHERE login=@login AND password=@password";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlRead, connection);
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@password", password);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //Console.WriteLine($"Login: {reader["login"]}, password: {reader["password"]}, Nom: {reader["nom"]}, metier:{reader["metier"]}");
                auth = new Authentification(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                return auth;
            }

            if (auth.Login != "" && auth.Login != null)
                { return auth;}
            else
                { return null;}
                
        }

        public List<Authentification> FindAll()
        {
            throw new NotImplementedException();
        }

        public Authentification FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Authentification obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Authentification obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
