using System.Security.Claims;
using WebAPI.Domain.Models;

namespace WebAPI.Application.Services;

public interface IUserService
{
    public User? GetCurrentUser(IEnumerable<Claim> claims);

    public string GetCurrentUserRole(IEnumerable<Claim> claims);
}
