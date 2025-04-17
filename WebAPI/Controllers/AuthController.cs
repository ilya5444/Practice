using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain.Constants;
using WebAPI.Application.Services;
using WebAPI.Domain.Models;
using WebAPI.Application.Dto;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    private readonly ITokenService tokenService;

    public AuthController(IAuthService authService, ITokenService tokenService)
    {
        this.authService = authService;
        this.tokenService = tokenService;
    }

    [HttpPost("Register")]
    public IActionResult Register([FromBody] RegisterDTO credentials)
    {
        User? user = authService.Register(credentials);

        if (user == null)
        {
            return Unauthorized("Аккаунт с таким Email уже существует.");
        }

        var token = tokenService.GenerateToken(user.UserId.ToString(), Roles.User);

        return Ok(token);
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginDTO credentials)
    {
        User? user = authService.Authorize(credentials);

        if (user == null)
        {
            return Unauthorized("Неверный логин или пароль.");
        }

        var token = tokenService.GenerateToken(user.UserId.ToString(), user.RoleNavigation.Name);

        return Ok(token);
    }
}
