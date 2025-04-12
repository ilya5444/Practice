using Microsoft.EntityFrameworkCore;
using WebMVC.Database;
using WebMVC.Models;
using WebMVC.Constants;
using System.Security.Claims;

namespace WebMVC.Services;

public class UserService : IUserService
{
    private readonly PracticeDbContext dbContext;

    public UserService(PracticeDbContext dbContext)
        => this.dbContext = dbContext;

    public User? GetCurrentUser(IEnumerable<Claim> claims)
    {
        Claim? userIdClaim = claims.FirstOrDefault(x => x.Type.Equals(Jwt.UserIdClaimType), null!);

        if (userIdClaim == null)
        {
            return null;
        }

        return GetUser(int.Parse(userIdClaim.Value));
    }

    public string GetCurrentUserRole(IEnumerable<Claim> claims)
    {
        Claim? userIdClaim = claims.FirstOrDefault(x => x.Type.Equals(Jwt.UserIdClaimType), null!);

        if (userIdClaim == null)
        {
            return Roles.Anonymous;
        }

        var user = GetUser(int.Parse(userIdClaim.Value));

        if (user == null)
        {
            return Roles.Anonymous;
        }

        return user.RoleNavigation.Name;
    }

    private User? GetUser(int userId)
    {
        User? user;

        try
        {
            user = dbContext.Users
                 .Include(x => x.RoleNavigation)
                 .First(x => x.UserId == userId);
        }
        catch (InvalidOperationException)
        {
            user = null;
        }

        return user;
    }
}