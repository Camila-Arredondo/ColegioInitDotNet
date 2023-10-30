using ColegioAPI.Logic;
using ColegioAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColegioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignaturaController : ControllerBase
    {
        [HttpGet]
        public ActionResult GET()
        {
            var asignatura = AsignaturaSQL.ObtenerAsignaturas();
            return Ok(asignatura);
        }

        [HttpGet("{id}")]
        public ActionResult GET(string id)
        {
            var asignatura = AsignaturaSQL.ObtenerAsignatura(id);
            return Ok(asignatura);
        }

        [HttpPost()]
        public ActionResult POST([FromBody] Asignatura asignatura)
        {
            asignatura.id = Guid.NewGuid();
            AsignaturaSQL.CrearAsignatura(asignatura);
            return Ok(asignatura);
        }

        [HttpPatch("{id}")]
        public ActionResult PATCH([FromBody] Asignatura asignatura, string id)
        {
            var asignaturaExiste = AsignaturaSQL.ObtenerAsignatura(id);
            if (asignaturaExiste == null)
            {
                return NotFound($"No existe la asignatura con id {id}");
            }

            AsignaturaSQL.ActualizarAsignatura(asignatura, id);
            return Ok(asignatura);
        }

        [HttpDelete("{id}")]
        public ActionResult DELETE(string id)
        {
            AsignaturaSQL.EliminarAsignatura(id);
            return Ok();
        }


    }
}
