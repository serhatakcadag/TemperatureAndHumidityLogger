using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Commands.UpdateDevice
{
    internal class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, WrapResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDeviceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WrapResponse<bool>> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            var deviceToUpdate = await _unitOfWork.Devices.GetBySerialNumberAsync(request.SerialNumber);
            if (deviceToUpdate == null)
            {
                return WrapResponse<bool>.Failure("The device cannot be found.");
            }

            var userId = _unitOfWork.Users.GetUserId();

            if(deviceToUpdate.UserId != userId)
            {
                return WrapResponse<bool>.Failure("You don't own this device.");
            }

            deviceToUpdate.Caption = request.Caption;

            var temperatureMessage = ValidateTemperatureAttributes(request.MinTemperature, request.MaxTemperature);
            
            if (temperatureMessage != string.Empty)
            {
                return WrapResponse<bool>.Failure(temperatureMessage);
            }

            var humidityMessage = ValidateHumidityAttributes(request.MinHumidity, request.MaxHumidity);

            if (humidityMessage != string.Empty)
            {
                return WrapResponse<bool>.Failure(humidityMessage);
            }

            deviceToUpdate.MinHumidity = request.MinHumidity;
            deviceToUpdate.MaxHumidity = request.MaxHumidity;
            deviceToUpdate.MinTemperature = request.MinTemperature;
            deviceToUpdate.MaxTemperature = request.MaxTemperature;

            await _unitOfWork.SaveChangesAsync();

            return WrapResponse<bool>.Success(true);
        }

        private string ValidateTemperatureAttributes(float? minTemperature, float? maxTemperature)
        {
            if (minTemperature == null && maxTemperature == null)
            {
                return string.Empty;
            }

            if ((minTemperature != null && maxTemperature == null) || (maxTemperature != null && minTemperature == null))
            {
                return "Both temperature values must be filled.";
            }

            if(minTemperature >= maxTemperature)
            {
                return "Max temperature must be bigger than min temperature.";
            }

            return string.Empty;
        }

        private string ValidateHumidityAttributes(float? minHumidity, float? maxHumidity)
        {
            if(minHumidity == null && maxHumidity == null)
            {
                return string.Empty;
            }

            if ((minHumidity != null && maxHumidity == null) || (maxHumidity != null && minHumidity == null))
            {
                return "Both humidity values must be filled.";
            }

            if (minHumidity >= maxHumidity)
            {
                return "Max humidity must be bigger than min humidity.";
            }

            return string.Empty;
        }

    }
}
