using Taxation.DAL.Models;

namespace Taxation.DAL.Services
{
    public interface ITaxService
    {
        public Task<decimal> GetTax(string municipality, DateTimeOffset date);
        public Task<bool> CreateYearlyTax(Yearlytax yearlyTax);
        public Task<bool> CreateMonthlyTax(MonthlyTax monthlyTax);
        public Task<bool> CreateDailyTax(DailyTax dailyTax);
    }
}
