using WebMVC.Contracts;
using WebMVC.Models;

namespace WebMVC.Services;

public interface IAuthService
{
    public User? Register(RegisterContract data);

    public User? Authorize(LoginContract data);
}
