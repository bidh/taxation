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

        public async Task<bool> CreateDailyTaxAsync(DailyTaxRequest dailyTax, CancellationToken cancellationToken)
        {
            return await _taxService.CreateDailyTaxAsync(new DailyTax
            {
                MunicipalityId = dailyTax.MunicipalityId,
                Tax = dailyTax.Tax,
                Date = DateTimeHelper.ConvertToDateTimeOffset(dailyTax.Date)
            }, cancellationToken);
        }

        public async Task<bool> CreateMonthlyTaxAsync(MonthlyTaxRequest monthlyTax, CancellationToken cancellationToken)
        {
            return await _taxService.CreateMonthlyTaxAsync(new MonthlyTax
            {
                MunicipalityId = monthlyTax.MunicipalityId,
                Tax = monthlyTax.Tax,
                StartDate = DateTimeHelper.ConvertToDateTimeOffset(monthlyTax.StartDate),
                EndDate = DateTimeHelper.ConvertToDateTimeOffset(monthlyTax.EndDate)
            }, cancellationToken);
        }

        public async Task<bool> CreateMunicipalityAsync(MunicipalityRequest municipality, CancellationToken cancellationToken)
        {
            var existingMunicipality = await _taxService.GetMunicipalityAsync(municipality.Name, cancellationToken);
            if(existingMunicipality != null)
            {
                return false;
            }
            return await _taxService.CreateMunicipalityAsync(new Municipality
            {
                Name = municipality.Name
            }, cancellationToken);
        }

        public async Task<bool> CreateYearlyTaxAsync(YearlyTaxRequest yearlyTax, CancellationToken cancellationToken)
        {
            return await _taxService.CreateYearlyTaxAsync(new Yearlytax
            {
                MunicipalityId = yearlyTax.MunicipalityId,
                Tax = yearlyTax.Tax,
                StartDate = DateTimeHelper.ConvertToDateTimeOffset(yearlyTax.StartDate),
                EndDate = DateTimeHelper.ConvertToDateTimeOffset(yearlyTax.EndDate)
            }, cancellationToken);
        }

        public async Task<float?> GetTaxAsync(string name, DateTimeOffset date, CancellationToken cancellationToken)
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

        public async Task<bool> UpdateDailyTaxAsync(int id, DailyTaxRequest dailyTax, CancellationToken cancellationToken)
        {
            return await _taxService.UpdateDailyTaxAsync(new DailyTax
            {
                Id = id,
                MunicipalityId = dailyTax.MunicipalityId,
                Tax = dailyTax.Tax,
                Date = DateTimeHelper.ConvertToDateTimeOffset(dailyTax.Date)
            }, cancellationToken);
        }

        public async Task<bool> UpdateMonthlyTaxAsync(int id, MonthlyTaxRequest monthlyTax, CancellationToken cancellationToken)
        {
            return await _taxService.UpdateMonthlyTaxAsync(new MonthlyTax
            {
                Id = id,
                MunicipalityId = monthlyTax.MunicipalityId,
                Tax = monthlyTax.Tax,
                StartDate = DateTimeHelper.ConvertToDateTimeOffset(monthlyTax.StartDate),
                EndDate = DateTimeHelper.ConvertToDateTimeOffset(monthlyTax.EndDate)
            }, cancellationToken);
        }

        public async Task<bool> UpdateYearlyTaxAsync(int id, YearlyTaxRequest yearlyTax, CancellationToken cancellationToken)
        {
            return await _taxService.UpdateYearlyTaxAsync(new Yearlytax
            {
                Id = id,
                MunicipalityId = yearlyTax.MunicipalityId,
                Tax = yearlyTax.Tax,
                StartDate = DateTimeHelper.ConvertToDateTimeOffset(yearlyTax.StartDate),
                EndDate = DateTimeHelper.ConvertToDateTimeOffset(yearlyTax.EndDate)
            }, cancellationToken);
        }
    }
}
