using System.Threading;
using Taxation.DAL.Context;
using Taxation.DAL.Models;

namespace Taxation.DAL.Services
{
    public class TaxService(TaxDbContext db) : ITaxService
    {
        private readonly TaxDbContext _db = db;
        public async Task<bool> CreateDailyTax(DailyTax dailyTax, CancellationToken cancellationToken)
        {
            using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                _db.DailyTaxes.Add(dailyTax);
                await _db.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                return false;
            }
        }

        public async Task<bool> CreateMonthlyTax(MonthlyTax monthlyTax, CancellationToken cancellationToken)
        {
            using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                _db.MonthlyTaxes.Add(monthlyTax);
                await _db.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                return false;
            }
        }

        public async Task<bool> CreateMunicipality(Municipality municipality, CancellationToken cancellationToken)
        {
            using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                _db.Municipalities.Add(municipality);
                await _db.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                return false;
            }            
        }

        public async Task<bool> CreateYearlyTax(Yearlytax yearlyTax, CancellationToken cancellationToken)
        {
            using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                _db.YearlyTaxes.Add(yearlyTax);
                await _db.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                return false;
            }
        }

        public async Task<float> GetTax(string municipality, DateTimeOffset date, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
