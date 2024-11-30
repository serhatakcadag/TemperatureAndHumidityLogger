using MediatR;
using Microsoft.Data.SqlClient;
using System;
using System.Threading;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Helpers.Common;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Core.Entities.Devices;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Commands.CreateDevice
{
    public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, WrapResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SecretKeyHelper _secretKeyHelper;

        public CreateDeviceCommandHandler(IUnitOfWork unitOfWork, SecretKeyHelper secretKeyHelper)
        {
            _unitOfWork = unitOfWork;
            _secretKeyHelper = secretKeyHelper;
        }
        public async Task<WrapResponse<string>> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(!await _unitOfWork.Users.IsAdmin())
                {
                    return WrapResponse<string>.Failure("Unauthorized");
                }

                var device = new Device
                {
                    SerialNumber = Guid.NewGuid(),
                };

                await _unitOfWork.Devices.AddAsync(device);
                await _unitOfWork.SaveChangesAsync();

                var secretKey = _secretKeyHelper.Encrypt($"{device.SerialNumber}|{device.Id}");

                return WrapResponse<string>.Success(secretKey);
            }
            catch (Exception ex)
            {
                //to-do handle it with a global exception handler
                if(ex.InnerException is SqlException sqlException && sqlException.Number == 2601)
                {
                    return WrapResponse<string>.Failure("This serial number already exists");
                }

                return WrapResponse<string>.Failure("An error occured");
            }
        }
    }
}
