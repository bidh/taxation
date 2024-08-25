using Taxation.DAL.Models;

namespace Taxation.DAL.Services
{
    public interface ITaxService
    {
        public Task<bool> CreateYearlyTax(Yearlytax yearlyTax, CancellationToken cancellationToken);
        public Task<bool> CreateMonthlyTax(MonthlyTax monthlyTax, CancellationToken cancellationToken);
        public Task<bool> CreateDailyTax(DailyTax dailyTax, CancellationToken cancellationToken);
        public Task<bool> CreateMunicipality(Municipality municipality, CancellationToken cancellationToken);
        public Task<Municipality?> GetMunicipalityAsync(string name, CancellationToken cancellationToken);
        public Task<DailyTax?> GetDailyTaxAsync(Municipality municipality, DateTimeOffset date, CancellationToken cancellationToken);
        public Task<Yearlytax?> GetYearlytaxAsync(Municipality municipality, DateTimeOffset date, CancellationToken cancellationToken);
        public Task<MonthlyTax?> GetMonthlyTaxAsync(Municipality municipality, DateTimeOffset date, CancellationToken cancellationToken);
    }
}
