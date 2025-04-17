namespace WebAPI.Domain.Models;

public class Group
{
    public string GroupNumber { get; set; } = null!;

    public short Institute { get; set; }

    public virtual Institute InstituteNavigation { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
