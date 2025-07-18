using AutoMapper;
using WebGrilla.DTOs;
using WebGrilla.Models;
using WebGrilla.Repository;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public class RecursoService:IRecursoService
    {
        private readonly IRecursoRepository _repository;
        private readonly IMapper _mapper;

        public RecursoService(IRecursoRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;   
        }

        public async Task<Result<RecursoDTO>> AddRecurso(RecursoDTO recurso)
        {
            var encontrado = await _repository.GetByIdAsync(recurso.IdRecurso);
            if (encontrado != null)
            {
                return Result<RecursoDTO>.Failure("Error: El id del recurso ya se encuentra registrado");
            }
            encontrado = await _repository.GetRecursoByCorreoElectronico(recurso.CorreoElectronico);
            if (encontrado != null)
            {
                return Result<RecursoDTO>.Failure("Error: El correo electronico del recurso ya se encuentra registrado");
            }
            var nuevoRecurso = _mapper.Map<Recurso>(recurso);
            var resultado = await _repository.AddAsync(nuevoRecurso);

            if (resultado == null)
            {
                return Result<RecursoDTO>.Failure("Error: No se pudo registrar el recurso.");
            }

            return Result<RecursoDTO>.Success(_mapper.Map<RecursoDTO>(resultado));
        }


        public async Task<Result<RecursoDTO>> DeleteRecurso(int idRecurso)
        {
            var encontrado = await _repository.GetByIdAsync(idRecurso);
            if (encontrado != null)
            {
                var resultado = await _repository.DeleteAsync(idRecurso);
                return Result<RecursoDTO>.Success(new RecursoDTO());
            }
            else
            {
                return Result<RecursoDTO>.Failure($"Error: No se pudo eliminar el recurso {idRecurso}");
            }
        }

        public async Task<IEnumerable<RecursoDTO>> GetAllRecurso()
        {
            var resultado = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<RecursoDTO>>(resultado);
        }

        public async Task<RecursoDTO> GetRecursoByCorreoElectronico(string correoElectronico)
        {
            var resultado = await _repository.GetRecursoByCorreoElectronico(correoElectronico);
            return _mapper.Map<RecursoDTO>(resultado);
        }

        public async Task<RecursoDTO> GetRecursoById(int idRrecurso)
        {
            var resultado = await _repository.GetByIdAsync(idRrecurso);
            return _mapper.Map<RecursoDTO>(resultado);
        }

        public async Task<Result<RecursoDTO>> UpdateRecurso(int idRecurso, RecursoDTO recurso)
        {
            if (idRecurso != recurso.IdRecurso)
            {
                return Result<RecursoDTO>.Failure("Error: Los identificadores son inconsistentes");
            }
            var encontrado = await _repository.GetRecursoByCorreoElectronico(recurso.CorreoElectronico);
            if (encontrado != null && encontrado.IdRecurso != idRecurso)
            {
                return Result<RecursoDTO>.Failure("Error: El correo electronico ya se encuentra vinculado a otro usuario");
            }
            var recursoModificado = _mapper.Map<Recurso>(recurso);
            var resultado = await _repository.UpdateAsync(recursoModificado);
            if (resultado != null)
            {
                return Result<RecursoDTO>.Success(_mapper.Map<RecursoDTO>(resultado));
            }
            else
            {
                return Result<RecursoDTO>.Failure("Error: No se pudo realizar la modificacion del recurso");
            }
        }
    }
}
