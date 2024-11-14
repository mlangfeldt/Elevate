using Elevate.PL;
using Microsoft.AspNetCore.Mvc;

namespace Elevate.UI.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly ElevateEntities _context;

        // Combined constructor
        public CoursesController(ILogger<CoursesController> logger, ElevateEntities context)
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
                ImgUrl = c.ImgUrl,
                Name = c.Name,
                Description = c.Description,
                Cost = c.Cost
            }).ToList();

            return View(courses);
        }
    }
}
