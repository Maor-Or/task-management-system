using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using TaskManagement.Application.Interfaces.Repositories;
using TaskManagement.Application.Services;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Exceptions;

namespace TaskManagement.Tests.Services
{
    public class RegisterUserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ILogger<RegisterUserService>> _loggerMock;
        private readonly RegisterUserService _registerUserService;
    
        public RegisterUserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<RegisterUserService>>();

            _registerUserService = new RegisterUserService(
                _userRepositoryMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task RegisterAsync_ShouldThrowException_WhenEmailAlreadyExists()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                Email = "test@test.com",
                Password = "password"
            };

            var existingUser = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(dto.Email))
                .ReturnsAsync(existingUser);

            // Act + Assert
            await Assert.ThrowsAsync<UserAlreadyExistsException>(
                () => _registerUserService.RegisterAsync(dto));
        }

        [Fact]
        public async Task RegisterAsync_ShouldAddUser_WhenEmailIsUnique()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                Email = "test@test.com",
                Password = "password"
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(dto.Email))
                .ReturnsAsync((User?)null);

            User? capturedUser = null;

            _userRepositoryMock
                .Setup( r => r.AddAsync(It.IsAny<User>()))
                .Callback<User>(user =>
                {
                    capturedUser = user;
                })
                .Returns(Task.CompletedTask);
            
            // Act
            await _registerUserService.RegisterAsync(dto);

            // Assert

            _userRepositoryMock.Verify(
                r => r.AddAsync(It.IsAny<User>()),
                Times.Once());

            Assert.NotNull(capturedUser);

            Assert.Equal(dto.Email, capturedUser!.Email);
        }

        [Fact]
        public async Task RegisterAsync_ShouldHashPassword()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                Email = "test@test.com",
                Password = "my-plain-password"
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(dto.Email))
                .ReturnsAsync((User?)null);

            var capturedUser = (User?)null;

            _userRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<User>()))
                .Callback<User>(user =>
                {
                    capturedUser = user;
                })
                .Returns(Task.CompletedTask);

            // Act
            await _registerUserService.RegisterAsync(dto);

            // Assert

            Assert.NotEqual(dto.Password, capturedUser!.PasswordHash);

            Assert.True(BCrypt.Net.BCrypt.Verify(dto.Password, capturedUser!.PasswordHash));
        }
    }
}
