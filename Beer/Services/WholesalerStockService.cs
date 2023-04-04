using BeerVendor.Data.Repositories;
using BeerVendor.Models;

namespace BeerVendor.Services
{
    public class WholesalerStockService
    {
        private readonly IWholesalerRepository _wholesalerRepository;
        private readonly IBeerRepository _beerRepository;

        public WholesalerStockService(IWholesalerRepository repository, IBeerRepository beerRepository)
        {
            _wholesalerRepository = repository;
            _beerRepository = beerRepository;
        }
        public async Task AddBeerSaleToWholesalerAsync(int wholesalerId, int beerId, int quantity)
        {
            var wholesaler = await _wholesalerRepository.GetByIdAsync(wholesalerId);
            var beer = await _beerRepository.GetByIdAsync(beerId);

            if (wholesaler == null)
            {
                throw new ArgumentException("Wholesaler not found.");
            }

            if (beer == null)
            {
                throw new ArgumentException("Beer not found.");
            }

            var stock = await _wholesalerRepository.GetWholesalerStockAsync(beerId, wholesalerId);

            var sale = new WholesalerStock
            {
                WholesalerId = wholesalerId,
                BeerId = beerId,
                Quantity = quantity,
            };

            await _wholesalerRepository.AddAsync(sale);

        }

        public async Task<QuoteDto> RequestQuoteAsync(int wholesalerId, QuoteRequestDto quoteRequestDto)
        {
            if (quoteRequestDto == null)
            {
                throw new ArgumentException("The order cannot be empty");
            }

            var wholesaler = await _wholesalerRepository.GetByIdAsync(wholesalerId);

            if (wholesaler == null)
            {
                throw new ArgumentException("The wholesaler must exist");
            }


            var beers = await _beerRepository.GetAllAsync();

            if (quoteRequestDto.Quantity > wholesaler.Stocks?.Where(b => b.BeerId == quoteRequestDto.BeerId)?.FirstOrDefault()?.Quantity)
            {
                throw new ArgumentException("The number of beers ordered cannot be greater than the wholesaler's stock");
            }

            var quotePrice = 0.0;
            var summary = "";

                var beer = beers.FirstOrDefault(b => b.Id == quoteRequestDto.BeerId);
                if (beer != null)
                {
                    var beerPrice = beer.Price * quoteRequestDto.Quantity;
                    if (quoteRequestDto.Quantity > 20)
                    {
                    quotePrice *= 0.8; // 20% discount
                    }
                    else if (quoteRequestDto.Quantity > 10)
                    {
                    quotePrice *= 0.9; // 10% discount
                    }
                 summary = $"{quoteRequestDto.Quantity} x {beer.Name}: {beerPrice:0.00}";
                }

            return new QuoteDto
            {
                Price = quotePrice,
                Summary = summary
            };
        }


    }
}
