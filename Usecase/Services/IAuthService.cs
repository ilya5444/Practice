using WebApp.Usecase.Dto;
using WebApp.Domain.Entities;

namespace WebApp.Usecase.Services;

public interface IAuthService
{
    public User? Register(RegisterDTO data);

    public User? Authorize(LoginDTO data);
}
