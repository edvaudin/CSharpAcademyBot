using CSharpAcademyBot.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademyBot.Contexts;

public class AcademyContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AcademyContext(DbContextOptions<AcademyContext> options) : base(options) { }

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
