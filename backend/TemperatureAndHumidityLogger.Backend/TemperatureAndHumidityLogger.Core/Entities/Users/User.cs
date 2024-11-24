using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using TemperatureAndHumidityLogger.Core.Entities.Devices;
using TemperatureAndHumidityLogger.Core.Interfaces.Common;

namespace TemperatureAndHumidityLogger.Core.Entities.Users
{
    public class User : IdentityUser<Guid>, ISoftDelete
    {
        public List<Device> Devices { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public User() 
        { 
            CreatedAt = DateTime.UtcNow;
        }
    }
}
