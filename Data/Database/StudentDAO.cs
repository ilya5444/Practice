using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Repositories;
using WebAPI.Domain.Models;

namespace WebAPI.Data.Database;

public class StudentDAO : IStudentRepository
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
