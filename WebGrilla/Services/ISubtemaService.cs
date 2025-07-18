using WebGrilla.DTOs;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public interface ISubtemaService
    {
        Task<IEnumerable<SubtemaDTO>> GetAllSubtema();
        Task<SubtemaDTO> GetSubtemaById(int id);
        Task<IEnumerable<SubtemaDTO>> GetSubtemaByIdTema(int id);
        Task<Result<SubtemaDTO>> AddSubtema(SubtemaDTO SubtemaDTO);
        Task<bool> DeleteSubtemaById(int id);
        Task<Result<SubtemaDTO>> UpdateSubtemaById(int id, SubtemaDTO SubtemaDTO);
    }
}
