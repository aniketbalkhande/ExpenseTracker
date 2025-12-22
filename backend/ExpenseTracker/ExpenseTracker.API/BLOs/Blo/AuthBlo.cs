using ExpenseTracker.API.BLOs.IBlo;
using ExpenseTracker.API.Models.DTOs;
using ExpenseTracker.API.Repositories.IRepository;

namespace ExpenseTracker.API.BLOs.Blo
{
    public class AuthBlo : IAuthBlo
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenBlo _tokenBlo;
        public AuthBlo(IAuthRepository authRepository, ITokenBlo tokenBlo)
        {
            _authRepository = authRepository;
            _tokenBlo = tokenBlo;
        }
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _authRepository.GetByEmailAsync(request.Email);

            if (user == null ||
                !await _authRepository.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var roles = await _authRepository.GetRolesAsync(user);

            var token = _tokenBlo.CreateJwtToken(user, roles.ToList());

            return new LoginResponseDto
            {
                Email = user.Email!,
                Roles = roles.ToList(),
                Token = token
            };
        }
    }
}
