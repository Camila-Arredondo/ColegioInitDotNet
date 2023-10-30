using ColegioAPI.Logic;
using ColegioAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioApiTest
{
    public class AsignaturaTest
    {
        private Asignatura asignatura;
        [SetUp]
        public void Setup()
        {

        }

        [Test, Order(1)]
        public void CrearAsignatura()
        {
            asignatura = new ColegioAPI.Model.Asignatura
            {
                nombre = "Matematicas",
                id = Guid.NewGuid()
            };

            var resultado = AsignaturaSQL.CrearAsignatura(asignatura);
            Assert.IsNotNull(resultado);

        }

        [Test, Order(2)]
        public void ObtenerAsigatura()
        {
            var encontrado = AsignaturaSQL.ObtenerAsignatura(asignatura.id.ToString());
            Assert.IsTrue(encontrado != null);
        }

        [Test, Order(3)]
        public void ObtenerTodosLasAsignaturas()
        {
            var encontrados = AsignaturaSQL.ObtenerAsignaturas();
            Assert.IsTrue(encontrados.Any());
        }

        [Test, Order(4)]
        public void ActualizarAsignatura()
        {
            asignatura.nombre = "Lenguaje";
            var actualizados = AsignaturaSQL.ActualizarAsignatura(asignatura, asignatura.id.ToString());
            Assert.IsTrue(actualizados == 1);
        }

        [Test, Order(5)]
        public void Eliminar()
        {
            var eliminado = AsignaturaSQL.EliminarAsignatura(asignatura.id.ToString());
            Assert.IsTrue(eliminado == 1);

        }
    }
}
