using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Helpers.Common;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Core.Entities.Common;
using TemperatureAndHumidityLogger.Core.Entities.Logs;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Logs.Commands.CreateLog
{
    public class CreateLogCommandHandler : IRequestHandler<CreateLogCommand, WrapResponse<bool>>
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public SecretKeyHelper _secretKeyHelper { get; set; }

        public CreateLogCommandHandler(IUnitOfWork unitOfWork, SecretKeyHelper secretKeyHelper)
        {
            _unitOfWork = unitOfWork;
            _secretKeyHelper = secretKeyHelper;
        }

        public async Task<WrapResponse<bool>> Handle(CreateLogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var decryptedSecret = ValidateSecretKey(request.SecretKey);

                if (!decryptedSecret.Success)
                {
                    return WrapResponse<bool>.Failure("Invalid secret key.");
                }

                var device = await _unitOfWork.Devices.FindAsync(d => d.SerialNumber == decryptedSecret.SerialNumber && d.Id == decryptedSecret.DeviceId);

                if(device is null)
                {
                    return WrapResponse<bool>.Failure("An error occured.");
                }

                var log = new Log
                {
                    DeviceId = decryptedSecret.DeviceId,
                    Humidity = request.Humidity,
                    Temperature = request.Temperature
                };

                await _unitOfWork.Logs.AddAsync(log);
                await _unitOfWork.SaveChangesAsync();

                return WrapResponse<bool>.Success(true);
            }
            catch
            {
                return WrapResponse<bool>.Failure("An error occured.");
            }
        }
        
        private DecryptedSecretKey ValidateSecretKey(string secretKey)
        {
            if(string.IsNullOrWhiteSpace(secretKey) || !IsBase64String(secretKey))
            {
                return new DecryptedSecretKey { Success = false };
            }

            try
            {
                var encryptedKey = _secretKeyHelper.Decrypt(secretKey);

                var splittedIds = encryptedKey.Split('|');

                if(splittedIds.Length != 2 ) 
                {
                    return new DecryptedSecretKey { Success = false };
                }

                if (Guid.TryParse(splittedIds[0], out Guid serialNumber) && Guid.TryParse(splittedIds[1], out Guid deviceId))
                {
                    return new DecryptedSecretKey
                    {
                        DeviceId = deviceId,
                        SerialNumber = serialNumber,
                        Success = true
                    };
                }

                return new DecryptedSecretKey { Success = false }; ;
            }
            catch
            {
                return new DecryptedSecretKey { Success = false };
            }
            
        }

        private bool IsBase64String(string value)
        {
            Span<byte> buffer = new Span<byte>(new byte[value.Length]);
            return Convert.TryFromBase64String(value, buffer, out _);
        }
    }
}
