using ColegioAPI.Logic;
using ColegioAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioApiTest
{
    public class AlumnoTest
    {
        private Alumno alumno;
        [SetUp]
        public void Setup()
        {
          
        }

        [Test, Order(1)]
        public void CrearAlumnoTest()
        {
            var curso = CursoSQL.ObtenerCursos();

            if (curso.Count == 0)
            {
                Assert.Fail("No se encontraron cursos");
            }

            alumno = new ColegioAPI.Model.Alumno
            {
                nombre = "test",
                apellido = "test",
                cursoid = curso[0].id,
                fechaNacimiento = new DateTime(1994, 7, 12),
                id = Guid.NewGuid(),
            };
            var resultado = AlumnoSQL.CrearAlumno(alumno);
            Assert.IsTrue(resultado == 1);

        }
        [Test, Order(2)]
        public void ObtenerAlumno()
        {
            var encontrado = AlumnoSQL.ObtenerAlumno(alumno.id.ToString());
            Assert.IsTrue(encontrado != null);
        }
        [Test, Order(3)]
        public void ObtenerTodosLosAlumnos()
        {
            var encontrados = AlumnoSQL.ObtenerAlumnos();
            Assert.IsTrue(encontrados.Any());
        }
        [Test, Order(4)]
        public void ActualizarAlumno()
        {
            alumno.nombre = "test2";
            var actualizados = AlumnoSQL.ActualizarAlumno(alumno, alumno.id.ToString());
            Assert.IsTrue(actualizados == 1);

        }

        [Test, Order(5)]
        public void Eliminar()
        {
            var eliminado = AlumnoSQL.EliminarAlumno(alumno.id.ToString());
            Assert.IsTrue(eliminado == 1);


        }
    }
}
