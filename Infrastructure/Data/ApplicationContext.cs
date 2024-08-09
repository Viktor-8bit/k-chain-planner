
using Microsoft.EntityFrameworkCore;
using Core.Common;

namespace Infrastructure.Data;

public class ApplicationContext : DbContext
{
    public DbSet<Chain> Chains { get; set; }
    public DbSet<ChainStep> ChainSteps { get; set; }
    public DbSet<Tag> Tags { get; set; }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConnectionString.connectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chain>()
            .HasMany(p => p.Tags)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ChainTag",  // Имя промежуточной таблицы
                j => j.HasOne<Tag>().WithMany().HasForeignKey("id"),
                j => j.HasOne<Chain>().WithMany().HasForeignKey("id")
            );
        
    }
    
}