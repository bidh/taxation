using Taxation.API.Models;

namespace Taxation.API.Managers
{
    public interface ITaxManager
    {
        public Task<float?> GetTax(string name, DateTimeOffset date, CancellationToken cancellationToken);
        public Task<bool> CreateYearlyTax(YearlyTaxRequest yearlyTax, CancellationToken cancellationToken);
        public Task<bool> CreateMonthlyTax(MonthlyTaxRequest monthlyTax, CancellationToken cancellationToken);
        public Task<bool> CreateDailyTax(DailyTaxRequest dailyTax, CancellationToken cancellationToken);
        public Task<bool> CreateMunicipality(MunicipalityRequest municipality, CancellationToken cancellationToken);
    }
}
