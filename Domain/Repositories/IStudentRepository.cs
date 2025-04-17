using WebAPI.Domain.Models;

namespace WebAPI.Domain.Repositories;

public interface IStudentRepository
{
    public List<Student> FindAll();

    public List<Student> FindAllByGroup(string groupNumber);
}
