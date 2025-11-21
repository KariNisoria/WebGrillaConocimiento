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
        Task<EvaluacionDTO> UpdateAsync(int id, EvaluacionDTO evaluacionDto);
        Task<bool> DeleteAsync(int id);
        Task<EvaluacionDTO?> GetEvaluacionActivaAsync();
        Task<EvaluacionDTO?> GetEvaluacionActivaPorRecursoAsync(int idRecurso);
        Task<List<EvaluacionDTO>> GetEvaluacionesPorRecursoAsync(int idRecurso);
        Task<EvaluacionDTO> IniciarEvaluacionParaRecursoAsync(int idRecurso, int idGrilla, string descripcion);
    }

    public class EvaluacionService : IEvaluacionService
    {
        private readonly IEvaluacionRepository _repository;
        private readonly IConocimientoRecursoRepository _conocimientoRepository;
        private readonly IGrillaSubtemaRepository _grillaSubtemaRepository;
        private readonly IMapper _mapper;

        public EvaluacionService(
            IEvaluacionRepository repository, 
            IConocimientoRecursoRepository conocimientoRepository,
            IGrillaSubtemaRepository grillaSubtemaRepository,
            IMapper mapper)
        {
            _repository = repository;
            _conocimientoRepository = conocimientoRepository;
            _grillaSubtemaRepository = grillaSubtemaRepository;
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
            var evaluacion = _mapper.Map<Evaluacion>(evaluacionDto);
            var created = await _repository.AddAsync(evaluacion);
            
            // Crear registros de conocimiento para todos los subtemas de la grilla para este recurso específico
            await CrearConocimientosParaEvaluacionAsync(created.IdEvaluacion, created.IdRecurso, created.IdGrilla);
            
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

            return await CreateAsync(evaluacionDto);
        }

        public async Task<EvaluacionDTO?> GetEvaluacionActivaPorRecursoAsync(int idRecurso)
        {
            var evaluacion = await _repository.GetEvaluacionActivaPorRecursoAsync(idRecurso);
            return evaluacion != null ? _mapper.Map<EvaluacionDTO>(evaluacion) : null;
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