using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using HopitalData;
//Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\devil\Documents\formation\POEC_dotNET\ADONET\ProjetHopital\ProjetHopital\Hopital.mdf;Integrated Security=True

namespace HopitalDll
{
    public class HopitalPatientsSqlServer : IhopitalPatients
    {
        public void Create(Patients obj)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Hopital.mdf;Integrated Security=True";
            string sql = "INSERT INTO patients(Id, nom, prenom, age, telephone, adresse) VALUES(@Id, @nom, @prenom, @age, @telephone, @adresse)";
            string sqlRead = "SELECT * FROM patients";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", obj.IdPatient);
                command.Parameters.AddWithValue("@nom", obj.Nom);
                command.Parameters.AddWithValue("@prenom", obj.Prenom);
                command.Parameters.AddWithValue("@age", obj.Age);
                command.Parameters.AddWithValue("@telephone", obj.Telephone);
                command.Parameters.AddWithValue("@adresse", obj.Adresse);

                SqlCommand command2 = new SqlCommand(sqlRead, connection);

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Patient ajouté");

                using (SqlDataReader reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Idpatient: {reader["id"]}, Nom: {reader["nom"]}, prenom: {reader["prenom"]}");
                    }
                }

                connection.Close();
            }
        }

        public void Delete(int id)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Hopital.mdf;Integrated Security=True";
            string sql = "DELETE FROM patients WHERE Id=@id";
            string sqlRead = "SELECT * FROM patients";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);

                SqlCommand command2 = new SqlCommand(sqlRead, connection);

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("article supprimé");

                using (SqlDataReader reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Idpatient: {reader["id"]}, Nom: {reader["nom"]}, prenom: {reader["prenom"]}");
                    }
                }
                connection.Close();
            }
        }

        public List<Patients> FindAll()
        {
            List<Patients> liste = new List<Patients>();
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Hopital.mdf;Integrated Security=True";
            string sqlRead = "SELECT * FROM patients";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlRead, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Patients a = new Patients(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetString(5));
                liste.Add(a);
            }

            return liste;
        }

        public Patients FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Patients obj)
        {
            throw new NotImplementedException();
        }
    }
}
