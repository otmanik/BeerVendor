using BeerVendor.Data.Repositories;
using BeerVendor.Models;
using BeerVendor.Services;
using Moq;

[TestFixture]
public class WholesalerServiceTests
{
    private Mock<IWholesalerRepository> _mockWholesalerRepository;
    private Mock<IBeerRepository> _mockBeerRepository;
    private WholesalerStockService _wholesalerService;

    [SetUp]
    public void SetUp()
    {
        _mockWholesalerRepository = new Mock<IWholesalerRepository>();
        _mockBeerRepository = new Mock<IBeerRepository>();
        _wholesalerService = new WholesalerStockService(_mockWholesalerRepository.Object, _mockBeerRepository.Object);
    }

    [Test]
    public async Task RequestQuoteAsync_WithValidData_ReturnsQuoteDto()
    {
        // Arrange
        int wholesalerId = 1;
        var quoteRequestDto = new QuoteRequestDto
        {
            BeerId = 1,
            Quantity = 20,
            WholesalerId = 1
        };
        var wholesaler = new Wholesaler
        {
            Id = wholesalerId,
            Stocks = new List<WholesalerStock>
            {
                new WholesalerStock
                {
                    BeerId = quoteRequestDto.BeerId,
                    Quantity = quoteRequestDto.Quantity + 10 // Add 10 more to stock 
                }
            }
        };
        _mockWholesalerRepository.Setup(r => r.GetByIdAsync(wholesalerId)).ReturnsAsync(wholesaler);
        var beers = new List<Beer>
        {
            new Beer
            {
                Id = quoteRequestDto.BeerId,
                Name = "Test Beer",
                Price = 5.0m
            }
        };
        _mockBeerRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(beers);

        // Act
        var result = await _wholesalerService.RequestQuoteAsync(wholesalerId, quoteRequestDto);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(quoteRequestDto.Quantity * 5.0 * 0.9, result.Price);
        Assert.AreEqual($"{quoteRequestDto.Quantity} x Test Beer: {quoteRequestDto.Quantity * 5.0 * 0.9:0.00}", result.Summary);
    }

    [Test]
    public void RequestQuoteAsync_WithNullQuoteRequestDto_ThrowsArgumentException()
    {
        // Arrange
        int wholesalerId = 1;
        QuoteRequestDto quoteRequestDto = null;

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => _wholesalerService.RequestQuoteAsync(wholesalerId, quoteRequestDto));
    }

    [Test]
    public void RequestQuoteAsync_WithNonExistentWholesaler_ThrowsArgumentException()
    {
        // Arrange
        int wholesalerId = 1;
        var quoteRequestDto = new QuoteRequestDto
        {
            BeerId = 1,
            Quantity = 10
        };
        _mockWholesalerRepository.Setup(r => r.GetByIdAsync(wholesalerId)).ReturnsAsync((Wholesaler)null);

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => _wholesalerService.RequestQuoteAsync(wholesalerId, quoteRequestDto));
    }

}
