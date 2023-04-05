using BeerVendor.Models;

namespace BeerVendor.Services
{
    public interface IWholesalerStockService
    {
        Task AddBeerSaleToWholesalerAsync(int wholesalerId, int beerId, int quantity);
        Task<QuoteDto> RequestQuoteAsync(int wholesalerId, QuoteRequestDto quoteRequestDto);
    }
}
