using Microsoft.AspNetCore.Mvc;
using BeerVendor.Models;
using BeerVendor.Data.Repositories;

namespace BeerVendor.Services
{
    public class BeerService
    {
        private readonly IBeerRepository _repository;

        public BeerService(IBeerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Beer>> GetAllBeersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Beer> GetBeerByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddBeerAsync(Beer beer)
        {
            await _repository.AddAsync(beer);
        }

        public async Task UpdateBeerAsync(int id, Beer beer)
        {
            var existingBeer = await _repository.GetByIdAsync(id);

            if (existingBeer == null)
            {
                throw new ArgumentException("Beer not found");
            }

            existingBeer.Name = beer.Name;
            existingBeer.AlcoholContent = beer.AlcoholContent;

            await _repository.UpdateAsync(existingBeer);
        }

        public async Task DeleteBeerAsync(int id)
        {
            var existingBeer = await _repository.GetByIdAsync(id);

            if (existingBeer == null)
            {
                throw new ArgumentException("Beer not found");
            }

            await _repository.DeleteAsync(existingBeer.Id);
        }
    }
}
