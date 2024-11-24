using MediatR;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Users.Commands.Login
{
    public class LoginCommand : IRequest<WrapResponse<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
