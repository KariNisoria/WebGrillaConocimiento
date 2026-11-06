using WebGrilla.DTOs.MapaCalor;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public interface IMapaCalorService
    {
        Task<Result<MapaCalorDTO>> GetMapaCalor(int idGrilla);
    }
}
