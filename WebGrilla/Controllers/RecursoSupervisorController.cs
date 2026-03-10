using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs;
using WebGrilla.Services;

namespace WebGrilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecursoSupervisorController : ControllerBase
    {
        private readonly IRecursoSupervisorService _service;

        public RecursoSupervisorController(IRecursoSupervisorService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtener todas las relaciones supervisor-supervisado
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecursoSupervisorDTO>>> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener las relaciones: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtener todos los recursos disponibles para supervisión
        /// </summary>
        [HttpGet("recursos-disponibles")]
        public async Task<ActionResult<IEnumerable<RecursoSimpleDTO>>> GetRecursosDisponibles()
        {
            try
            {
                var result = await _service.GetRecursosDisponiblesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener recursos disponibles: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtener la vista de supervisión para un supervisor específico
        /// </summary>
        [HttpGet("supervision/{idSupervisor}")]
        public async Task<ActionResult<SupervisionViewDTO>> GetSupervisionView(int idSupervisor)
        {
            try
            {
                var result = await _service.GetSupervisionViewAsync(idSupervisor);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener vista de supervisión: {ex.Message}");
            }
        }

        /// <summary>
        /// Asignar un supervisado a un supervisor
        /// </summary>
        [HttpPost("asignar")]
        public async Task<ActionResult> AsignarSupervisado([FromBody] AsignarSupervisadoRequest request)
        {
            try
            {
                if (request.IdSupervisor == request.IdSupervisado)
                {
                    return BadRequest("Un recurso no puede supervisarse a sí mismo");
                }

                var resultado = await _service.AsignarSupervisadoAsync(
                    request.IdSupervisor, 
                    request.IdSupervisado, 
                    request.Observaciones);

                if (resultado)
                {
                    return Ok(new { mensaje = "Supervisado asignado correctamente" });
                }
                else
                {
                    return BadRequest("No se pudo asignar el supervisado. Verifique que no exista ya la relación.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al asignar supervisado: {ex.Message}");
            }
        }

        /// <summary>
        /// Desasignar un supervisado de un supervisor
        /// </summary>
        [HttpDelete("desasignar/{idSupervisor}/{idSupervisado}")]
        public async Task<ActionResult> DesasignarSupervisado(int idSupervisor, int idSupervisado)
        {
            try
            {
                var resultado = await _service.DesasignarSupervisadoAsync(idSupervisor, idSupervisado);

                if (resultado)
                {
                    return Ok(new { mensaje = "Supervisado desasignado correctamente" });
                }
                else
                {
                    return BadRequest("No se pudo desasignar el supervisado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al desasignar supervisado: {ex.Message}");
            }
        }

        /// <summary>
        /// Verificar si existe una relación supervisor-supervisado
        /// </summary>
        [HttpGet("existe-relacion/{idSupervisor}/{idSupervisado}")]
        public async Task<ActionResult<bool>> ExisteRelacion(int idSupervisor, int idSupervisado)
        {
            try
            {
                var resultado = await _service.ExisteRelacionAsync(idSupervisor, idSupervisado);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al verificar relación: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// DTO para la solicitud de asignación de supervisado
    /// </summary>
    public class AsignarSupervisadoRequest
    {
        public int IdSupervisor { get; set; }
        public int IdSupervisado { get; set; }
        public string? Observaciones { get; set; }
    }
}