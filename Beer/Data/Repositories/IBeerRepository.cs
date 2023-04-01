using BeerVendor.Models;

namespace BeerVendor.Data.Repositories
{
    public interface IBeerRepository
    {
        Task<List<Beer>> GetAllAsync();
        Task<Beer> GetByIdAsync(int id);
        Task AddAsync(Beer beer);
        Task UpdateAsync(Beer beer);
        Task DeleteAsync(int id);
    }
}
