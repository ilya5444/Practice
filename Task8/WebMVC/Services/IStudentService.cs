using Microsoft.EntityFrameworkCore.Query;
using WebMVC.Models;

namespace WebMVC.Services;

public interface IStudentService
{
    public IIncludableQueryable<Student, Institute> GetAllStudents();

    public IQueryable<Student> GetStudentsByGroup(string groupNumber);
}
