using WebGrilla.DTOs;
using WebGrilla.Models;
using WebGrilla.Repository;

namespace WebGrilla.Services
{
    public interface IRecursoSupervisorService
    {
        Task<IEnumerable<RecursoSupervisorDTO>> GetAllAsync();
        Task<RecursoSupervisorDTO?> GetByIdAsync(int id);
        Task<IEnumerable<RecursoSimpleDTO>> GetRecursosDisponiblesAsync();
        Task<SupervisionViewDTO> GetSupervisionViewAsync(int idSupervisor);
        Task<bool> AsignarSupervisadoAsync(int idSupervisor, int idSupervisado, string? observaciones = null);
        Task<bool> DesasignarSupervisadoAsync(int idSupervisor, int idSupervisado);
        Task<bool> ExisteRelacionAsync(int idSupervisor, int idSupervisado);
    }

    public class RecursoSupervisorService : IRecursoSupervisorService
    {
        private readonly IRecursoSupervisorRepository _repository;

        public RecursoSupervisorService(IRecursoSupervisorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RecursoSupervisorDTO>> GetAllAsync()
        {
            return await _repository.GetAllWithDetailsAsync();
        }

        public async Task<RecursoSupervisorDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            return new RecursoSupervisorDTO
            {
                IdRecursoSupervisor = entity.IdRecursoSupervisor,
                IdRecursoSupervisorAsignado = entity.IdRecursoSupervisorAsignado,
                IdRecursoSupervisado = entity.IdRecursoSupervisado,
                FechaAsignacion = entity.FechaAsignacion,
                Activo = entity.Activo,
                FechaBaja = entity.FechaBaja,
                Observaciones = entity.Observaciones,
                NombreSupervisor = entity.RecursoSupervisorAsignado?.NombreCompleto,
                NombreSupervisado = entity.RecursoSupervisado?.NombreCompleto
            };
        }

        public async Task<IEnumerable<RecursoSimpleDTO>> GetRecursosDisponiblesAsync()
        {
            return await _repository.GetRecursosDisponiblesAsync();
        }

        public async Task<SupervisionViewDTO> GetSupervisionViewAsync(int idSupervisor)
        {
            return await _repository.GetSupervisionViewAsync(idSupervisor);
        }

        public async Task<bool> AsignarSupervisadoAsync(int idSupervisor, int idSupervisado, string? observaciones = null)
        {
            try
            {
                // Validar que no es el mismo recurso
                if (idSupervisor == idSupervisado)
                {
                    return false;
                }

                // Validar que no existe ya la relación
                if (await _repository.ExisteRelacionAsync(idSupervisor, idSupervisado))
                {
                    return false;
                }

                var nuevaRelacion = new RecursoSupervisor
                {
                    IdRecursoSupervisorAsignado = idSupervisor,
                    IdRecursoSupervisado = idSupervisado,
                    FechaAsignacion = DateTime.Now,
                    Activo = true,
                    Observaciones = observaciones
                };

                await _repository.AddAsync(nuevaRelacion);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DesasignarSupervisadoAsync(int idSupervisor, int idSupervisado)
        {
            try
            {
                await _repository.DesactivarRelacionAsync(idSupervisor, idSupervisado);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ExisteRelacionAsync(int idSupervisor, int idSupervisado)
        {
            return await _repository.ExisteRelacionAsync(idSupervisor, idSupervisado);
        }
    }
}