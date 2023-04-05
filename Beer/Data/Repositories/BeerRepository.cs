using BeerVendor.Models;
using Microsoft.EntityFrameworkCore;

namespace BeerVendor.Data.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly DataContext _context;

        public BeerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Beer>> GetAllAsync()
        {
            return await _context.Beers.Include(b => b.Brewery).ToListAsync();
        }

        public async Task<Beer> GetByIdAsync(int id)
        {
            return await _context.Beers.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Beer beer)
        {
            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Beer beer)
        {
            _context.Entry(beer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer != null)
            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();
        }
        public async Task AddWholesalerStock(WholesalerStock stock)
        {
            await _context.WholesalerStocks.AddAsync(stock);
            await _context.SaveChangesAsync();
        }
    }

}
