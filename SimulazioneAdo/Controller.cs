using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace SimulazioneAdo
{
    class Controller
    {
        public static string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SimulazioneAdo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void CreaPartita(Partita partita, IEnumerable<int> giocatoriIds)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO Partite (Tipo,Campo,OraInizio,OraFine,Risultato) VALUES (@Tipo,@Campo,@OraInizio,@OraFine,@Risultato)" +
                       "SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Tipo", partita.Tipo);
                    cmd.Parameters.AddWithValue("@Campo", partita.Campo);
                    cmd.Parameters.AddWithValue("@OraInizio", partita.OraInizio);
                    cmd.Parameters.AddWithValue("@OraFine", partita.OraFine);
                    cmd.Parameters.AddWithValue("@Risultato", partita.Risultato);
                    int id = Convert.ToInt32(cmd.ExecuteScalar());

                    CreaPrenotazione(id, giocatoriIds);
                    Console.WriteLine("Partita aggiunta correttamente \n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void CreaGiocatore(Giocatore giocatore)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO Giocatori (Nome,Cognome,DataNascita,Nickname,Livello) VALUES (@Nome,@Cognome,@DataNascita,@Nickname,@Livello)";
                SqlCommand cmd = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Nome", giocatore.Nome);
                    cmd.Parameters.AddWithValue("@Cognome", giocatore.Cognome);
                    cmd.Parameters.AddWithValue("@DataNascita", giocatore.DataNascita);
                    cmd.Parameters.AddWithValue("@Nickname", giocatore.Nickname);
                    cmd.Parameters.AddWithValue("@Livello", giocatore.Livello);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Giocatore aggiunto correttamente \n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public IEnumerable<Partita> CercaPartitePerData(DateTime data)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Partite WHERE OraInizio BETWEEN @OraInizio AND DATEADD (day,1,@OraInizio)";
                SqlCommand cmd = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    List<Partita> partite = new List<Partita>();
                    cmd.Parameters.AddWithValue("@OraInizio", data);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        partite.Add(new Partita(
                            reader["Tipo"].ToString(),
                            Convert.ToInt32(reader["Campo"]),
                            DateTime.Parse(reader["OraInizio"].ToString()),
                            DateTime.Parse(reader["OraFine"].ToString()),
                            reader["Risultato"].ToString()));
                    }
                    return partite;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return null;
            }
        }

        public int CercaGiocatorePerNome(string nome)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Giocatori WHERE Nome =  @Nome";
                SqlCommand cmd = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    int id = 0;
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader["Id"]);
                    }
                    return id;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return 0;
            }
        }

        public void CreaPrenotazione(int idPartita, IEnumerable<int> ids)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO Prenotazioni (IdPartita,IdGiocatore) VALUES (@IdPartita,@IdGiocatore)";
                connection.Open();
                foreach (var id in ids)
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    try
                    {
                        cmd.Parameters.AddWithValue("@IdPartita", idPartita);
                        cmd.Parameters.AddWithValue("@IdGiocatore", id);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        public float LivelloMedio(int eta)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT AVG(Livello) AS media FROM Giocatori WHERE DATEDIFF(year, DataNascita, SYSDATETIME()) < @eta";
                SqlCommand cmd = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    List<Giocatore> giocatori = new List<Giocatore>();
                    cmd.Parameters.AddWithValue("@eta", eta);
                    SqlDataReader reader = cmd.ExecuteReader();
                    float media = 0;
                    while (reader.Read())
                    {
                        media = float.Parse(reader["media"].ToString());
                    }
                    return media;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                return 0;
            }
        }

        public IEnumerable<Partita> MostraPartite()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Partite";
                SqlCommand cmd = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    List<Partita> partite = new List<Partita>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        partite.Add(new Partita(
                            reader["Tipo"].ToString(),
                            Convert.ToInt32(reader["Campo"]),
                            DateTime.Parse(reader["OraInizio"].ToString()),
                            DateTime.Parse(reader["OraFine"].ToString()),
                            reader["Risultato"].ToString()));
                    }
                    return partite;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return null;
            }
        }

        public IEnumerable<Giocatore> MostraGiocatori()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Giocatori";
                SqlCommand cmd = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    List<Giocatore> giocatori = new List<Giocatore>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        giocatori.Add(new Giocatore(
                            reader["Nome"].ToString(),
                            reader["Cognome"].ToString(),     
                            DateTime.Parse(reader["DataNascita"].ToString()),
                            reader["Nickname"].ToString(),
                            Convert.ToInt32(reader["Livello"])));
                    }
                    return giocatori;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return null;
            }
        }
    }
}
