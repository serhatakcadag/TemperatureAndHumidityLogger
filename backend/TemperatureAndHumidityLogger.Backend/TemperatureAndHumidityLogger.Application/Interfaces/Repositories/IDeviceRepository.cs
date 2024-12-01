using System;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Core.Entities.Devices;

namespace TemperatureAndHumidityLogger.Application.Interfaces.Repositories
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        Task<Device> GetBySerialNumberAsync(Guid serialNumber);
    }
}
