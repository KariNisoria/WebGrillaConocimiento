using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs;
using WebGrilla.Services;

namespace WebGrilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemaController : ControllerBase
    {
        private readonly ITemaService _temaService;

        public TemaController(ITemaService temaService)
        {
            _temaService = temaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemaDTO>>> GetAllTema()
        {
            var resultado = await _temaService.GetAllTema();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TemaDTO>> GetTemaById(int id)
        {
            var resultado = await _temaService.GetTemaById(id);
            if (resultado == null)
            {
                return NotFound($"Error: No se encontró un tema con ID {id}.");
            }
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<TemaDTO>> AddTema([FromBody] TemaDTO tema)
        {
            if (tema == null)
            {
                return BadRequest("Error: El tema no puede ser nulo.");
            }

            var resultado = await _temaService.AddTema(tema);
            if (!resultado.isSuccess)
            {
                return BadRequest($"Error: No se pudo realizar el registro: {resultado.message}");
            }

            return CreatedAtAction(nameof(GetTemaById), new { id = resultado.value.IdTema }, resultado.value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTema(int id, [FromBody] TemaDTO tema)
        {
            if (tema == null)
            {
                return BadRequest("Error: El tema no puede ser nulo.");
            }

            var resultado = await _temaService.UpdateTemaById(id, tema);
            if (!resultado.isSuccess)
            {
                return BadRequest($"Error: No se pudo actualizar el registro: {resultado.message}");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemaById(int id)
        {
            var resultado = await _temaService.DeleteTemaById(id);
            if (resultado.isSuccess)
            {
                return NoContent();
                
            }
            return BadRequest($"Error: {resultado.message}.");

        }
    }
}
