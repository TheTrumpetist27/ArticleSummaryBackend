using Core.Models;
using Core.Repositories;
using Core.Services;
using Moq;
using Xunit;

namespace ArticleSummaryBackend.Core.Tests
{
    public class CommentServiceTests
    {
        [Fact]
        public async Task AddCommentAsync_CreatesAndBroadcastsComment()
        {
            // Arrange
            var comment = new Comment { Id = 1, ArticleId = 42, Content = "Nice article!" };

            var mockRepo = new Mock<ICommentRepository>();
            var mockBroadcast = new Mock<ICommentBroadcastService>();

            mockRepo.Setup(r => r.AddCommentAsync(comment)).ReturnsAsync(comment);

            var service = new CommentService(mockRepo.Object, mockBroadcast.Object);

            // Act
            var result = await service.AddCommentAsync(comment);

            // Assert
            Assert.Equal(comment, result);
            mockRepo.Verify(r => r.AddCommentAsync(comment), Times.Once);
            mockBroadcast.Verify(b => b.BroadcastComment(comment), Times.Once);
        }

        [Fact]
        public async Task GetCommentsForArticleAsync_ReturnsComments()
        {
            // Arrange
            var articleId = 42;
            var expected = new List<Comment>
        {
            new Comment { Id = 1, ArticleId = 42, Content = "First" },
            new Comment { Id = 2, ArticleId = 42, Content = "Second" }
        };

            var mockRepo = new Mock<ICommentRepository>();
            mockRepo.Setup(r => r.GetCommentsForArticleAsync(articleId)).ReturnsAsync(expected);

            var mockBroadcast = new Mock<ICommentBroadcastService>(); // niet nodig hier, maar vereist door constructor

            var service = new CommentService(mockRepo.Object, mockBroadcast.Object);

            // Act
            var result = await service.GetCommentsForArticleAsync(articleId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("First", result[0].Content);
        }

        [Fact]
        public async Task DeleteCommentAsync_DeletesAndBroadcasts_WhenSuccessful()
        {
            // Arrange
            var commentId = 99;

            var mockRepo = new Mock<ICommentRepository>();
            var mockBroadcast = new Mock<ICommentBroadcastService>();

            mockRepo.Setup(r => r.DeleteCommentAsync(commentId)).ReturnsAsync(true);

            var service = new CommentService(mockRepo.Object, mockBroadcast.Object);

            // Act
            var result = await service.DeleteCommentAsync(commentId);

            // Assert
            Assert.True(result);
            mockRepo.Verify(r => r.DeleteCommentAsync(commentId), Times.Once);
            mockBroadcast.Verify(b => b.BroadcastDelete(commentId), Times.Once);
        }

        [Fact]
        public async Task DeleteCommentAsync_Fails_DoesNotBroadcast()
        {
            // Arrange
            var commentId = 99;

            var mockRepo = new Mock<ICommentRepository>();
            var mockBroadcast = new Mock<ICommentBroadcastService>();

            mockRepo.Setup(r => r.DeleteCommentAsync(commentId)).ReturnsAsync(false);

            var service = new CommentService(mockRepo.Object, mockBroadcast.Object);

            // Act
            var result = await service.DeleteCommentAsync(commentId);

            // Assert
            Assert.False(result);
            mockRepo.Verify(r => r.DeleteCommentAsync(commentId), Times.Once);
            mockBroadcast.Verify(b => b.BroadcastDelete(It.IsAny<int>()), Times.Never);
        }
    }
}
