using Microsoft.EntityFrameworkCore;
using it09_backend.Models;

namespace it09_backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(x => x.AuthorName)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(x => x.ImageUrl)
                  .IsRequired();
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.AuthorName)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(x => x.Message)
                  .IsRequired();

            entity.HasOne(x => x.Post)
                  .WithMany(x => x.Comments)
                  .HasForeignKey(x => x.PostId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}