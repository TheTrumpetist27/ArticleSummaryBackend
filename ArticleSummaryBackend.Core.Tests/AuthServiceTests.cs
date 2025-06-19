using Core.Models;
using Core.Repositories;
using Core.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Text;
using Xunit;

namespace ArticleSummaryBackend.Core.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["Jwt:Key"]).Returns("supersecretkey12345678901234567890");

            _authService = new AuthService(_userRepositoryMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var username = "kian";
            var password = "test123";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            _userRepositoryMock.Setup(r => r.GetByUsernameAsync(username))
                .ReturnsAsync(new User { Username = username, PasswordHash = hashedPassword });

            // Act
            var token = await _authService.LoginAsync(username, password);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(token));
            _userRepositoryMock.Verify(r => r.GetByUsernameAsync(username), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_WrongPassword_ThrowsException()
        {
            // Arrange
            var username = "kian";
            var password = "wrongpass";
            var correctHash = BCrypt.Net.BCrypt.HashPassword("test123");

            _userRepositoryMock.Setup(r => r.GetByUsernameAsync(username))
                .ReturnsAsync(new User { Username = username, PasswordHash = correctHash });

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _authService.LoginAsync(username, password));
        }

        [Fact]
        public async Task LoginAsync_UserDoesNotExist_ThrowsException()
        {
            _userRepositoryMock.Setup(r => r.GetByUsernameAsync("nonexistent")).ReturnsAsync((User?)null);

            await Assert.ThrowsAsync<Exception>(() => _authService.LoginAsync("nonexistent", "pass"));
        }

        [Fact]
        public async Task RegisterAsync_NewUser_AddsUser()
        {
            var username = "newuser";
            var password = "securepass";
            User? capturedUser = null;

            _userRepositoryMock.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync((User?)null);
            _userRepositoryMock.Setup(r => r.AddUserAsync(It.IsAny<User>()))
                .Callback<User>(u => capturedUser = u)
                .Returns(Task.CompletedTask);

            await _authService.RegisterAsync(username, password);

            _userRepositoryMock.Verify(r => r.AddUserAsync(It.IsAny<User>()), Times.Once);
            Assert.NotNull(capturedUser);
            Assert.Equal(username, capturedUser!.Username);
            Assert.True(BCrypt.Net.BCrypt.Verify(password, capturedUser.PasswordHash));
        }

        [Fact]
        public async Task RegisterAsync_ExistingUser_ThrowsException()
        {
            var username = "kian";
            _userRepositoryMock.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync(new User { Username = username });

            await Assert.ThrowsAsync<Exception>(() => _authService.RegisterAsync(username, "pass"));
        }
    }
}
