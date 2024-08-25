using Taxation.API.Models;
using Taxation.DAL.Models;

namespace Taxation.API.Managers
{
    public interface ITaxManager
    {
        public Task<float?> GetTaxAsync(string name, DateTimeOffset date, CancellationToken cancellationToken);
        public Task<bool> CreateYearlyTaxAsync(YearlyTaxRequest yearlyTax, CancellationToken cancellationToken);
        public Task<bool> CreateMonthlyTaxAsync(MonthlyTaxRequest monthlyTax, CancellationToken cancellationToken);
        public Task<bool> CreateDailyTaxAsync(DailyTaxRequest dailyTax, CancellationToken cancellationToken);
        public Task<bool> CreateMunicipalityAsync(MunicipalityRequest municipality, CancellationToken cancellationToken);
        public Task<bool?> UpdateDailyTaxAsync(int id, DailyTaxRequest dailyTax, CancellationToken cancellationToken);
        public Task<bool?> UpdateMonthlyTaxAsync(int id, MonthlyTaxRequest monthlyTax, CancellationToken cancellationToken);
        public Task<bool?> UpdateYearlyTaxAsync(int id, YearlyTaxRequest yearlyTax, CancellationToken cancellationToken);
    }
}
