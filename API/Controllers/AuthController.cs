using Microsoft.AspNetCore.Mvc;
using Core.Services;
using API.DTOModels;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCredentialsDTO userCredentialsDTO)
        {
            try
            {
                await _authService.RegisterAsync(userCredentialsDTO.Username, userCredentialsDTO.Password);
                return Ok("Gebruiker geregistreerd");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsDTO userCredentialsDTO)
        {
            try
            {
                var token = await _authService.LoginAsync(userCredentialsDTO.Username, userCredentialsDTO.Password);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
