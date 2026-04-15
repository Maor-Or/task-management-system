using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Services;
using TaskManagement.Application.Common;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/auth")]
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

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        _logger.LogInformation("Register attempt for {Email}", dto.Email);

        await _registerUserService.RegisterAsync(dto);

        _logger.LogInformation("User registered successfully: {Email}", dto.Email);

        return Ok(new ApiResponse<object>(true, "User registered successfully"));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        _logger.LogInformation("Login attempt for {Email}", dto.Email);

        var token = await _loginUserService.LoginAsync(dto);

        _logger.LogInformation("Login successful for {Email}", dto.Email);

        return Ok(new ApiResponse<object>(true, "Login successful", new { token }));
    }
}