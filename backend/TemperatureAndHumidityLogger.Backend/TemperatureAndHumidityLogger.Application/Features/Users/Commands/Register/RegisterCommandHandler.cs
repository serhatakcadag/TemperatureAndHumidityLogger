using MediatR;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Helpers.Common;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Core.Entities.Users;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Users.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, WrapResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtHelper _jwtHelper;

        public RegisterCommandHandler(IUnitOfWork unitOfWork, JwtHelper jwtHelper)
        {
            _unitOfWork = unitOfWork;
            _jwtHelper = jwtHelper;
        }
        public async Task<WrapResponse<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validationResult = ValidatePhoneNumber(request.PhoneNumber);

            if(!validationResult.Status)
            {
                return validationResult;
            }

            User user = new()
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            var createResult = await _unitOfWork.Users.RegisterAsync(user, request.Password);

            if(!createResult.Succeeded)
            {
                var errors = createResult.Errors.Any() ? string.Join('\n', createResult.Errors.Select(e => e.Description)) : "An error occured";

                return WrapResponse<string>.Failure(errors);
            }

            await _unitOfWork.Users.AddToRoleAsync(user, "User");

            return WrapResponse<string>.Success("You have been successfully registered.");
        }

        private WrapResponse<string> ValidatePhoneNumber(string phoneNumber)
        {
            if(string.IsNullOrWhiteSpace(phoneNumber))
            {
                return WrapResponse<string>.Failure("Phone number is required.");
            }

            string pattern = @"^5\d{9}$";

            if (!Regex.IsMatch(phoneNumber, pattern))
            {
                return WrapResponse<string>.Failure("Phone number must be in (5XX XXX XX XX) format.");
            }

            return WrapResponse<string>.Success(phoneNumber);
        }
    }
}
