using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public class EquipoDesarrolloRepository : IRepository<EquipoDesarrollo>
    {
        private readonly AppDbContext _contexto;
        public EquipoDesarrolloRepository(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<EquipoDesarrollo> AddAsync(EquipoDesarrollo item)
        {
            try
            {
                await _contexto.Equipos.AddAsync(item);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: No se pudo registrar el nuevo equipo: {ex.Message}");
                return null;
            }
            return item;    
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var encontrado = await _contexto.Equipos.FirstOrDefaultAsync(x => x.IdEquipoDesarrollo == id);
            if (encontrado != null)
            {
                try
                {
                    _contexto.Equipos.Remove(encontrado);
                    await _contexto.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: No se pudo eliminar el equipo: {ex.Message}");
                    return false;
                }
                return true;
            }
            else {
                Console.WriteLine($"Error: No se pudo encontrar el equipo {id} a eliminar");
                return false; 
            }
        }

        public async Task<IEnumerable<EquipoDesarrollo>> GetAllAsync()
        {
            var resultado = await _contexto.Equipos.ToListAsync();
            return resultado;
        }

        public async Task<EquipoDesarrollo> GetByIdAsync(int id)
        {
            var resultado = await _contexto.Equipos.FirstOrDefaultAsync(x=>x.IdEquipoDesarrollo == id);
            return resultado;
        }

        public async Task<EquipoDesarrollo> UpdateAsync(EquipoDesarrollo item)
        {
            var encontrado = await _contexto.Equipos.FirstOrDefaultAsync(x => x.IdEquipoDesarrollo == item.IdEquipoDesarrollo);
            if (encontrado != null)
            {
                try
                {
                    encontrado.Nombre = item.Nombre;
                    encontrado.IdCliente = item.IdCliente;
                    _contexto.Entry(encontrado).State = EntityState.Modified;
                    await _contexto.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: No se pudo modificar el equipo: {ex.Message}");
                    return null;
                }
                return encontrado;
            }
            else
            {
                Console.WriteLine($"Error: No se pudo encontrar el equipo {item.IdEquipoDesarrollo} a modificar");
                return null;

            }
        }
    }
}
