using Microsoft.AspNetCore.Identity;
using WebAPI.Application.Dto;
using WebAPI.Domain.Repositories;
using WebAPI.Domain.Constants;
using WebAPI.Domain.Models;
using WebAPI.Application.Services;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository users;

    private readonly IRoleRepository roles;

    private readonly IPasswordHasher<object> passwordHasher;

    public AuthService(IUserRepository users, IRoleRepository roles, IPasswordHasher<object> passwordHasher)
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
