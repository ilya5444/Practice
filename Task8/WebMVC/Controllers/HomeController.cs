using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Constants;
using WebMVC.Services;

namespace WebMVC.Controllers;

[Route("")]
public class HomeController : Controller
{
    private readonly IStudentService studentService;

    private readonly IUserService userService;

    public HomeController(IStudentService studentService, IUserService userService)
    {
        this.studentService = studentService;
        this.userService = userService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewData["UserRole"] = userService.GetCurrentUserRole(User.Claims);

        return View();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("Students")]
    public IActionResult Students()
    {
        ViewData["Students"] = studentService.GetAllStudents();
        ViewData["UserRole"] = Roles.Admin;

        return View("Students");
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("Search")]
    public IActionResult Search()
    {
        ViewData["Students"] = studentService.GetAllStudents();
        ViewData["UserRole"] = Roles.Admin;

        return View();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("Search")]
    public IActionResult StudentsByGroup([FromForm] string? groupNumber)
    {
        groupNumber ??= string.Empty;

        var studentsByGroup = studentService.GetStudentsByGroup(groupNumber);

        ViewData["Students"] = studentsByGroup.ToList();
        ViewData["UserRole"] = Roles.Admin;

        return View("Search");
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("SearchAsync")]
    public IActionResult SearchAsync()
    {
        ViewData["Students"] = studentService.GetAllStudents();
        ViewData["UserRole"] = Roles.Admin;

        return View("SearchAsync");
    }

    [Authorize]
    [HttpGet("Account")]
    public IActionResult Account()
    {
        var user = userService.GetCurrentUser(User.Claims)!;

        ViewData["UserRole"] = user.RoleNavigation.Name;

        ViewData["Name"] = user.Name;
        ViewData["Email"] = user.Email;
        ViewData["Role"] = Roles.GetLocalized(user.RoleNavigation.Name);

        return View();
    }

    [HttpGet("Privacy")]
    public IActionResult Privacy()
    {
        ViewData["UserRole"] = userService.GetCurrentUserRole(User.Claims);

        return View();
    }
}
