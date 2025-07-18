using AutoMapper;
using WebGrilla.DTOs;
using WebGrilla.Models;
using WebGrilla.Repository;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public class RolService : IRolService
    {
        private readonly IRepository<Rol> _repository;
        private readonly IMapper _mapper;
        public RolService(IRepository<Rol> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Result<RolDTO>> AddRol(RolDTO rol)
        {
            var encontrado = await _repository.GetByIdAsync(rol.IdRol);
            if(encontrado != null) 
            {
                return Result<RolDTO>.Failure($"Error: El rol ingresado ya existe con el identificador {rol.IdRol}");
            }
            var nuevoRol = _mapper.Map<Rol>(rol);

            var resultado = await _repository.AddAsync(nuevoRol);

            if(resultado != null)
            {
                return Result<RolDTO>.Success(_mapper.Map<RolDTO>(resultado));
            }
            else
            {
                return Result<RolDTO>.Failure("Error: No se pudo registrar el nuevo rol");
            }
            
        }

        public async Task<Result<RolDTO>> DeleteRol(int idRol)
        {
            var resultado = await _repository.DeleteAsync(idRol);
            if (resultado)
            {
                return Result<RolDTO>.Success(new RolDTO());
            }
            else
            {
                return Result<RolDTO>.Failure($"Error: No se pudo eliminar el rol {idRol}");
            }
        }

        public async Task<IEnumerable<RolDTO>> GetAllRol()
        {
            var resultado = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<RolDTO>>(resultado);
        }

        public async Task<RolDTO> GetRolById(int idRol)
        {
            var resultado = await _repository.GetByIdAsync(idRol);
            return _mapper.Map<RolDTO>(resultado);
        }

        public async Task<Result<RolDTO>> UpdateRol(int idRol, RolDTO rol)
        {
            if (idRol != rol.IdRol)
            {
                return Result<RolDTO>.Failure("Error: Inconsistencia en los identificadores");
            }

            var rolModificado = _mapper.Map<Rol>(rol);

            var resultado = await _repository.UpdateAsync(rolModificado);

            if (resultado!=null)
            {
                return Result<RolDTO>.Success(_mapper.Map<RolDTO>(resultado));
            }
            else
            {
                return Result<RolDTO>.Failure("Error: No se pudo actualizar el rol");
            }
        }
    }
}
