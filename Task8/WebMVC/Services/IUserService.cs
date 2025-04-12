using System.Security.Claims;
using WebMVC.Models;

namespace WebMVC.Services;

public interface IUserService
{
    public User? GetCurrentUser(IEnumerable<Claim> claims);

    public string GetCurrentUserRole(IEnumerable<Claim> claims);
}
