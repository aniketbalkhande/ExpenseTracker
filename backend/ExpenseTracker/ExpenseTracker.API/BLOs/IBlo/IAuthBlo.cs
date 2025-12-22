using ExpenseTracker.API.Models.DTOs;

namespace ExpenseTracker.API.BLOs.IBlo
{
    public interface IAuthBlo
    {
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    }
}
