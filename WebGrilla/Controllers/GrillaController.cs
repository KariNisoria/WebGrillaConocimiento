using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs;
using WebGrilla.Services;

namespace WebGrilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrillaController : ControllerBase
    {
        private readonly IGrillaService _service;
        public GrillaController(IGrillaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrillaDTO>>> GetAllGrilla()
        {
            var resultado = await _service.GetAllGrilla();
            return Ok(resultado);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GrillaDTO>> GetGrillaById(int id)
        {
            var resultado = await _service.GetGrillaById(id);
            if (resultado == null)
            {
                return NotFound($"Error: No se encontró una grilla con ID {id}.");
            }
            return Ok(resultado);
        }
        [HttpPost]
        public async Task<ActionResult<GrillaDTO>> AddGrilla([FromBody] GrillaDTO grilla)
        {
            var resultado = await _service.AddGrilla(grilla);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return CreatedAtAction(nameof(GetGrillaById), new { id = resultado.value.IdGrilla }, resultado.value);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGrilla(int id)
        {
            var resultado = await _service.DeleteGrilla(id);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGrilla(int id, [FromBody] GrillaDTO grilla)
        {
            var resultado = await _service.UpdateGrilla(id, grilla);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return NoContent();
        } 

    }
}
