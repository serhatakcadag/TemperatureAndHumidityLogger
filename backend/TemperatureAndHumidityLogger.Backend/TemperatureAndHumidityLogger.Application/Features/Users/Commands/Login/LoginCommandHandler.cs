using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Helpers.Common;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Users.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, WrapResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtHelper _jwtHelper;

        public LoginCommandHandler(IUnitOfWork unitOfWork, JwtHelper jwtHelper)
        {
            _unitOfWork = unitOfWork;
            _jwtHelper = jwtHelper;
        }

        public async Task<WrapResponse<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return WrapResponse<string>.Failure("Invalid credentials.");
            }

            bool isPasswordValid = await _unitOfWork.Users.CheckPasswordAsync(user, request.Password);
            
            if(!isPasswordValid) 
            {
                return WrapResponse<string>.Failure("Invalid credentials.");
            }

            return WrapResponse<string>.Success(_jwtHelper.GenerateJwtToken(user.Id.ToString(), user.Email));
        }
    }
}
