using MediatR;
using System.Collections.Generic;
using TemperatureAndHumidityLogger.Core.Entities.Devices;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Queries.GetAllDevices
{
    public class GetAllDevicesQuery : IRequest<WrapResponse<IEnumerable<Device>>>
    {
        public bool TrackChanges { get; set; } = false;
    }
}
