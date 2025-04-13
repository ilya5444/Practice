using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebMVC.Constants;
using WebMVC.Contracts;
using WebMVC.Dao;
using WebMVC.Database;
using WebMVC.Models;

namespace WebMVC.Services;

public class AuthService : IAuthService
{
    private IUserDao users;

    private IRoleDao roles;

    private IPasswordHasher<object> passwordHasher;

    public AuthService(IUserDao users, IRoleDao roles, IPasswordHasher<object> passwordHasher)
    {
        this.users = users;
        this.roles = roles;
        this.passwordHasher = passwordHasher;
    }

    public User? Register(RegisterContract data)
    {
        User user = new()
        {
            Name = data.Name,
            Email = data.Email,
            Password = passwordHasher.HashPassword(null!, data.Password),
            Role = roles.GetId(Roles.User)
        };

        return users.Add(user);
    }

    public User? Authorize(LoginContract data)
    {
        User? user = users.FindByEmail(data.Email);

        if (user == null)
        {
            return null;
        }

        var passwordVerifyResult = passwordHasher.VerifyHashedPassword(null!, user.Password, data.Password);

        if (passwordVerifyResult == PasswordVerificationResult.Success)
        {
            return user;
        }

        return null;
    }
}
