using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebMVC.Database;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class StudentService : IStudentService
    {
        private PracticeDbContext dbContext;

        public StudentService(PracticeDbContext dbContext)
            => this.dbContext = dbContext;

        public IIncludableQueryable<Student, Institute> GetAllStudents()
            => dbContext.Students
                .Include(student => student.SpecializationNavigation)
                .Include(student => student.GroupNavigation)
                .ThenInclude(group => group.InstituteNavigation);

        public IQueryable<Student> GetStudentsByGroup(string groupNumber)
            => from student in GetAllStudents()
               where student.GroupNavigation.GroupNumber.Contains(groupNumber)
               select student;
    }
}
