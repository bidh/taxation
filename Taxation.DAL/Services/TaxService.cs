﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<Municipality?> GetMunicipalityAsync(string name, CancellationToken cancellationToken)
        {
            return await _db.Municipalities.FirstOrDefaultAsync(x => x.Name.Equals(name), cancellationToken: cancellationToken);
        }

        public async Task<DailyTax?> GetDailyTaxAsync(Municipality municipality, DateTimeOffset date, CancellationToken cancellationToken)
        {
           return await _db.DailyTaxes.FirstOrDefaultAsync(x => x.MunicipalityId == municipality.Id && x.Date == date, cancellationToken: cancellationToken);
        }

        public async Task<Yearlytax?> GetYearlytaxAsync(Municipality municipality, DateTimeOffset date, CancellationToken cancellationToken)
        {
            return await _db.YearlyTaxes.FirstOrDefaultAsync(x => x.MunicipalityId == municipality.Id && x.StartDate <= date && x.EndDate >= date, cancellationToken: cancellationToken);
        }

        public async Task<MonthlyTax?> GetMonthlyTaxAsync(Municipality municipality, DateTimeOffset date, CancellationToken cancellationToken)
        {
            return await _db.MonthlyTaxes.FirstOrDefaultAsync(x => x.MunicipalityId == municipality.Id && x.StartDate <= date && x.EndDate >= date, cancellationToken: cancellationToken);
        }
    }
}
