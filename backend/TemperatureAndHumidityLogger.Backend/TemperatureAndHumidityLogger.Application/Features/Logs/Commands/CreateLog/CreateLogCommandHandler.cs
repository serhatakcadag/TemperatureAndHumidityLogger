using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Helpers.Common;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Core.Entities.Common;
using TemperatureAndHumidityLogger.Core.Entities.Devices;
using TemperatureAndHumidityLogger.Core.Entities.Logs;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Logs.Commands.CreateLog
{
    public class CreateLogCommandHandler : IRequestHandler<CreateLogCommand, WrapResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SecretKeyHelper _secretKeyHelper;
        private readonly SmsHelper _smsHelper;


        public CreateLogCommandHandler(IUnitOfWork unitOfWork, SecretKeyHelper secretKeyHelper, SmsHelper smsHelper)
        {
            _unitOfWork = unitOfWork;
            _secretKeyHelper = secretKeyHelper;
            _smsHelper = smsHelper;
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

                var device = (await _unitOfWork.Devices.FindAsync(d => d.SerialNumber == decryptedSecret.SerialNumber && d.Id == decryptedSecret.DeviceId)).FirstOrDefault();

                if(device is null)
                {
                    return WrapResponse<bool>.Failure("An error occured.");
                }

                if (device.UserId is null)
                {
                    return WrapResponse<bool>.Failure("Device is not owned.");
                }

                var log = new Log
                {
                    DeviceId = decryptedSecret.DeviceId,
                    Humidity = request.Humidity,
                    Temperature = request.Temperature
                };

                await _unitOfWork.Logs.AddAsync(log);

                await SendSmsIfRequired(device, log);

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

        private async Task SendSmsIfRequired(Device device, Log log)
        {
            var message = CheckDeviceConditions(device, log.Temperature, log.Humidity);

            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            if (device.LastMessageDate is not null)
            {
                TimeSpan difference = DateTime.Now - device.LastMessageDate.Value;

                if (Math.Abs(difference.TotalMinutes) < 10) return;
            }

            var user = await _unitOfWork.Users.GetByIdAsync(device.UserId.Value);

            await _smsHelper.SendSms(user.PhoneNumber, message);
            device.LastMessageDate = DateTime.Now;
        }

        private string CheckDeviceConditions(Device device, float temperature, float humidity)
        {
            var issues = new List<string>();

            if (device.MinTemperature != null && device.MaxTemperature != null)
            {
                if (temperature < device.MinTemperature || temperature > device.MaxTemperature)
                {
                    issues.Add($"temperature is now {temperature}°C");
                }
            }

            if (device.MinHumidity != null && device.MaxHumidity != null)
            {
                if (humidity < device.MinHumidity || humidity > device.MaxHumidity)
                {
                    issues.Add($"humidity is now {humidity}%");
                }
            }

            if (issues.Any())
            {
                string issueDetails = string.Join(", and ", issues);
                string bothOrSingle = issues.Count < 2 ? "It is" : "Both are";
                return $"Alert: The {issueDetails} for the device with serial number {device.SerialNumber}. {bothOrSingle} out of the desired range. Please take the necessary actions.";
            }

            return string.Empty;
        }

    }
}
