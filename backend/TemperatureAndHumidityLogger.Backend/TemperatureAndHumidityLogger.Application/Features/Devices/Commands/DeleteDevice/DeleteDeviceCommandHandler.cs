using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Commands.DeleteDevice
{
    public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, WrapResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDeviceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WrapResponse<bool>> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deviceToDelete = await _unitOfWork.Devices.GetByIdAsync(request.Id);

                if(deviceToDelete == null)
                {
                    return WrapResponse<bool>.Failure("The device cannot be found.");
                }

                deviceToDelete.DeletedAt = DateTime.Now;
                await _unitOfWork.SaveChangesAsync();

                return WrapResponse<bool>.Success(true);
            }
            catch
            {
                return WrapResponse<bool>.Failure("An error occured.");
            }
        }
    }
}
