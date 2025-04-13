using Microsoft.EntityFrameworkCore;
using WebMVC.Dao;
using WebMVC.Models;

namespace WebMVC.Database;

public class StudentDao : IStudentDao
{
    private PracticeDbContext db;

    public StudentDao(PracticeDbContext db)
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
