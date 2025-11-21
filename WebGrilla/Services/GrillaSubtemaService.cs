using WebGrilla.DTOs;
using WebGrilla.Models;
using WebGrilla.Repository;
using WebGrilla.Utils;
using AutoMapper;

namespace WebGrilla.Services
{
    public class GrillaSubtemaService : IGrillaSubtemaService
    {
        private readonly IRepository<GrillaSubtema> _repository;
        private readonly IMapper _mapper;

        public GrillaSubtemaService(IRepository<GrillaSubtema> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GrillaSubtemaDTO>> GetAllGrillaSubtema()
        {
            var grillaSubtemas = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GrillaSubtemaDTO>>(grillaSubtemas);
        }

        public async Task<GrillaSubtemaDTO?> GetGrillaSubtemaById(int idGrillaSubtema)
        {
            var grillaSubtema = await _repository.GetByIdAsync(idGrillaSubtema);
            return grillaSubtema != null ? _mapper.Map<GrillaSubtemaDTO>(grillaSubtema) : null;
        }

        public async Task<IEnumerable<GrillaSubtemaDTO>> GetGrillaSubtemasByGrillaTema(int idGrillaTema)
        {
            var allGrillaSubtemas = await _repository.GetAllAsync();
            var filteredGrillaSubtemas = allGrillaSubtemas.Where(gs => gs.IdGrillaTema == idGrillaTema);
            return _mapper.Map<IEnumerable<GrillaSubtemaDTO>>(filteredGrillaSubtemas);
        }

        public async Task<IEnumerable<GrillaSubtemaDTO>> GetGrillaSubtemasByGrilla(int idGrilla)
        {
            var allGrillaSubtemas = await _repository.GetAllAsync();
            var filteredGrillaSubtemas = allGrillaSubtemas
                .Where(gs => gs.GrillaTema != null && gs.GrillaTema.IdGrilla == idGrilla);
            return _mapper.Map<IEnumerable<GrillaSubtemaDTO>>(filteredGrillaSubtemas);
        }

        public async Task<Result<GrillaSubtemaDTO>> AddGrillaSubtema(GrillaSubtemaDTO grillaSubtemaDTO)
        {
            try
            {
                var grillaSubtema = _mapper.Map<GrillaSubtema>(grillaSubtemaDTO);
                var addedGrillaSubtema = await _repository.AddAsync(grillaSubtema);

                if (addedGrillaSubtema != null)
                {
                    var result = _mapper.Map<GrillaSubtemaDTO>(addedGrillaSubtema);
                    return Result<GrillaSubtemaDTO>.Success(result);
                }

                return Result<GrillaSubtemaDTO>.Failure("No se pudo agregar el GrillaSubtema");
            }
            catch (Exception ex)
            {
                return Result<GrillaSubtemaDTO>.Failure($"Error al agregar GrillaSubtema: {ex.Message}");
            }
        }

        public async Task<Result<GrillaSubtemaDTO>> DeleteGrillaSubtema(int idGrillaSubtema)
        {
            try
            {
                var success = await _repository.DeleteAsync(idGrillaSubtema);
                
                if (success)
                {
                    return Result<GrillaSubtemaDTO>.Success(null);
                }

                return Result<GrillaSubtemaDTO>.Failure("No se pudo eliminar el GrillaSubtema");
            }
            catch (Exception ex)
            {
                return Result<GrillaSubtemaDTO>.Failure($"Error al eliminar GrillaSubtema: {ex.Message}");
            }
        }

        public async Task<Result<GrillaSubtemaDTO>> UpdateGrillaSubtema(int idGrillaSubtema, GrillaSubtemaDTO grillaSubtemaDTO)
        {
            try
            {
                grillaSubtemaDTO.IdGrillaSubtema = idGrillaSubtema; // Asegurar que el ID sea correcto
                var grillaSubtema = _mapper.Map<GrillaSubtema>(grillaSubtemaDTO);
                var updatedGrillaSubtema = await _repository.UpdateAsync(grillaSubtema);

                if (updatedGrillaSubtema != null)
                {
                    var result = _mapper.Map<GrillaSubtemaDTO>(updatedGrillaSubtema);
                    return Result<GrillaSubtemaDTO>.Success(result);
                }

                return Result<GrillaSubtemaDTO>.Failure("No se pudo actualizar el GrillaSubtema");
            }
            catch (Exception ex)
            {
                return Result<GrillaSubtemaDTO>.Failure($"Error al actualizar GrillaSubtema: {ex.Message}");
            }
        }
    }
}