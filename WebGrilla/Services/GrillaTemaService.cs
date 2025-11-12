using WebGrilla.DTOs;
using WebGrilla.Models;
using WebGrilla.Repository;
using WebGrilla.Utils;
using AutoMapper;

namespace WebGrilla.Services
{
    public class GrillaTemaService : IGrillaTemaService
    {
        private readonly IRepository<GrillaTema> _repository;
        private readonly IMapper _mapper;

        public GrillaTemaService(IRepository<GrillaTema> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GrillaTemaDTO>> GetAllGrillaTema()
        {
            var grillaTemas = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GrillaTemaDTO>>(grillaTemas);
        }

        public async Task<GrillaTemaDTO?> GetGrillaTemaById(int idGrillaTema)
        {
            var grillaTema = await _repository.GetByIdAsync(idGrillaTema);
            return grillaTema != null ? _mapper.Map<GrillaTemaDTO>(grillaTema) : null;
        }

        public async Task<IEnumerable<GrillaTemaDTO>> GetGrillaTemasByGrilla(int idGrilla)
        {
            var allGrillaTemas = await _repository.GetAllAsync();
            var filteredGrillaTemas = allGrillaTemas.Where(gt => gt.IdGrilla == idGrilla);
            return _mapper.Map<IEnumerable<GrillaTemaDTO>>(filteredGrillaTemas);
        }

        public async Task<Result<GrillaTemaDTO>> AddGrillaTema(GrillaTemaDTO grillaTemaDTO)
        {
            try
            {
                var grillaTema = _mapper.Map<GrillaTema>(grillaTemaDTO);
                var addedGrillaTema = await _repository.AddAsync(grillaTema);

                if (addedGrillaTema != null)
                {
                    var result = _mapper.Map<GrillaTemaDTO>(addedGrillaTema);
                    return Result<GrillaTemaDTO>.Success(result);
                }

                return Result<GrillaTemaDTO>.Failure("No se pudo agregar el GrillaTema");
            }
            catch (Exception ex)
            {
                return Result<GrillaTemaDTO>.Failure($"Error al agregar GrillaTema: {ex.Message}");
            }
        }

        public async Task<Result<GrillaTemaDTO>> DeleteGrillaTema(int idGrillaTema)
        {
            try
            {
                var success = await _repository.DeleteAsync(idGrillaTema);
                
                if (success)
                {
                    return Result<GrillaTemaDTO>.Success(null);
                }

                return Result<GrillaTemaDTO>.Failure("No se pudo eliminar el GrillaTema");
            }
            catch (Exception ex)
            {
                return Result<GrillaTemaDTO>.Failure($"Error al eliminar GrillaTema: {ex.Message}");
            }
        }

        public async Task<Result<GrillaTemaDTO>> UpdateGrillaTema(int idGrillaTema, GrillaTemaDTO grillaTemaDTO)
        {
            try
            {
                grillaTemaDTO.IdGrillaTema = idGrillaTema; // Asegurar que el ID sea correcto
                var grillaTema = _mapper.Map<GrillaTema>(grillaTemaDTO);
                var updatedGrillaTema = await _repository.UpdateAsync(grillaTema);

                if (updatedGrillaTema != null)
                {
                    var result = _mapper.Map<GrillaTemaDTO>(updatedGrillaTema);
                    return Result<GrillaTemaDTO>.Success(result);
                }

                return Result<GrillaTemaDTO>.Failure("No se pudo actualizar el GrillaTema");
            }
            catch (Exception ex)
            {
                return Result<GrillaTemaDTO>.Failure($"Error al actualizar GrillaTema: {ex.Message}");
            }
        }
    }
}