using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Constants;
using WebMVC.Contracts;
using WebMVC.Services;

namespace WebMVC.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private IStudentService service;

    public StudentController(IStudentService service)
    {
        this.service = service;
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IEnumerable<StudentContract> Get([FromBody] string? groupNumber)
    {
        groupNumber ??= string.Empty;

        var students = service.GetStudentsByGroup(groupNumber);

        return from student in students
               select new StudentContract(student);
    }
}
