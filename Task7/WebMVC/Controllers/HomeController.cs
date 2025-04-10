using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private IStudentService studentService;

        private IWebHostEnvironment environment;

        public HomeController(IStudentService studentService, IWebHostEnvironment environment)
        {
            this.studentService = studentService;
            environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Students"] = studentService.GetAllStudents().ToList();

            return View();
        }

        [HttpGet("/Search")]
        public IActionResult Search()
        {
            ViewData["Students"] = studentService.GetAllStudents().ToList();

            return View();
        }

        [HttpPost("/Search")]
        public IActionResult StudentsByGroup([FromForm] string? groupNumber)
        {
            groupNumber ??= string.Empty;

            var studentsByGroup = studentService.GetStudentsByGroup(groupNumber);

            ViewData["Students"] = studentsByGroup.ToList();

            return View("Search");
        }

        [HttpGet("/SearchAsync")]
        public IActionResult SearchAsync()
        {
            ViewData["Students"] = studentService.GetAllStudents().ToList();

            return View("SearchAsync");
        }

        [HttpGet("/Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
