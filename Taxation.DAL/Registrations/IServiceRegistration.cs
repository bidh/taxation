using Microsoft.Extensions.DependencyInjection;

namespace Taxation.DAL.Registrations
{
    public interface IServiceRegistration
    {
        void ConfigureServices(IServiceCollection services);
    }
}
