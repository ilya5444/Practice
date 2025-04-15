using WebApp.Domain.Constants;
using WebApp.Domain.Dao;

namespace WebApp.Infrastructure.Database;

public class RoleDAO : IRoleDAO
{
    private readonly PracticeDbContext db;

    public RoleDAO(PracticeDbContext db)
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
