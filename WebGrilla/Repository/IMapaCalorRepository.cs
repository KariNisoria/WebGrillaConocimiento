using WebGrilla.DTOs.MapaCalor;

namespace WebGrilla.Repository
{
    public interface IMapaCalorRepository
    {
        Task<MapaCalorGrillaDTO?> GetGrillaAsync(int idGrilla);

        Task<List<MapaCalorTemaDTO>> GetTemasAsync(int idGrilla);

        Task<List<MapaCalorRecursoDTO>> GetRecursosAsync(int idGrilla);

        Task<List<MapaCalorDetalleDTO>> GetDetalleAsync(int idGrilla);

        Task<List<MapaCalorValorDTO>> GetValoresAsync(int idGrilla);
    }
}
