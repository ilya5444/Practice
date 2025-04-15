namespace WebApp.Usecase.Services;

public interface ITokenService
{
    public string GenerateToken(string userId, string role);
}