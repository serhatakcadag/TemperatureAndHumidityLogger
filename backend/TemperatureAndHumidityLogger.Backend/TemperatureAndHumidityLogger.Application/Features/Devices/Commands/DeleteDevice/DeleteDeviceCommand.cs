using MediatR;
using System;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Commands.DeleteDevice
{
    public class DeleteDeviceCommand : IRequest<WrapResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
