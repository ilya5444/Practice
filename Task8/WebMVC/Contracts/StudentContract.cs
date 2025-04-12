using WebMVC.Models;

namespace WebMVC.Contracts;

public class StudentContract(Student student)
{
    public string Lastname { get; set; } = student.Lastname;

    public string Firstname { get; set; } = student.Firstname;

    public string Specialization { get; set; } = student.SpecializationNavigation.Name;

    public string Institute { get; set; } = student.GroupNavigation.InstituteNavigation.InstituteName;

    public string Group { get; set; } = student.GroupNavigation.GroupNumber;

    public int Year { get; set; } = student.Year;

    public int AdmissionYear { get; set; } = student.AdmissionYear;

    public DateOnly Birthdate { get; set; } = student.Birthdate;
}
