using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Taxation.API.Controllers;
using Taxation.API.Managers;
using Taxation.API.Models;

namespace Taxation.Test
{
    [TestClass]
    public class TaxControllerTests
    {
        private Mock<ITaxManager> _taxManagerMock;
        private Mock<ILogger<TaxController>> _loggerMock;
        private TaxController _controller;

        [TestInitialize]
        public void Setup()
        {
            _taxManagerMock = new Mock<ITaxManager>();
            _loggerMock = new Mock<ILogger<TaxController>>();
            _controller = new TaxController(_loggerMock.Object, _taxManagerMock.Object);
        }

        [TestMethod]
        public async Task Get_ReturnsOkResult_WithExpectedTax()
        {
            // Arrange
            var municipality = "TestCity";
            var date = DateTimeOffset.UtcNow;
            var expectedTax = 100m;
            _taxManagerMock.Setup(x => x.GetTax(municipality, date)).ReturnsAsync(expectedTax);

            // Act
            var result = await _controller.Get(municipality, date) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedTax, result.Value);
        }

        [TestMethod]
        public async Task Get_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var municipality = "TestCity";
            var date = DateTimeOffset.UtcNow;
            _taxManagerMock.Setup(x => x.GetTax(municipality, date)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.Get(municipality, date) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            _loggerMock.Verify(x => x.LogError(It.IsAny<Exception>(), "An error occurred while getting tax"), Times.Once);
        }

        [TestMethod]
        public async Task CreateYearlyTax_ReturnsCreatedResult()
        {
            // Arrange
            var yearlyTaxRequest = new YearlyTaxRequest { /* Initialize properties */ };
            _taxManagerMock.Setup(x => x.CreateYearlyTax(yearlyTaxRequest)).ReturnsAsync(true);

            // Act
            var result = await _controller.CreateYearlyTax(yearlyTaxRequest) as CreatedResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateYearlyTax_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var yearlyTaxRequest = new YearlyTaxRequest { /* Initialize properties */ };
            _taxManagerMock.Setup(x => x.CreateYearlyTax(yearlyTaxRequest)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.CreateYearlyTax(yearlyTaxRequest) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            _loggerMock.Verify(x => x.LogError(It.IsAny<Exception>(), "An error occurred while creating yearly tax"), Times.Once);
        }

        [TestMethod]
        public async Task CreateMonthlyTax_ReturnsCreatedResult()
        {
            // Arrange
            var monthlyTaxRequest = new MonthlyTaxRequest { /* Initialize properties */ };
            _taxManagerMock.Setup(x => x.CreateMonthlyTax(monthlyTaxRequest)).ReturnsAsync(true);

            // Act
            var result = await _controller.CreateMonthlyTax(monthlyTaxRequest) as CreatedResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateMonthlyTax_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var monthlyTaxRequest = new MonthlyTaxRequest { /* Initialize properties */ };
            _taxManagerMock.Setup(x => x.CreateMonthlyTax(monthlyTaxRequest)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.CreateMonthlyTax(monthlyTaxRequest) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            _loggerMock.Verify(x => x.LogError(It.IsAny<Exception>(), "An error occurred while creating monthly tax"), Times.Once);
        }

        [TestMethod]
        public async Task CreateDailyTax_ReturnsCreatedResult()
        {
            // Arrange
            var dailyTaxRequest = new DailyTaxRequest { /* Initialize properties */ };
            _taxManagerMock.Setup(x => x.CreateDailyTax(dailyTaxRequest)).ReturnsAsync(true);

            // Act
            var result = await _controller.CreateDailyTax(dailyTaxRequest) as CreatedResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateDailyTax_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var dailyTaxRequest = new DailyTaxRequest { /* Initialize properties */ };
            _taxManagerMock.Setup(x => x.CreateDailyTax(dailyTaxRequest)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.CreateDailyTax(dailyTaxRequest) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            _loggerMock.Verify(x => x.LogError(It.IsAny<Exception>(), "An error occurred while creating daily tax"), Times.Once);
        }
    }
}