using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemperatureAndHumidityLogger.Application.Features.Logs.Commands.CreateLog;
using TemperatureAndHumidityLogger.Application.Features.Logs.Queries;

namespace TemperatureAndHumidityLogger.WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogController(IMediator mediator)
        {
             _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLog(CreateLogCommand command)
        {
            var response = await _mediator.Send(command);

            if(!response.Status)
            {
                return StatusCode(500, response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLogsByDeviceId([FromRoute] Guid id)
        {
            var query = new GetLogsByDeviceQuery() { DeviceId = id };

            var response = await _mediator.Send(query);

            if (!response.Status)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }
    }
}
