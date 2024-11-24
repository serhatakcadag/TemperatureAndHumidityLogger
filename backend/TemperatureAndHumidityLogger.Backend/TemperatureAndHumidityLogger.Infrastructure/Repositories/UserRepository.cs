using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Interfaces.Repositories;
using TemperatureAndHumidityLogger.Core.Entities.Users;
using TemperatureAndHumidityLogger.Infrastructure.EFCore;

namespace TemperatureAndHumidityLogger.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(EfDbContext context, UserManager<User> userManager) : base(context) 
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }
    }
}
