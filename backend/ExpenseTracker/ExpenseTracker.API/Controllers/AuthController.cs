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
        public AuthController(UserManager<IdentityUser> userManger)
        {
            _userManger = userManger;
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
    }
}
