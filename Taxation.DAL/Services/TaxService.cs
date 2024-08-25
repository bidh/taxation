using Taxation.DAL.Models;

namespace Taxation.DAL.Services
{
    public class TaxService : ITaxService
    {
        public Task<bool> CreateDailyTax(DailyTax dailyTax)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateMonthlyTax(MonthlyTax monthlyTax)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateYearlyTax(Yearlytax yearlyTax)
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> GetTax(string municipality, DateTimeOffset date)
        {
            throw new NotImplementedException();
        }
    }
}
