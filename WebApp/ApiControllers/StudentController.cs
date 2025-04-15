using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain.Constants;
using WebApp.Dto;
using WebApp.Usecase.Services;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService service;

    public StudentController(IStudentService service)
    {
        this.service = service;
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IEnumerable<StudentDTO> Get([FromBody] string? groupNumber)
    {
        groupNumber ??= string.Empty;

        var students = service.GetStudentsByGroup(groupNumber);

        return from student in students
               select new StudentDTO(student);
    }
}
