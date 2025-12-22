using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.API.Repositories.IRepository
{
    public interface IAuthRepository
    {
        Task<IdentityUser?> GetByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(IdentityUser user, string password);
        Task<IList<string>> GetRolesAsync(IdentityUser user);
    }
}
