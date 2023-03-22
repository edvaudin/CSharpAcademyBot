using CSharpAcademyBot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CSharpAcademyBot.Contexts;

public class MySqlAcademyContext : DbContext, IAcademyContext
{
    public DbSet<User> Users { get; set; }

    // https://stackoverflow.com/questions/70273434/unable-to-resolve-service-for-type-%C2%A8microsoft-entityframeworkcore-dbcontextopti
    public MySqlAcademyContext() { } // For scaffolding

    public MySqlAcademyContext(DbContextOptions<MySqlAcademyContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<Bot>()
                .Build();

        optionsBuilder.UseMySql(configuration["ConnectionStrings:MySqlConnection"],
                ServerVersion.AutoDetect(configuration["ConnectionStrings:MySqlConnection"]));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<User>()
            .HasOne(u => u.Reputation)
            .WithOne(u => u.User)
            .HasForeignKey<UserReputation>(u => u.UserId);
    }
}
