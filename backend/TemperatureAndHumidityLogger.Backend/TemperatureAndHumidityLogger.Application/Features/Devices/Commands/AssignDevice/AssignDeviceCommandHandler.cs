using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Commands.AssignDevice
{
    public class AssignDeviceCommandHandler : IRequestHandler<AssignDeviceCommand, WrapResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssignDeviceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WrapResponse<bool>> Handle(AssignDeviceCommand request, CancellationToken cancellationToken)
        {
            var userId = _unitOfWork.Users.GetUserId();

            var device = await _unitOfWork.Devices.GetBySerialNumberAsync(request.SerialNumber);

            if (device == null)
            {
                return WrapResponse<bool>.Failure("Device has not found.");
            }

            if(device.UserId is not null)
            {
                return WrapResponse<bool>.Failure("The device is already owned.");
            }
            
            device.UserId = userId;

            await _unitOfWork.SaveChangesAsync();

            return WrapResponse<bool>.Success(true);
        }
    }
}
