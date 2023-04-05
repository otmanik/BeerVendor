using Microsoft.AspNetCore.Mvc;
using BeerVendor.Models;
using BeerVendor.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeerVendor.Services
{
    public class BeerService: IBeerService
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

        public async Task DeleteBeerAsync(int beerId)
        {
            var beer = await _repository.GetByIdAsync(beerId);

            if (beer == null)
            {
                throw new ArgumentException("Beer not found.");
            }

            await _repository.DeleteAsync(beerId);
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

        
    }
}
