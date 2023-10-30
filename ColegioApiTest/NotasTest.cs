using ColegioAPI.Logic;
using ColegioAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace ColegioApiTest
{
    public class NotasTest
    {
        private Notas notas;
        [SetUp]
        public void Setup()
        {

        }

        [Test, Order(1)]
        public void CrearNotas()
        {
            var asignatura = AsignaturaSQL.ObtenerAsignaturas();
            if (asignatura.Count == 0)
            {
                Assert.Fail("No se encontraron asignaturas");
            }
            
            var alumno = AlumnoSQL.ObtenerAlumnos();
            if (alumno.Count == 0)
            {
                Assert.Fail("No se encontraron alumnos");
            }

            notas = new ColegioAPI.Model.Notas
            {
                nota = 4,
                alumnoid = alumno[0].id,
                asignaturaid = asignatura[0].id,
                id = Guid.NewGuid()
            };

            var resultado = NotasSQL.CrearNotas(notas);
            Assert.IsTrue(resultado==1);

        }

        [Test, Order(2)]

        public void ObtenerNotasporAlumno()
        {
            var curso = new ColegioAPI.Model.Curso
            {
                nivel = 4,
                letra = "B",
                id = Guid.NewGuid()
            };
            var resultadocurso = CursoSQL.CrearCurso(curso);

            var alumno = new ColegioAPI.Model.Alumno
            {
                nombre = "test",
                apellido = "test",
                cursoid = curso.id,
                fechaNacimiento = new DateTime(1994, 7, 12),
                id = Guid.NewGuid(),
            };
            var resultadoalumno = AlumnoSQL.CrearAlumno(alumno);

            var asignatura = new ColegioAPI.Model.Asignatura
            {
                nombre = "Matematicas",
                id = Guid.NewGuid()
            };
            var resultadoasignatura = AsignaturaSQL.CrearAsignatura(asignatura);

            var notas = new ColegioAPI.Model.Notas
            {
                nota = 4,
                alumnoid = alumno.id,
                asignaturaid = asignatura.id,
                id = Guid.NewGuid()
            };
            var resultadonotas = NotasSQL.CrearNotas(notas);


            var encontrados = NotasSQL.ObtenerNotasporAlumno(alumno.id.ToString());
            var eliminadoNotas = NotasSQL.EliminarNota(notas.id.ToString());
            var eliminadoAlumno = AlumnoSQL.EliminarAlumno(alumno.id.ToString());
            var eliminadoAsignatura = AsignaturaSQL.EliminarAsignatura(asignatura.id.ToString());
            var eliminadoCurso = CursoSQL.EliminarCurso(curso.id.ToString());
            Assert.IsTrue(encontrados.Any());

        }

        [Test, Order(3)]

        public void ObtenerNotasporAsignaturao()
        {
            var curso = new ColegioAPI.Model.Curso
            {
                nivel = 4,
                letra = "B",
                id = Guid.NewGuid()
            };
            var resultadocurso = CursoSQL.CrearCurso(curso);

            var alumno = new ColegioAPI.Model.Alumno
            {
                nombre = "test",
                apellido = "test",
                cursoid = curso.id,
                fechaNacimiento = new DateTime(1994, 7, 12),
                id = Guid.NewGuid(),
            };
            var resultadoalumno = AlumnoSQL.CrearAlumno(alumno);

            var asignatura = new ColegioAPI.Model.Asignatura
            {
                nombre = "Matematicas",
                id = Guid.NewGuid()
            };
            var resultadoasignatura = AsignaturaSQL.CrearAsignatura(asignatura);

            var notas = new ColegioAPI.Model.Notas
            {
                nota = 4,
                alumnoid = alumno.id,
                asignaturaid = asignatura.id,
                id = Guid.NewGuid()
            };
            var resultadonotas = NotasSQL.CrearNotas(notas);


            var encontrados = NotasSQL.ObtenerNotasporAsignatura(asignatura.id.ToString());
            var eliminadoNotas = NotasSQL.EliminarNota(notas.id.ToString());
            var eliminadoAlumno = AlumnoSQL.EliminarAlumno(alumno.id.ToString());
            var eliminadoAsignatura = AsignaturaSQL.EliminarAsignatura(asignatura.id.ToString());
            var eliminadoCurso = CursoSQL.EliminarCurso(curso.id.ToString());
            Assert.IsTrue(encontrados.Any());

        }


        [Test, Order(4)]
        public void Eliminar()
        {
            var eliminado = NotasSQL.EliminarNota(notas.id.ToString());
            Assert.IsTrue(eliminado == 1);

        }




    }
}
