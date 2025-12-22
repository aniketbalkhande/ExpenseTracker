using ExpenseTracker.API.BLOs.IBlo;
using ExpenseTracker.API.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTracker.API.BLOs.Blo
{
    public class TokenBlo : ITokenBlo
    {
        private readonly JwtTokenConfig _jwtTokenConfig;
        public TokenBlo(IConfiguration configuration)
        {
            _jwtTokenConfig = configuration.GetSection("JwtToken").Get<JwtTokenConfig>()!;
        }
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            // Create Claims 
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email!)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // jwt security token parameters 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Key));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtTokenConfig.Issuer,
                audience: _jwtTokenConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtTokenConfig.ExpiryMinutes),
                signingCredentials: credentials
            );

            //return token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
