using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs;
using WebGrilla.Services;

namespace WebGrilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrillaTemaController : ControllerBase
    {
        private readonly IGrillaTemaService _service;
        public GrillaTemaController(IGrillaTemaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrillaTemaDTO>>> GetAllGrillaTema()
        {
            var resultado = await _service.GetAllGrillaTema();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GrillaTemaDTO>> GetGrillaTemaById(int id)
        {
            var resultado = await _service.GetGrillaTemaById(id);
            if (resultado == null)
            {
                return NotFound($"Error: No se encontró un GrillaTema con ID {id}.");
            }
            return Ok(resultado);
        }

        [HttpGet("ByGrilla/{idGrilla}")]
        public async Task<ActionResult<IEnumerable<GrillaTemaDTO>>> GetGrillaTemasByGrilla(int idGrilla)
        {
            var resultado = await _service.GetGrillaTemasByGrilla(idGrilla);
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<GrillaTemaDTO>> AddGrillaTema([FromBody] GrillaTemaDTO grillaTema)
        {
            var resultado = await _service.AddGrillaTema(grillaTema);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return CreatedAtAction(nameof(GetGrillaTemaById), new { id = resultado.value.IdGrillaTema }, resultado.value);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGrillaTema(int id)
        {
            var resultado = await _service.DeleteGrillaTema(id);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGrillaTema(int id, [FromBody] GrillaTemaDTO grillaTema)
        {
            var resultado = await _service.UpdateGrillaTema(id, grillaTema);
            if (!resultado.isSuccess)
            {
                return BadRequest(resultado.message);
            }
            return NoContent();
        }
    }
}