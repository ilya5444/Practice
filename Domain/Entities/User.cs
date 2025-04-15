namespace WebApp.Domain.Entities;

public class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public short Role { get; set; }

    public virtual Role RoleNavigation { get; set; } = null!;
}
