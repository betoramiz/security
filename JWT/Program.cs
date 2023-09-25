using System.Text;
using Carter;
using JWT.Features;
using JWT.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Options));

builder.Services
    .AddSqlServer<AppDbContext>(builder.Configuration.GetConnectionString("default"))
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services
    .AddHttpContextAccessor()
    .AddAuthentication(config =>
    {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var configKey = builder.Configuration["Jwt:Key"];
        var key = Encoding.UTF8.GetBytes(configKey);
        
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true, 
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddTransient<UserManager<IdentityUser>>();
builder.Services.AddTransient<SignInManager<IdentityUser>>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();

app.Run();