using BeerVendor.Controllers;
using BeerVendor.Models;
using BeerVendor.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace BeerVendor.UnitTests.Controllers
{
    [TestFixture]
    public class WholesalerControllerTests
    {
        private WholesalerController _wholesalerController;
        private Mock<IWholesalerStockService> _mockWholesalerStockService;

        [SetUp]
        public void Setup()
        {
            _mockWholesalerStockService = new Mock<IWholesalerStockService>();
            _wholesalerController = new WholesalerController(_mockWholesalerStockService.Object, null);
        }

        [Test]
        public async Task RequestQuote_WithValidRequest_ReturnsOk()
        {
            // Arrange
            var quoteRequestDto = new QuoteRequestDto
            {
                WholesalerId = 1,
                BeerId = 1,
                Quantity = 10
            };

            var expectedQuoteDto = new QuoteDto
            {
                Price = 100
            };

            _mockWholesalerStockService.Setup(x => x.RequestQuoteAsync(quoteRequestDto.WholesalerId, quoteRequestDto)).ReturnsAsync(expectedQuoteDto);

            // Act
            var result = await _wholesalerController.RequestQuote(quoteRequestDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(expectedQuoteDto, okResult.Value);
        }

        [Test]
        public async Task RequestQuote_WithInvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var requestDto = new QuoteRequestDto
            {
                WholesalerId = 1,
                BeerId = 1,
                Quantity = -1 // invalid quantity
            };
            var errorMessage = "Invalid request";
            _mockWholesalerStockService
                .Setup(s => s.RequestQuoteAsync(requestDto.WholesalerId, requestDto))
                .ThrowsAsync(new ArgumentException(errorMessage));

            // Act
            var result = await _wholesalerController.RequestQuote(requestDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var badRequestResult = (BadRequestObjectResult)result.Result;
            Assert.AreEqual(errorMessage, badRequestResult.Value);
        }
    }
}