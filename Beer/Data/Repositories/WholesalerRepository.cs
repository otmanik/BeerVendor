using BeerVendor.Models;
using Microsoft.EntityFrameworkCore;

namespace BeerVendor.Data.Repositories
{
    public class WholesalerRepository : IWholesalerRepository
    {
        private readonly DataContext _context;

        public WholesalerRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(WholesalerStock stock)
        {
            await _context.WholesalerStocks.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Wholesaler>> GetAllAsync()
        {
            return await _context.Wholesalers.Include(b => b.Stocks).ToListAsync();
        }

        public async Task<Wholesaler> GetByIdAsync(int id)
        {
            return await _context.Wholesalers.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<WholesalerStock> GetWholesalerStockAsync(int beerId, int wholesalerId)
        {
            return await _context.WholesalerStocks.FirstOrDefaultAsync(b => b.BeerId == beerId && b.WholesalerId == wholesalerId);
        }
    }
}
