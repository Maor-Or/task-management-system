using BCrypt.Net;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces.Repositories;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace TaskManagement.Application.Services
{
    public class RegisterUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<RegisterUserService> _logger;

        public RegisterUserService(IUserRepository userRepository, ILogger<RegisterUserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task RegisterAsync(RegisterUserDto dto)
        {
            _logger.LogInformation("Registering new user with Email {Email}", dto.Email);
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);

            if(existingUser != null)
            {
                throw new UserAlreadyExistsException(dto.Email);
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                PasswordHash = passwordHash
            };

            await _userRepository.AddAsync(user);

            _logger.LogInformation("Registered user, Id:{userId}, Email: {Email}",user.Id,user.Email);
        }
    }
}
