using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public class TipoDocumentoRepository : IRepository<TipoDocumento>
    {
        private readonly AppDbContext _contexto;
        public TipoDocumentoRepository(AppDbContext contexto )
        {
            _contexto = contexto;
        }

        public async Task<TipoDocumento> AddAsync(TipoDocumento item)
        {
            try
            {
                await _contexto.TiposDocumentos.AddAsync(item);
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
            var encontrado = await _contexto.TiposDocumentos.FirstOrDefaultAsync(x => x.IdTipoDocumento == id);
            if (encontrado != null)
            {
                try
                {
                    _contexto.TiposDocumentos.Remove(encontrado);
                    await _contexto.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: No se pudo eliminar el registro: {ex.Message}");
                    return false;
                }
                return true;
            }
            {
                Console.WriteLine($"Error: No se pudo encontrar el registro a eliminar. Id:{id}");
                return false;
            }
        }

        public async Task<IEnumerable<TipoDocumento>> GetAllAsync()
        {
            var resultado = await _contexto.TiposDocumentos.ToListAsync();
            return resultado;
        }

        public async Task<TipoDocumento> GetByIdAsync(int id)
        {
            var resultado = await _contexto.TiposDocumentos.FirstOrDefaultAsync(x=>x.IdTipoDocumento==id);
            return resultado;
        }

        public async Task<TipoDocumento> UpdateAsync(TipoDocumento item)
        {
            var encontrado = await _contexto.TiposDocumentos.FirstOrDefaultAsync(x => x.IdTipoDocumento == item.IdTipoDocumento);
            if (encontrado != null)
            {
                try
                {
                    encontrado.Nombre = item.Nombre;
                    _contexto.Entry(encontrado).State = EntityState.Modified;
                    await _contexto.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: No se pudo actualizar elr registro: {ex.Message}");
                    return null;
                }
                return item;
            }
            else
            {
                Console.WriteLine($"Error: No se pudo encontrar el registro a actualizar. Id: {item.IdTipoDocumento}");
                return null;
            }
        }
    }
}
