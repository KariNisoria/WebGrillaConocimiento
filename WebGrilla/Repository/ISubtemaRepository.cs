using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public interface ISubtemaRepository : IRepository<Subtema>
    {
        Task<IEnumerable<Subtema>> GetSubtemaByIdTema(int id);

        Task<bool> HasSutemaByIdTema(int id);
    }
}
