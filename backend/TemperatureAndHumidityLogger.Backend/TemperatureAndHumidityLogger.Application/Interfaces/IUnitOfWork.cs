using System;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Interfaces.Repositories;

namespace TemperatureAndHumidityLogger.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IDeviceRepository Devices { get; }
        ILogRepository Logs { get; }
        Task<int> SaveChangesAsync();
    }
}
