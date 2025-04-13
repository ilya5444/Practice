using WebMVC.Dao;
using WebMVC.Models;

namespace WebMVC.Services;

public class StudentService : IStudentService
{
    private IStudentDao students;

    public StudentService(IStudentDao students)
    {
        this.students = students;
    }

    public List<Student> GetAllStudents()
    {
        return students.FindAll();
    }

    public List<Student> GetStudentsByGroup(string groupNumber)
    {
        return students.FindAllByGroup(groupNumber);
    }
}
