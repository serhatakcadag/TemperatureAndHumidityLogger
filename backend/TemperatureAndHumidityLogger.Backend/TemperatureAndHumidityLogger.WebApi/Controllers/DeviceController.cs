using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemperatureAndHumidityLogger.Application.Features.Devices.Commands.CreateDevice;
using TemperatureAndHumidityLogger.Application.Features.Devices.Queries.GetDevice;
using TemperatureAndHumidityLogger.Application.Features.Devices.Queries.GetUser;

namespace TemperatureAndHumidityLogger.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeviceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice([FromRoute] Guid id)
        {
            var query = new GetDeviceQuery() { Id = id };
            var response = await _mediator.Send(query);

            if (response.Result == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

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
    }
}
