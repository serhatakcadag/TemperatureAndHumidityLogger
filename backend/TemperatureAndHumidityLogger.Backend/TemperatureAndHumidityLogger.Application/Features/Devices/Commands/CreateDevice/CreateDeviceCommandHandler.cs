using MediatR;
using Microsoft.Data.SqlClient;
using System;
using System.Data.SqlTypes;
using System.Threading;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Core.Entities.Devices;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Commands.CreateDevice
{
    public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, WrapResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDeviceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<WrapResponse<object>> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var device = new Device
                {
                    SerialNumber = request.SerialNumber,
                };

                await _unitOfWork.Devices.AddAsync(device);
                await _unitOfWork.SaveChangesAsync();

                return WrapResponse<object>.Success(device.Id);
            }
            catch (Exception ex)
            {
                //to-do handle it with a global exception handler
                if(ex.InnerException is SqlException sqlException && sqlException.Number == 2601)
                {
                    return WrapResponse<object>.Failure("This serial number already exists");
                }

                return WrapResponse<object>.Failure("An error occured");
            }
        }
    }
}
