using System.Security.Claims;
using WebApp.Domain.Entities;

namespace WebApp.Usecase.Services;

public interface IUserService
{
    public User? GetCurrentUser(IEnumerable<Claim> claims);

    public string GetCurrentUserRole(IEnumerable<Claim> claims);
}
