using Microsoft.AspNetCore.Mvc;
using Taxation.API.Managers;
using Taxation.API.Models;

namespace Taxation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController(ILogger<TaxController> logger, ITaxManager taxManager) : ControllerBase
    {
        private readonly ITaxManager _taxManager = taxManager;
        private readonly ILogger<TaxController> _logger = logger;


        /// <summary>
        /// Retrieves the tax for a given municipality and date
        /// </summary>
        /// <param name="municipality"></param>
        /// <param name="date"></param>
        [HttpGet]
        [ProducesResponseType(typeof(decimal), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(string municipality, DateTimeOffset date)
        {
            try
            {
                return Ok(await _taxManager.GetTax(municipality,date));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting tax");
                return StatusCode(500);
            }            
        }

        /// <summary>
        /// Creates a yearly tax
        /// </summary>
        /// <param name="yearlyTax"></param>
        [HttpPost("yearly")]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateYearlyTax(YearlyTax yearlyTax)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating yearly tax");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates a monthly tax
        /// </summary>
        /// <param name="monthlyTax"></param>
        [HttpPost("monthly")]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateMonthlyTax(MonthlyTax monthlyTax)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating monthly tax");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates a daily tax
        /// </summary>
        /// <param name="daily"></param>
        [HttpPost("daily")]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateDailyTax(DailyTax daily)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating daily tax");
                return StatusCode(500);
            }
        }
    }
}
