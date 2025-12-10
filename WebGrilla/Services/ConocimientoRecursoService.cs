using AutoMapper;
using WebGrilla.DTOs;
using WebGrilla.Models;
using WebGrilla.Repository;

namespace WebGrilla.Services
{
    public interface IConocimientoRecursoService
    {
        Task<List<ConocimientoRecursoDTO>> GetAllAsync();
        Task<ConocimientoRecursoDTO?> GetByIdAsync(int id);
        Task<ConocimientoRecursoDTO> CreateAsync(ConocimientoRecursoDTO conocimientoDto);
        Task<ConocimientoRecursoDTO> UpdateAsync(int id, ConocimientoRecursoDTO conocimientoDto);
        Task<bool> DeleteAsync(int id);
        Task<List<ConocimientoRecursoDTO>> GetConocimientosPorEvaluacionYRecursoAsync(int idEvaluacion, int idRecurso);
        Task<List<ConocimientoRecursoDTO>> GetConocimientosPorGrillaYRecursoAsync(int idGrilla, int idRecurso);
        Task<decimal> GetPorcentajeCompletitudSubtemasAsync(int idGrillaTema);
        Task<List<ConocimientoRecursoDTO>> GuardarMultiplesAsync(List<ConocimientoRecursoDTO> conocimientosDto);
        Task<ConocimientoRecursoDTO> CrearOActualizarAsync(ConocimientoRecursoDTO conocimientoDto);
        Task<Dictionary<int, decimal>> GetCompletitudSubtemasPorGrillaAsync(int idGrilla);
    }

    public class ConocimientoRecursoService : IConocimientoRecursoService
    {
        private readonly IConocimientoRecursoRepository _repository;
        private readonly IMapper _mapper;

        public ConocimientoRecursoService(IConocimientoRecursoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ConocimientoRecursoDTO>> GetAllAsync()
        {
            var conocimientos = await _repository.GetAllAsync();
            return _mapper.Map<List<ConocimientoRecursoDTO>>(conocimientos);
        }

        public async Task<ConocimientoRecursoDTO?> GetByIdAsync(int id)
        {
            var conocimiento = await _repository.GetByIdAsync(id);
            return conocimiento != null ? _mapper.Map<ConocimientoRecursoDTO>(conocimiento) : null;
        }

        public async Task<ConocimientoRecursoDTO> CreateAsync(ConocimientoRecursoDTO conocimientoDto)
        {
            var conocimiento = _mapper.Map<ConocimientoRecurso>(conocimientoDto);
            conocimiento.IdConocimientoRecurso = 0; // Asegurar que sea nuevo

            var conocimientoCreado = await _repository.AddAsync(conocimiento);
            return _mapper.Map<ConocimientoRecursoDTO>(conocimientoCreado);
        }

        public async Task<ConocimientoRecursoDTO> UpdateAsync(int id, ConocimientoRecursoDTO conocimientoDto)
        {
            var conocimiento = _mapper.Map<ConocimientoRecurso>(conocimientoDto);
            conocimiento.IdConocimientoRecurso = id;
            var updated = await _repository.UpdateAsync(conocimiento);
            return _mapper.Map<ConocimientoRecursoDTO>(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<ConocimientoRecursoDTO>> GetConocimientosPorEvaluacionYRecursoAsync(int idEvaluacion, int idRecurso)
        {
            try
            {
                Console.WriteLine($"Service: Iniciando GetConocimientosPorEvaluacionYRecursoAsync con idEvaluacion={idEvaluacion}, idRecurso={idRecurso}");
                
                var conocimientos = await _repository.GetConocimientosPorEvaluacionYRecursoAsync(idEvaluacion, idRecurso);
                
                Console.WriteLine($"Service: Repository devolvió {conocimientos.Count} conocimientos");
                Console.WriteLine($"Service: Iniciando mapeo con AutoMapper...");
                
                var result = _mapper.Map<List<ConocimientoRecursoDTO>>(conocimientos);
                
                Console.WriteLine($"Service: Mapeo completado exitosamente, devolviendo {result.Count} DTOs");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Service Error: {ex.Message}");
                Console.WriteLine($"Service StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Service InnerException: {ex.InnerException.Message}");
                    Console.WriteLine($"Service InnerException StackTrace: {ex.InnerException.StackTrace}");
                }
                throw new Exception($"Error al obtener conocimientos por evaluación y recurso: {ex.Message}", ex);
            }
        }

        public async Task<List<ConocimientoRecursoDTO>> GetConocimientosPorGrillaYRecursoAsync(int idGrilla, int idRecurso)
        {
            var conocimientos = await _repository.GetConocimientosPorGrillaYRecursoAsync(idGrilla, idRecurso);
            return _mapper.Map<List<ConocimientoRecursoDTO>>(conocimientos);
        }

        public async Task<decimal> GetPorcentajeCompletitudSubtemasAsync(int idGrillaTema)
        {
            return await _repository.GetPorcentajeCompletitudSubtemasAsync(idGrillaTema);
        }

        public async Task<List<ConocimientoRecursoDTO>> GuardarMultiplesAsync(List<ConocimientoRecursoDTO> conocimientosDto)
        {
            var resultados = new List<ConocimientoRecursoDTO>();

            foreach (var conocimientoDto in conocimientosDto)
            {
                var resultado = await CrearOActualizarAsync(conocimientoDto);
                resultados.Add(resultado);
            }

            return resultados;
        }

        public async Task<ConocimientoRecursoDTO> CrearOActualizarAsync(ConocimientoRecursoDTO conocimientoDto)
        {
            // Buscar si ya existe un conocimiento para este recurso, evaluación y subtema
            var conocimientoExistente = await _repository.BuscarConocimientoAsync(
                conocimientoDto.IdEvaluacion,
                conocimientoDto.IdRecurso,
                conocimientoDto.IdSubtema);

            if (conocimientoExistente != null)
            {
                // Actualizar existente
                conocimientoExistente.ValorFuncional = conocimientoDto.ValorFuncional;
                conocimientoExistente.ValorTecnico = conocimientoDto.ValorTecnico;
                conocimientoExistente.ValorFuncionalVerif = conocimientoDto.ValorFuncionalVerif;
                conocimientoExistente.ValorTecnicoVerif = conocimientoDto.ValorTecnicoVerif;
                conocimientoExistente.IdGrilla = conocimientoDto.IdGrilla;

                var actualizado = await _repository.UpdateAsync(conocimientoExistente);
                return _mapper.Map<ConocimientoRecursoDTO>(actualizado);
            }
            else
            {
                // Crear nuevo
                var nuevoConocimiento = _mapper.Map<ConocimientoRecurso>(conocimientoDto);
                nuevoConocimiento.IdConocimientoRecurso = 0;

                var creado = await _repository.AddAsync(nuevoConocimiento);
                return _mapper.Map<ConocimientoRecursoDTO>(creado);
            }
        }

        public async Task<Dictionary<int, decimal>> GetCompletitudSubtemasPorGrillaAsync(int idGrilla)
        {
            return await _repository.GetCompletitudSubtemasPorGrillaAsync(idGrilla);
        }
    }
}