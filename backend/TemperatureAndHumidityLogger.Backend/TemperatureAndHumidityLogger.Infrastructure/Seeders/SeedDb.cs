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
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EfDbContext>();
                dbContext.Database.Migrate();
            }

            await serviceProvider.SeedRolesAsync();
            await serviceProvider.SeedAdminUserAsync();
        }
    }
}
