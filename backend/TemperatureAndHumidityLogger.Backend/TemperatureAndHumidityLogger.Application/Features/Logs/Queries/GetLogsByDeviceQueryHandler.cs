using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TemperatureAndHumidityLogger.Application.Interfaces;
using TemperatureAndHumidityLogger.Core.Entities.Logs;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Logs.Queries
{
    internal class GetLogsByDeviceQueryHandler : IRequestHandler<GetLogsByDeviceQuery, WrapResponse<List<GetLogsByDeviceResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLogsByDeviceQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<WrapResponse<List<GetLogsByDeviceResponse>>> Handle(GetLogsByDeviceQuery request, CancellationToken cancellationToken)
        {
            var device = await _unitOfWork.Devices.GetByIdAsync(request.DeviceId);

            if(device is null)
            {
                return WrapResponse<List<GetLogsByDeviceResponse>>.Failure("Device is not found");
            }

            if(device.UserId is null)
            {
                return WrapResponse<List<GetLogsByDeviceResponse>>.Failure("Device does not have owner");
            }

            var logs = await _unitOfWork.Logs.FindAsync(log => log.DeviceId == request.DeviceId);

            logs = logs.OrderBy(log => log.CreatedAt);

            return WrapResponse<List<GetLogsByDeviceResponse>>.Success(MapListToResponse(logs));
        }

        private List<GetLogsByDeviceResponse> MapListToResponse(IEnumerable<Log> list)
        {
            return list.Select(log => new GetLogsByDeviceResponse(log)).ToList();
        }
    }
}
