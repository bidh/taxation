using Taxation.DAL.Models;

namespace Taxation.DAL.Services
{
    public interface ITaxService
    {
        public Task<bool> CreateYearlyTaxAsync(Yearlytax yearlyTax, CancellationToken cancellationToken);
        public Task<bool> CreateMonthlyTaxAsync(MonthlyTax monthlyTax, CancellationToken cancellationToken);
        public Task<bool> CreateDailyTaxAsync(DailyTax dailyTax, CancellationToken cancellationToken);
        public Task<bool> CreateMunicipalityAsync(Municipality municipality, CancellationToken cancellationToken);
        public Task<Municipality?> GetMunicipalityAsync(string name, CancellationToken cancellationToken);
        public Task<DailyTax?> GetDailyTaxAsync(Municipality municipality, DateTimeOffset date, CancellationToken cancellationToken);
        public Task<Yearlytax?> GetYearlytaxAsync(Municipality municipality, DateTimeOffset date, CancellationToken cancellationToken);
        public Task<MonthlyTax?> GetMonthlyTaxAsync(Municipality municipality, DateTimeOffset date, CancellationToken cancellationToken);
        public Task<DailyTax?> GetDailyTaxByIdAsync(int Id, CancellationToken cancellationToken);
        public Task<Yearlytax?> GetYearlytaxByIdAsync(int Id, CancellationToken cancellationToken);
        public Task<MonthlyTax?> GetMonthlyTaxByIdAsync(int Id, CancellationToken cancellationToken);
        public Task<bool?> UpdateDailyTaxAsync(DailyTax dailyTax, CancellationToken cancellationToken);
        public Task<bool?> UpdateMonthlyTaxAsync(MonthlyTax monthlyTax, CancellationToken cancellationToken);
        public Task<bool?> UpdateYearlyTaxAsync(Yearlytax yearlyTax, CancellationToken cancellationToken);
    }
}
