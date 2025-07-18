using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public class RolRepository : IRepository<Rol>
    {
        private readonly AppDbContext _context;
        public RolRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Rol> AddAsync(Rol item)
        {
            try
            {
                await _context.Roles.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: No se pudo registrar el nuevo rol: {ex.Message}");
                return null;
            }
            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var encontrado = await _context.Roles.FirstOrDefaultAsync(x => x.IdRol == id);
            if (encontrado != null)
            {
                try
                {
                    _context.Roles.Remove(encontrado);
                    await _context.SaveChangesAsync();

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Error: No se pudo eliminar el rol: {ex.Message}");
                    return false;
                }
                return true;
            }
            else 
            {
                Console.WriteLine($"Error: El rol {id} solicitado no existe");
                return false; 
            }
        }

        public async Task<IEnumerable<Rol>> GetAllAsync()
        {
            var resultado = await _context.Roles.ToListAsync();
            return resultado;
        }

        public async Task<Rol> GetByIdAsync(int id)
        {
            var resultado = await _context.Roles.FirstOrDefaultAsync(x=>x.IdRol == id);
            return resultado;
        }

        public async Task<Rol> UpdateAsync(Rol item)
        {
            var encontrado = await _context.Roles.FirstOrDefaultAsync(x => x.IdRol == item.IdRol);
            if (encontrado != null)
            {

                try
                {
                    encontrado.Nombre = item.Nombre;
                    _context.Entry(encontrado).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: No se pudo actualizar el rol: {ex.Message}");
                    return null;
                }
                return item;
            }
            else
            {
                Console.WriteLine($"Error: No se pudo encontrar el rol {item.IdRol}");
                return null;
            }
        }
    }
}
