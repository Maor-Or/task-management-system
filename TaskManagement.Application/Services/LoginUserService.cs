using BCrypt.Net;
using Microsoft.Extensions.Logging;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Interfaces.Repositories;
using TaskManagement.Application.Interfaces.Security;

namespace TaskManagement.Application.Services
{
    public class LoginUserService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<LoginUserService> _logger;

        public LoginUserService(IUserRepository userRepository, IJwtTokenService jwtTokenService, ILogger<LoginUserService> logger)
        {
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<string> LoginAsync(LoginUserDto dto)
        {
            _logger.LogInformation("Logging in user with Email: {Email}", dto.Email);
            var user =  await _userRepository.GetByEmailAsync(dto.Email);

            bool invalidCredentials = user == null 
                || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (invalidCredentials)
            {
                _logger.LogWarning("invalid login attempt for Email: {Email}", dto.Email);
                throw new InvalidCredentialsException();
            }
            _logger.LogInformation("Login successful for userId: {userId}", user.Id);

            return _jwtTokenService.CreateToken(user);
        }
    }
}
