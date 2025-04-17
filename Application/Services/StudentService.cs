using WebAPI.Domain.Repositories;
using WebAPI.Domain.Models;

namespace WebAPI.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository students;

    public StudentService(IStudentRepository students)
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
