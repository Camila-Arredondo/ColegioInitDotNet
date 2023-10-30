namespace ColegioAPI.Model
{
    public class Alumno
    {
        public Guid id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int edad { get {

                var hoy = DateTime.Today;
                var edad = hoy.Year - fechaNacimiento.Year;

                //19-12-2010 >  29-10-2010
                if (fechaNacimiento.Date > hoy.AddYears(-edad))
                {
                    edad--;
                }

                return edad;

            }
        }

        public Curso? curso { get; set; }
        public Guid cursoid { get; set; }

    }
}
