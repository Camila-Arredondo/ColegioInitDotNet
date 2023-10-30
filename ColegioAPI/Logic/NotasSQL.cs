using ColegioAPI.Model;
using System.Data.SqlClient;

namespace ColegioAPI.Logic
{
    public class NotasSQL
    {
        public static void CrearNotas(Notas notas)
        {
            var connectionString = Utils.ConexionSQL();
            
            var query = $"insert into notas (id, nota, alumnoid, asignaturaid) values(@id, @nota, @alumnoid, @asignaturaid)";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@id", notas.id);
                    command.Parameters.AddWithValue("@nota", notas.nota);
                    command.Parameters.AddWithValue("@alumnoid", notas.alumnoid);
                    command.Parameters.AddWithValue("@asignaturaid", notas.asignaturaid);
                    command.ExecuteNonQuery();

                }
            }
        }
        public static List<Notas> ObtenerNotasporAlumno(string idalumno)
        {
            var connectionString = Utils.ConexionSQL();

            string query = @"select n.*,
                                a.nombre, a.apellido, a.fechaNacimiento, a.cursoid, a.id as aid,
                                ag.nombre as agnombre, ag.id as agid,
                                c.nivel, c.letra, c.id as cid
                                from notas n
                                join alumno a on n.alumnoid = a.id
                                join Asignatura ag on n.asignaturaid = ag.id
                                join Curso c on a.cursoid = c.id
                            where n.alumnoid = @id
                                ";
            List<Notas> notas = new List<Notas>();
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
                            #region Notas
                            Guid id = (Guid)reader["id"];
                            double nota = Convert.ToDouble(reader["nota"]);
                            Guid alumnoid = (Guid)reader["alumnoid"];
                            Guid asignaturaid = (Guid)reader["asignaturaid"];
                            #endregion

                            #region Curso
                            Guid cid = (Guid)reader["cid"];
                            int nivel = Convert.ToInt32(reader["nivel"]);
                            string letra = reader["letra"].ToString();
                            Curso curso = new Curso
                            {
                                id = cid,
                                nivel = nivel,
                                letra = letra
                            };
                            #endregion

                            #region Alumno
                            Guid aid = (Guid)reader["aid"];
                            string nombre = reader["nombre"].ToString();
                            string apellido = reader["apellido"].ToString();
                            DateTime fechaNacimiento = (DateTime)reader["fechaNacimiento"];
                            Guid cursoid = (Guid)reader["cursoid"];
                            Alumno alumno = new Alumno
                            {
                                id = aid,
                                nombre = nombre,
                                apellido = apellido,
                                cursoid = cursoid,
                                fechaNacimiento = fechaNacimiento,
                                curso = curso

                            };
                            #endregion

                            #region Asignatura
                            Guid agid = (Guid)reader["agid"];
                            string agnombre = reader["agnombre"].ToString();
                            Asignatura asignatura = new Asignatura
                            {
                                id = agid,
                                nombre = agnombre
                            };
                            #endregion

                            Notas notaN = new Notas
                            {
                                id = id,
                                nota = nota,
                                alumnoid = alumnoid,
                                asignaturaid = asignaturaid,
                                alumno = alumno,
                                asignatura = asignatura
                            };

                            notas.Add(notaN);
                        }
                    }
                }

            }

            return notas;
        }

        public static List<Notas> ObtenerNotasporAsignatura(string idasignatura)
        {
            var connectionString = Utils.ConexionSQL();

            string query = @"select n.*,
                                a.nombre, a.apellido, a.fechaNacimiento, a.cursoid, a.id as aid,
                                ag.nombre as agnombre, ag.id as agid,
                                c.nivel, c.letra, c.id as cid
                                from notas n
                                join alumno a on n.alumnoid = a.id
                                join Asignatura ag on n.asignaturaid = ag.id
                                join Curso c on a.cursoid = c.id
                            where n.asignaturaid = @id
                                ";
            List<Notas> notas = new List<Notas>();
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


                            #region Curso
                            Guid cid = (Guid)reader["cid"];
                            int nivel = Convert.ToInt32(reader["nivel"]);
                            string letra = reader["letra"].ToString();
                            Curso curso = new Curso
                            {
                                id = cid,
                                nivel = nivel,
                                letra = letra
                            };
                            #endregion

                            #region Alumno
                            Guid aid = (Guid)reader["aid"];
                            string nombre = reader["nombre"].ToString();
                            string apellido = reader["apellido"].ToString();
                            DateTime fechaNacimiento = (DateTime)reader["fechaNacimiento"];
                            Guid cursoid = (Guid)reader["cursoid"];
                            Alumno alumno = new Alumno
                            {
                                id = aid,
                                nombre = nombre,
                                apellido = apellido,
                                cursoid = cursoid,
                                fechaNacimiento = fechaNacimiento,
                                curso = curso

                            };
                            #endregion

                            #region Asignatura
                            Guid agid = (Guid)reader["agid"];
                            string agnombre = reader["agnombre"].ToString();
                            Asignatura asignatura = new Asignatura
                            {
                                id = agid,
                                nombre = agnombre
                            };
                            #endregion

                            #region Notas
                            Guid id = (Guid)reader["id"];
                            double nota = Convert.ToDouble(reader["nota"]);
                            Guid alumnoid = (Guid)reader["alumnoid"];
                            Guid asignaturaid = (Guid)reader["asignaturaid"];  
                            Notas notaN = new Notas
                            {
                                id = id,
                                nota = nota,
                                alumnoid = alumnoid,
                                asignaturaid = asignaturaid,
                                alumno = alumno,
                                asignatura = asignatura
                            };
                            #endregion
                       

                            notas.Add(notaN);
                        }
                    }
                }

            }

            return notas;
        }






    }
}
