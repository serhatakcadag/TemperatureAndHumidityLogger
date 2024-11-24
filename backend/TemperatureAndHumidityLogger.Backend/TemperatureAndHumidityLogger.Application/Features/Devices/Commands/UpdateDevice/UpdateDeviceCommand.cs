using MediatR;
using System;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Commands.UpdateDevice
{
    public class UpdateDeviceCommand : IRequest<WrapResponse<bool>>
    {
        public Guid Id { get; set; }
        public Guid SerialNumber { get; set; }
        public string Caption { get; set; }
        public Guid? UserId { get; set; }
    }
}
