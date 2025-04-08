using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Database;
using Microsoft.EntityFrameworkCore;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private PracticeDbContext dbContext;

        public HomeController(PracticeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Students"] = dbContext.Students
                .Include(student => student.SpecializationNavigation)
                .Include(student => student.GroupNavigation)
                .ThenInclude(group => group.InstituteNavigation)
                .ToList();

            return View();
        }

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
