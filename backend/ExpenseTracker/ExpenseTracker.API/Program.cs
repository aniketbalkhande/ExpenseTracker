using ExpenseTracker.API.BLOs.Blo;
using ExpenseTracker.API.BLOs.IBlo;
using ExpenseTracker.API.Configurations;
using ExpenseTracker.API.Data;
using ExpenseTracker.API.Middlewares;
using ExpenseTracker.API.Repositories.IRepository;
using ExpenseTracker.API.Repositories.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container. [Add Dependencies]

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthBlo, AuthBlo>();
builder.Services.AddScoped<ITokenBlo, TokenBlo>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ExpenseTrackerConnectionString"));
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ExpenseTrackerConnectionString"));
});

// IdentityUser: built-in class representing a user in ASP.NET Core Identity
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("ExpenseTracker")
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();
// IdentityOptions: configuration options for ASP.NET Core Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        AuthenticationType = "Jwt",
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("JwtToken").Get<JwtTokenConfig>()!.Issuer,
        ValidAudience = builder.Configuration.GetSection("JwtToken").Get<JwtTokenConfig>()!.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("JwtToken").Get<JwtTokenConfig>()!.Key))
    };
});
#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline. [Add Middlewares here]

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();
