using Taxation.DAL.Models;

namespace Taxation.DAL.Services
{
    public interface ITaxService
    {
        public Task<float> GetTax(string municipality, DateTimeOffset date, CancellationToken cancellationToken);
        public Task<bool> CreateYearlyTax(Yearlytax yearlyTax, CancellationToken cancellationToken);
        public Task<bool> CreateMonthlyTax(MonthlyTax monthlyTax, CancellationToken cancellationToken);
        public Task<bool> CreateDailyTax(DailyTax dailyTax, CancellationToken cancellationToken);
        public Task<bool> CreateMunicipality(Municipality municipality, CancellationToken cancellationToken);    
    }
}
