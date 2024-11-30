using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Interfaces.Repositories;
using TemperatureAndHumidityLogger.Core.Entities.Users;

namespace TemperatureAndHumidityLogger.Infrastructure.Seeders
{
    internal static class SeedAdminUser
    {
        internal static async Task SeedAdminUserAsync(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                var user = (await userRepository.FindAsync(u => u.UserName == "admin")).FirstOrDefault();
                
                if (user == null)
                {
                    user = new User()
                    {
                        UserName = "admin",
                        Email = "admin@admin.com",
                        PhoneNumber = "5075483909"
                    };

                    await userRepository.RegisterAsync(user, "12345678");
                    await userRepository.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
