﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TemperatureAndHumidityLogger.Core.Entities.Logs;
using TemperatureAndHumidityLogger.Core.Entities.Users;
using TemperatureAndHumidityLogger.Core.Interfaces.Common;

namespace TemperatureAndHumidityLogger.Core.Entities.Devices
{
    public class Device : BaseEntity, ISoftDelete
    {
        public Guid SerialNumber { get; set; }
        public string Caption { get; set; }
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("Users")]
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public List<Log> Logs { get; set; }

        public float? MinTemperature { get; set; }
        public float? MaxTemperature { get; set;}
        public float? MinHumidity { get; set; }
        public float? MaxHumidity { get;set; }
        public DateTime? LastMessageDate { get; set; }
    }
}
