using ExpenseTracker.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }

        //Composite unique index prevents duplicate categories per user & type
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasIndex(c => new { c.Name, c.UserId, c.Type })
                .IsUnique();
        }
    }
}
