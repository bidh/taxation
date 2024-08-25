using Microsoft.Extensions.DependencyInjection;
using Taxation.DAL.Services;

namespace Taxation.DAL.Registrations
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITaxService, TaxService>();
        }
    }
}
