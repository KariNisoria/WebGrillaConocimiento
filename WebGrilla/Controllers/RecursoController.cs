using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs;
using WebGrilla.Services;

namespace WebGrilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecursoController : ControllerBase
    {
        private readonly IRecursoService _service;
        public RecursoController(IRecursoService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecursoDTO>>> GetAllRecurso()
        {
            var resultado = await _service.GetAllRecurso();
            return Ok(resultado);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RecursoDTO>> GetRecursoById(int id)
        {
            var resultado = await _service.GetRecursoById(id);
            if (resultado == null)
            {
                return NotFound($"Error: No se encontró un recurso con ID {id}.");
            }
            return Ok(resultado);
        }
        [HttpGet("ByCorreo/{correo}")]
        public async Task<ActionResult<RecursoDTO>> GetRecursoByCorreoElectronico(string correo)
        {
            var resultado = await _service.GetRecursoByCorreoElectronico(correo);
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }
        [HttpPost]
        public async Task<ActionResult<RecursoDTO>> AddRecurso([FromBody] RecursoDTO recurso)
        {
            var resultado = await _service.AddRecurso(recurso);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return CreatedAtAction(nameof(GetRecursoById), new { id = resultado.value.IdRecurso }, resultado.value);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecurso(int id)
        {
            var resultado = await _service.DeleteRecurso(id);
            if (!resultado.isSuccess)
            {
                return BadRequest($"Error: No se pudo eliminar el registro: {resultado.message}");
            }
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update (int id, [FromBody]RecursoDTO recurso)
        {
            var resultado = await _service.UpdateRecurso(id, recurso);
            if (!resultado.isSuccess)
            {
                return BadRequest($"Error: No se pudo actualizar elr recurso: {resultado.message}");
            }
            return NoContent();
        }
    }
}
