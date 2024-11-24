using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemperatureAndHumidityLogger.Application.Features.Devices.Commands.CreateDevice;
using TemperatureAndHumidityLogger.Application.Features.Devices.Commands.DeleteDevice;
using TemperatureAndHumidityLogger.Application.Features.Devices.Commands.UpdateDevice;
using TemperatureAndHumidityLogger.Application.Features.Devices.Queries.GetAllDevices;
using TemperatureAndHumidityLogger.Application.Features.Devices.Queries.GetDevice;

namespace TemperatureAndHumidityLogger.WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeviceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDevices()
        {
            var query = new GetAllDevicesQuery();
            var response = await _mediator.Send(query);

            if (response.Result == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice([FromRoute] Guid id)
        {
            var query = new GetDeviceQuery() { Id = id };
            var response = await _mediator.Send(query);

            if (!response.Status)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateDevice(CreateDeviceCommand command)
        {
            var response = await _mediator.Send(command);

            if (!response.Status)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut]
        public async Task<IActionResult> UpdateDevice(UpdateDeviceCommand command)
        {
            var response = await _mediator.Send(command);

            if (!response.Status)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteDevice(DeleteDeviceCommand command)
        {
            var response = await _mediator.Send(command);

            if (!response.Status)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }
    }
}
