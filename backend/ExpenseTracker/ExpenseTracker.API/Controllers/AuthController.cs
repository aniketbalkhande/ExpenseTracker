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
            // create Identity user here
            var user = new IdentityUser
            {
                UserName = request.Email.Trim(),
                Email = request.Email.Trim()
            };

            // Create user with password
            var identityResult = await _userManger.CreateAsync(user, request.Password.Trim());

            if (identityResult.Succeeded)
            {
                // Add role to user (reader)
                identityResult = await _userManger.AddToRoleAsync(user, "reader");

                if (identityResult.Succeeded)
                {
                    return Ok("User registered successfully");
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                    }
                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);
        }

        // POST: {apibaseurl}/api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var result = await _authBlo.LoginAsync(request);
            return Ok(result);
        }

    }
}
