using ExpenseTracker.API.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.API.Repositories.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CheckPasswordAsync(IdentityUser user, string password)
        => await _userManager.CheckPasswordAsync(user, password);

        public async Task<IdentityUser?> GetByEmailAsync(string email)
        => await _userManager.FindByEmailAsync(email);

        public async Task<IList<string>> GetRolesAsync(IdentityUser user)
        => await _userManager.GetRolesAsync(user);
    }
}
