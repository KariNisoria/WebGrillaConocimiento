using WebGrilla.DTOs;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public interface IGrillaService
    {
        Task<IEnumerable<GrillaDTO>> GetAllGrilla();
        Task<GrillaDTO> GetGrillaById(int idGrilla);
        Task<Result<GrillaDTO>> AddGrilla(GrillaDTO grilla);
        Task<Result<GrillaDTO>> DeleteGrilla(int idGrilla);
        Task<Result<GrillaDTO>> UpdateGrilla(int idGrilla, GrillaDTO grilla);
    }
}
