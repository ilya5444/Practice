using WebAPI.Domain.Constants;
using System.Security.Claims;
using WebAPI.Domain.Models;
using WebAPI.Domain.Repositories;

namespace WebAPI.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository users;

    public UserService(IUserRepository users)
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