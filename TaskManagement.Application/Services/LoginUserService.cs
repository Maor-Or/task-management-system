using BCrypt.Net;
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

        public LoginUserService(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
        }

        public async Task<string> LoginAsync(LoginUserDto dto)
        {
            var user =  await _userRepository.GetByEmailAsync(dto.Email);

            bool invalidCredentials = user == null 
                || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (invalidCredentials)
            {
                throw new InvalidCredentialsException();
            }

            return _jwtTokenService.CreateToken(user);
        }
    }
}
