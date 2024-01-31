using Microsoft.EntityFrameworkCore;

namespace TodoListApp.Services.Database;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class TodoListDbContext : DbContext
{
    public TodoListDbContext()
    {
    }

    public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options)
    {
    }

    public virtual DbSet<TodoListEntity> TodoListEntities { get; set; }

    public virtual DbSet<TodoTaskEntity> TodoTaskEntities { get; set; }

    public virtual DbSet<TodoTaskCommentEntity> TodoTaskCommentEntities { get; set; }

    public virtual DbSet<TagEntity> Tags { get; set; }

    public virtual DbSet<EditorEntity> Editors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            _ = optionsBuilder.UseSqlServer("Server=localhost; Database=TodoListDB; TrustServerCertificate=true; Trusted_Connection=true;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.Entity<TodoTaskEntity>().Property(p => p.Status).HasDefaultValue(false);
    }
}
