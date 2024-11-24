using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemperatureAndHumidityLogger.Application.Features.Users.Commands.Login;
using TemperatureAndHumidityLogger.Application.Features.Users.Commands.Register;

namespace TemperatureAndHumidityLogger.WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand registerCommand)
        {
            var response = await _mediator.Send(registerCommand);

            if (!response.Status)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var response = await _mediator.Send(loginCommand);
            
            if (!response.Status)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }
    }
}
