using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain.Constants;
using WebAPI.Dto;
using WebAPI.Application.Services;

namespace WebAPI.Controllers;

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
    [HttpGet]
    public IEnumerable<StudentDTO> Get()
    {
        return from student in service.GetAllStudents()
               select new StudentDTO(student);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("{groupNumber}")]
    public IEnumerable<StudentDTO> Get([FromRoute] string? groupNumber)
    {
        if (groupNumber == null)
        {
            return from student in service.GetAllStudents()
                   select new StudentDTO(student);
        }

        var students = service.GetStudentsByGroup(groupNumber);

        return from student in students
               select new StudentDTO(student);
    }
}
