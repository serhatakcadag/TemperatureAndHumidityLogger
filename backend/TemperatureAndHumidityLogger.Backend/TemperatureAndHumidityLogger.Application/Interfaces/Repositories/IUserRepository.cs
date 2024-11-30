using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Core.Entities.Users;

namespace TemperatureAndHumidityLogger.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IdentityResult> RegisterAsync(User user, string password);
        Task<User> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<IList<string>> GetRolesAsync(User user);
        Task<IdentityResult> AddToRoleAsync(User user, string role);
        Guid GetUserId();
        Task<bool> IsAdmin();
    }
}
