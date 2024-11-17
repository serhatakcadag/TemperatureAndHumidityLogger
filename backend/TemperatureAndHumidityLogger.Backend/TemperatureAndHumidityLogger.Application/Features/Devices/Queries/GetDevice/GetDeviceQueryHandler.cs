using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Core.Entities.Devices;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Queries.GetDevice
{
    internal class GetDeviceQueryHandler : IRequestHandler<GetDeviceQuery, WrapResponse<Device>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDeviceQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WrapResponse<Device>> Handle(GetDeviceQuery request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(request.Id.ToString()))
            {
                return WrapResponse<Device>.Failure("Id cannot be empty.");
            }

            var device = await _unitOfWork.Devices.GetByIdAsync(request.Id);

            return WrapResponse<Device>.Success(device);
        }
    }
}
