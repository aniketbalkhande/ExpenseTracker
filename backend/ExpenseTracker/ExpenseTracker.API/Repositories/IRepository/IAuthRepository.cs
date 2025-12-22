using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.API.Repositories.IRepository
{
    public interface IAuthRepository
    {
        Task<IdentityResult> CreateUserAsync(IdentityUser user, string password);
        Task<IdentityResult> AddUserToRoleAsync(IdentityUser user, string role);
        Task<IdentityUser?> GetByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(IdentityUser user, string password);
        Task<IList<string>> GetRolesAsync(IdentityUser user);
    }
}
