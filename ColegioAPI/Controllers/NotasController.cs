using ColegioAPI.Logic;
using ColegioAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace ColegioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotasController : ControllerBase
    {
        [HttpGet("alumno/{alumno}")]
        public ActionResult GETAlumno(string alumno)
        {
            var notas = NotasSQL.ObtenerNotasporAlumno(alumno);
            return Ok(notas);
        }

        [HttpGet("asignatura/{asignatura}")]
        public ActionResult GETAsignatura(string asignatura)
        {
            var notas = NotasSQL.ObtenerNotasporAsignatura(asignatura);
            return Ok(notas);
        }

        [HttpPost()]
        public ActionResult POST([FromBody] Notas notas)
        {
            if(notas.nota < 1 || notas.nota > 7)
            {
                return BadRequest("La nota debe ser entre 1,0 y 7,0");
            }

            notas.id = Guid.NewGuid();
            NotasSQL.CrearNotas(notas);
            return Ok(notas);
        }

    }
}
