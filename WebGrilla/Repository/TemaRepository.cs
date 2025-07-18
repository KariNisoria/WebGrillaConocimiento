using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public class TemaRepository : IRepository<Tema>
    {
        private readonly AppDbContext _context;

        public TemaRepository(AppDbContext context)
        {
           _context = context;
        }

        public async Task<Tema> AddAsync(Tema item)
        {
            var nuevo = await _context.Temas.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var encontrado = await _context.Temas.FirstOrDefaultAsync(x=>x.IdTema==id);
            if (encontrado != null)
            {
                _context.Temas.Remove(encontrado);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Tema>> GetAllAsync()
        {
            var resultado = await _context.Temas.ToListAsync();
            return resultado;
        }

        public async Task<Tema> GetByIdAsync(int id)
        {
            var resultado = await _context.Temas.FirstOrDefaultAsync(x=>x.IdTema==id);
            return resultado;
        }

        public async Task<Tema> UpdateAsync(Tema item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
