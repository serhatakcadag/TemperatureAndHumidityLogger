using MediatR;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Commands.CreateDevice
{
    public class CreateDeviceCommand : IRequest<WrapResponse<string>>
    {
    }
}
