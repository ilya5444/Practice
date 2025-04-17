using WebAPI.Domain.Models;

namespace WebAPI.Domain.Repositories;

public interface IUserRepository
{
    public User? Add(User user);

    public User? FindByEmail(string email);

    public User? FindById(int id);
}