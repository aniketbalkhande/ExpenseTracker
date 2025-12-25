using ExpenseTracker.API.Models.Enums;

namespace ExpenseTracker.API.Models.DTOs.Category
{
    public class CategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public CategoryType Type { get; set; }
    }
}
