using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public interface IRecursoRepository : IRepository<Recurso>
    {
        Task<Recurso> GetRecursoByCorreoElectronico(string correoElectronico);
    }
}
