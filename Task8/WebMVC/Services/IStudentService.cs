using Microsoft.EntityFrameworkCore.Query;
using WebMVC.Models;

namespace WebMVC.Services;

public interface IStudentService
{
    public List<Student> GetAllStudents();

    public List<Student> GetStudentsByGroup(string groupNumber);
}
