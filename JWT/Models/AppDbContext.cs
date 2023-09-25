using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWT.Models;

public class AppDbContext: IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        var hasher = new PasswordHasher<IdentityUser>();
        var user1 = new IdentityUser()
        {
            Email = "alberto@email.com",
            NormalizedEmail = "ALBERTO@EMAIL.COM",
            UserName = "Beto",
            NormalizedUserName = "BETO"
        };
        user1.PasswordHash = hasher.HashPassword(user1, "admin10");

        builder.Entity<IdentityUser>().HasData(new List<IdentityUser>() { user1 });
    }
}