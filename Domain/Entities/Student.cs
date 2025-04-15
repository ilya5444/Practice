namespace WebApp.Domain.Entities;

public class Student
{
    public int StudentId { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public short Specialization { get; set; }

    public string Group { get; set; } = null!;

    public short Year { get; set; }

    public short AdmissionYear { get; set; }

    public DateOnly Birthdate { get; set; }

    public virtual Group GroupNavigation { get; set; } = null!;

    public virtual Specialization SpecializationNavigation { get; set; } = null!;
}
