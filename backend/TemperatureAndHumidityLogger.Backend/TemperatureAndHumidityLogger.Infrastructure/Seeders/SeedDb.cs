using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using TemperatureAndHumidityLogger.Core.Entities.Users;
using Microsoft.Extensions.DependencyInjection;

namespace TemperatureAndHumidityLogger.Infrastructure.Seeders
{
    public static class SeedDb
    {
        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
        {
            await serviceProvider.SeedRolesAsync();
        }
    }
}
