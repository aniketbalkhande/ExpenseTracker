using ExpenseTracker.API.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.API.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        private readonly AdminUserConfig _adminConfig;
        public AuthDbContext(DbContextOptions<AuthDbContext> options, IConfiguration configuration) : base(options)
        {
            _adminConfig = configuration.GetSection("AdminUser").Get<AdminUserConfig>()!;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "e80adf2a-2f18-4a9a-a4f6-a8716b8c23b6";
            var writerRoleId = "b4f5c3d9-3e2a-4f7b-9c1d-5e6f7a8b9c0d";

            // create reader and writer roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole() {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER",
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole() {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "WRITER",
                    ConcurrencyStamp = writerRoleId
                }
            };

            // seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            var admin = new IdentityUser
            {
                Id = _adminConfig.Id,
                UserName = _adminConfig.Email,
                Email = _adminConfig.Email,
                NormalizedEmail = _adminConfig.Email.ToUpper(),
                NormalizedUserName = _adminConfig.Email.ToUpper(),
                EmailConfirmed = true
            };

            // Set user password
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, _adminConfig.Password);

            // seed the admin user
            builder.Entity<IdentityUser>().HasData(admin);

            // Give Roles to the admin user
            var adminRoles = new List<IdentityUserRole<string>>
            {
                new()
                {
                    UserId = _adminConfig.Id,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = _adminConfig.Id,
                    RoleId = writerRoleId
                }
            };

            // seed the user roles
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }

    }
}
