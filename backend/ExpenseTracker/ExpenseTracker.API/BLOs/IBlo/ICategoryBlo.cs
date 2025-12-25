using ExpenseTracker.API.Models.DTOs.Category;

namespace ExpenseTracker.API.BLOs.IBlo
{
    public interface ICategoryBlo
    {
        Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto, string userId);
        Task<List<CategoryResponseDto>> GetAllAsync(string userId);
        Task<CategoryResponseDto> GetByIdAsync(Guid id, string userId);
        Task<CategoryResponseDto> UpdateAsync(UpdateCategoryDto dto, string userId);
        Task DeleteAsync(Guid id, string userId);
    }
}
