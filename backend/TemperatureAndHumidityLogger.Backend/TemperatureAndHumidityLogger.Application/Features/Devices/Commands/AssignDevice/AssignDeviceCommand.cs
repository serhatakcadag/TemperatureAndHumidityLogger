using MediatR;
using System;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Commands.AssignDevice
{
    public class AssignDeviceCommand : IRequest<WrapResponse<bool>>
    {
        public Guid SerialNumber { get; set; }
    }
}
