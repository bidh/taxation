using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Taxation.API.Controllers;
using Taxation.API.Managers;
using Taxation.API.Models;

namespace Taxation.UnitTest
{
    public class TaxControllerTests
    {
        private readonly Mock<ITaxManager> _taxManagerMock;
        private readonly Mock<ILogger<TaxController>> _loggerMock;
        private readonly TaxController _taxController;

        public TaxControllerTests()
        {
            _taxManagerMock = new Mock<ITaxManager>();
            _loggerMock = new Mock<ILogger<TaxController>>();
            _taxController = new TaxController(_loggerMock.Object, _taxManagerMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult()
        {
            // Arrange
            string municipality = "Copenhagen";
            DateTimeOffset date = DateTimeOffset.Now;
            decimal expectedTax = 10.0m;
            _taxManagerMock.Setup(x => x.GetTax(municipality, date)).ReturnsAsync(expectedTax);

            // Act
            var result = await _taxController.Get(municipality, date);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(expectedTax, okResult.Value);
        }

        [Fact]
        public async Task Get_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            string municipality = "Copenhagen";
            DateTimeOffset date = DateTimeOffset.Now;
            _taxManagerMock.Setup(x => x.GetTax(municipality, date)).ThrowsAsync(new Exception());

            // Act
            var result = await _taxController.Get(municipality, date);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            var statusCodeResult = result as StatusCodeResult;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CreateYearlyTax_ReturnsCreatedResult()
        {
            // Arrange
            var yearlyTax = new YearlyTaxRequest();
            _taxManagerMock.Setup(x => x.CreateYearlyTax(yearlyTax)).ReturnsAsync(true);

            // Act
            var result = await _taxController.CreateYearlyTax(yearlyTax);

            // Assert
            Assert.IsType<CreatedResult>(result);
            var createdResult = result as CreatedResult;
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async Task CreateYearlyTax_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var yearlyTax = new YearlyTaxRequest();
            _taxManagerMock.Setup(x => x.CreateYearlyTax(yearlyTax)).ThrowsAsync(new Exception());

            // Act
            var result = await _taxController.CreateYearlyTax(yearlyTax);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            var statusCodeResult = result as StatusCodeResult;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CreateMonthlyTax_ReturnsCreatedResult()
        {
            // Arrange
            var monthlyTax = new MonthlyTaxRequest();
            _taxManagerMock.Setup(x => x.CreateMonthlyTax(monthlyTax)).ReturnsAsync(true);

            // Act
            var result = await _taxController.CreateMonthlyTax(monthlyTax);

            // Assert
            Assert.IsType<CreatedResult>(result);
            var createdResult = result as CreatedResult;
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async Task CreateMonthlyTax_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var monthlyTax = new MonthlyTaxRequest();
            _taxManagerMock.Setup(x => x.CreateMonthlyTax(monthlyTax)).ThrowsAsync(new Exception());

            // Act
            var result = await _taxController.CreateMonthlyTax(monthlyTax);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            var statusCodeResult = result as StatusCodeResult;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CreateDailyTax_ReturnsCreatedResult()
        {
            // Arrange
            var dailyTax = new DailyTaxRequest();
            _taxManagerMock.Setup(x => x.CreateDailyTax(dailyTax)).ReturnsAsync(true);

            // Act
            var result = await _taxController.CreateDailyTax(dailyTax);

            // Assert
            Assert.IsType<CreatedResult>(result);
            var createdResult = result as CreatedResult;
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async Task CreateDailyTax_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var dailyTax = new DailyTaxRequest();
            _taxManagerMock.Setup(x => x.CreateDailyTax(dailyTax)).ThrowsAsync(new Exception());

            // Act
            var result = await _taxController.CreateDailyTax(dailyTax);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            var statusCodeResult = result as StatusCodeResult;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
