using Microsoft.AspNetCore.Mvc;
using WebApp.Domain.Constants;
using WebApp.Usecase.Services;
using WebApp.Domain.Entities;
using WebApp.Attributes;
using Microsoft.AspNetCore.Authorization;
using WebApp.Usecase.Dto;

namespace WebApp.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService authService;

    private readonly ITokenService tokenService;

    public AuthController(IAuthService authService, ITokenService tokenService)
    {
        this.authService = authService;
        this.tokenService = tokenService;
    }

    [UnauthorizedOnly]
    [HttpGet("Register")]
    public IActionResult RegisterPage()
    {
        return View("Register");
    }

    [UnauthorizedOnly]
    [HttpPost("Register")]
    public IActionResult Register([FromForm] RegisterDTO credentials)
    {
        User? user = authService.Register(credentials);

        if (user == null)
        {
            ViewData["EmailIsOccupied"] = "Email адрес уже занят";

            return View();
        }

        var token = tokenService.GenerateToken(user.UserId.ToString(), Roles.User);

        AddTokenToCookies(token);

        return Redirect("/");
    }

    [UnauthorizedOnly]
    [HttpGet("Login")]
    public IActionResult LoginPage()
    {
        return View("Login");
    }

    [UnauthorizedOnly]
    [HttpPost("Login")]
    public IActionResult Login([FromForm] LoginDTO credentials)
    {
        User? user = authService.Authorize(credentials);

        if (user == null)
        {
            ViewData["InvalidCredentials"] = "Неверный логин или пароль";

            return View();
        }

        var token = tokenService.GenerateToken(user.UserId.ToString(), user.RoleNavigation.Name);

        AddTokenToCookies(token);

        return Redirect("/");
    }

    private void AddTokenToCookies(string token)
    {
        Response.Cookies.Append(Jwt.TokenCookieKey, token, new()
        {
            HttpOnly = true,
            Secure = true,
        });
    }

    [Authorize]
    [HttpPost("Logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Append(Jwt.TokenCookieKey, string.Empty, new()
        {
            Expires = DateTime.Now.AddDays(-1)
        });

        return Redirect("/");
    }
}
