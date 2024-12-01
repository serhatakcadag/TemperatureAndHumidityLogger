using MediatR;
using System;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Commands.UpdateDevice
{
    public class UpdateDeviceCommand : IRequest<WrapResponse<bool>>
    {
        public Guid SerialNumber { get; set; }
        public string Caption { get; set; }

        public float? MinTemperature { get; set; }
        public float? MaxTemperature { get; set;}
        public float? MinHumidity { get; set; }
        public float? MaxHumidity { get; set; }
    }
}
