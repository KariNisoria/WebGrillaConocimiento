using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs;
using WebGrilla.Services;

namespace WebGrilla.Controllers
{
    [Route("api/[controller]")]
    public class SubtemaController : Controller
    {
        private readonly ISubtemaService _subtemaService;
        public SubtemaController(ISubtemaService subtemaService)
        {
            _subtemaService = subtemaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubtemaDTO>>> GetAllSubtema()
        {
            var resultados = await _subtemaService.GetAllSubtema();
            return Ok(resultados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubtemaDTO>> GetSubtemaById(int id)
        {
            var resultado = await _subtemaService.GetSubtemaById(id);
            if (resultado == null)
            {
                return NotFound($"Error: No se encontró un tema con ID {id}.");
            }
            return Ok(resultado);
        }

        [HttpGet("ByIdTema/{id}")]
        public async Task<ActionResult<IEnumerable<SubtemaDTO>>> GetSubtemaByIdTema(int id)
        {
            var resultados = await _subtemaService.GetSubtemaByIdTema(id);
            return Ok(resultados);
        }

        [HttpPost]
        public async Task<ActionResult<SubtemaDTO>> AddSubtema([FromBody] SubtemaDTO subtemaDTO)
        {
           var resultado = await _subtemaService.AddSubtema(subtemaDTO);
            if (!resultado.isSuccess)
            {
                return BadRequest($"Error: No se pudo ingresar el registro: {resultado.message}");
            }
            return CreatedAtAction("GetSubtemaById", new { id = resultado.value.IdSubtema }, resultado.value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SubtemaDTO>> UpdateSubtema(int id, [FromBody] SubtemaDTO subtemaDTO)
        {
            Console.WriteLine($"id :{id}");
            Console.WriteLine($"subtema :{subtemaDTO.ToString()}");

            var resultado = await _subtemaService.UpdateSubtemaById(id, subtemaDTO);

            if (!resultado.isSuccess)
            {
                return BadRequest($"Error: No se pudo actualizar el registro: {resultado.message}");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SubtemaDTO>> DeleteSubtema (int id)
        {
            var resultado = await _subtemaService.DeleteSubtemaById(id);
            if (!resultado)
            {
                return BadRequest($"Error: No se pudo eliminar el registro");
            }
            return NoContent();
        }

    }
}
