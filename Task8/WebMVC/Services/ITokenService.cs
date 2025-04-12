namespace WebMVC.Services;

public interface ITokenService
{
    public string GenerateToken(string userId, string role);
}