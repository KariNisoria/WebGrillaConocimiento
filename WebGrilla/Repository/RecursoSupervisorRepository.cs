using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.DTOs;
using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public interface IRecursoSupervisorRepository : IRepository<RecursoSupervisor>
    {
        Task<IEnumerable<RecursoSupervisorDTO>> GetAllWithDetailsAsync();
        Task<IEnumerable<RecursoSimpleDTO>> GetRecursosDisponiblesAsync();
        Task<IEnumerable<RecursoSimpleDTO>> GetRecursosSupervisadosAsync(int idSupervisor);
        Task<SupervisionViewDTO> GetSupervisionViewAsync(int idSupervisor);
        Task<bool> ExisteRelacionAsync(int idSupervisor, int idSupervisado);
        Task<RecursoSupervisor?> GetRelacionActivaAsync(int idSupervisor, int idSupervisado);
        Task DesactivarRelacionAsync(int idSupervisor, int idSupervisado);
    }

    public class RecursoSupervisorRepository : IRecursoSupervisorRepository
    {
        private readonly AppDbContext _context;

        public RecursoSupervisorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RecursoSupervisor>> GetAllAsync()
        {
            return await _context.RecursosSupervisores
                .Include(rs => rs.RecursoSupervisorAsignado)
                    .ThenInclude(r => r!.Rol)
                .Include(rs => rs.RecursoSupervisado)
                    .ThenInclude(r => r!.Rol)
                .Where(rs => rs.Activo)
                .ToListAsync();
        }

        public async Task<IEnumerable<RecursoSupervisorDTO>> GetAllWithDetailsAsync()
        {
            return await _context.RecursosSupervisores
                .Include(rs => rs.RecursoSupervisorAsignado)
                    .ThenInclude(r => r!.Rol)
                .Include(rs => rs.RecursoSupervisado)
                    .ThenInclude(r => r!.Rol)
                .Where(rs => rs.Activo)
                .Select(rs => new RecursoSupervisorDTO
                {
                    IdRecursoSupervisor = rs.IdRecursoSupervisor,
                    IdRecursoSupervisorAsignado = rs.IdRecursoSupervisorAsignado,
                    IdRecursoSupervisado = rs.IdRecursoSupervisado,
                    FechaAsignacion = rs.FechaAsignacion,
                    Activo = rs.Activo,
                    FechaBaja = rs.FechaBaja,
                    Observaciones = rs.Observaciones,
                    NombreSupervisor = rs.RecursoSupervisorAsignado!.Nombre + " " + rs.RecursoSupervisorAsignado.Apellido,
                    NombreSupervisado = rs.RecursoSupervisado!.Nombre + " " + rs.RecursoSupervisado.Apellido,
                    CorreoSupervisor = rs.RecursoSupervisorAsignado.CorreoElectronico,
                    CorreoSupervisado = rs.RecursoSupervisado.CorreoElectronico,
                    RolSupervisor = rs.RecursoSupervisorAsignado.Rol!.Nombre,
                    RolSupervisado = rs.RecursoSupervisado.Rol!.Nombre
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<RecursoSimpleDTO>> GetRecursosDisponiblesAsync()
        {
            return await _context.Recursos
                .Include(r => r.Rol)
                .Include(r => r.EquipoDesarrollo)
                .Select(r => new RecursoSimpleDTO
                {
                    IdRecurso = r.IdRecurso,
                    NombreCompleto = r.Nombre + " " + r.Apellido,
                    CorreoElectronico = r.CorreoElectronico,
                    NombreRol = r.Rol!.Nombre,
                    NombreEquipo = r.EquipoDesarrollo!.Nombre
                })
                .OrderBy(r => r.NombreCompleto)
                .ToListAsync();
        }

        public async Task<IEnumerable<RecursoSimpleDTO>> GetRecursosSupervisadosAsync(int idSupervisor)
        {
            return await _context.RecursosSupervisores
                .Include(rs => rs.RecursoSupervisado)
                    .ThenInclude(r => r!.Rol)
                .Include(rs => rs.RecursoSupervisado)
                    .ThenInclude(r => r!.EquipoDesarrollo)
                .Where(rs => rs.IdRecursoSupervisorAsignado == idSupervisor && rs.Activo)
                .Select(rs => new RecursoSimpleDTO
                {
                    IdRecurso = rs.RecursoSupervisado!.IdRecurso,
                    NombreCompleto = rs.RecursoSupervisado.Nombre + " " + rs.RecursoSupervisado.Apellido,
                    CorreoElectronico = rs.RecursoSupervisado.CorreoElectronico,
                    NombreRol = rs.RecursoSupervisado.Rol!.Nombre,
                    NombreEquipo = rs.RecursoSupervisado.EquipoDesarrollo!.Nombre
                })
                .ToListAsync();
        }

        public async Task<SupervisionViewDTO> GetSupervisionViewAsync(int idSupervisor)
        {
            var supervisor = await _context.Recursos
                .Include(r => r.Rol)
                .Include(r => r.EquipoDesarrollo)
                .Where(r => r.IdRecurso == idSupervisor)
                .Select(r => new RecursoSimpleDTO
                {
                    IdRecurso = r.IdRecurso,
                    NombreCompleto = r.Nombre + " " + r.Apellido,
                    CorreoElectronico = r.CorreoElectronico,
                    NombreRol = r.Rol!.Nombre,
                    NombreEquipo = r.EquipoDesarrollo!.Nombre
                })
                .FirstOrDefaultAsync();

            var recursosDisponibles = await GetRecursosDisponiblesAsync();
            var recursosSupervisados = await GetRecursosSupervisadosAsync(idSupervisor);

            return new SupervisionViewDTO
            {
                Supervisor = supervisor ?? new RecursoSimpleDTO(),
                RecursosDisponibles = recursosDisponibles.Where(r => r.IdRecurso != idSupervisor).ToList(),
                RecursosSupervisados = recursosSupervisados.ToList()
            };
        }

        public async Task<bool> ExisteRelacionAsync(int idSupervisor, int idSupervisado)
        {
            return await _context.RecursosSupervisores
                .AnyAsync(rs => rs.IdRecursoSupervisorAsignado == idSupervisor && 
                               rs.IdRecursoSupervisado == idSupervisado && 
                               rs.Activo);
        }

        public async Task<RecursoSupervisor?> GetRelacionActivaAsync(int idSupervisor, int idSupervisado)
        {
            return await _context.RecursosSupervisores
                .FirstOrDefaultAsync(rs => rs.IdRecursoSupervisorAsignado == idSupervisor && 
                                          rs.IdRecursoSupervisado == idSupervisado && 
                                          rs.Activo);
        }

        public async Task DesactivarRelacionAsync(int idSupervisor, int idSupervisado)
        {
            var relacion = await GetRelacionActivaAsync(idSupervisor, idSupervisado);
            if (relacion != null)
            {
                relacion.Activo = false;
                relacion.FechaBaja = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RecursoSupervisor?> GetByIdAsync(int id)
        {
            return await _context.RecursosSupervisores
                .Include(rs => rs.RecursoSupervisorAsignado)
                .Include(rs => rs.RecursoSupervisado)
                .FirstOrDefaultAsync(rs => rs.IdRecursoSupervisor == id);
        }

        public async Task<RecursoSupervisor> AddAsync(RecursoSupervisor entity)
        {
            _context.RecursosSupervisores.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RecursoSupervisor> UpdateAsync(RecursoSupervisor entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _context.RecursosSupervisores.Remove(entity);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}