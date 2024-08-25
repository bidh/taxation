using Taxation.API.Models;

namespace Taxation.API.Managers
{
    public interface ITaxManager
    {
        public Task<decimal> GetTax(string municipality, DateTimeOffset date);
        public Task<bool> CreateYearlyTax(YearlyTaxRequest yearlyTax);
        public Task<bool> CreateMonthlyTax(MonthlyTaxRequest monthlyTax);
        public Task<bool> CreateDailyTax(DailyTaxRequest dailyTax);
    }
}
