using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TemperatureAndHumidityLogger.Application.Extensions;
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

            services.AddInfrastructureServices(_configuration);
            services.AddApplicationServices();

            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<EfDbContext>()
            .AddDefaultTokenProviders();

            services.AddJwtSettings(_configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options =>
                    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Maps attribute-routed controllers.
            });

            app.ApplicationServices.SeedDatabaseAsync().Wait();
        }
    }
}
