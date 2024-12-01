using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;
using TemperatureAndHumidityLogger.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace TemperatureAndHumidityLogger.Infrastructure.Seeders
{
    public static class SeedDb
    {
        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
        {
            await serviceProvider.SeedRolesAsync();
            await serviceProvider.SeedAdminUserAsync();

            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EfDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
