using Microsoft.Extensions.Caching.Memory;
using Taxation.API.Models;
using Taxation.DAL.Helpers;
using Taxation.DAL.Models;
using Taxation.DAL.Services;

namespace Taxation.API.Managers
{
    public class TaxManager : ITaxManager
    {
        private readonly ITaxService _taxService;
        private readonly IMemoryCache _memoryCache;
        public TaxManager(ITaxService taxService, IMemoryCache memoryCache)
        {
            _taxService = taxService;
            _memoryCache = memoryCache;
        }

        public Task<bool> CreateDailyTax(DailyTaxRequest dailyTax, CancellationToken cancellationToken)
        {
            return _taxService.CreateDailyTax(new DailyTax
            {
                MunicipalityId = dailyTax.MunicipalityId,
                Tax = dailyTax.Tax,
                Date = DateTimeHelper.ConvertToDateTimeOffset(dailyTax.Date)
            }, cancellationToken);
        }

        public Task<bool> CreateMonthlyTax(MonthlyTaxRequest monthlyTax, CancellationToken cancellationToken)
        {
            return _taxService.CreateMonthlyTax(new MonthlyTax
            {
                MunicipalityId = monthlyTax.MunicipalityId,
                Tax = monthlyTax.Tax,
                StartDate = DateTimeHelper.ConvertToDateTimeOffset(monthlyTax.StartDate),
                EndDate = DateTimeHelper.ConvertToDateTimeOffset(monthlyTax.EndDate)
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
                StartDate = DateTimeHelper.ConvertToDateTimeOffset(yearlyTax.StartDate),
                EndDate = DateTimeHelper.ConvertToDateTimeOffset(yearlyTax.EndDate)
            }, cancellationToken);
        }

        public async Task<float?> GetTax(string name, DateTimeOffset date, CancellationToken cancellationToken)
        {
            string cacheKey = $"{name}_{date}"; 

            return await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                var municipality = await _taxService.GetMunicipalityAsync(name, cancellationToken);

                if (municipality == null)
                {
                    return null;
                }

                float? tax = null;

                var dailyTax = await _taxService.GetDailyTaxAsync(municipality, date, cancellationToken);

                if (dailyTax != null)
                {
                    tax = dailyTax.Tax;
                }
                else
                {
                    var monthlyTax = await _taxService.GetMonthlyTaxAsync(municipality, date, cancellationToken);

                    if (monthlyTax != null)
                    {
                        tax = monthlyTax.Tax;
                    }
                    else
                    {
                        var yearlyTax = await _taxService.GetYearlytaxAsync(municipality, date, cancellationToken);

                        if (yearlyTax != null)
                        {
                            tax = yearlyTax.Tax;
                        }
                    }
                }

                if (tax == null)
                {
                    return null;
                }
                return tax;
            });
        }
    }
}
