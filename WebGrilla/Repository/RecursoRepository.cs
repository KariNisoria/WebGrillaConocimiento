using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public class RecursoRepository : IRecursoRepository
    {
        private readonly AppDbContext _contexto;

        public RecursoRepository(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<Recurso> AddAsync(Recurso item)
        {
            try
            {
                await _contexto.Recursos.AddAsync(item);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: No se pudo realizar el registro: {ex.Message}");
                return null;
            }
            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var encontrado = await _contexto.Recursos.FirstOrDefaultAsync(x=>x.IdRecurso==id);
            if (encontrado != null) 
            {
                try
                {
                    _contexto.Recursos.Remove(encontrado);
                    await _contexto.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: No se pudo eliminar el registro: {ex.Message}");
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"Error: No se pudo encontrar el registro a eliminar Id: {id}");
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Recurso>> GetAllAsync()
        {
            var resultado = await _contexto.Recursos.ToListAsync();
            return resultado;
        }

        public async Task<Recurso> GetByIdAsync(int id)
        {
            var resultado = await _contexto.Recursos.FirstOrDefaultAsync(x=>x.IdRecurso==id);
            return resultado;
        }

        public async Task<Recurso> GetRecursoByCorreoElectronico(string correoElectronico)
        {
            var resultado = await _contexto.Recursos.FirstOrDefaultAsync(x=>x.CorreoElectronico==correoElectronico);
            return resultado;
        }

        public async Task<Recurso> UpdateAsync(Recurso item)
        {
            var encontrado = await _contexto.Recursos.FirstOrDefaultAsync(x=>x.IdRecurso == item.IdRecurso);
            if (encontrado != null)
            {
                encontrado.Nombre = item.Nombre;
                encontrado.Apellido = item.Apellido;
                encontrado.FechaIngreso = item.FechaIngreso;
                encontrado.IdEquipoDesarrollo  = item.IdEquipoDesarrollo;
                encontrado.IdTipoDocumento = item.IdTipoDocumento;
                encontrado.NumeroDocumento = item.NumeroDocumento;
                encontrado.IdRol = item.IdRol;
                encontrado.PerfilSeguridad = item.PerfilSeguridad;
                encontrado.CorreoElectronico = item.CorreoElectronico;

                try
                {
                    _contexto.Entry(encontrado).State = EntityState.Modified;
                    await _contexto.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Error: No se pudo actualizar el registro: {ex.Message}");
                    return null;
                }
                return encontrado;
            }
            else
            {
                Console.WriteLine($"Error: No se pudo encontrar el registro Id: {item.IdRecurso}");
                return null;
            }
        }
    }
}
