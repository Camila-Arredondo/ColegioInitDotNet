using ColegioAPI.Logic;
using ColegioAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColegioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        [HttpGet]
        public ActionResult GET()
        {
            var curso = CursoSQL.ObtenerCursos();
            return Ok(curso);
        }

        [HttpGet("{id}")]
        public ActionResult GET(string id)
        {
            var curso = CursoSQL.ObtenerCurso(id);
            return Ok(curso);
        }

        [HttpPost()]
        public ActionResult POST([FromBody] Curso curso)
        {
            if(curso.nivel < 0 || curso.nivel > 12)
            {
                return BadRequest("El nivel debe ser entre 0 y 12");
            }

            curso.id = Guid.NewGuid();
            CursoSQL.CrearCurso(curso);
            return Ok(curso);
        }

        [HttpPatch("{id}")]
        public ActionResult PATCH([FromBody] Curso curso, string id)
        {
            var cursoExiste = CursoSQL.ObtenerCurso(id);
            if (cursoExiste == null)
            {
                return NotFound($"No existe el curso con id {id}");
            }

            CursoSQL.ActualizarCurso(curso, id);
            return Ok(curso);
        }

        [HttpDelete("{id}")]
        public ActionResult DELETE(string id)
        {
            CursoSQL.EliminarCurso(id);
            return Ok();
        }


    }
}
