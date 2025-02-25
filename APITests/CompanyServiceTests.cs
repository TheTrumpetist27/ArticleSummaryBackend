using API.Models;
using API.Services.CompanyService;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

namespace APITests
{
    public class CompanyServiceTests : TestBase
    {
        [Fact]
        public async Task GetAllCompanies_ShouldReturnAllCompanies()
        {
            // Arrange
            var context = await GetDatabaseContextAsync();
            var service = new CompanyService(context);
            // Act
            var companies = await service.GetAllCompanies();
            // Assert
            Assert.Equal(3, companies.Count());
        }

        [Fact]
        public async Task GetCompanyById_ShouldReturnCompany_WhenCompanyExists()
        {
            // Arrange
            var context = await GetDatabaseContextAsync();
            var service = new CompanyService(context);

            // Act
            var company = await service.GetCompanyById(1);

            // Assert
            Assert.NotNull(company);
            Assert.Equal(1, company.Id);
        }

        [Fact]
        public async Task CreateCompany_ShouldReturnOne_WhenCompanyIsCreated()
        {
            // Arrange
            var context = await GetDatabaseContextAsync();
            var service = new CompanyService(context);

            var newCompany = new Company {Id = 4, Name = "New Company" };

            // Act
            var result = await service.CreateCompany(newCompany);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task UpdateCompany_ShouldReturnOne_WhenCompanyIsUpdated()
        {
            // Arrange
            var context = await GetDatabaseContextAsync();
            var service = new CompanyService(context);
            var company = await service.GetCompanyById(1);
            company.Name = "Updated Company";
            // Act
            var result = await service.UpdateCompany(company);
            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteCompany_ShouldReturnOne_WhenCompanyIsDeleted()
        {
            // Arrange
            var context = await GetDatabaseContextAsync();
            var service = new CompanyService(context);
            // Act
            var result = await service.DeleteCompany(1);
            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteCompany_ShouldReturnZero_WhenCompanyDoesNotExist()
        {
            // Arrange
            var context = await GetDatabaseContextAsync();
            var service = new CompanyService(context);
            // Act
            var result = await service.DeleteCompany(4);
            // Assert
            Assert.Equal(0, result);
        }
    }
}