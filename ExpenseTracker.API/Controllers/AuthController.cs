using ExpenseTracker.Common.DTOs;
using ExpenseTracker.Common.Models;
using ExpenseTracker.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;


        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUser(UserRequestDTO user)
        {
            var registeredUser = await _authService.RegisterAsync(user);
            if (registeredUser == null || registeredUser.Token == null)
            {
                _logger.LogError($"AuthController : RegisterUser() Registration failed for user : {user.Name}");
                return StatusCode(StatusCodes.Status400BadRequest, "User name already exists");
            }

            _logger.LogInformation($"AuthController : RegisterUser() Registration successful for user : {user.Name}");
            return Ok(registeredUser);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginUser(UserRequestDTO user)
        {
            var loggedInUser = await _authService.LoginAsync(user);

            if (loggedInUser == null || loggedInUser.Token == null)
            {
                _logger.LogError($"AuthController : LoginUser() Login failed for user : {user.Name}");
                return StatusCode(StatusCodes.Status400BadRequest, "User name or password is wrong!");
            }

            _logger.LogInformation($"AuthController : LoginUser() Login successful for user : {user.Name}");
            return Ok(loggedInUser);

        }

        [HttpGet("ping")]
        [ProducesResponseType(typeof(BaseResponseDto), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult HealthCheck()
        {
            _logger.LogInformation($"AuthController : HealthCheck() pinged!");
            return Ok(new BaseResponseDto { Message = "Ping Successful!" });
        }
    }
}
