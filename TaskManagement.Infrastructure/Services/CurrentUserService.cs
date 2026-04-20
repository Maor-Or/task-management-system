using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskManagement.Application.Interfaces.Services;

namespace TaskManagement.Infrastructure.Services;

public class CurrentUserService
    : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        var userId =
            _httpContextAccessor
            .HttpContext?
            .User
            .FindFirstValue(
                ClaimTypes.NameIdentifier);

        if (userId == null)
            throw new UnauthorizedAccessException();

        return Guid.Parse(userId);
    }
}