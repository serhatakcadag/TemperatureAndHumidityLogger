using MediatR;
using System.Linq;
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
            User user = new()
            {
                UserName = request.UserName,
                Email = request.Email,
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
    }
}
