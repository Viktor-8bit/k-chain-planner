
using Microsoft.EntityFrameworkCore;
using Core.Common;

namespace Infrastructure.Data;

public class ApplicationContext : DbContext
{
    public DbSet<Chain> Chains { get; set; }
    public DbSet<ChainStep> ChainSteps { get; set; }
    public DbSet<Tag> Tags { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chain>()
            .HasMany(p => p.Tags)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ChainTag", // Имя промежуточной таблицы
                j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                j => j.HasOne<Chain>().WithMany().HasForeignKey("ChainId")
            );
        
    }
    
}