using ExpenseTracker.API.Models.Entities;
using ExpenseTracker.API.Models.Enums;

namespace ExpenseTracker.API.Repositories.IRepository
{
    public interface ICategoryRepository
    {
        Task<Category> AddAsync(Category category);
        Task<Category?> GetByIdAsync(Guid id, string userId);
        Task<List<Category>> GetAllAsync(string userId);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<bool> ExistsAsync(string name, string userId, CategoryType type);

    }
}
