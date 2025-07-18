using WebGrilla.DTOs;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public interface IEquipoDesarrolloService
    {
        Task<IEnumerable<EquipoDesarrolloDTO>> GetAllEquipoDesarrollo();
        Task<EquipoDesarrolloDTO> GetEquipoDesarrollolById(int idEquipoDesarrollo);
        Task<Result<EquipoDesarrolloDTO>> AddEquipoDesarrollo(EquipoDesarrolloDTO equipo);
        Task<Result<EquipoDesarrolloDTO>> DeleteEquipoDesarrollo(int idEquipoDesarrollo);
        Task<Result<EquipoDesarrolloDTO>> UpdateEquipoDesarrollo(int idEquipoDesarrollo, EquipoDesarrolloDTO equipo);
    }
}
