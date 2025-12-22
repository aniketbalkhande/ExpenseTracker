using ExpenseTracker.API.Models.DTOs;

namespace ExpenseTracker.API.BLOs.IBlo
{
    public interface IAuthBlo
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    }
}
