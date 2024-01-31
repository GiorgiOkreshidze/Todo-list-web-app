using Microsoft.EntityFrameworkCore;

namespace TodoListApp.Services.Database;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

public class UsersDbContext : DbContext
{
    public UsersDbContext()
    {
    }
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    public virtual DbSet<UserEntity> UserEntities { get; set; }
    public virtual DbSet<UserRoleEntity> UserRoleEntities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            _ = optionsBuilder.UseSqlServer("Server=localhost; Database=UsersDb; TrustServerCertificate=true; Trusted_Connection=true;");
        }
    }
}
