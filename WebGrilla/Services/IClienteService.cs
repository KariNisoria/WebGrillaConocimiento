using WebGrilla.DTOs;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> GetAllCliente();
        Task<ClienteDTO> GetClienteById(int idCliente);
        Task<Result<ClienteDTO>> AddCliente(ClienteDTO cliente);
        Task<Result<ClienteDTO>> DeleteCliente(int idCliente);
        Task<Result<ClienteDTO>> UpdateCliente(int idCliente, ClienteDTO cliente);
    }
}
