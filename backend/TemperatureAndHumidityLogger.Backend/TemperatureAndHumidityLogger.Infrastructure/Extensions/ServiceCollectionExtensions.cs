using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TemperatureAndHumidityLogger.Application.Features.Devices.Commands.CreateDevice;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Application.Interfaces.Repositories;
using TemperatureAndHumidityLogger.Infrastructure.EFCore;
using TemperatureAndHumidityLogger.Infrastructure.Repositories;


namespace TemperatureAndHumidityLogger.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // TO-DO ADD AN IF STATEMENT TO DECIDE IF IT IS IN PRODUCTION OR NOT
            var connectionString = configuration.GetConnectionString("DEV");

            services.AddDbContext<EfDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>(); 

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(a => a.RegisterServicesFromAssembly(typeof(CreateDeviceCommand).Assembly));
            return services;
        }
    }
}
