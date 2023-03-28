using CSharpAcademyBot.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CSharpAcademyBot.Factories
{
    public class AcademyContextFactory : IDesignTimeDbContextFactory<DbContext>
    {
        public DbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<AcademyContextFactory>()
                .Build();
            var provider = configuration["provider"];

            if (provider == "mysql")
            {
                var optionsBuilder = new DbContextOptionsBuilder<MySqlAcademyContext>();
                return new MySqlAcademyContext(optionsBuilder.Options);
            }
            else if (provider == "sqlserver")
            {
                var optionsBuilder = new DbContextOptionsBuilder<SqlServerAcademyContext>();
                return new SqlServerAcademyContext(optionsBuilder.Options);
            }
            else
            {
                throw new Exception($"Unsupported provider: {provider}");
            }
        }
    }
}