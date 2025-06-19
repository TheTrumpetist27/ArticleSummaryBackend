using Core.Models;
using Core.Repositories;
using Core.Services;
using Moq;

namespace ArticleSummaryBackend.Core.Tests
{
    public class CompanyServiceTests
    {
        [Fact]
        public async Task GetAllCompanies_ReturnsListFromRepository()
        {
            // Arrange
            var mockRepo = new Mock<ICompanyRepository>();
            var expected = new List<Company>
            {
                new Company { Id = 1, Name = "Test 1", CEOId = 1 },
                new Company { Id = 2, Name = "Test 2", CEOId = 2 }
            };

            mockRepo.Setup(repo => repo.GetAllCompanies()).ReturnsAsync(expected);
            var service = new CompanyService(mockRepo.Object);

            // Act
            var result = (await service.GetAllCompanies()).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Test 1", result[0].Name);
            mockRepo.Verify(r => r.GetAllCompanies(), Times.Once);
        }

        [Fact]
        public async Task GetCompanyById_ValidId_ReturnsCompany()
        {
            // Arrange
            var company = new Company { Id = 1, Name = "Kian Corp", CEOId = 42 };
            var mockRepo = new Mock<ICompanyRepository>();
            mockRepo.Setup(repo => repo.GetCompanyById(1)).ReturnsAsync(company);

            var service = new CompanyService(mockRepo.Object);

            // Act
            var result = await service.GetCompanyById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Kian Corp", result!.Name);
            mockRepo.Verify(r => r.GetCompanyById(1), Times.Once);
        }

        [Fact]
        public async Task CreateCompany_CallsRepositoryAndReturnsResult()
        {
            // Arrange
            var company = new Company { Name = "NieuwBedrijf", CEOId = 99 };
            var mockRepo = new Mock<ICompanyRepository>();
            mockRepo.Setup(repo => repo.CreateCompany(company)).ReturnsAsync(1);

            var service = new CompanyService(mockRepo.Object);

            // Act
            var result = await service.CreateCompany(company);

            // Assert
            Assert.Equal(1, result);
            mockRepo.Verify(r => r.CreateCompany(company), Times.Once);
        }

        [Fact]
        public async Task UpdateCompany_CallsRepositoryAndReturnsResult()
        {
            // Arrange
            var company = new Company { Id = 1, Name = "Updated Company", CEOId = 42 };
            var mockRepo = new Mock<ICompanyRepository>();
            mockRepo.Setup(repo => repo.UpdateCompany(company)).ReturnsAsync(1);
            var service = new CompanyService(mockRepo.Object);
            // Act
            var result = await service.UpdateCompany(company);
            // Assert
            Assert.Equal(1, result);
            mockRepo.Verify(r => r.UpdateCompany(company), Times.Once);
        }

        [Fact]
        public async Task DeleteCompany_ValidId_CallsRepositoryAndReturnsResult()
        {
            // Arrange
            var mockRepo = new Mock<ICompanyRepository>();
            mockRepo.Setup(repo => repo.DeleteCompany(1)).ReturnsAsync(1);
            var service = new CompanyService(mockRepo.Object);
            // Act
            var result = await service.DeleteCompany(1);
            // Assert
            Assert.Equal(1, result);
            mockRepo.Verify(r => r.DeleteCompany(1), Times.Once);
        }
    }
}
