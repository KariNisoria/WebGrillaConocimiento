using WebGrilla.DTOs;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public interface IGrillaSubtemaService
    {
        Task<IEnumerable<GrillaSubtemaDTO>> GetAllGrillaSubtema();
        Task<GrillaSubtemaDTO?> GetGrillaSubtemaById(int idGrillaSubtema);
        Task<IEnumerable<GrillaSubtemaDTO>> GetGrillaSubtemasByGrillaTema(int idGrillaTema);
        Task<IEnumerable<GrillaSubtemaDTO>> GetGrillaSubtemasByGrilla(int idGrilla);
        Task<Result<GrillaSubtemaDTO>> AddGrillaSubtema(GrillaSubtemaDTO grillaSubtema);
        Task<Result<GrillaSubtemaDTO>> DeleteGrillaSubtema(int idGrillaSubtema);
        Task<Result<GrillaSubtemaDTO>> UpdateGrillaSubtema(int idGrillaSubtema, GrillaSubtemaDTO grillaSubtema);
    }
}