using ExpenseTracker.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.API.Models.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        public CategoryType Type { get; set; }
    }
}
