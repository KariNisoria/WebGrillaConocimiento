using AutoMapper;
using WebGrilla.DTOs;
using WebGrilla.Models;
using WebGrilla.Repository;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public class TemaService : ITemaService
    {
        private readonly IRepository<Tema> _temaRepository;
        private readonly ISubtemaRepository _subtemaRepository;
        private readonly IMapper _mapper;

        public TemaService(IRepository<Tema> temaRepository, ISubtemaRepository subtemaRepository, IMapper mapper)
        {
            _temaRepository = temaRepository;
            _subtemaRepository = subtemaRepository;
            _mapper = mapper;
        }

        public async Task<Result<TemaDTO>> AddTema(TemaDTO temaDTO)
        {
            var nuevo = _mapper.Map<Tema>(temaDTO);
            var resultado = await _temaRepository.AddAsync(nuevo);
            return Result<TemaDTO>.Success(_mapper.Map<TemaDTO>(resultado));
        }

        public async Task<Result<TemaDTO>> DeleteTemaById(int id)
        {

            var encontrado = await _subtemaRepository.HasSutemaByIdTema(id);

            if (encontrado)
            {
                return Result<TemaDTO>.Failure("No se puede eliminar, porque tiene subtema asociados");
            }

            var borrado = await _temaRepository.DeleteAsync(id);

            if (!borrado)
            {
                return Result<TemaDTO>.Failure("No se pudo eliminar el registro");
            }

            return Result<TemaDTO>.Success(new TemaDTO());

        }

        public async Task<IEnumerable<TemaDTO>> GetAllTema()
        {
            var resultado = await _temaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TemaDTO>>(resultado);
        }

        public async Task<TemaDTO> GetTemaById(int id)
        {
            var resultado = await _temaRepository.GetByIdAsync(id);
            return _mapper.Map<TemaDTO>(resultado);
        }

        public async Task<Result<TemaDTO>> UpdateTemaById(int id, TemaDTO temaDTO)
        {
            if(id != temaDTO.IdTema) 
            {
                return Result<TemaDTO>.Failure("Error: Inconsistencia en el id");
            }

            var modificado = _mapper.Map<Tema>(temaDTO);
            var resultado = await _temaRepository.UpdateAsync(modificado);
            return Result<TemaDTO>.Success(_mapper.Map<TemaDTO>(resultado));

        }
    }

}
