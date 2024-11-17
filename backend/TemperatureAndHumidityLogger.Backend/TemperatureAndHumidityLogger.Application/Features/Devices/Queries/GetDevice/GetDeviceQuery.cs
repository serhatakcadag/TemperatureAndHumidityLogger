using MediatR;
using System;
using TemperatureAndHumidityLogger.Core.Entities.Devices;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Queries.GetDevice
{
    public class GetDeviceQuery : IRequest<WrapResponse<Device>>
    {
        public Guid Id { get; set; }
    }
}
