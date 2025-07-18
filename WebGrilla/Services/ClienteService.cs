using AutoMapper;
using WebGrilla.DTOs;
using WebGrilla.Models;
using WebGrilla.Repository;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IRepository<Cliente> _repository;
        private readonly IMapper _mapper;

        public ClienteService(IRepository<Cliente> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;   
        }

        public async Task<Result<ClienteDTO>> AddCliente(ClienteDTO cliente)
        {
            var encontrado = await _repository.GetByIdAsync(cliente.IdCliente);
            if (encontrado != null)
            {
                return Result<ClienteDTO>.Failure("Error: El identificador Id de cliente ya fue utilizado");
            }

            var nuevoCliente = _mapper.Map<Cliente>(cliente);

            var resultado = await _repository.AddAsync(nuevoCliente);

            if (resultado == null)
            {
                return Result<ClienteDTO>.Failure("Error: No se pudo registrar el cliente");
            }

            return Result<ClienteDTO>.Success(_mapper.Map<ClienteDTO>(resultado));
        }

        public async Task<Result<ClienteDTO>> DeleteCliente(int idCliente)
        {
            var resultado = await _repository.DeleteAsync(idCliente);
            if (!resultado)
            {
                return Result<ClienteDTO>.Failure("Error: No se pudo eliminar el cliente");
            }
            return Result<ClienteDTO>.Success(new ClienteDTO());
        }

        public async Task<IEnumerable<ClienteDTO>> GetAllCliente()
        {
            var resultado = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClienteDTO>>(resultado);
        }

        public async Task<ClienteDTO> GetClienteById(int idCliente)
        {
            var resultado = await _repository.GetByIdAsync(idCliente);
            return _mapper.Map<ClienteDTO>(resultado);
        }

        public async Task<Result<ClienteDTO>> UpdateCliente(int idCliente, ClienteDTO cliente)
        {
            if (idCliente!=cliente.IdCliente)
            {
                return Result<ClienteDTO>.Failure("Error: Los identificadores son inconsistentes");
            }
            var clienteModificado = _mapper.Map<Cliente>(cliente);

            var resultado = await _repository.UpdateAsync(clienteModificado);

            if (resultado == null)
            {
                return Result<ClienteDTO>.Failure("Error: El cliente no pudo ser modificado");
            }

            return Result<ClienteDTO>.Success(_mapper.Map<ClienteDTO>(resultado));
        }
    }
}
