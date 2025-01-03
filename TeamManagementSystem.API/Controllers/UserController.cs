using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamManagementSystem.Application.DTOs;
using TeamManagementSystem.Application.Interfaces;

namespace TeamManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userService;  

        public UserController(IUserRepository userService ) {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginUser(LoginDTO loginDTO) {
            var user = await _userService.LoginUserAsync(loginDTO);

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterUser(RegisterUserDTO registerUserDTO) {
            var user = await _userService.RegisterUserAsync(registerUserDTO);

            return Ok(user);
        }
    }
}
