using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Interfaces.Repositories;
using TemperatureAndHumidityLogger.Core.Entities.Users;
using TemperatureAndHumidityLogger.Infrastructure.EFCore;

namespace TemperatureAndHumidityLogger.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(EfDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) : base(context) 
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
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

        public Guid GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier) ??
                   user?.FindFirstValue("sub");

            if(Guid.TryParse(userId, out Guid result))
            {
                return result;
            }

            return Guid.Empty;
        }

        public async Task<bool> IsAdmin()
        {
            var userId = GetUserId();

            var user = await GetByIdAsync(userId);

            if(user == null)
            {
                return false;
            }

            if(!(await GetRolesAsync(user)).Contains("Admin"))
            {
                return false;
            }

            return true;
        }
    }
}
