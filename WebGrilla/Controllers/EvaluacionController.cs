using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs;
using WebGrilla.Services;

namespace WebGrilla.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluacionController : ControllerBase
    {
        private readonly IEvaluacionService _service;

        public EvaluacionController(IEvaluacionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluacionDTO>>> GetAll()
        {
            try
            {
                var evaluaciones = await _service.GetAllAsync();
                return Ok(evaluaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluacionDTO>> GetById(int id)
        {
            try
            {
                var evaluacion = await _service.GetByIdAsync(id);
                if (evaluacion == null)
                    return NotFound($"Evaluación con ID {id} no encontrada.");
                
                return Ok(evaluacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("activa")]
        public async Task<ActionResult<EvaluacionDTO>> GetEvaluacionActiva()
        {
            try
            {
                var evaluacion = await _service.GetEvaluacionActivaAsync();
                if (evaluacion == null)
                    return NotFound("No hay evaluación activa.");
                
                return Ok(evaluacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("activa/recurso/{idRecurso}")]
        public async Task<ActionResult<EvaluacionDTO>> GetEvaluacionActivaPorRecurso(int idRecurso)
        {
            try
            {
                var evaluacion = await _service.GetEvaluacionActivaPorRecursoAsync(idRecurso);
                if (evaluacion == null)
                    return NotFound($"No hay evaluación activa para el recurso {idRecurso}.");
                
                return Ok(evaluacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("por-recurso/{idRecurso}")]
        public async Task<ActionResult<IEnumerable<EvaluacionDTO>>> GetEvaluacionesPorRecurso(int idRecurso)
        {
            try
            {
                var evaluaciones = await _service.GetEvaluacionesPorRecursoAsync(idRecurso);
                return Ok(evaluaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EvaluacionDTO>> Create(EvaluacionDTO evaluacionDto)
        {
            try
            {
                var created = await _service.CreateAsync(evaluacionDto);
                return CreatedAtAction(nameof(GetById), new { id = created.IdEvaluacion }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("iniciar-para-recurso")]
        public async Task<ActionResult<EvaluacionDTO>> IniciarEvaluacionParaRecurso([FromBody] IniciarEvaluacionRequest request)
        {
            try
            {
                var evaluacion = await _service.IniciarEvaluacionParaRecursoAsync(
                    request.IdRecurso, 
                    request.IdGrilla, 
                    request.Descripcion);
                
                return CreatedAtAction(nameof(GetById), new { id = evaluacion.IdEvaluacion }, evaluacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EvaluacionDTO>> Update(int id, EvaluacionDTO evaluacionDto)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, evaluacionDto);
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
                    return NotFound($"Evaluación con ID {id} no encontrada.");
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }

    public class IniciarEvaluacionRequest
    {
        public int IdRecurso { get; set; }
        public int IdGrilla { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}