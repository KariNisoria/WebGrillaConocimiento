using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs;
using WebGrilla.Services;

namespace WebGrilla.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConocimientoRecursoController : ControllerBase
    {
        private readonly IConocimientoRecursoService _service;

        public ConocimientoRecursoController(IConocimientoRecursoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConocimientoRecursoDTO>>> GetAll()
        {
            try
            {
                var conocimientos = await _service.GetAllAsync();
                return Ok(conocimientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConocimientoRecursoDTO>> GetById(int id)
        {
            try
            {
                var conocimiento = await _service.GetByIdAsync(id);
                if (conocimiento == null)
                    return NotFound($"Conocimiento con ID {id} no encontrado.");
                
                return Ok(conocimiento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("evaluacion/{idEvaluacion}/recurso/{idRecurso}")]
        public async Task<ActionResult<IEnumerable<ConocimientoRecursoDTO>>> GetConocimientosPorEvaluacionYRecurso(int idEvaluacion, int idRecurso)
        {
            try
            {
                var conocimientos = await _service.GetConocimientosPorEvaluacionYRecursoAsync(idEvaluacion, idRecurso);
                return Ok(conocimientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("grilla/{idGrilla}/recurso/{idRecurso}")]
        public async Task<ActionResult<IEnumerable<ConocimientoRecursoDTO>>> GetConocimientosPorGrillaYRecurso(int idGrilla, int idRecurso)
        {
            try
            {
                var conocimientos = await _service.GetConocimientosPorGrillaYRecursoAsync(idGrilla, idRecurso);
                return Ok(conocimientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("completitud-subtemas/{idGrillaTema}")]
        public async Task<ActionResult<decimal>> GetPorcentajeCompletitudSubtemas(int idGrillaTema)
        {
            try
            {
                var porcentaje = await _service.GetPorcentajeCompletitudSubtemasAsync(idGrillaTema);
                return Ok(porcentaje);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ConocimientoRecursoDTO>> Create(ConocimientoRecursoDTO conocimientoDto)
        {
            try
            {
                var created = await _service.CreateAsync(conocimientoDto);
                return CreatedAtAction(nameof(GetById), new { id = created.IdConocimientoRecurso }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ConocimientoRecursoDTO>> Update(int id, ConocimientoRecursoDTO conocimientoDto)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, conocimientoDto);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                    return NotFound($"Conocimiento con ID {id} no encontrado.");
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}