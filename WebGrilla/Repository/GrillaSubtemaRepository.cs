using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public class GrillaSubtemaRepository : IGrillaSubtemaRepository
    {
        private readonly AppDbContext _context;

        public GrillaSubtemaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GrillaSubtema>> GetAllAsync()
        {
            var resultados = await _context.GrillaSubtemas
                .Include(gs => gs.GrillaTema)
                .Include(gs => gs.Subtema)
                .ToListAsync();
            return resultados;
        }

        public async Task<GrillaSubtema> GetByIdAsync(int id)
        {
            var resultado = await _context.GrillaSubtemas
                .Include(gs => gs.GrillaTema)
                .Include(gs => gs.Subtema)
                .FirstOrDefaultAsync(x => x.IdGrillaSubtema == id);
            return resultado;
        }

        public async Task<GrillaSubtema> AddAsync(GrillaSubtema item)
        {
            try
            {
                await _context.GrillaSubtemas.AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: No se pudo dar de alta el registro {ex.Message}");
                return null;
            }
        }

        public async Task<GrillaSubtema> UpdateAsync(GrillaSubtema item)
        {
            var encontrado = await _context.GrillaSubtemas.FirstOrDefaultAsync(x => x.IdGrillaSubtema == item.IdGrillaSubtema);
            if (encontrado != null)
            {
                try
                {
                    encontrado.IdGrillaTema = item.IdGrillaTema;
                    encontrado.IdSubtema = item.IdSubtema;
                    encontrado.Ponderacion = item.Ponderacion;

                    _context.Entry(encontrado).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return encontrado;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: No se pudo modificar el registro: {ex.Message}");
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"Error: No se pudo encontrar el GrillaSubtema: {item.IdGrillaSubtema}");
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var encontrado = await _context.GrillaSubtemas.FirstOrDefaultAsync(x => x.IdGrillaSubtema == id);
            if (encontrado != null)
            {
                try
                {
                    _context.GrillaSubtemas.Remove(encontrado);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: No se pudo eliminar el GrillaSubtema: {ex.Message}");
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"Error: No se encontró el GrillaSubtema {id} a eliminar");
                return false;
            }
        }

        public async Task<List<GrillaSubtema>> GetByGrillaAsync(int idGrilla)
        {
            return await _context.GrillaSubtemas
                .Include(gs => gs.GrillaTema)
                .Include(gs => gs.Subtema)
                .Where(gs => gs.GrillaTema.IdGrilla == idGrilla)
                .ToListAsync();
        }

        public async Task<List<GrillaSubtema>> GetByGrillaTemaAsync(int idGrillaTema)
        {
            return await _context.GrillaSubtemas
                .Include(gs => gs.GrillaTema)
                .Include(gs => gs.Subtema)
                .Where(gs => gs.IdGrillaTema == idGrillaTema)
                .ToListAsync();
        }
    }
}