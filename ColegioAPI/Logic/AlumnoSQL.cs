using ColegioAPI.Model;
using System.Data.SqlClient;

namespace ColegioAPI.Logic
{
    public class AlumnoSQL
    {
        public static List<Alumno> ObtenerAlumnos()
        {
            var connectionString = Utils.ConexionSQL();

            string query = "select a.*, c.nivel, c.letra from alumno a join curso c on a.cursoid = c.id";
            List<Alumno> alumnos = new List<Alumno>();
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
                        string apellido = reader["apellido"].ToString();
                        DateTime fechaNacimiento = (DateTime)reader["fechaNacimiento"];
                        Guid cursoid = (Guid)reader["cursoid"];
                        int nivel = Convert.ToInt32(reader["nivel"]);
                        string letra = reader["letra"].ToString();
                        Curso curso = new Curso
                        {
                            id = cursoid,
                            nivel = nivel,
                            letra = letra
                        };

                        Alumno alumno = new Alumno
                        {
                            id = id,
                            nombre = nombre,
                            apellido = apellido,
                            cursoid = cursoid,
                            fechaNacimiento = fechaNacimiento,
                            curso = curso
                        };

                        alumnos.Add(alumno);
                    }
                }
            }

            return alumnos;
        }

        public static Alumno ObtenerAlumno(string idalumno)
        {
            var connectionString = Utils.ConexionSQL();

            string query = "select a.*, c.nivel, c.letra from alumno a join curso c on a.cursoid = c.id where a.id = @id";
            List<Alumno> alumnos = new List<Alumno>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idalumno);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guid id = (Guid)reader["id"];
                            string nombre = reader["nombre"].ToString();
                            string apellido = reader["apellido"].ToString();
                            DateTime fechaNacimiento = (DateTime)reader["fechaNacimiento"];
                            Guid cursoid = (Guid)reader["cursoid"];
                            int nivel = Convert.ToInt32(reader["nivel"]);
                            string letra = reader["letra"].ToString();
                            Curso curso = new Curso
                            {
                                id = cursoid,
                                nivel = nivel,
                                letra = letra
                            };

                            Alumno alumno = new Alumno
                            {
                                id = id,
                                nombre = nombre,
                                apellido = apellido,
                                cursoid = cursoid,
                                fechaNacimiento = fechaNacimiento,
                                curso = curso

                            };

                            alumnos.Add(alumno);
                        }
                    }
                }

            }

            if (alumnos.Count > 0)
            {
                return alumnos[0];
            }

            return null;
        }

        public static int CrearAlumno(Alumno alumno)
        {
            var connectionString = Utils.ConexionSQL();

            var query = $"insert into alumno (id, nombre, apellido, fechaNacimiento, cursoid) values(@id, @nombre, @apellido, @fechaNacimiento, @cursoid)";
            int resultado = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@id", alumno.id);
                    command.Parameters.AddWithValue("@nombre", alumno.nombre);
                    command.Parameters.AddWithValue("@apellido", alumno.apellido);
                    command.Parameters.AddWithValue("@fechaNacimiento", alumno.fechaNacimiento);
                    command.Parameters.AddWithValue("@cursoid", alumno.cursoid);
                    resultado = command.ExecuteNonQuery();

                }

            }
            return resultado;
        }

        public static int ActualizarAlumno(Alumno alumno, string id)
        {
            var connectionString = Utils.ConexionSQL();
            int resultado = 0;

            var query = $"update alumno set nombre = @nombre, apellido = @apellido, fechaNacimiento = @fechaNacimiento, cursoid = @cursoid where id = @id";
            using(SqlConnection sqlConnection = new SqlConnection( connectionString)) 
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@nombre", alumno.nombre);
                    command.Parameters.AddWithValue("@apellido", alumno.apellido);
                    command.Parameters.AddWithValue("@fechaNacimiento", alumno.fechaNacimiento);
                    command.Parameters.AddWithValue("@cursoid", alumno.cursoid);
                    resultado = command.ExecuteNonQuery();
                }
            }
            return resultado;
        }

        public static int EliminarAlumno (string id)
        {
            var connectionString = Utils.ConexionSQL();

            var query = "delete from alumno where id=@id";
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
