using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces.Security
{
    public interface IJwtTokenService
    {
        string CreateToken(User user);
    }
}
