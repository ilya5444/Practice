using WebAPI.Application.Dto;
using WebAPI.Domain.Models;

namespace WebAPI.Application.Services;

public interface IAuthService
{
    public User? Register(RegisterDTO data);

    public User? Authorize(LoginDTO data);
}
