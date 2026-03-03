using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WongaAssessment.API.Models.DTOs.reguests;
using WongaAssessment.API.Service.Interface;

namespace WongaAssessment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;          
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDTO request)
        {
            var response = await _authService.RegisterAsync(request);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }
    }
}
