using MediatR;
using System;
using TemperatureAndHumidityLogger.Core.Entities.Users;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Queries.GetUser
{
    public class GetUserQuery : IRequest<WrapResponse<User>>
    {
        public Guid Id { get; set; }
    }
}
