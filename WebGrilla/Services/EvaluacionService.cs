using AutoMapper;
using WebGrilla.DTOs;
using WebGrilla.Models;
using WebGrilla.Repository;

namespace WebGrilla.Services
{
    public interface IEvaluacionService
    {
        Task<List<EvaluacionDTO>> GetAllAsync();
        Task<EvaluacionDTO?> GetByIdAsync(int id);
        Task<EvaluacionDTO> CreateAsync(EvaluacionDTO evaluacionDto);
        Task<EvaluacionDTO> CreateAsync(EvaluacionDTO evaluacionDto, bool generarConocimientosAutomatico);
        Task<EvaluacionDTO> UpdateAsync(int id, EvaluacionDTO evaluacionDto);
        Task<bool> DeleteAsync(int id);
        Task<EvaluacionDTO?> GetEvaluacionActivaAsync();
        Task<EvaluacionDTO?> GetEvaluacionActivaPorRecursoAsync(int idRecurso);
        Task<List<EvaluacionDTO>> GetEvaluacionesPorRecursoAsync(int idRecurso);
        Task<EvaluacionDTO> IniciarEvaluacionParaRecursoAsync(int idRecurso, int idGrilla, string descripcion);
        Task<List<ConocimientoRecursoDTO>> GenerarConocimientosTemporalesAsync(int idEvaluacion, int idRecurso, int idGrilla);
        
        // Nuevos métodos para evaluación global
        Task<List<EvaluacionDTO>> CrearEvaluacionGlobalAsync(int idGrilla, string descripcion, DateTime fechaFin);
        Task<List<EvaluacionDTO>> GetEvaluacionesGlobalesAsync();
        Task<List<EvaluacionDTO>> GetEvaluacionesPorGrillaYPeriodoAsync(int idGrilla, DateTime fechaInicio, DateTime fechaFin);
        Task<EvaluacionGlobalResumenDTO> GetResumenEvaluacionGlobalAsync(int idGrilla, DateTime fechaInicio, DateTime fechaFin);
    }

    public class EvaluacionService : IEvaluacionService
    {
        private readonly IEvaluacionRepository _repository;
        private readonly IConocimientoRecursoRepository _conocimientoRepository;
        private readonly IGrillaSubtemaRepository _grillaSubtemaRepository;
        private readonly IRecursoRepository _recursoRepository;
        private readonly IMapper _mapper;

        public EvaluacionService(
            IEvaluacionRepository repository, 
            IConocimientoRecursoRepository conocimientoRepository,
            IGrillaSubtemaRepository grillaSubtemaRepository,
            IRecursoRepository recursoRepository,
            IMapper mapper)
        {
            _repository = repository;
            _conocimientoRepository = conocimientoRepository;
            _grillaSubtemaRepository = grillaSubtemaRepository;
            _recursoRepository = recursoRepository;
            _mapper = mapper;
        }

        public async Task<List<EvaluacionDTO>> GetAllAsync()
        {
            var evaluaciones = await _repository.GetAllAsync();
            return _mapper.Map<List<EvaluacionDTO>>(evaluaciones);
        }

        public async Task<EvaluacionDTO?> GetByIdAsync(int id)
        {
            var evaluacion = await _repository.GetByIdAsync(id);
            return evaluacion != null ? _mapper.Map<EvaluacionDTO>(evaluacion) : null;
        }

        public async Task<EvaluacionDTO> CreateAsync(EvaluacionDTO evaluacionDto)
        {
            // Por defecto, NO generar conocimientos automáticamente
            return await CreateAsync(evaluacionDto, false);
        }

        public async Task<EvaluacionDTO> CreateAsync(EvaluacionDTO evaluacionDto, bool generarConocimientosAutomatico)
        {
            var evaluacion = _mapper.Map<Evaluacion>(evaluacionDto);
            var created = await _repository.AddAsync(evaluacion);
            
            // Solo crear registros de conocimiento si se solicita explícitamente
            if (generarConocimientosAutomatico)
            {
                await CrearConocimientosParaEvaluacionAsync(created.IdEvaluacion, created.IdRecurso, created.IdGrilla);
            }
            
            return _mapper.Map<EvaluacionDTO>(created);
        }

        public async Task<EvaluacionDTO> UpdateAsync(int id, EvaluacionDTO evaluacionDto)
        {
            var evaluacion = _mapper.Map<Evaluacion>(evaluacionDto);
            evaluacion.IdEvaluacion = id;
            var updated = await _repository.UpdateAsync(evaluacion);
            return _mapper.Map<EvaluacionDTO>(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<EvaluacionDTO?> GetEvaluacionActivaAsync()
        {
            var evaluacion = await _repository.GetEvaluacionActivaAsync();
            return evaluacion != null ? _mapper.Map<EvaluacionDTO>(evaluacion) : null;
        }

        public async Task<List<EvaluacionDTO>> GetEvaluacionesPorRecursoAsync(int idRecurso)
        {
            var evaluaciones = await _repository.GetEvaluacionesPorRecursoAsync(idRecurso);
            return _mapper.Map<List<EvaluacionDTO>>(evaluaciones);
        }

        public async Task<EvaluacionDTO> IniciarEvaluacionParaRecursoAsync(int idRecurso, int idGrilla, string descripcion)
        {
            // Crear nueva evaluación específica para este recurso y grilla
            var fechaInicio = DateTime.Now;
            var fechaFin = fechaInicio.AddDays(10); // 10 días de tolerancia

            var evaluacionDto = new EvaluacionDTO
            {
                Descripcion = descripcion,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                IdRecurso = idRecurso,
                IdGrilla = idGrilla
            };

            // No generar conocimientos automáticamente al iniciar evaluación
            return await CreateAsync(evaluacionDto, false);
        }

        public async Task<EvaluacionDTO?> GetEvaluacionActivaPorRecursoAsync(int idRecurso)
        {
            var evaluacion = await _repository.GetEvaluacionActivaPorRecursoAsync(idRecurso);
            return evaluacion != null ? _mapper.Map<EvaluacionDTO>(evaluacion) : null;
        }

        /// <summary>
        /// Genera una lista temporal de ConocimientoRecurso para todos los subtemas de una grilla
        /// sin guardarlos en la base de datos
        /// </summary>
        public async Task<List<ConocimientoRecursoDTO>> GenerarConocimientosTemporalesAsync(int idEvaluacion, int idRecurso, int idGrilla)
        {
            // Obtener todos los subtemas de la grilla
            var grillaSubtemas = await _grillaSubtemaRepository.GetByGrillaAsync(idGrilla);

            var conocimientosTemporales = new List<ConocimientoRecursoDTO>();

            // Crear registros temporales para cada subtema
            foreach (var grillaSubtema in grillaSubtemas)
            {
                var conocimientoTemporal = new ConocimientoRecursoDTO
                {
                    IdConocimientoRecurso = 0, // 0 indica que no está guardado en BD
                    IdRecurso = idRecurso,
                    IdGrilla = idGrilla,
                    IdSubtema = grillaSubtema.IdSubtema,
                    IdEvaluacion = idEvaluacion,
                    ValorFuncional = 0,
                    ValorTecnico = 0,
                    ValorFuncionalVerif = null,
                    ValorTecnicoVerif = null,
                    // Información adicional para mostrar en la UI
                    NombreSubtema = grillaSubtema.Nombre
                };

                conocimientosTemporales.Add(conocimientoTemporal);
            }

            return conocimientosTemporales;
        }

        /// <summary>
        /// Crea evaluaciones automáticas para todos los recursos activos con la grilla especificada
        /// </summary>
        public async Task<List<EvaluacionDTO>> CrearEvaluacionGlobalAsync(int idGrilla, string descripcion, DateTime fechaFin)
        {
            var fechaInicio = DateTime.Now;
            var evaluacionesCreadas = new List<EvaluacionDTO>();

            // Obtener todos los recursos activos
            var recursos = await _recursoRepository.GetAllAsync();
            
            foreach (var recurso in recursos)
            {
                // Crear evaluación para cada recurso
                var evaluacionDto = new EvaluacionDTO
                {
                    Descripcion = $"{descripcion} - {recurso.Nombre} {recurso.Apellido}",
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    IdRecurso = recurso.IdRecurso,
                    IdGrilla = idGrilla
                };

                var evaluacionCreada = await CreateAsync(evaluacionDto, false);
                evaluacionesCreadas.Add(evaluacionCreada);
            }

            return evaluacionesCreadas;
        }

        /// <summary>
        /// Obtiene todas las evaluaciones que fueron creadas como parte de una evaluación global
        /// (evaluaciones con la misma grilla, descripción base y fechas similares)
        /// </summary>
        public async Task<List<EvaluacionDTO>> GetEvaluacionesGlobalesAsync()
        {
            var evaluaciones = await _repository.GetAllAsync();
            
            // Agrupar evaluaciones por características similares (misma fecha inicio y grilla)
            var evaluacionesGlobales = evaluaciones
                .GroupBy(e => new { e.IdGrilla, Fecha = e.FechaInicio.Date })
                .Where(g => g.Count() > 1) // Solo grupos con múltiples evaluaciones (evaluación global)
                .SelectMany(g => g)
                .ToList();

            return _mapper.Map<List<EvaluacionDTO>>(evaluacionesGlobales);
        }

        /// <summary>
        /// Obtiene evaluaciones por grilla y período específico
        /// </summary>
        public async Task<List<EvaluacionDTO>> GetEvaluacionesPorGrillaYPeriodoAsync(int idGrilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var evaluaciones = await _repository.GetAllAsync();
            
            var evaluacionesFiltradas = evaluaciones
                .Where(e => e.IdGrilla == idGrilla && 
                           e.FechaInicio.Date >= fechaInicio.Date && 
                           e.FechaInicio.Date <= fechaFin.Date)
                .ToList();

            return _mapper.Map<List<EvaluacionDTO>>(evaluacionesFiltradas);
        }

        /// <summary>
        /// Obtiene un resumen de la evaluación global con estadísticas
        /// </summary>
        public async Task<EvaluacionGlobalResumenDTO> GetResumenEvaluacionGlobalAsync(int idGrilla, DateTime fechaInicio, DateTime fechaFin)
        {
            var evaluaciones = await GetEvaluacionesPorGrillaYPeriodoAsync(idGrilla, fechaInicio, fechaFin);
            
            var resumen = new EvaluacionGlobalResumenDTO
            {
                IdGrilla = idGrilla,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                TotalEvaluaciones = evaluaciones.Count,
                EvaluacionesCompletadas = 0, // Se calculará basado en conocimientos guardados
                EvaluacionesPendientes = evaluaciones.Count,
                PorcentajeCompletitud = 0,
                Evaluaciones = evaluaciones
            };

            // Calcular completitud revisando conocimientos guardados
            foreach (var evaluacion in evaluaciones)
            {
                var conocimientos = await _conocimientoRepository.GetByEvaluacionAndRecursoAsync(evaluacion.IdEvaluacion, evaluacion.IdRecurso);
                if (conocimientos.Any())
                {
                    resumen.EvaluacionesCompletadas++;
                    resumen.EvaluacionesPendientes--;
                }
            }

            resumen.PorcentajeCompletitud = resumen.TotalEvaluaciones > 0 
                ? (decimal)resumen.EvaluacionesCompletadas / resumen.TotalEvaluaciones * 100 
                : 0;

            return resumen;
        }

        private async Task CrearConocimientosParaEvaluacionAsync(int idEvaluacion, int idRecurso, int idGrilla)
        {
            // Obtener todos los subtemas de la grilla
            var grillaSubtemas = await _grillaSubtemaRepository.GetByGrillaAsync(idGrilla);

            // Crear registros de conocimiento para cada subtema para este recurso específico
            foreach (var grillaSubtema in grillaSubtemas)
            {
                var conocimiento = new ConocimientoRecurso
                {
                    IdRecurso = idRecurso,
                    IdGrilla = idGrilla,
                    IdSubtema = grillaSubtema.IdSubtema,
                    IdEvaluacion = idEvaluacion,
                    ValorFuncional = 0,
                    ValorTecnico = 0,
                    ValorFuncionalVerif = null,
                    ValorTecnicoVerif = null
                };

                await _conocimientoRepository.AddAsync(conocimiento);
            }
        }
    }
}