using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamManagementSystem.Application.DTOs;
using TeamManagementSystem.Application.Users.Commands;

namespace TeamManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator ) {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginUser(LoginUserCommand command) {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterUser(RegisterUserCommand command) {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
