using MediatR;
using System;
using System.Collections.Generic;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Logs.Queries
{
    public class GetLogsByDeviceQuery : IRequest<WrapResponse<List<GetLogsByDeviceResponse>>>
    {
        public Guid DeviceId { get; set; }
    }
}
