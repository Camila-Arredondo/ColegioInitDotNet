using ColegioAPI.Model;
using System.Data.SqlClient;

namespace ColegioAPI.Logic
{
    public class CursoSQL
    {
        public static List<Curso> ObtenerCursos()
        {
            var connectionString = Utils.ConexionSQL();

            string query = "select * from curso";
            List<Curso> cursos = new List<Curso>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Guid id = (Guid)reader["id"];
                        int nivel = Convert.ToInt32(reader["nivel"]);
                        string letra = reader["letra"].ToString();

                        Curso curso = new Curso
                        {
                            id = id,
                            nivel = nivel,
                            letra = letra
                        };

                        cursos.Add(curso);
                    }
                }
            }
            return cursos;
        }

        public static Curso ObtenerCurso(string idcurso)
        {
            var connectionString = Utils.ConexionSQL();

            string query = "select * from curso where id = @id";
            List<Curso> cursos = new List<Curso>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idcurso);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guid id = (Guid)reader["id"];
                            int nivel = Convert.ToInt32(reader["nivel"]);
                            string letra = reader["letra"].ToString();

                            Curso curso = new Curso
                            {
                                id = id,
                                nivel = nivel,
                                letra = letra
                            };

                            cursos.Add(curso);
                        }
                    }
                }

            }

            if (cursos.Count > 0)
            {
                return cursos[0];
            }

            return null;
        }

        public static int CrearCurso(Curso curso)
        {
            var connectionString = Utils.ConexionSQL();

            var query = $"insert into curso (id, nivel, letra) values(@id, @nivel, @letra)";
            int resultado = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@id", curso.id);
                    command.Parameters.AddWithValue("@nivel", curso.nivel);
                    command.Parameters.AddWithValue("@letra", curso.letra);
                    resultado = command.ExecuteNonQuery();

                }

            }
            return resultado;
        }

        public static int ActualizarCurso(Curso curso, string id)
        {
            var connectionString = Utils.ConexionSQL();
            int resultado = 0;

            var query = $"update curso set nivel = @nivel, letra = @letra where id = @id";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@nivel", curso.nivel);
                    command.Parameters.AddWithValue("@letra", curso.letra);
                    resultado = command.ExecuteNonQuery();
                }
            }
            return resultado;
        }

        public static int EliminarCurso(string id)
        {
            var connectionString = Utils.ConexionSQL();

            var query = "delete from curso where id=@id";
            int resultado = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    resultado=command.ExecuteNonQuery();

                }
            }
            return resultado;
        }

    }
}
