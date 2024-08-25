using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Taxation.DAL.Helpers;

namespace Taxation.DAL.Context
{
    public class TaxDbContext : DbContext
    {
        public TaxDbContext(DbContextOptions<TaxDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DbConnection"));
        }
        public DbSet<Models.Municipality> Municipalities { get; set; }
        public DbSet<Models.MonthlyTax> MonthlyTaxes { get; set; }
        public DbSet<Models.Yearlytax> YearlyTaxes { get; set; }
        public DbSet<Models.DailyTax> DailyTaxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AddMunicipalityData(modelBuilder);
            AddYearlyTaxData(modelBuilder);
            AddMonthlyTaxData(modelBuilder);
            AddDailyTaxData(modelBuilder);
        }

        private static void AddMunicipalityData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Municipality>().HasData(
                               new Models.Municipality { Id = 1, Name = "Copenhagen" }
                               );
        }

        private static void AddYearlyTaxData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Yearlytax>().HasData(
                               new Models.Yearlytax
                               {
                                   Id = 1,
                                   MunicipalityId = 1,
                                   Tax = 0.2f,
                                   StartDate = DateTimeHelper.ConvertToDateTimeOffset("January 1, 2024"),
                                   EndDate = DateTimeHelper.ConvertToDateTimeOffset("December 31, 2024")
                               });
        }

        private static void AddMonthlyTaxData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.MonthlyTax>().HasData(
                                new Models.MonthlyTax
                                {
                                    Id = 1,
                                    MunicipalityId = 1,
                                    Tax = 0.4f,
                                    StartDate = DateTimeHelper.ConvertToDateTimeOffset("May 1, 2024"),
                                    EndDate = DateTimeHelper.ConvertToDateTimeOffset("May 31, 2024")
                                });
        }

        private static void AddDailyTaxData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.DailyTax>().HasData(
                                new Models.DailyTax
                                {
                                    Id = 1,
                                    MunicipalityId = 1,
                                    Tax = 0.1f,
                                    Date = DateTimeHelper.ConvertToDateTimeOffset("January 1, 2024")
                                },
                                new Models.DailyTax
                                {
                                    Id = 2,
                                    MunicipalityId = 1,
                                    Tax = 0.1f,
                                    Date = DateTimeHelper.ConvertToDateTimeOffset("December 25, 2024")
                                });
        }
    }
}
