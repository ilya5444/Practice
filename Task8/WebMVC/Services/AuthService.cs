using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebMVC.Constants;
using WebMVC.Contracts;
using WebMVC.Database;
using WebMVC.Models;

namespace WebMVC.Services;

public class AuthService : IAuthService
{
    private readonly PracticeDbContext dbContext;

    private readonly IPasswordHasher<object> passwordHasher;

    public AuthService(PracticeDbContext dbContext, IPasswordHasher<object> passwordHasher)
    {
        this.dbContext = dbContext;
        this.passwordHasher = passwordHasher;
    }

    public User? Register(RegisterContract data)
    {
        User user = new()
        {
            Name = data.Name,
            Email = data.Email,
            Password = passwordHasher.HashPassword(null!, data.Password),
            Role = dbContext.Roles.First(role => role.Name.Equals(Roles.User)).RoleId
        };

        var userEntity = dbContext.Users.Add(user);

        try
        {
            dbContext.SaveChanges();
        }
        catch (DbUpdateException)
        {
            return null;
        }

        return userEntity.Entity;
    }

    public User? Authorize(LoginContract data)
    {
        User user;

        try
        {
            user = dbContext.Users
                            .Include(x => x.RoleNavigation)
                            .First(x => x.Email.Equals(data.Email));
        }
        catch (InvalidOperationException)
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
