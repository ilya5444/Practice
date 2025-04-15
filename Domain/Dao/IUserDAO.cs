using WebApp.Domain.Entities;

namespace WebApp.Domain.Dao;

public interface IUserDAO
{
    public User? Add(User user);

    public User? FindByEmail(string email);

    public User? FindById(int id);
}