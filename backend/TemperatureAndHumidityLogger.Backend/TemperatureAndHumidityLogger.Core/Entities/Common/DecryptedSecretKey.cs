using System;

namespace TemperatureAndHumidityLogger.Core.Entities.Common
{
    public class DecryptedSecretKey
    {
        public Guid DeviceId { get; set; }
        public Guid SerialNumber { get; set; }
        public bool Success { get; set; }
    }
}
