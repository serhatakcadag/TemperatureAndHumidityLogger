using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Core.Entities.Devices;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Queries.GetAllDevices
{
    public class GetAllDevicesQueryHandler : IRequestHandler<GetAllDevicesQuery, WrapResponse<IEnumerable<Device>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllDevicesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WrapResponse<IEnumerable<Device>>> Handle(GetAllDevicesQuery request, CancellationToken cancellationToken)
        {
            var devices = await _unitOfWork.Devices.GetAllAsync(request.TrackChanges);

            return WrapResponse<IEnumerable<Device>>.Success(devices);
        }
    }
}
