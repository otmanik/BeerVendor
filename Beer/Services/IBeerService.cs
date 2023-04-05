using BeerVendor.Models;

namespace BeerVendor.Services
{
    public interface IBeerService
    {
        Task<List<Beer>> GetAllBeersAsync();
        Task<Beer> GetBeerByIdAsync(int id);
        Task DeleteBeerAsync(int beerId);
        Task AddBeerAsync(Beer beer);
        Task UpdateBeerAsync(int id, Beer beer);
    }
}
