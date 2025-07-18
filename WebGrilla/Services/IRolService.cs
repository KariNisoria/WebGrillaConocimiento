using WebGrilla.DTOs;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public interface IRolService
    {
        Task<IEnumerable<RolDTO>> GetAllRol();
        Task<RolDTO> GetRolById(int idRol);
        Task<Result<RolDTO>> AddRol(RolDTO rol);
        Task<Result<RolDTO>> DeleteRol(int idRol);
        Task<Result<RolDTO>> UpdateRol(int idRol, RolDTO rol);

    }
}
