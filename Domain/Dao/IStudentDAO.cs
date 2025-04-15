using WebApp.Domain.Entities;

namespace WebApp.Domain.Dao;

public interface IStudentDAO
{
    public List<Student> FindAll();

    public List<Student> FindAllByGroup(string groupNumber);
}
