using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebGrilla.DTOs;
using WebGrilla.Services;

namespace WebGrilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConocimientoRecursoController : ControllerBase
    {
        private readonly IConocimientoRecursoService _conocimientoService;
        private readonly IMapper _mapper;

        public ConocimientoRecursoController(IConocimientoRecursoService conocimientoService, IMapper mapper)
        {
            _conocimientoService = conocimientoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConocimientoRecursoDTO>>> GetAll()
        {
            try
            {
                var conocimientos = await _conocimientoService.GetAllAsync();
                return Ok(conocimientos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener conocimientos: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConocimientoRecursoDTO>> GetById(int id)
        {
            try
            {
                var conocimiento = await _conocimientoService.GetByIdAsync(id);
                if (conocimiento == null)
                    return NotFound($"Conocimiento con ID {id} no encontrado");

                return Ok(conocimiento);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener conocimiento: {ex.Message}");
            }
        }

        [HttpGet("evaluacion/{idEvaluacion}/recurso/{idRecurso}")]
        public async Task<ActionResult<IEnumerable<ConocimientoRecursoDTO>>> GetConocimientosPorEvaluacionYRecurso(int idEvaluacion, int idRecurso)
        {
            try
            {
                Console.WriteLine($"Controller: Recibida petición para evaluación {idEvaluacion} y recurso {idRecurso}");
                
                // Verificación de parámetros
                if (idEvaluacion <= 0 || idRecurso <= 0)
                {
                    Console.WriteLine($"Controller: Parámetros inválidos - idEvaluacion: {idEvaluacion}, idRecurso: {idRecurso}");
                    return BadRequest("Los parámetros idEvaluacion e idRecurso deben ser números positivos");
                }
                
                var conocimientos = await _conocimientoService.GetConocimientosPorEvaluacionYRecursoAsync(idEvaluacion, idRecurso);
                
                Console.WriteLine($"Controller: Servicio devolvió {conocimientos.Count} conocimientos");
                return Ok(conocimientos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller Error: {ex.Message}");
                Console.WriteLine($"Controller StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Controller InnerException: {ex.InnerException.Message}");
                    Console.WriteLine($"Controller InnerException StackTrace: {ex.InnerException.StackTrace}");
                }
                return BadRequest($"Error al obtener conocimientos por evaluación y recurso: {ex.Message}");
            }
        }

        [HttpGet("grilla/{idGrilla}/recurso/{idRecurso}")]
        public async Task<ActionResult<IEnumerable<ConocimientoRecursoDTO>>> GetConocimientosPorGrillaYRecurso(int idGrilla, int idRecurso)
        {
            try
            {
                var conocimientos = await _conocimientoService.GetConocimientosPorGrillaYRecursoAsync(idGrilla, idRecurso);
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
                var porcentaje = await _conocimientoService.GetPorcentajeCompletitudSubtemasAsync(idGrillaTema);
                return Ok(porcentaje);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ConocimientoRecursoDTO>> Create([FromBody] ConocimientoRecursoDTO conocimientoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var conocimientoCreado = await _conocimientoService.CreateAsync(conocimientoDto);
                return CreatedAtAction(nameof(GetById), new { id = conocimientoCreado.IdConocimientoRecurso }, conocimientoCreado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear conocimiento: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ConocimientoRecursoDTO>> Update(int id, [FromBody] ConocimientoRecursoDTO conocimientoDto)
        {
            try
            {
                if (id != conocimientoDto.IdConocimientoRecurso)
                    return BadRequest("El ID no coincide");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var conocimientoActualizado = await _conocimientoService.UpdateAsync(id, conocimientoDto);
                if (conocimientoActualizado == null)
                    return NotFound($"Conocimiento con ID {id} no encontrado");

                return Ok(conocimientoActualizado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar conocimiento: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _conocimientoService.DeleteAsync(id);
                if (!resultado)
                    return NotFound($"Conocimiento con ID {id} no encontrado");

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar conocimiento: {ex.Message}");
            }
        }

        [HttpPost("crear-o-actualizar")]
        public async Task<ActionResult<ConocimientoRecursoDTO>> CrearOActualizar([FromBody] ConocimientoRecursoDTO conocimientoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var conocimientoResultado = await _conocimientoService.CrearOActualizarAsync(conocimientoDto);
                return Ok(conocimientoResultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear o actualizar conocimiento: {ex.Message}");
            }
        }

        [HttpPost("guardar-multiples")]
        public async Task<ActionResult<IEnumerable<ConocimientoRecursoDTO>>> GuardarMultiples([FromBody] List<ConocimientoRecursoDTO> conocimientosDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var conocimientosResultado = await _conocimientoService.GuardarMultiplesAsync(conocimientosDto);
                return Ok(conocimientosResultado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al guardar múltiples conocimientos: {ex.Message}");
            }
        }

        [HttpGet("test")]
        public async Task<ActionResult> TestEndpoint()
        {
            try
            {
                Console.WriteLine("Test endpoint llamado");
                return Ok(new { message = "ConocimientoRecurso controller is working", timestamp = DateTime.Now });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test endpoint error: {ex.Message}");
                return BadRequest($"Test error: {ex.Message}");
            }
        }

        [HttpGet("test-db")]
        public async Task<ActionResult> TestDatabase()
        {
            try
            {
                Console.WriteLine("Test database endpoint llamado");
                var count = await _conocimientoService.GetAllAsync();
                return Ok(new { 
                    message = "Database connection working", 
                    totalConocimientos = count.Count,
                    timestamp = DateTime.Now 
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test database error: {ex.Message}");
                return BadRequest($"Database test error: {ex.Message}");
            }
        }

        [HttpGet("completitud-grilla/{idGrilla}")]
        public async Task<ActionResult<Dictionary<int, decimal>>> GetCompletitudSubtemasPorGrilla(int idGrilla)
        {
            try
            {
                Console.WriteLine($"Controller: Obteniendo completitud para grilla {idGrilla}");
                
                var completitud = await _conocimientoService.GetCompletitudSubtemasPorGrillaAsync(idGrilla);
                
                Console.WriteLine($"Controller: Completitud obtenida para {completitud.Count} temas");
                return Ok(completitud);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Controller Error en GetCompletitudSubtemasPorGrilla: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}