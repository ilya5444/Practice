using WebApp.Domain.Dao;
using WebApp.Domain.Entities;

namespace WebApp.Usecase.Services;

public class StudentService : IStudentService
{
    private readonly IStudentDAO students;

    public StudentService(IStudentDAO students)
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
