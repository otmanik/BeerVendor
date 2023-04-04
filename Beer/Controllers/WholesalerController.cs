using BeerVendor.Models;
using BeerVendor.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeerVendor.Controllers
{
    public class WholesalerController : Controller
    {
        private readonly WholesalerStockService _wholesalerService;
        private readonly BeerService _BeerService;

        public WholesalerController(WholesalerStockService wholesalerService, BeerService beerService)
        {
            _wholesalerService = wholesalerService;
            _BeerService = beerService;
        }
        [HttpPost("{wholesalerId}/beers/{beerId}/sales")]
        public async Task<IActionResult> AddBeerSaleToWholesaler(int wholesalerId, int beerId, int quantity)
        {
            try
            {
                await _wholesalerService.AddBeerSaleToWholesalerAsync(wholesalerId, beerId, quantity);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{wholesalerId}/quote")]
        public async Task<ActionResult<QuoteDto>> RequestQuote(int wholesalerId, [FromBody] QuoteRequestDto quoteRequestDto)
        {
            try
            {
                var quoteDto = await _wholesalerService.RequestQuoteAsync(wholesalerId, quoteRequestDto);
                return Ok(quoteDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
