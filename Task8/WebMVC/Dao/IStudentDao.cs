using WebMVC.Models;

namespace WebMVC.Dao;

public interface IStudentDao
{
    public List<Student> FindAll();

    public List<Student> FindAllByGroup(string groupNumber);
}
