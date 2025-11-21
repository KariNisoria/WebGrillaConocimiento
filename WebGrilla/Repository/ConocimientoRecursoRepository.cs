using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public interface IConocimientoRecursoRepository : IRepository<ConocimientoRecurso>
    {
        Task<List<ConocimientoRecurso>> GetConocimientosPorEvaluacionYRecursoAsync(int idEvaluacion, int idRecurso);
        Task<List<ConocimientoRecurso>> GetConocimientosPorGrillaYRecursoAsync(int idGrilla, int idRecurso);
        Task<decimal> GetPorcentajeCompletitudSubtemasAsync(int idGrillaTema);
    }

    public class ConocimientoRecursoRepository : IConocimientoRecursoRepository
    {
        private readonly AppDbContext _context;

        public ConocimientoRecursoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConocimientoRecurso>> GetAllAsync()
        {
            return await _context.ConocimientoRecurso
                .Include(c => c.Subtema)
                    .ThenInclude(s => s.Tema)
                .Include(c => c.Recurso)
                .ToListAsync();
        }

        public async Task<ConocimientoRecurso> GetByIdAsync(int id)
        {
            return await _context.ConocimientoRecurso
                .Include(c => c.Subtema)
                    .ThenInclude(s => s.Tema)
                .Include(c => c.Recurso)
                .FirstOrDefaultAsync(c => c.IdConocimientoRecurso == id);
        }

        public async Task<ConocimientoRecurso> AddAsync(ConocimientoRecurso item)
        {
            await _context.ConocimientoRecurso.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<ConocimientoRecurso> UpdateAsync(ConocimientoRecurso item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var conocimiento = await _context.ConocimientoRecurso.FirstOrDefaultAsync(c => c.IdConocimientoRecurso == id);
            if (conocimiento != null)
            {
                _context.ConocimientoRecurso.Remove(conocimiento);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<ConocimientoRecurso>> GetConocimientosPorEvaluacionYRecursoAsync(int idEvaluacion, int idRecurso)
        {
            return await _context.ConocimientoRecurso
                .Include(c => c.Subtema)
                    .ThenInclude(s => s.Tema)
                .Include(c => c.Recurso)
                .Where(c => c.IdEvaluacion == idEvaluacion && c.IdRecurso == idRecurso)
                .ToListAsync();
        }

        public async Task<List<ConocimientoRecurso>> GetConocimientosPorGrillaYRecursoAsync(int idGrilla, int idRecurso)
        {
            return await _context.ConocimientoRecurso
                .Include(c => c.Subtema)
                    .ThenInclude(s => s.Tema)
                .Include(c => c.Recurso)
                .Where(c => c.IdGrilla == idGrilla && c.IdRecurso == idRecurso)
                .ToListAsync();
        }

        public async Task<decimal> GetPorcentajeCompletitudSubtemasAsync(int idGrillaTema)
        {
            // Obtener todos los subtemas del tema en la grilla
            var totalSubtemas = await _context.GrillaSubtemas
                .Where(gs => gs.IdGrillaTema == idGrillaTema)
                .CountAsync();

            if (totalSubtemas == 0) return 0;

            // Obtener subtemas que tienen ponderación = 100%
            var subtemasCompletos = await _context.GrillaSubtemas
                .Where(gs => gs.IdGrillaTema == idGrillaTema && gs.Ponderacion == 100)
                .CountAsync();

            return (decimal)subtemasCompletos / totalSubtemas * 100;
        }
    }
}