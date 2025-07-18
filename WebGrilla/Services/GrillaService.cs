using AutoMapper;
using WebGrilla.DTOs;
using WebGrilla.Models;
using WebGrilla.Repository;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public class GrillaService : IGrillaService
    {
        private readonly IRepository<Grilla> _repository;
        private readonly IMapper _mapper;
        public GrillaService(IRepository<Grilla> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;   
        }

        public async Task<Result<GrillaDTO>> AddGrilla(GrillaDTO grilla)
        {
            var encontrado = await _repository.GetByIdAsync(grilla.IdGrilla);
            if (encontrado != null)
            {
                return Result<GrillaDTO>.Failure("Error: El identificador de grilla ya existe");

            }
            var nuevaGrilla = _mapper.Map<Grilla>(grilla);
            var resultado = await _repository.AddAsync(nuevaGrilla);
            if(resultado != null) 
            {
                return Result<GrillaDTO>.Success(_mapper.Map<GrillaDTO>(resultado));
            }
            return Result<GrillaDTO>.Failure("Error: No se pudo registrar la nueva grilla");

        }

        public async Task<Result<GrillaDTO>> DeleteGrilla(int idGrilla)
        {
            var resultado = await _repository.DeleteAsync(idGrilla);
            if (!resultado)
            {
                return Result<GrillaDTO>.Failure("Error: No se pudo eliminar la grilla");
            }
            return Result<GrillaDTO>.Success(new GrillaDTO());
        }

        public async Task<IEnumerable<GrillaDTO>> GetAllGrilla()
        {
            var resultado = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GrillaDTO>>(resultado);
        }

        public async Task<GrillaDTO> GetGrillaById(int idGrilla)
        {
            var resultado = await _repository.GetByIdAsync(idGrilla);
            return _mapper.Map<GrillaDTO>(resultado);
        }

        public async Task<Result<GrillaDTO>> UpdateGrilla(int idGrilla, GrillaDTO grilla)
        {
            if (idGrilla != grilla.IdGrilla)
            {
                return Result<GrillaDTO>.Failure($"Error: Los identificadores de Grilla grilla son inconsistentes");
            }
            var grillaModificada = _mapper.Map<Grilla>(grilla);

            var resultado = await _repository.UpdateAsync(grillaModificada);

            if (resultado == null)
            {
                return Result<GrillaDTO>.Failure("Error: No se pudo actualizar la grilla");
            }
            return Result<GrillaDTO>.Success(_mapper.Map<GrillaDTO>(resultado));
        }
    }
}
