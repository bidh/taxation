using Taxation.DAL.Registrations;

namespace Taxation.API
{
    public class Program
    {
        private static readonly IReadOnlyList<IServiceRegistration> ProjectDependencies =
            [
             new ServiceRegistration(),
             new DAL.Registrations.ServiceRegistration()
            ];
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            foreach (IServiceRegistration projectDependency in ProjectDependencies)
            {
                projectDependency.ConfigureServices(builder.Services);
            }
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
