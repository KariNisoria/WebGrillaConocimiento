using AutoMapper;
using WebGrilla.DTOs;
using WebGrilla.Models;
using WebGrilla.Repository;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public class SubtemaService : ISubtemaService
    {
        private readonly ISubtemaRepository _subtemaRepository;
        private readonly IMapper _mapper;
        public SubtemaService(ISubtemaRepository subtemaRepository, IMapper mapper)
        {
            _subtemaRepository = subtemaRepository;
            _mapper = mapper;
        }
        public async Task<Result<SubtemaDTO>> AddSubtema(SubtemaDTO subtema)
        {
            var nuevoSubtema = _mapper.Map<Subtema>(subtema);
            var resultado = await _subtemaRepository.AddAsync(nuevoSubtema);
            return Result<SubtemaDTO>.Success(_mapper.Map<SubtemaDTO>(resultado));
        }

        public async Task<bool> DeleteSubtemaById(int id)
        {
            var resultado = await _subtemaRepository.DeleteAsync(id);
            return resultado;
        }

        public async Task<IEnumerable<SubtemaDTO>> GetAllSubtema()
        {
            var resultado = await _subtemaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SubtemaDTO>>(resultado);
        }

        public async Task<IEnumerable<SubtemaDTO>> GetSubtemaByIdTema(int id)
        {
            var resultado = await _subtemaRepository.GetSubtemaByIdTema(id);
            return _mapper.Map<IEnumerable<SubtemaDTO>>(resultado);

        }

        public async Task<SubtemaDTO> GetSubtemaById(int id)
        {
            var resultado = await _subtemaRepository.GetByIdAsync(id);
            return _mapper.Map<SubtemaDTO>(resultado);
        }

        public async Task<Result<SubtemaDTO>> UpdateSubtemaById(int id, SubtemaDTO subtema)
        {
            if (id != subtema.IdSubtema)
            {
                return Result<SubtemaDTO>.Failure("Error: Inconsistencia entre indentificadores.");
            }
            var subtemaModificado = _mapper.Map<Subtema>(subtema);
            var resultado = await _subtemaRepository.UpdateAsync(subtemaModificado);
            return Result<SubtemaDTO>.Success(_mapper.Map<SubtemaDTO>(resultado));
        }
    }
}
