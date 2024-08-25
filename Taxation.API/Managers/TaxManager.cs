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

        public Task<bool> CreateDailyTax(DailyTaxRequest dailyTax, CancellationToken cancellationToken)
        {
            return _taxService.CreateDailyTax(new DailyTax
            {
                MunicipalityId = dailyTax.MunicipalityId,
                Tax = dailyTax.Tax,
                Date = dailyTax.Date
            }, cancellationToken);
        }

        public Task<bool> CreateMonthlyTax(MonthlyTaxRequest monthlyTax, CancellationToken cancellationToken)
        {
            return _taxService.CreateMonthlyTax(new MonthlyTax
            {
                MunicipalityId = monthlyTax.MunicipalityId,
                Tax = monthlyTax.Tax,
                StartDate = monthlyTax.StartDate,
                EndDate = monthlyTax.EndDate
            }, cancellationToken);
        }

        public Task<bool> CreateMunicipality(MunicipalityRequest municipality, CancellationToken cancellationToken)
        {
            return _taxService.CreateMunicipality(new Municipality
            {
                Name = municipality.Name
            }, cancellationToken);
        }

        public Task<bool> CreateYearlyTax(YearlyTaxRequest yearlyTax, CancellationToken cancellationToken)
        {
            return _taxService.CreateYearlyTax(new Yearlytax
            {
                MunicipalityId = yearlyTax.MunicipalityId,
                Tax = yearlyTax.Tax,
                StartDate = yearlyTax.StartDate,
                EndDate = yearlyTax.EndDate
            }, cancellationToken);
        }

        public async Task<float> GetTax(string municipality, DateTimeOffset date, CancellationToken cancellationToken)
        {
            return await _taxService.GetTax(municipality, date, cancellationToken);
        }
    }
}
