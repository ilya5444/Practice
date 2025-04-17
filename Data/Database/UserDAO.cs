using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Repositories;
using WebAPI.Domain.Models;

namespace WebAPI.Data.Database;

public class UserDAO : IUserRepository
{
    private readonly PracticeDbContext db;

    public UserDAO(PracticeDbContext db)
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
