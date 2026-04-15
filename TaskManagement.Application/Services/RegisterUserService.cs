using BCrypt.Net;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces.Repositories;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Exceptions;

namespace TaskManagement.Application.Services
{
    public class RegisterUserService
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(RegisterUserDto dto)
        {
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
        }
    }
}
