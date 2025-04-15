using WebApp.Domain.Constants;
using System.Security.Claims;
using WebApp.Domain.Entities;
using WebApp.Domain.Dao;

namespace WebApp.Usecase.Services;

public class UserService : IUserService
{
    private readonly IUserDAO users;

    public UserService(IUserDAO users)
    {
        this.users = users;
    }

    public User? GetCurrentUser(IEnumerable<Claim> claims)
    {
        Claim? userIdClaim = claims.FirstOrDefault(x => x.Type.Equals(Jwt.UserIdClaimType), null!);

        if (userIdClaim == null)
        {
            return null;
        }

        return users.FindById(int.Parse(userIdClaim.Value));
    }

    public string GetCurrentUserRole(IEnumerable<Claim> claims)
    {
        Claim? userIdClaim = claims.FirstOrDefault(x => x.Type.Equals(Jwt.UserIdClaimType), null!);

        if (userIdClaim == null)
        {
            return Roles.Anonymous;
        }

        var user = users.FindById(int.Parse(userIdClaim.Value));

        if (user == null)
        {
            return Roles.Anonymous;
        }

        return user.RoleNavigation.Name;
    }
}