using Core.Models;
using Core.Repositories;
using Core.Services;
using Moq;

namespace ArticleSummaryBackend.Core.Tests
{
    public class ArticleServiceTests
    {
        [Fact]
        public async Task CreateArticleAsync_CallsSummaryServiceAndReturnsSavedArticle()
        {
            // Arrange
            var article = new Article
            {
                Id = 1,
                Title = "Test",
                Source = new Source { Content = "Test content" }
            };

            var mockRepo = new Mock<IArticleRepository>();
            var mockSummaryService = new Mock<ITextSummaryService>();

            mockSummaryService
                .Setup(s => s.SummarizeTextAsync("Test content"))
                .ReturnsAsync("This is the summary");

            var expectedSavedArticle = new Article
            {
                Id = 1,
                Title = "Test",
                Source = new Source { Content = "Test content" },
                Summary = "This is the summary"
            };

            mockRepo
                .Setup(r => r.CreateArticleAsync(It.Is<Article>(a => a.Summary == "This is the summary")))
                .ReturnsAsync(expectedSavedArticle);

            var service = new ArticleService(mockRepo.Object, mockSummaryService.Object);

            // Act
            var result = await service.CreateArticleAsync(article);

            // Assert
            Assert.Equal("This is the summary", result.Summary);
            mockSummaryService.Verify(s => s.SummarizeTextAsync("Test content"), Times.Once);
            mockRepo.Verify(r => r.CreateArticleAsync(It.IsAny<Article>()), Times.Once);
        }

        [Fact]
        public async Task GetAllArticles_ReturnsListFromRepository()
        {
            // Arrange
            var mockRepo = new Mock<IArticleRepository>();
            var expected = new List<Article>
            {
                new Article { Id = 1, Title = "Test 1", Source = new Source { Content = "Content 1" } },
                new Article { Id = 2, Title = "Test 2", Source = new Source { Content = "Content 2" } }
            };
            mockRepo.Setup(repo => repo.GetAllArticles()).ReturnsAsync(expected);
            var service = new ArticleService(mockRepo.Object, Mock.Of<ITextSummaryService>());
            // Act
            var result = (await service.GetAllArticles()).ToList();
            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Test 1", result[0].Title);
            mockRepo.Verify(r => r.GetAllArticles(), Times.Once);
        }

        [Fact]
        public async Task GetArticleById_ValidId_ReturnsCompany()
        {
            // Arrange
            var mockRepo = new Mock<IArticleRepository>();
            var article = new Article { Comments = new List<Comment>(), Id = 1, Title = "Test Article", Source = new Source { Content = "Test content" } };
            mockRepo.Setup(repo => repo.GetArticleById(1)).ReturnsAsync(article);

            var service = new ArticleService(mockRepo.Object, Mock.Of<ITextSummaryService>());

            // Act
            var result = await service.GetArticleById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Article", result!.Title);
            mockRepo.Verify(r => r.GetArticleById(1), Times.Once);
        }

        [Fact]
        public async Task DeleteArticle_ValidId_ReturnsDeletedCount()
        {
            // Arrange
            var mockRepo = new Mock<IArticleRepository>();
            mockRepo.Setup(repo => repo.DeleteArticle(1)).ReturnsAsync(1);
            var service = new ArticleService(mockRepo.Object, Mock.Of<ITextSummaryService>());
            // Act
            var result = await service.DeleteArticle(1);
            // Assert
            Assert.Equal(1, result);
            mockRepo.Verify(r => r.DeleteArticle(1), Times.Once);
        }
    }
}
