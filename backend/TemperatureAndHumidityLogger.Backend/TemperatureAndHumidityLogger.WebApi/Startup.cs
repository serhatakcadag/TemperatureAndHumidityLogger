using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TemperatureAndHumidityLogger.Core.Entities.Users;
using TemperatureAndHumidityLogger.Infrastructure.EFCore;
using TemperatureAndHumidityLogger.Infrastructure.Extensions;
using TemperatureAndHumidityLogger.Infrastructure.Seeders;

namespace TemperatureAndHumidityLogger.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext(_configuration);

            services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<EfDbContext>()
            .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Maps attribute-routed controllers.
            });

            app.ApplicationServices.SeedDatabaseAsync().Wait();
        }
    }
}
