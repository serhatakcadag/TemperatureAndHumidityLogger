using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Core.Entities.Users;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, WrapResponse<User>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WrapResponse<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Id.ToString()))
            {
                return WrapResponse<User>.Failure("Id cannot be empty.");
            }

            var user = await _unitOfWork.Users.GetByIdAsync(request.Id);

            return WrapResponse<User>.Success(user);
        }
    }
}
