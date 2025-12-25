using ExpenseTracker.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.API.Models.Entities
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        public CategoryType Type { get; set; }

        // Multi-user support
        [Required]
        public string UserId { get; set; } = null!;

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }

}
