using WebGrilla.DTOs;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public interface ITemaService
    {
        Task<IEnumerable<TemaDTO>> GetAllTema();
        Task<TemaDTO> GetTemaById(int id);  
        Task<Result<TemaDTO>> AddTema(TemaDTO temaDTO);
        Task<Result<TemaDTO>> DeleteTemaById(int id);
        Task<Result<TemaDTO>> UpdateTemaById(int id, TemaDTO temaDTO);
    }
}
