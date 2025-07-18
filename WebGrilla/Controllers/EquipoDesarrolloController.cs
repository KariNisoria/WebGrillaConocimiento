using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs;
using WebGrilla.Services;

namespace WebGrilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoDesarrolloController : ControllerBase
    {
        private readonly IEquipoDesarrolloService _service;
        public EquipoDesarrolloController(IEquipoDesarrolloService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipoDesarrolloDTO>>> GetAllEquipoDesarrollo()
        {
            var resultado = await _service.GetAllEquipoDesarrollo();
            return Ok(resultado);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipoDesarrolloDTO>> GetEquipoDesarrolloById(int id)
        {
            var resultado = await _service.GetEquipoDesarrollolById(id);
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }
        [HttpPost]
        public async Task<ActionResult<EquipoDesarrolloDTO>> AddEquipoDesarrollo([FromBody]EquipoDesarrolloDTO equipo)
        {
            var resultado = await _service.AddEquipoDesarrollo(equipo);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return CreatedAtAction(nameof(GetEquipoDesarrolloById), new { id = resultado.value.IdEquipoDesarrollo }, resultado.value);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEquipoDesarrollo(int id)
        {
            var resultado = await _service.DeleteEquipoDesarrollo(id);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEquipoDesarrollo(int id, [FromBody] EquipoDesarrolloDTO equipo)
        {
            var resultado = await _service.UpdateEquipoDesarrollo(id, equipo);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return NoContent();
        }

    }
}
