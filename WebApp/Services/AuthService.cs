using Microsoft.AspNetCore.Identity;
using WebApp.Usecase.Dto;
using WebApp.Domain.Dao;
using WebApp.Domain.Constants;
using WebApp.Domain.Entities;
using WebApp.Usecase.Services;

namespace WebApp.Services;

public class AuthService : IAuthService
{
    private readonly IUserDAO users;

    private readonly IRoleDAO roles;

    private readonly IPasswordHasher<object> passwordHasher;

    public AuthService(IUserDAO users, IRoleDAO roles, IPasswordHasher<object> passwordHasher)
    {
        this.users = users;
        this.roles = roles;
        this.passwordHasher = passwordHasher;
    }

    public User? Register(RegisterDTO data)
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

    public User? Authorize(LoginDTO data)
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
