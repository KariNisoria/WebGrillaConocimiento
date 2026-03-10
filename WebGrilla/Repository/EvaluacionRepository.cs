using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public interface IEvaluacionRepository : IRepository<Evaluacion>
    {
        Task<Evaluacion?> GetEvaluacionActivaAsync();
        Task<Evaluacion?> GetEvaluacionActivaPorRecursoAsync(int idRecurso);
        Task<List<Evaluacion>> GetEvaluacionesPorRecursoAsync(int idRecurso);
        Task<List<Evaluacion>> GetEvaluacionesPorSupervisionAsync(int idSupervisor);
        Task<List<Evaluacion>> GetEvaluacionesPorRecursoYSupervisionAsync(int idRecurso);
    }

    public class EvaluacionRepository : IEvaluacionRepository
    {
        private readonly AppDbContext _context;

        public EvaluacionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Evaluacion>> GetAllAsync()
        {
            try
            {
                return await _context.Evaluacion.ToListAsync();
            }
            catch (Exception)
            {
                return new List<Evaluacion>();
            }
        }

        public async Task<Evaluacion> GetByIdAsync(int id)
        {
            return await _context.Evaluacion.FirstOrDefaultAsync(e => e.IdEvaluacion == id);
        }

        public async Task<Evaluacion> AddAsync(Evaluacion item)
        {
            await _context.Evaluacion.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Evaluacion> UpdateAsync(Evaluacion item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var evaluacion = await _context.Evaluacion.FirstOrDefaultAsync(e => e.IdEvaluacion == id);
            if (evaluacion != null)
            {
                _context.Evaluacion.Remove(evaluacion);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Evaluacion?> GetEvaluacionActivaAsync()
        {
            try
            {
                var fechaActual = DateTime.Now;
                return await _context.Evaluacion
                    .Where(e => e.FechaInicio <= fechaActual && e.FechaFin >= fechaActual)
                    .OrderByDescending(e => e.FechaInicio)
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Evaluacion>> GetEvaluacionesPorRecursoAsync(int idRecurso)
        {
            try
            {
                return await _context.Evaluacion
                    .Where(e => e.IdRecurso == idRecurso)
                    .OrderByDescending(e => e.FechaInicio)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<Evaluacion>();
            }
        }

        public async Task<Evaluacion?> GetEvaluacionActivaPorRecursoAsync(int idRecurso)
        {
            try
            {
                var fechaActual = DateTime.Now;
                return await _context.Evaluacion
                    .Where(e => e.IdRecurso == idRecurso && e.FechaInicio <= fechaActual && e.FechaFin >= fechaActual)
                    .OrderByDescending(e => e.FechaInicio)
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Evaluacion>> GetEvaluacionesPorSupervisionAsync(int idSupervisor)
        {
            try
            {
                return await _context.Evaluacion
                    .Where(e => _context.RecursosSupervisores
                        .Any(rs => rs.IdRecursoSupervisorAsignado == idSupervisor && rs.IdRecursoSupervisado == e.IdRecurso && rs.Activo))
                    .OrderByDescending(e => e.FechaInicio)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<Evaluacion>();
            }
        }

        public async Task<List<Evaluacion>> GetEvaluacionesPorRecursoYSupervisionAsync(int idRecurso)
        {
            try
            {
                return await _context.Evaluacion
                    .Where(e => e.IdRecurso == idRecurso || 
                               _context.RecursosSupervisores
                                   .Any(rs => rs.IdRecursoSupervisorAsignado == idRecurso && 
                                             rs.IdRecursoSupervisado == e.IdRecurso && 
                                             rs.Activo))
                    .OrderByDescending(e => e.FechaInicio)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<Evaluacion>();
            }
        }
    }
}