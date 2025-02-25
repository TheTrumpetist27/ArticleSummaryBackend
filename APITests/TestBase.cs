using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APITests
{
    public class TestBase
    {
        protected async Task<DataContext> GetDatabaseContextAsync()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);

            // Check of er al data is
            if (!await context.companies.AnyAsync())
            {
                await SeedDatabase(context);
            }

            return context;
        }

        private async Task SeedDatabase(DataContext context)
        {
            var companies = new List<Company>
            {
                new Company { Id = 1, Name = "Company 1" },
                new Company { Id = 2, Name = "Company 2" },
                new Company { Id = 3, Name = "Company 3" },
            };
            context.companies.AddRange(companies);
            await context.SaveChangesAsync();
        }
    }
}
