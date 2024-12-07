
using Microsoft.EntityFrameworkCore;
using Core.Common;
using Core.Enums;

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

        modelBuilder.Entity<User>().HasData(
            new User(1, "Admin", "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3")
            );
        
        modelBuilder.Entity<Tag>().HasData(
            new Tag(-1, TagColor.primary, "nmap", "https://nmap.org/"),
            new Tag(-2, TagColor.success, "BurpSuite", "https://portswigger.net/burp"),
            new Tag(-3, TagColor.danger, "Metasploit", "https://github.com/rapid7/metasploit-framework"),
            new Tag(-4, TagColor.warning, "OWASP", "https://owasp.org/www-community/Vulnerability_Scanning_Tools"),
            new Tag(-5, TagColor.info, "WPScan", "https://kali.tools/?p=156"),
            new Tag(-6, TagColor.light, "sqlmap", "https://kali.tools/?p=816"),
            new Tag(-7, TagColor.dark, "wfuz", "https://github.com/xmendez/wfuzz")
        );
    }
    
}