using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Interfaces.Repositories;
using TemperatureAndHumidityLogger.Core.Entities.Devices;
using TemperatureAndHumidityLogger.Infrastructure.EFCore;

namespace TemperatureAndHumidityLogger.Infrastructure.Repositories
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(EfDbContext context) : base(context)
        {
            
        }

        public async Task<Device> GetBySerialNumberAsync(Guid serialNumber)
        {
            return await _dbSet.FirstOrDefaultAsync(d => d.SerialNumber == serialNumber);
        }
    }
}
