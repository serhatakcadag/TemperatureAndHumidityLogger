using MediatR;
using System.Collections;
using System.Collections.Generic;
using TemperatureAndHumidityLogger.Core.Entities.Devices;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Queries.GetUserDevices
{
    public class GetUserDevicesQuery : IRequest<WrapResponse<IEnumerable<Device>>>
    {
    }
}
