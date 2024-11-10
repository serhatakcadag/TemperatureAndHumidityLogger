using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemperatureAndHumidityLogger.Infrastructure.EFCore;

namespace TemperatureAndHumidityLogger.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // TO-DO ADD AN IF STATEMENT TO DECIDE IF IT IS IN PRODUCTION OR NOT
            var connectionString = configuration.GetConnectionString("DEV");

            services.AddDbContext<EfDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
