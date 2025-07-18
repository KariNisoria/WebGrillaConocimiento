using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public class GrillaRepository : IRepository<Grilla>
    {
        private readonly AppDbContext _context;
        public GrillaRepository(AppDbContext contexto)
        {
            _context = contexto;
        }
        public async Task<Grilla> AddAsync(Grilla item)
        {
            try
            {
                await _context.Grillas.AddAsync(item);
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
            var encontrado = await _context.Grillas.FirstOrDefaultAsync(x=>x.IdGrilla == id);
            if (encontrado != null)
            {
                try
                {
                    _context.Grillas.Remove(encontrado);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: No se pudo eliminar la grilla: {ex.Message}");
                    return false;
                }
                
            }
            else
            {
                Console.WriteLine($"Error: No se encontro la grila {id} a eliminar");
                return false;
            }
        }

        public async Task<IEnumerable<Grilla>> GetAllAsync()
        {
            var resultados = await _context.Grillas.ToListAsync();
            return resultados;
        }

        public async Task<Grilla> GetByIdAsync(int id)
        {
            var resultado = await _context.Grillas.FirstOrDefaultAsync(x=>x.IdGrilla==id);
            return resultado;
        }

        public async Task<Grilla> UpdateAsync(Grilla item)
        {
            var encontrado = await _context.Grillas.FirstOrDefaultAsync(x => x.IdGrilla == item.IdGrilla);
            if (encontrado != null)
            {
                try
                {
                    encontrado.Nombre = item.Nombre;
                    encontrado.Descripcion = item.Descripcion;
                    encontrado.FechaVigencia = item.FechaVigencia;
                    encontrado.Estado = item.Estado;

                    _context.Entry(encontrado).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return encontrado;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: No se modificar el registro: {ex.Message}");
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"Error: No se pudo encontrar la grilla: {item.IdGrilla}");
                return null;
            }
        }
    }
}
