using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.API.BLOs.IBlo
{
    public interface ITokenBlo
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
