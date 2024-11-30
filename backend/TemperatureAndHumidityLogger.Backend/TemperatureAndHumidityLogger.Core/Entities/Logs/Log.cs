using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TemperatureAndHumidityLogger.Core.Entities.Devices;

namespace TemperatureAndHumidityLogger.Core.Entities.Logs
{
    public class Log
    {
        public int Id { get; set; }
        public float Humidity { get; set; }
        public float Temperature { get; set; }

        [ForeignKey("Devices")]
        public Guid DeviceId { get; set; }
        public Device Device { get; set; }
        public DateTime CreatedAt { get; set; }

        public Log()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
