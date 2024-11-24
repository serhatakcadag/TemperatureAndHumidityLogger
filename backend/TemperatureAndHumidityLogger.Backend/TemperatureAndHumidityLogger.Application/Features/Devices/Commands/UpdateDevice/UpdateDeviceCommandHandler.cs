using MediatR;
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
            var deviceToUpdate = await _unitOfWork.Devices.GetByIdAsync(request.Id);
            if (deviceToUpdate == null)
            {
                return WrapResponse<bool>.Failure("The device cannot be found.");
            }

            deviceToUpdate.SerialNumber = request.SerialNumber;
            deviceToUpdate.UserId = request.UserId;
            deviceToUpdate.Caption = request.Caption;

            await _unitOfWork.SaveChangesAsync();

            return WrapResponse<bool>.Success(true);
        }
    }
}
