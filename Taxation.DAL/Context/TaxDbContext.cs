using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
                                   StartDate = new DateTime(2024, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc),
                                   EndDate = new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc)
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
                                    StartDate = new DateTime(2024, 05, 01, 0, 0, 0, 0, DateTimeKind.Utc),
                                    EndDate = new DateTime(2024, 05, 31, 0, 0, 0, 0, DateTimeKind.Utc)
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
                                    Date = new DateTime(2024, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc)
                                },
                                new Models.DailyTax
                                {
                                    Id = 2,
                                    MunicipalityId = 1,
                                    Tax = 0.1f,
                                    Date = new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc)
                                });
        }
    }
}
