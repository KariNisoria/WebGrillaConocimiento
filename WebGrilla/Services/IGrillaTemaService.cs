using WebGrilla.DTOs;
using WebGrilla.Utils;

public interface IGrillaTemaService
{
    Task<IEnumerable<GrillaTemaDTO>> GetAllGrillaTema();
    Task<GrillaTemaDTO?> GetGrillaTemaById(int idGrillaTema);
    Task<IEnumerable<GrillaTemaDTO>> GetGrillaTemasByGrilla(int idGrilla);
    Task<Result<GrillaTemaDTO>> AddGrillaTema(GrillaTemaDTO grillaTema);
    Task<Result<GrillaTemaDTO>> DeleteGrillaTema(int idGrillaTema);
    Task<Result<GrillaTemaDTO>> UpdateGrillaTema(int idGrillaTema, GrillaTemaDTO grillaTema);
}