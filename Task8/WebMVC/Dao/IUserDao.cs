using WebMVC.Models;

namespace WebMVC.Dao;

public interface IUserDao
{
    public User? Add(User user);

    public User? FindByEmail(string email);

    public User? FindById(int id);
}