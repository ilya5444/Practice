namespace WebAPI.Application.Services;

public interface ITokenService
{
    public string GenerateToken(string userId, string role);
}