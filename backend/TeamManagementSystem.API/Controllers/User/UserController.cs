using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamManagementSystem.Application.Users.Commands;
using TeamManagementSystem.Application.Users.Services;

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
        public async Task<ActionResult<LoginResponse>> LoginUser(LoginUserRequest request) {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterUser(RegisterUserRequest request) {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPost("loginWithRefreshToken")]
        public async Task<ActionResult<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest request) {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

    }
}
