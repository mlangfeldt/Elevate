using Elevate.Models;
using Elevate.PL;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Elevate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ElevateEntities _context;

        // Combined constructor
        public HomeController(ILogger<HomeController> logger, ElevateEntities context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var tblCourses = _context.tblCourses.ToList();

            var courses = tblCourses.Select(c => new Elevate.BL.Models.Course
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Cost = c.Cost
            }).ToList();

            return View(courses);
        }

        public IActionResult About()
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
