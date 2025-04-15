using WebApp.Domain.Entities;

namespace WebApp.Usecase.Services;

public interface IStudentService
{
    public List<Student> GetAllStudents();

    public List<Student> GetStudentsByGroup(string groupNumber);
}
