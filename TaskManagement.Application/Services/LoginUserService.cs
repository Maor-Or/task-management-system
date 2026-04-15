using BCrypt.Net;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Exceptions;
//using TaskManagement.Domain.Entities;
using TaskManagement.Application.Interfaces.Repositories;

namespace TaskManagement.Application.Services
{
    public class LoginUserService
    {
        private readonly IUserRepository _userRepository;

        public LoginUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task LoginAsync(LoginUserDto dto)
        {
            var user =  await _userRepository.GetByEmailAsync(dto.Email);

            bool invalidCredentials = user == null 
                || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (invalidCredentials)
            {
                throw new InvalidCredentialsException();
            }

            return;
        }
    }
}
