using Microsoft.EntityFrameworkCore;
using WebMVC.Dao;
using WebMVC.Models;

namespace WebMVC.Database;

public class UserDao : IUserDao
{
    private PracticeDbContext db;

    public UserDao(PracticeDbContext db)
    {
        this.db = db;
    }

    public User? Add(User user)
    {
        var userEntity = db.Users.Add(user);

        try
        {
            db.SaveChanges();
        }
        catch (DbUpdateException)
        {
            return null;
        }

        return userEntity.Entity;
    }

    public User? FindByEmail(string email)
    {
        User user;

        try
        {
            user = db.Users
                    .Include(x => x.RoleNavigation)
                    .First(x => x.Email.Equals(email));
        }
        catch (InvalidOperationException)
        {
            return null;
        }

        return user;
    }

    public User? FindById(int id)
    {
        User? user;

        try
        {
            user = db.Users
                 .Include(x => x.RoleNavigation)
                 .First(x => x.UserId == id);
        }
        catch (InvalidOperationException)
        {
            user = null;
        }

        return user;
    }
}
