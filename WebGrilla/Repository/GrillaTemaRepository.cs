using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public class GrillaTemaRepository : IRepository<GrillaTema>
    {
        private readonly AppDbContext _context;

        public GrillaTemaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GrillaTema> AddAsync(GrillaTema item)
        {
            try
            {
                await _context.GrillaTemas.AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: No se pudo dar de alta el registro {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var encontrado = await _context.GrillaTemas.FirstOrDefaultAsync(x => x.IdGrillaTema == id);
            if (encontrado != null)
            {
                try
                {
                    _context.GrillaTemas.Remove(encontrado);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: No se pudo eliminar el GrillaTema: {ex.Message}");
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"Error: No se encontró el GrillaTema {id} a eliminar");
                return false;
            }
        }

        public async Task<IEnumerable<GrillaTema>> GetAllAsync()
        {
            var resultados = await _context.GrillaTemas.ToListAsync();
            return resultados;
        }

        public async Task<GrillaTema> GetByIdAsync(int id)
        {
            var resultado = await _context.GrillaTemas.FirstOrDefaultAsync(x => x.IdGrillaTema == id);
            return resultado;
        }

        public async Task<GrillaTema> UpdateAsync(GrillaTema item)
        {
            var encontrado = await _context.GrillaTemas.FirstOrDefaultAsync(x => x.IdGrillaTema == item.IdGrillaTema);
            if (encontrado != null)
            {
                try
                {
                    encontrado.IdGrilla = item.IdGrilla;
                    encontrado.IdTema = item.IdTema;
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
                Console.WriteLine($"Error: No se pudo encontrar el GrillaTema: {item.IdGrillaTema}");
                return null;
            }
        }
    }
}