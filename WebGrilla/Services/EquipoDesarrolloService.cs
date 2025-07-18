using AutoMapper;
using WebGrilla.DTOs;
using WebGrilla.Models;
using WebGrilla.Repository;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public class EquipoDesarrolloService : IEquipoDesarrolloService
    {
        private readonly IRepository<EquipoDesarrollo> _repository;
        private readonly IMapper _mapper;

        public EquipoDesarrolloService(IRepository<EquipoDesarrollo> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;   
        }

        public async Task<Result<EquipoDesarrolloDTO>> AddEquipoDesarrollo(EquipoDesarrolloDTO equipo)
        {
            var encontrado = await _repository.GetByIdAsync(equipo.IdEquipoDesarrollo);
            if (encontrado != null)
            {
                return Result<EquipoDesarrolloDTO>.Failure($"Error: El equipo de desarrollo {equipo.IdEquipoDesarrollo} ya existe");
            }
            var nuevoEquipo = _mapper.Map<EquipoDesarrollo>(equipo);
            var resultado = await  _repository.AddAsync(nuevoEquipo);
            if(resultado == null)
            {
                return Result<EquipoDesarrolloDTO>.Failure("Error: No se pudo realizar el registro del equipo de desarrollo");
            }
            return Result<EquipoDesarrolloDTO>.Success(_mapper.Map<EquipoDesarrolloDTO>(resultado));
        }

        public async Task<Result<EquipoDesarrolloDTO>> DeleteEquipoDesarrollo(int idEquipoDesarrollo)
        {
            var resultado = await _repository.DeleteAsync(idEquipoDesarrollo);
            if (!resultado)
            {
                return Result<EquipoDesarrolloDTO>.Failure($"Error: No se pudo eliminar el equipo: {idEquipoDesarrollo}");
            }
            else
            {
                return Result<EquipoDesarrolloDTO>.Success(new EquipoDesarrolloDTO());
            }
            
        }

        public async Task<IEnumerable<EquipoDesarrolloDTO>> GetAllEquipoDesarrollo()
        {
            var resultado = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<EquipoDesarrolloDTO>>(resultado);
        }

        public async Task<EquipoDesarrolloDTO> GetEquipoDesarrollolById(int idEquipoDesarrollo)
        {
            var resultado = await _repository.GetByIdAsync(idEquipoDesarrollo);
            return _mapper.Map<EquipoDesarrolloDTO>(resultado);
        }

        public async Task<Result<EquipoDesarrolloDTO>> UpdateEquipoDesarrollo(int idEquipoDesarrollo, EquipoDesarrolloDTO equipo)
        {
            if (equipo.IdEquipoDesarrollo != idEquipoDesarrollo)
            {
                return Result<EquipoDesarrolloDTO>.Failure("Error: Inconsistencia en los identificadores");
            }
            var equipoModificado = _mapper.Map<EquipoDesarrollo>(equipo);

            var resultado = await _repository.UpdateAsync(equipoModificado);
            if (resultado==null)
            {
                return Result<EquipoDesarrolloDTO>.Failure($"Error: No se pudo actualizar el equipo: {idEquipoDesarrollo}");
            }
            return Result<EquipoDesarrolloDTO>.Success(_mapper.Map<EquipoDesarrolloDTO>(resultado));

        }
    }
}
