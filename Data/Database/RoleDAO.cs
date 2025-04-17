using WebAPI.Domain.Constants;
using WebAPI.Domain.Repositories;

namespace WebAPI.Data.Database;

public class RoleDAO : IRoleRepository
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
