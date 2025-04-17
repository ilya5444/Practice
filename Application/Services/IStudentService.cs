using WebAPI.Domain.Models;

namespace WebAPI.Application.Services;

public interface IStudentService
{
    public List<Student> GetAllStudents();

    public List<Student> GetStudentsByGroup(string groupNumber);
}
