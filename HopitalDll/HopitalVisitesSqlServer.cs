using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopitalData;
using System.Data;
using System.Data.SqlClient;

/*
 * int num_visite 
 * int id_patient
 * str nom_medecin
 * str date_visite
 * int cout_visite
 * int num_salle
 */

namespace HopitalDll
{
    public class HopitalVisitesSqlServer : IHopitalVisites
    {
        public void Create(Visites obj)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Hopital.mdf;Integrated Security=True";
            string sql = "INSERT INTO visites(id_patient, nom_medecin, date_visite, cout_visite, num_salle) VALUES( @id_patient, @nom_medecin, @date_visite, @cout_visite, @num_salle)";
            string sqlRead = "SELECT * FROM visites";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id_patient", obj.IdPatient);
                command.Parameters.AddWithValue("@nom_medecin", obj.NomMedecin);
                command.Parameters.AddWithValue("@date_visite", obj.DateVisite);
                command.Parameters.AddWithValue("@cout_visite", obj.CoutVisite);
                command.Parameters.AddWithValue("@num_salle", obj.NumSalle);

                SqlCommand command2 = new SqlCommand(sqlRead, connection);

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Patient ajouté");

                using (SqlDataReader reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"IdVisite: {reader["num_visite"]}, Idpatient: {reader["id_patient"]}, Nom Medecin: {reader["nom_medecin"]}, date: {reader["date_visite"]}, Prix: {reader["cout_visite"]}, Salle: {reader["num_salle"]}");
                    }
                }

                connection.Close();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Visites> FindAll()
        {
            List<Visites> liste = new List<Visites>();
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Hopital.mdf;Integrated Security=True";
            string sqlRead = "SELECT * FROM visites";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlRead, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Visites a = new Visites(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5));
                liste.Add(a);
            }

            return liste;
        }

        public Visites FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Visites> FindByPatient(int idPatient)
        {
            
            List<Visites> liste = new List<Visites>();
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Hopital.mdf;Integrated Security=True";
            string sqlRead = "SELECT * FROM visites WHERE id_patient=@id";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlRead, connection);
            command.Parameters.AddWithValue("@id", idPatient);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Visites vis = new Visites(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5));
                liste.Add(vis);
            }

            return liste;
        }

        public void Update(Visites obj)
        {
            throw new NotImplementedException();
        }
    }
}
