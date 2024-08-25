using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Taxation.DAL.Context
{
    public class TaxDbContext : DbContext
    {
        public TaxDbContext(DbContextOptions<TaxDbContext> options):base(options)
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
    }
}
