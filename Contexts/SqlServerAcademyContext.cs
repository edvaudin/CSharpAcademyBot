using CSharpAcademyBot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAcademyBot.Contexts
{
    internal class SqlServerAcademyContext : DbContext, IAcademyContext
    {
        public DbSet<User> Users { get; set; }

        // https://stackoverflow.com/questions/70273434/unable-to-resolve-service-for-type-%C2%A8microsoft-entityframeworkcore-dbcontextopti
        public SqlServerAcademyContext() { } // For scaffolding

        public SqlServerAcademyContext(DbContextOptions<SqlServerAcademyContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddUserSecrets<Bot>()
                    .Build();

            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:SqlConnection"]);
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
}
