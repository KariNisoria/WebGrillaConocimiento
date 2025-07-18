using WebGrilla.Models;

namespace WebGrilla.Repository
{
    public interface IRepository<T>
    {
            Task<IEnumerable<T>> GetAllAsync();
            Task<T> GetByIdAsync(int id);
            Task<T> AddAsync(T item);
            Task<T> UpdateAsync(T item);
            Task<bool> DeleteAsync(int id);
    }
}
