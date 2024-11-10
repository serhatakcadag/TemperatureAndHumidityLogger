using System;
using System.ComponentModel.DataAnnotations.Schema;
using TemperatureAndHumidityLogger.Core.Entities.Devices;

namespace TemperatureAndHumidityLogger.Core.Entities.Logs
{
    public class Log : BaseEntity
    {
        public float Humidity { get; set; }
        public float Temperature { get; set; }

        [ForeignKey("Devices")]
        public Guid DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
