using System;
using TemperatureAndHumidityLogger.Core.Entities.Logs;

namespace TemperatureAndHumidityLogger.Core.Responses
{
    public class GetLogsByDeviceResponse
    {
        public Guid DeviceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Humidity { get; set; }
        public decimal Temperature { get; set; }

        public GetLogsByDeviceResponse(Log log)
        {
            DeviceId = log.DeviceId;
            CreatedAt = log.CreatedAt;
            Humidity = Math.Round((decimal)log.Humidity, 2);
            Temperature = Math.Round((decimal)log.Temperature, 2);
        }
    }
}
