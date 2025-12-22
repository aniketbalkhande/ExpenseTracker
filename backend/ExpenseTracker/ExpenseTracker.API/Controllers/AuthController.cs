using ExpenseTracker.API.BLOs.IBlo;
using ExpenseTracker.API.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManger;
        private readonly IAuthBlo _authBlo;
        public AuthController(UserManager<IdentityUser> userManger, IAuthBlo authBlo)
        {
            _userManger = userManger;
            _authBlo = authBlo;
        }

        // POST: {apibaseurl}/api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _authBlo.RegisterAsync(request);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok("User registered successfully");
        }

        // POST: {apibaseurl}/api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authBlo.LoginAsync(request);
            return Ok(result);
        }

    }
}
