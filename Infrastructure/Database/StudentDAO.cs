using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Dao;
using WebApp.Domain.Entities;

namespace WebApp.Infrastructure.Database;

public class StudentDAO : IStudentDAO
{
    private readonly PracticeDbContext db;

    public StudentDAO(PracticeDbContext db)
    {
        this.db = db;
    }

    public List<Student> FindAll()
    {
        return db.Students
            .Include(student => student.SpecializationNavigation)
            .Include(student => student.GroupNavigation)
            .ThenInclude(group => group.InstituteNavigation)
            .ToList();
    }

    public List<Student> FindAllByGroup(string groupNumber)
    {
        return (from student in FindAll()
               where student.GroupNavigation.GroupNumber.Contains(groupNumber)
               select student).ToList();
    }
}
