using ColegioAPI.Logic;
using ColegioAPI.Model;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioApiTest
{
    public class CursoTest
    {
        private Curso curso;
        [SetUp]
        public void Setup()
        {

        }

        [Test, Order(1)]
        public void CrearCursoTest()
        {
            curso = new ColegioAPI.Model.Curso
            {
                nivel = 4,
                letra = "B",
                id = Guid.NewGuid()
            };

            var resultado=CursoSQL.CrearCurso(curso);
            Assert.IsNotNull(resultado);

        }

        [Test, Order(2)]
        public void ObtenerCurso()
        {
            var encontrado = CursoSQL.ObtenerCurso(curso.id.ToString());
            Assert.IsTrue(encontrado != null);
        }

        [Test, Order(3)]
        public void ObtenerTodosLosCursos()
        {
            var encontrados = CursoSQL.ObtenerCursos();
            Assert.IsTrue(encontrados.Any());
        }
        
        [Test, Order(4)]
        public void ActualizarCurso()
        {
            curso.nivel = 5;
            var actualizados = CursoSQL.ActualizarCurso(curso, curso.id.ToString());
            Assert.IsTrue(actualizados == 1);
        }

        [Test, Order(5)]
        public void Eliminar()
        {
            var eliminado = CursoSQL.EliminarCurso(curso.id.ToString());
            Assert.IsTrue(eliminado == 1);

        }
    }
}

