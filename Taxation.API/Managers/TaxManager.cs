using Taxation.API.Models;
using Taxation.DAL.Models;
using Taxation.DAL.Services;

namespace Taxation.API.Managers
{
    public class TaxManager : ITaxManager
    {
        private readonly ITaxService _taxService;
        public TaxManager(ITaxService taxService)
        {
            _taxService = taxService;
        }

        public Task<bool> CreateDailyTax(DailyTaxRequest dailyTax)
        {
            return _taxService.CreateDailyTax(new DailyTax
            {
                MunicipalityId = dailyTax.MunicipalityId,
                Tax = dailyTax.Tax,
                Date = dailyTax.Date
            });
        }

        public Task<bool> CreateMonthlyTax(MonthlyTaxRequest monthlyTax)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateYearlyTax(YearlyTaxRequest yearlyTax)
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> GetTax(string municipality, DateTimeOffset date)
        {
            return await _taxService.GetTax(municipality, date);
        }
    }
}
