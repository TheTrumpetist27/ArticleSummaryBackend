using Core.Models;
using DAL;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArticleSummaryBackend.DAL.Tests
{
    public class CompanyRepositoryTests
    {
        private DbContextOptions<DataContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetAllCompanies_ReturnAllMappedCompanies()
        {
            var _options = CreateNewContextOptions();
            // Arrange
            using (var context = new DataContext(_options))
            {
                context.Companies.Add(new CompanyEntity { Id = 1, Name = "Test Company 1", CEOId = 1 });
                context.Companies.Add(new CompanyEntity { Id = 2, Name = "Test Company 2", CEOId = 2 });
                await context.SaveChangesAsync();
            }

            List<Company> result;
            using (var context = new DataContext(_options))
            {
                var repository = new CompanyRepository(context);

                // Act
                result = (await repository.GetAllCompanies()).ToList();
            }

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Id == 1 && c.Name == "Test Company 1" && c.CEOId == 1);
            Assert.Contains(result, c => c.Id == 2 && c.Name == "Test Company 2" && c.CEOId == 2);
        }

        [Fact]
        public async Task GetCompanyById_ReturnMappedCompany()
        {
            var _options = CreateNewContextOptions();
            // Arrange
            using (var context = new DataContext(_options))
            {
                context.Companies.Add(new CompanyEntity { Id = 1, Name = "Test Company 1", CEOId = 1 });
                await context.SaveChangesAsync();
            }
            
            Company result;
            using (var context = new DataContext(_options))
            {
                var repository = new CompanyRepository(context);
                // Act
                result = await repository.GetCompanyById(1);
            }
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Company 1", result.Name);
        }

        [Fact]
        public async Task CreateCompany_AddCompanyToDatabase()
        {
            var _options = CreateNewContextOptions();
            // Arrange
            var newCompany = new Company { Id = 1, Name = "Test Company 1", CEOId = 1 };
            using (var context = new DataContext(_options))
            {
                var repository = new CompanyRepository(context);
                // Act
                await repository.CreateCompany(newCompany);
            }
            // Assert
            using (var context = new DataContext(_options))
            {
                var company = await context.Companies.FindAsync(1);
                Assert.NotNull(company);
                Assert.Equal("Test Company 1", company.Name);
            }
        }

        [Fact]
        public async Task UpdateCompany_UpdateCompanyInDatabase()
        {
            var _options = CreateNewContextOptions();
            // Arrange
            var updatedCompany = new Company { Id = 1, Name = "Updated Company", CEOId = 1 };
            using (var context = new DataContext(_options))
            {
                context.Companies.Add(new CompanyEntity { Id = 1, Name = "Test Company 1", CEOId = 1 });
                await context.SaveChangesAsync();
            }
            using (var context = new DataContext(_options))
            {
                var repository = new CompanyRepository(context);
                // Act
                await repository.UpdateCompany(updatedCompany);
            }
            // Assert
            using (var context = new DataContext(_options))
            {
                var company = await context.Companies.FindAsync(1);
                Assert.NotNull(company);
                Assert.Equal("Updated Company", company.Name);
            }
        }

        [Fact]
        public async Task DeleteCompany_RemoveCompanyFromDatabase()
        {
            var _options = CreateNewContextOptions();
            // Arrange
            using (var context = new DataContext(_options))
            {
                context.Companies.Add(new CompanyEntity { Id = 1, Name = "Test Company 1", CEOId = 1 });
                await context.SaveChangesAsync();
            }
            using (var context = new DataContext(_options))
            {
                var repository = new CompanyRepository(context);
                // Act
                await repository.DeleteCompany(1);
            }
            // Assert
            using (var context = new DataContext(_options))
            {
                var company = await context.Companies.FindAsync(1);
                Assert.Null(company);
            }
        }

        [Fact]
        public async Task DeleteCompany_NonExistentCompany_ReturnsZero()
        {
            var _options = CreateNewContextOptions();
            // Arrange
            using (var context = new DataContext(_options))
            {
                context.Companies.Add(new CompanyEntity { Id = 1, Name = "Test Company 1", CEOId = 1 });
                await context.SaveChangesAsync();
            }
            using (var context = new DataContext(_options))
            {
                var repository = new CompanyRepository(context);
                // Act
                var result = await repository.DeleteCompany(2);
                // Assert
                Assert.Equal(0, result);
            }
        }
    }
}