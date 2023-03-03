using CSharpAcademyBot.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CSharpAcademyBot.Factories
{
    public class AcademyContextFactory : IDesignTimeDbContextFactory<AcademyContext>
    {
        public AcademyContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<AcademyContext>();
            optionsBuilder.UseMySql(configuration.GetConnectionString("MySqlConnection"), ServerVersion.AutoDetect(configuration.GetConnectionString("MySqlConnection")));

            return new AcademyContext(optionsBuilder.Options);
        }
    }
}
