using WebGrilla.DTOs;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public interface IRecursoService
    {
        Task<IEnumerable<RecursoDTO>> GetAllRecurso();
        Task<RecursoDTO> GetRecursoById(int idRrecurso);
        Task<RecursoDTO> GetRecursoByCorreoElectronico(string correoElectronico);
        Task<Result<RecursoDTO>> AddRecurso(RecursoDTO recurso);
        Task<Result<RecursoDTO>> DeleteRecurso(int idRecurso);
        Task<Result<RecursoDTO>> UpdateRecurso(int idRecurso, RecursoDTO recurso);
    }
}
