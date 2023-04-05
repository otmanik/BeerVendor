using BeerVendor.Models;

namespace BeerVendor.Data.Repositories
{
    public interface IWholesalerRepository
    {
        Task<List<Wholesaler>> GetAllAsync();
        Task<WholesalerStock> GetWholesalerStockAsync(int beerId, int wholesalerId);
        Task AddAsync(WholesalerStock WholesalerStock);
        Task UpdateAsync(WholesalerStock WholesalerStock);
        Task<Wholesaler> GetByIdAsync(int id);
    }
}
