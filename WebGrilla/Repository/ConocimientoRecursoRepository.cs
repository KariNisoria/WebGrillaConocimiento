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
        Task<ConocimientoRecurso> BuscarConocimientoAsync(int idEvaluacion, int idRecurso, int idSubtema);
        Task<Dictionary<int, decimal>> GetCompletitudSubtemasPorGrillaAsync(int idGrilla);
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
                .Include(c => c.Grilla)
                .Include(c => c.Evaluacion)
                .ToListAsync();
        }

        public async Task<ConocimientoRecurso> GetByIdAsync(int id)
        {
            return await _context.ConocimientoRecurso
                .Include(c => c.Subtema)
                    .ThenInclude(s => s.Tema)
                .Include(c => c.Recurso)
                .Include(c => c.Grilla)
                .Include(c => c.Evaluacion)
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
            try
            {
                Console.WriteLine($"Repository: Buscando conocimientos para evaluación {idEvaluacion} y recurso {idRecurso}");
                
                var result = await _context.ConocimientoRecurso
                    .Include(c => c.Subtema)
                        .ThenInclude(s => s.Tema)
                    .Include(c => c.Recurso)
                    .Include(c => c.Grilla)
                    .Include(c => c.Evaluacion)
                    .Where(c => c.IdEvaluacion == idEvaluacion && c.IdRecurso == idRecurso)
                    .ToListAsync();
                    
                Console.WriteLine($"Repository: Encontrados {result.Count} conocimientos");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Repository GetConocimientosPorEvaluacionYRecursoAsync: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<ConocimientoRecurso>> GetConocimientosPorGrillaYRecursoAsync(int idGrilla, int idRecurso)
        {
            return await _context.ConocimientoRecurso
                .Include(c => c.Subtema)
                    .ThenInclude(s => s.Tema)
                .Include(c => c.Recurso)
                .Include(c => c.Grilla)
                .Include(c => c.Evaluacion)
                .Where(c => c.IdGrilla == idGrilla && c.IdRecurso == idRecurso)
                .ToListAsync();
        }

        public async Task<decimal> GetPorcentajeCompletitudSubtemasAsync(int idGrillaTema)
        {
            try
            {
                // Obtener la suma total de ponderaciones de los subtemas de este tema
                var totalPonderacion = await _context.GrillaSubtemas
                    .Where(gs => gs.IdGrillaTema == idGrillaTema)
                    .SumAsync(gs => gs.Ponderacion);

                // La completitud es 100% solo si la suma de ponderaciones es exactamente 100%
                return totalPonderacion == 100 ? 100 : totalPonderacion;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al calcular completitud de subtemas: {ex.Message}");
                return 0;
            }
        }

        public async Task<ConocimientoRecurso> BuscarConocimientoAsync(int idEvaluacion, int idRecurso, int idSubtema)
        {
            return await _context.ConocimientoRecurso
                .FirstOrDefaultAsync(cr => 
                    cr.IdEvaluacion == idEvaluacion && 
                    cr.IdRecurso == idRecurso && 
                    cr.IdSubtema == idSubtema);
        }

        public async Task<Dictionary<int, decimal>> GetCompletitudSubtemasPorGrillaAsync(int idGrilla)
        {
            try
            {
                // Obtener todos los temas de la grilla con sus subtemas agrupados
                var completitudPorTema = await _context.GrillaTemas
                    .Where(gt => gt.IdGrilla == idGrilla)
                    .Select(gt => new
                    {
                        IdGrillaTema = gt.IdGrillaTema,
                        TotalPonderacion = _context.GrillaSubtemas
                            .Where(gs => gs.IdGrillaTema == gt.IdGrillaTema)
                            .Sum(gs => gs.Ponderacion)
                    })
                    .ToListAsync();

                // Convertir a diccionario con la lógica de completitud
                return completitudPorTema.ToDictionary(
                    item => item.IdGrillaTema,
                    item => item.TotalPonderacion == 100 ? 100m : item.TotalPonderacion
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener completitud por grilla: {ex.Message}");
                return new Dictionary<int, decimal>();
            }
        }
    }
}