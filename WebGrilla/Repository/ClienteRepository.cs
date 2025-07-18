using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public class ClienteRepository : IRepository<Cliente>
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
               _context = context;
        }

        public async Task<Cliente> AddAsync(Cliente item)
        {
            try
            {
                await _context.Clientes.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: No se pudo registrar el cliente: {ex.Message}");
                return null;
            }
            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var encontrado = await _context.Clientes.FirstOrDefaultAsync(x=>x.IdCliente == id); 
            if (encontrado != null)
            {
                try
                {
                    _context.Clientes.Remove(encontrado);
                    await _context.SaveChangesAsync();

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Error: No se pudo eliminar el cliente: {ex.Message}");
                    return false;
   
                }
                return true;
            }
            else
            {
                Console.WriteLine($"Error: No se encontro el cliente con id {id}");
                return false;
            }
            
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            var resultado = await _context.Clientes.ToListAsync();
            return resultado;
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            var resultado = await _context.Clientes.FirstOrDefaultAsync(_ => _.IdCliente == id);
            return resultado;
        }

        public async Task<Cliente> UpdateAsync(Cliente item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: No se pudo actualizar el cliente: {ex.Message}");
                return null;
            }
            return item;
        }
    }
}
