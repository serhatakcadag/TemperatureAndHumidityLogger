using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Core.Entities.Devices;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Queries.GetUserDevices
{
    public class GetUserDevicesQueryHandler : IRequestHandler<GetUserDevicesQuery, WrapResponse<IEnumerable<Device>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserDevicesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WrapResponse<IEnumerable<Device>>> Handle(GetUserDevicesQuery request, CancellationToken cancellationToken)
        {
            var userId = _unitOfWork.Users.GetUserId();

            if(userId == Guid.Empty)
            {
                return WrapResponse<IEnumerable<Device>>.Failure("UserId cannot be resolved");
            }

            var devices = await _unitOfWork.Devices.FindAsync(d => d.UserId == userId);

            return WrapResponse<IEnumerable<Device>>.Success(devices);
        }
    }
}
