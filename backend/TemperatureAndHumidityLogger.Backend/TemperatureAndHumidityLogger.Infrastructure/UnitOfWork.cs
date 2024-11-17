using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Application.Interfaces.Repositories;
using TemperatureAndHumidityLogger.Infrastructure.EFCore;

namespace TemperatureAndHumidityLogger.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EfDbContext _context;
        public IUserRepository Users { get; }

        public IDeviceRepository Devices { get; }

        public ILogRepository Logs { get; }
        public UnitOfWork(EfDbContext context, IUserRepository users, IDeviceRepository devices, ILogRepository logs)
        {
            _context = context;
            Users = users;
            Devices = devices;
            Logs = logs;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
