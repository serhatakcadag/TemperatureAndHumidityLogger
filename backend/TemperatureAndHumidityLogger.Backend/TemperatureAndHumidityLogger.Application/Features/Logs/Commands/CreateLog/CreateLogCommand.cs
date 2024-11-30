using MediatR;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Logs.Commands.CreateLog
{
    public class CreateLogCommand : IRequest<WrapResponse<bool>>
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public string SecretKey { get; set; } = "";
    }
}
