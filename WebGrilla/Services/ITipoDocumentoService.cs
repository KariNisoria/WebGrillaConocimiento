using WebGrilla.DTOs;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public interface ITipoDocumentoService
    {
        Task<IEnumerable<TipoDocumentoDTO>> GetAllTipoDocumento();
        Task<TipoDocumentoDTO> GetTipoDocumentoById(int idTipoDocumento);
        Task<Result<TipoDocumentoDTO>> AddTipoDocumento(TipoDocumentoDTO tipodocumento);
        Task<Result<TipoDocumentoDTO>> DeleteTipoDocumento(int idTipoDocumento);
        Task<Result<TipoDocumentoDTO>> UpdateTipoDocumento(int idTipoDocumento, TipoDocumentoDTO tipodocumento);
    }
}
