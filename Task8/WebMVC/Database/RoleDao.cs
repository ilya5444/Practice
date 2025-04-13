using Microsoft.EntityFrameworkCore;
using WebMVC.Constants;
using WebMVC.Dao;

namespace WebMVC.Database;

public class RoleDao : IRoleDao
{
    private PracticeDbContext db;

    public RoleDao(PracticeDbContext db)
    {
        this.db = db;
    }

    public short GetId(string role)
    {
        return db.Roles
            .First(role => role.Name.Equals(Roles.User))
            .RoleId;
    }
}
