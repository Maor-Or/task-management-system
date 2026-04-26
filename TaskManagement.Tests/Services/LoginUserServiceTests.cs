using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Interfaces.Repositories;
using TaskManagement.Application.Interfaces.Security;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Entities;
using Xunit;

namespace TaskManagement.Tests.Services
{
    public class LoginUserServiceTests
    {
        private readonly Mock<IJwtTokenService> _jwtTokenServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ILogger<LoginUserService>> _loggerMock;

        private readonly LoginUserService _loginUserService;

        public LoginUserServiceTests() 
        {
            _jwtTokenServiceMock = new Mock<IJwtTokenService>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<LoginUserService>>();

            _loginUserService = new LoginUserService(_userRepositoryMock.Object, _jwtTokenServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange

            var dto = new LoginUserDto
            {
                Email = "test@test.com",
                Password = "password"
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(dto.Email))
                .ReturnsAsync((User?)null);

            // Act + assert

            await Assert.ThrowsAsync<InvalidCredentialsException>(
                () => _loginUserService.LoginAsync(dto));
        }

        [Fact]
        public async Task LoginAsync_ShouldThrowException_IncorrectPassword()
        {
            // Arrange
            var dto = new LoginUserDto
            {
                Email = "test@test.com",
                Password = "wrong-password"
            };

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("correct-password")
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(dto.Email))
                .ReturnsAsync(user);

            //act + assert
            await Assert.ThrowsAsync<InvalidCredentialsException>(
                () => _loginUserService.LoginAsync(dto));
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var dto = new LoginUserDto
            {
                Email = "test@test.com",
                Password = "password"
            };

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password")
            };

            _userRepositoryMock
                .Setup( r => r.GetByEmailAsync(dto.Email))
                .ReturnsAsync(user);

            var token = "fake-jwt-token";
            _jwtTokenServiceMock
                .Setup(t => t.CreateToken(user))
                .Returns(token);

            // Act 
            var result = await _loginUserService.LoginAsync(dto);

            // Assert
            Assert.Equal(token, result);

            _jwtTokenServiceMock.Verify(
                t => t.CreateToken(user),
                Times.Once);
        }
    }
}
