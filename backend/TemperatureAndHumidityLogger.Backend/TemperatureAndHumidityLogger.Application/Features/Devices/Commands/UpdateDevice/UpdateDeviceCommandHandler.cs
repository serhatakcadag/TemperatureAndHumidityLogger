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
            var deviceToUpdate = (await _unitOfWork.Devices.FindAsync(d => d.SerialNumber == request.SerialNumber)).FirstOrDefault();
            if (deviceToUpdate == null)
            {
                return WrapResponse<bool>.Failure("The device cannot be found.");
            }

            var userId = _unitOfWork.Users.GetUserId();

            if(deviceToUpdate.UserId != null && deviceToUpdate.UserId != userId)
            {
                return WrapResponse<bool>.Failure("You don't own this device.");
            }

            deviceToUpdate.SerialNumber = request.SerialNumber;
            deviceToUpdate.UserId = userId;
            deviceToUpdate.Caption = request.Caption;

            await _unitOfWork.SaveChangesAsync();

            return WrapResponse<bool>.Success(true);
        }
    }
}
