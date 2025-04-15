namespace WebApp.Domain.Entities;

public class Role
{
    public short RoleId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
