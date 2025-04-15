namespace WebApp.Domain.Entities;

public class Specialization
{
    public short SpecializationId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
