using ExpenseTracker.API.BLOs.IBlo;
using ExpenseTracker.API.Models.DTOs.Category;
using ExpenseTracker.API.Models.Entities;
using ExpenseTracker.API.Repositories.IRepository;

namespace ExpenseTracker.API.BLOs.Blo
{
    public class CategoryBlo : ICategoryBlo
    {
        public readonly ICategoryRepository _categoryRepository;
        public CategoryBlo(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto, string userId)
        {
            if (await _categoryRepository.ExistsAsync(dto.Name, userId, dto.Type))
            {
                throw new InvalidOperationException("Category already exists");
            }

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Type = dto.Type,
                UserId = userId
            };

            await _categoryRepository.AddAsync(category);

            return MapToResponse(category);
        }

        public async Task<List<CategoryResponseDto>> GetAllAsync(string userId)
        {
            var categories = await _categoryRepository.GetAllAsync(userId);
            return [.. categories.Select(MapToResponse)];
        }

        public async Task<CategoryResponseDto> GetByIdAsync(Guid id, string userId)
        {
            var category = await _categoryRepository.GetByIdAsync(id, userId)
                ?? throw new KeyNotFoundException("Category not found");

            return MapToResponse(category);
        }
        public async Task<CategoryResponseDto> UpdateAsync(UpdateCategoryDto dto, string userId)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.Id, userId)
                ?? throw new KeyNotFoundException("Category not found");

            category.Name = dto.Name;
            category.Type = dto.Type;
            category.UpdatedAt = DateTime.UtcNow;

            await _categoryRepository.UpdateAsync(category);

            return MapToResponse(category);
        }

        public async Task DeleteAsync(Guid id, string userId)
        {
            var category = await _categoryRepository.GetByIdAsync(id, userId)
                ?? throw new KeyNotFoundException("Category not found");

            await _categoryRepository.DeleteAsync(category);
        }

        private static CategoryResponseDto MapToResponse(Category category)
        {
            return new()
            {
                Id = category.Id,
                Name = category.Name,
                Type = category.Type
            };
        }


    }
}
