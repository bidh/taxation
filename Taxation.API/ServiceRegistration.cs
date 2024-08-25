using Taxation.API.Managers;
using Taxation.DAL.Context;
using Taxation.DAL.Registrations;

namespace Taxation.API
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TaxDbContext>();
            services.AddScoped<ITaxManager, TaxManager>();
        }
    }
}
