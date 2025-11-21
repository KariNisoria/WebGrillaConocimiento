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
            var created = await _repository.AddAsync(conocimiento);
            return _mapper.Map<ConocimientoRecursoDTO>(created);
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
            var conocimientos = await _repository.GetConocimientosPorEvaluacionYRecursoAsync(idEvaluacion, idRecurso);
            return _mapper.Map<List<ConocimientoRecursoDTO>>(conocimientos);
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
    }
}