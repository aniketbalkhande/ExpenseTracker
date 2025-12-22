namespace ExpenseTracker.API.Models.DTOs
{
    public class RegisterResponseDto
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = [];
    }
}
