using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System.Globalization;
using System.Net;
using Taxation.API;
using Taxation.API.Controllers;
using Taxation.API.Managers;
using Taxation.API.Models;
using Taxation.DAL.Context;
using Taxation.DAL.Helpers;
using Taxation.DAL.Models;

namespace Taxation.UnitTest
{
    public class TaxControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly Mock<ITaxManager> _taxManagerMock;
        private readonly Mock<ILogger<TaxController>> _loggerMock;
        private readonly TaxController _taxController;
        readonly CancellationToken cancellationToken = CancellationToken.None;
        private readonly WebApplicationFactory<Program> _factory;
        public TaxControllerTests(WebApplicationFactory<Program> factory)
        {
            _taxManagerMock = new Mock<ITaxManager>();
            _loggerMock = new Mock<ILogger<TaxController>>();
            _taxController = new TaxController(_loggerMock.Object, _taxManagerMock.Object);
            _factory = factory;
        }

        [Theory]
        [InlineData("Copenhagen", "January 1, 2024", 0.1f)]
        [InlineData("Copenhagen", "March 16, 2024", 0.2f)]
        [InlineData("Copenhagen", "May 2, 2024", 0.4f)]
        [InlineData("Copenhagen", "July 10, 2024", 0.2f)]
        public async Task Get_Tax_Returns_Correct_Tax(string municipality, string date, float expectedTax)
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<TaxDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });
                });
            }).CreateClient();

            // Create test data
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TaxDbContext>();
                dbContext.Municipalities.Add(new Municipality { Name = "Copenhagen" });
                dbContext.YearlyTaxes.Add(new Yearlytax { MunicipalityId = 1, StartDate = DateTimeHelper.ConvertToDateTimeOffset("January 1, 2024"), EndDate = DateTimeHelper.ConvertToDateTimeOffset("December 31, 2024"), Tax = 0.2f });
                dbContext.MonthlyTaxes.Add(new MonthlyTax { MunicipalityId = 1, StartDate = DateTimeHelper.ConvertToDateTimeOffset("May 1, 2024"), EndDate = DateTimeHelper.ConvertToDateTimeOffset("May 31, 2024"), Tax = 0.4f });                
                dbContext.DailyTaxes.Add(new DailyTax { MunicipalityId = 1, Date = DateTimeHelper.ConvertToDateTimeOffset("January 1, 2024"), Tax = 0.1f });
                dbContext.DailyTaxes.Add(new DailyTax { MunicipalityId = 1, Date = DateTimeHelper.ConvertToDateTimeOffset("December 25, 2024"), Tax = 0.1f });
                await dbContext.SaveChangesAsync();
            }

            // Act
            var response = await client.GetAsync($"/api/tax?municipality={municipality}&date={date}");
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var floatResult = float.Parse(result, CultureInfo.InvariantCulture);  
            Assert.Equal(expectedTax, floatResult);
        }

        [Fact]
        public async Task Get_ReturnsOkResult()
        {
            // Arrange
            string municipality = "Copenhagen";
            string dateString = "August 8, 2024";
            DateTimeOffset date = DateTimeHelper.ConvertToDateTimeOffset(dateString);
            float expectedTax = 10.0f;
            _taxManagerMock.Setup(x => x.GetTax(municipality, date, cancellationToken)).ReturnsAsync(expectedTax);

            // Act
            var result = await _taxController.Get(municipality, dateString, cancellationToken);

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
            string dateString = "August 8, 2024";
            DateTimeOffset date = DateTimeHelper.ConvertToDateTimeOffset(dateString);
            _taxManagerMock.Setup(x => x.GetTax(municipality, date, cancellationToken)).ThrowsAsync(new Exception());

            // Act
            var result = await _taxController.Get(municipality, dateString, cancellationToken);

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
            _taxManagerMock.Setup(x => x.CreateYearlyTax(yearlyTax, cancellationToken)).ReturnsAsync(true);

            // Act
            var result = await _taxController.CreateYearlyTax(yearlyTax, cancellationToken);

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
            _taxManagerMock.Setup(x => x.CreateYearlyTax(yearlyTax, new CancellationToken())).ThrowsAsync(new Exception());

            // Act
            var result = await _taxController.CreateYearlyTax(yearlyTax, cancellationToken);

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
            _taxManagerMock.Setup(x => x.CreateMonthlyTax(monthlyTax, cancellationToken)).ReturnsAsync(true);

            // Act
            var result = await _taxController.CreateMonthlyTax(monthlyTax, cancellationToken);

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
            _taxManagerMock.Setup(x => x.CreateMonthlyTax(monthlyTax, cancellationToken)).ThrowsAsync(new Exception());

            // Act
            var result = await _taxController.CreateMonthlyTax(monthlyTax, cancellationToken);

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
            _taxManagerMock.Setup(x => x.CreateDailyTax(dailyTax, cancellationToken)).ReturnsAsync(true);

            // Act
            var result = await _taxController.CreateDailyTax(dailyTax, cancellationToken);

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
            _taxManagerMock.Setup(x => x.CreateDailyTax(dailyTax, cancellationToken)).ThrowsAsync(new Exception());

            // Act
            var result = await _taxController.CreateDailyTax(dailyTax, cancellationToken);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            var statusCodeResult = result as StatusCodeResult;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
