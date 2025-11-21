using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs;
using WebGrilla.Services;

namespace WebGrilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrillaSubtemaController : ControllerBase
    {
        private readonly IGrillaSubtemaService _service;
        public GrillaSubtemaController(IGrillaSubtemaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrillaSubtemaDTO>>> GetAllGrillaSubtema()
        {
            var resultado = await _service.GetAllGrillaSubtema();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GrillaSubtemaDTO>> GetGrillaSubtemaById(int id)
        {
            var resultado = await _service.GetGrillaSubtemaById(id);
            if (resultado == null)
            {
                return NotFound($"Error: No se encontró un GrillaSubtema con ID {id}.");
            }
            return Ok(resultado);
        }

        [HttpGet("ByGrillaTema/{idGrillaTema}")]
        public async Task<ActionResult<IEnumerable<GrillaSubtemaDTO>>> GetGrillaSubtemasByGrillaTema(int idGrillaTema)
        {
            var resultado = await _service.GetGrillaSubtemasByGrillaTema(idGrillaTema);
            return Ok(resultado);
        }

        [HttpGet("ByGrilla/{idGrilla}")]
        public async Task<ActionResult<IEnumerable<GrillaSubtemaDTO>>> GetGrillaSubtemasByGrilla(int idGrilla)
        {
            var resultado = await _service.GetGrillaSubtemasByGrilla(idGrilla);
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<GrillaSubtemaDTO>> AddGrillaSubtema([FromBody] GrillaSubtemaDTO grillaSubtema)
        {
            var resultado = await _service.AddGrillaSubtema(grillaSubtema);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return CreatedAtAction(nameof(GetGrillaSubtemaById), new { id = resultado.value.IdGrillaSubtema }, resultado.value);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGrillaSubtema(int id)
        {
            var resultado = await _service.DeleteGrillaSubtema(id);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGrillaSubtema(int id, [FromBody] GrillaSubtemaDTO grillaSubtema)
        {
            var resultado = await _service.UpdateGrillaSubtema(id, grillaSubtema);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return NoContent();
        }
    }
}