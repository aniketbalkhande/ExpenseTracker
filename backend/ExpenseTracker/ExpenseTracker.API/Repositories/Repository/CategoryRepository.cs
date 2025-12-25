using ExpenseTracker.API.Data;
using ExpenseTracker.API.Models.Entities;
using ExpenseTracker.API.Models.Enums;
using ExpenseTracker.API.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.API.Repositories.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> AddAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> GetByIdAsync(Guid id, string userId) => await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        public async Task UpdateAsync(Category category)
        {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string name, string userId, CategoryType type)
        {
            return await _dbContext.Categories
                .AnyAsync(c =>
                    c.Name == name &&
                    c.UserId == userId &&
                    c.Type == type);
        }

        public async Task<List<Category>> GetAllAsync(string userId)
        {
            return await _dbContext.Categories.OrderBy(o => o.Name).ToListAsync();
        }



    }
}
