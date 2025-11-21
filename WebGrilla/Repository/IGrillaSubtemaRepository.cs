using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public interface IGrillaSubtemaRepository : IRepository<GrillaSubtema>
    {
        Task<List<GrillaSubtema>> GetByGrillaAsync(int idGrilla);
        Task<List<GrillaSubtema>> GetByGrillaTemaAsync(int idGrillaTema);
    }
}