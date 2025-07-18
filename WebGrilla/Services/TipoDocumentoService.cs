using AutoMapper;
using WebGrilla.DTOs;
using WebGrilla.Models;
using WebGrilla.Repository;
using WebGrilla.Utils;

namespace WebGrilla.Services
{
    public class TipoDocumentoService : ITipoDocumentoService
    {
        private readonly IRepository<TipoDocumento> _repository;
        private readonly IMapper _mapper;

        public TipoDocumentoService(IRepository<TipoDocumento> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<TipoDocumentoDTO>> AddTipoDocumento(TipoDocumentoDTO tipodocumento)
        {
            var encontrado = await _repository.GetByIdAsync(tipodocumento.IdTipoDocumento);
            if (encontrado != null)
            {
                return Result<TipoDocumentoDTO>.Failure($"Error: El tipo de documento ingresado ya existe: {encontrado.IdTipoDocumento}");
            }
            var nuevoTipoDocumento = _mapper.Map<TipoDocumento>(tipodocumento);
            var resultado = await _repository.AddAsync(nuevoTipoDocumento);
            if (resultado == null)
            {
                return Result<TipoDocumentoDTO>.Failure("Error: No se pudo registrar el tipo de documento");
            }
            return Result<TipoDocumentoDTO>.Success(_mapper.Map<TipoDocumentoDTO>(resultado));
        }

        public async Task<Result<TipoDocumentoDTO>> DeleteTipoDocumento(int idTipoDocumento)
        {
            var resultado = await _repository.DeleteAsync(idTipoDocumento);
            if (!resultado)
            {
                return Result<TipoDocumentoDTO>.Failure($"Error: El tipo de documento no pudo ser eliminado. Id: {idTipoDocumento}");
            }
            return Result<TipoDocumentoDTO>.Success(new TipoDocumentoDTO());
        }

        public async Task<IEnumerable<TipoDocumentoDTO>> GetAllTipoDocumento()
        {
            var resultado = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TipoDocumentoDTO>>(resultado);
        }

        public async Task<TipoDocumentoDTO> GetTipoDocumentoById(int idTipoDocumento)
        {
            var resultado = await _repository.GetByIdAsync(idTipoDocumento);
            return _mapper.Map<TipoDocumentoDTO>(resultado);
        }

        public async Task<Result<TipoDocumentoDTO>> UpdateTipoDocumento(int idTipoDocumento, TipoDocumentoDTO tipodocumento)
        {
            if (idTipoDocumento != tipodocumento.IdTipoDocumento)
            {
                return Result<TipoDocumentoDTO>.Failure("Error: Inconsistencia entre los identificadores de tipo documento");
            }
            var tipoDocumentoModificado = _mapper.Map<TipoDocumento>(tipodocumento);
            var resultado = await _repository.UpdateAsync(tipoDocumentoModificado);
            if (resultado == null)
            {
                return Result<TipoDocumentoDTO>.Failure("Error: El tipo de documento no pudo ser modificado");
            }
            return Result<TipoDocumentoDTO>.Success(_mapper.Map<TipoDocumentoDTO>(resultado));
        }
    }
}