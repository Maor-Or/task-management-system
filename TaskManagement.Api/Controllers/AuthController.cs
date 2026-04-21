using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Services;
using TaskManagement.Application.Common;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/auth")]
[Tags("Authentication")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserService _registerUserService;
    private readonly LoginUserService _loginUserService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(RegisterUserService registerUserService, LoginUserService loginUserService, ILogger<AuthController> logger)
    {
        _registerUserService = registerUserService;
        _loginUserService = loginUserService;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        _logger.LogInformation("Register attempt for {Email}", dto.Email);

        await _registerUserService.RegisterAsync(dto);

        _logger.LogInformation("User registered successfully: {Email}", dto.Email);

        return Ok(new ApiResponse<object>(true, "User registered successfully"));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        _logger.LogInformation("Login attempt for {Email}", dto.Email);

        var token = await _loginUserService.LoginAsync(dto);

        _logger.LogInformation("Login successful for {Email}", dto.Email);

        return Ok(new ApiResponse<LoginResponseDto>(true, "Login successful", new LoginResponseDto { Token = token }));
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var email = User.FindFirstValue(ClaimTypes.Email);

        return Ok(new ApiResponse<UserDto>(true, "user retrived", new UserDto {UserId = userId, Email = email}));
    }
}