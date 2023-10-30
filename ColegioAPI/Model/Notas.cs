namespace ColegioAPI.Model
{
    public class Notas
    {
        public Guid id { get; set; }
        public decimal nota { get; set; }
        public Alumno? alumno { get; set; }
        public Guid alumnoid { get; set; }
        public Asignatura? asignatura { get; set; }
        public Guid asignaturaid { get; set; } 
    }
}
