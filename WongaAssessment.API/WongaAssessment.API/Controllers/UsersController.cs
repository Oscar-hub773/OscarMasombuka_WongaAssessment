using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WongaAssessment.API.Service.Interface;

namespace WongaAssessment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;            
        }

        [Authorize]
        [HttpGet("GetCurrentLoggedInUserDetails")]
        public async Task<IActionResult> GetCurrentLoggedInUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null) return Unauthorized();

            var userId = Guid.Parse(userIdClaim);

            var user = await _userService.GetUserDetailsAsync(userId);

            if (user == null) return NotFound();

            return Ok(user);
        }
    }
}

