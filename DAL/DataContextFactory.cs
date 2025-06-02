using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder() // install package Microsoft.Extensions.Configuration
                .SetBasePath(Directory.GetCurrentDirectory()) // install package Microsoft.Extensions.Configuration.FileExtensions
                .AddJsonFile("appsettings.json") // Install package Microsoft.Extensions.Configuration.Json
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new DataContext(optionsBuilder.Options);
        }
    }
}
