using ColegioAPI.Model;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;

namespace ColegioAPI.Logic
{
    public class AsignaturaSQL
    {
        public static List<Asignatura> ObtenerAsignaturas()
        {
            var connectionString = Utils.ConexionSQL();

            string query = "select * from asignatura";
            List<Asignatura> asignaturas = new List<Asignatura>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Guid id = (Guid)reader["id"];
                        string nombre = reader["nombre"].ToString();

                        Asignatura asignatura = new Asignatura
                        {
                            id = id,
                            nombre = nombre
                        };

                        asignaturas.Add(asignatura);
                    }
                }
            }
            return asignaturas;
        }

        public static Asignatura ObtenerAsignatura(string idasignatura)
        {
            var connectionString = Utils.ConexionSQL();

            string query = "select * from asignatura where id = @id";
            List<Asignatura> asignaturas = new List<Asignatura>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idasignatura);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guid id = (Guid)reader["id"];
                            string nombre = reader["nombre"].ToString();

                            Asignatura asignatura = new Asignatura
                            {
                                id = id,
                                nombre = nombre
                            };

                            asignaturas.Add(asignatura);
                        }
                    }
                }
            }

            if (asignaturas.Count > 0)
            {
                return asignaturas[0];
            }

            return null;
        }

        public static int CrearAsignatura(Asignatura asignatura)
        {
            var connectionString = Utils.ConexionSQL();

            var query = $"insert into asignatura (id, nombre) values(@id, @nombre)";
            int resultado = 0;


            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@id", asignatura.id);
                    command.Parameters.AddWithValue("@nombre", asignatura.nombre);
                    resultado = command.ExecuteNonQuery();

                }

            }
            return resultado;

        }

        public static int ActualizarAsignatura(Asignatura asignatura, string id)
        {
            var connectionString = Utils.ConexionSQL();

            var query = $"update asignatura set nombre = @nombre where id = @id";
            int resultado = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@nombre", asignatura.nombre);
                    resultado = command.ExecuteNonQuery();
                }
            }
            return resultado;
        }

        public static int EliminarAsignatura(string id)
        {
            var connectionString = Utils.ConexionSQL();

            var query = "delete from asignatura where id=@id";
            int resultado = 0;


            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    resultado = command.ExecuteNonQuery();

                }
            }
            return resultado;
        }
    }
}
