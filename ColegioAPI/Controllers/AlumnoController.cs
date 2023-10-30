using ColegioAPI.Logic;
using ColegioAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColegioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        [HttpGet]
        public ActionResult GET()
        {
            var alumnos = AlumnoSQL.ObtenerAlumnos();
            return Ok(alumnos);
        }

        [HttpGet("{id}")]
        public ActionResult GET(string id)
        {
            var alumno = AlumnoSQL.ObtenerAlumno(id);
            return Ok(alumno);
        }

        [HttpPost()]
        public ActionResult POST([FromBody] Alumno alumno) 
        {
            alumno.id= Guid.NewGuid();
            AlumnoSQL.CrearAlumno(alumno);
            return Ok(alumno);
        }

        [HttpPatch("{id}")]
        public ActionResult PATCH([FromBody] Alumno alumno, string id)
        {
            var alumnoExiste = AlumnoSQL.ObtenerAlumno(id);
            if(alumnoExiste==null)
            {
                return NotFound($"No existe el alumno con id {id}");
            }

            AlumnoSQL.ActualizarAlumno(alumno, id);
            return Ok(alumno);    
        }

        [HttpDelete("{id}")]
        public ActionResult DELETE(string id) 
        {
            AlumnoSQL.EliminarAlumno(id);
            return Ok();
        }


    }
}
