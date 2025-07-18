using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public class SubtemaRepository : ISubtemaRepository
    {
        private readonly AppDbContext _context;
        public SubtemaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Subtema> AddAsync(Subtema item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var encontrado = await _context.Subtemas.FirstOrDefaultAsync(x => x.IdSubtema == id);
            if (encontrado != null)
            {
                _context.Subtemas.Remove(encontrado);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Subtema>> GetAllAsync()
        {
            var resultado = await _context.Subtemas.ToListAsync();
            return resultado;
        }

        public async Task<Subtema> GetByIdAsync(int id)
        {
            var resultado = await _context.Subtemas.FirstOrDefaultAsync(x => x.IdSubtema == id);
            return resultado;
        }

        public async Task<IEnumerable<Subtema>> GetSubtemaByIdTema(int id)
        {
            var resultado = await _context.Subtemas.Where(x=>x.IdTema == id).ToListAsync(); 
            return resultado;
        }

        public async Task<bool> HasSutemaByIdTema(int id)
        {
            return await _context.Subtemas.AnyAsync(x => x.IdTema == id);
        }

        public async Task<Subtema> UpdateAsync(Subtema item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;    
        }
    }
}
