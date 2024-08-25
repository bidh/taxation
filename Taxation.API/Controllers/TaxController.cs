using Microsoft.AspNetCore.Mvc;
using Taxation.API.Managers;
using Taxation.API.Models;
using Taxation.DAL.Helpers;

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
        /// <param name="municipality">Name of the municipality</param>
        /// <param name="date">Date format: January 1, 2024</param>
        [HttpGet]
        [ProducesResponseType(typeof(decimal), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromQuery]string municipality, [FromQuery]string date, CancellationToken cancellationToken)
        {
            try
            {
                if(string.IsNullOrEmpty(municipality) || string.IsNullOrEmpty(date))
                {
                    return BadRequest();
                }

                if (!DateTimeHelper.IsValidDate(date))
                {
                    return BadRequest();
                }

                var dateTimeOffset = DateTimeHelper.ConvertToDateTimeOffset(date);
                var result = await _taxManager.GetTaxAsync(municipality, dateTimeOffset, cancellationToken);
                if(result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting tax");
                return StatusCode(500);
            }            
        }

        /// <summary>
        /// Creates a municipality
        /// </summary>
        /// <param name="municipality"></param>
        [HttpPost("municipality")]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateYearlyTax([FromBody] MunicipalityRequest municipality, CancellationToken cancellationToken)
        {
            try
            {
                return Created("", await _taxManager.CreateMunicipalityAsync(municipality,cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating yearly tax");
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
        public async Task<IActionResult> CreateYearlyTax([FromBody]YearlyTaxRequest yearlyTax, CancellationToken cancellationToken)
        {
            try
            {
                return Created("", await _taxManager.CreateYearlyTaxAsync(yearlyTax, cancellationToken));
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
        public async Task<IActionResult> CreateMonthlyTax([FromBody] MonthlyTaxRequest monthlyTax, CancellationToken cancellationToken)
        {
            try
            {
                return Created("", await _taxManager.CreateMonthlyTaxAsync(monthlyTax, cancellationToken));
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
        public async Task<IActionResult> CreateDailyTax([FromBody] DailyTaxRequest daily, CancellationToken cancellationToken)
        {
            try
            {
                return Created("", await _taxManager.CreateDailyTaxAsync(daily, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating daily tax");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates yearly tax
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="yearlyTax"></param>
        [HttpPut("yearly")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateYearlyTax([FromQuery]int Id, [FromBody] YearlyTaxRequest yearlyTax, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _taxManager.UpdateYearlyTaxAsync(Id, yearlyTax, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating yearly tax");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates monthly tax
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="monthlyTax"></param>
        [HttpPut("monthly")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateMonthlyTax([FromQuery] int Id, [FromBody] MonthlyTaxRequest monthlyTax, CancellationToken cancellationToken)
        {
            try
            {   
                return Ok(await _taxManager.UpdateMonthlyTaxAsync(Id, monthlyTax, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating monthly tax");
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Updates daily tax
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="dailyTax"></param>
        [HttpPut("daily")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateDailyTax([FromQuery] int Id, [FromBody] DailyTaxRequest dailyTax, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _taxManager.UpdateDailyTaxAsync(Id, dailyTax, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating daily tax");
                return StatusCode(500);
            }
        }
    }
}
