using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Core.Entities.Users;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace TemperatureAndHumidityLogger.Infrastructure.Seeders
{
    internal static class SeedRoles
    {
        internal static async Task SeedRolesAsync(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new Role { Name = "Admin" });
                }

                if (!await roleManager.RoleExistsAsync("User"))
                {
                    await roleManager.CreateAsync(new Role { Name = "User" });
                }
            }
        }
    }
}
