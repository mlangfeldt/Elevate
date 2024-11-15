using Elevate.BL.Models;
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

        public IActionResult CourseDetails(int id)
        {
            var courseEntity = _context.tblCourses.FirstOrDefault(c => c.Id == id);

            if (courseEntity == null)
            {
                return NotFound();
            }

            var course = new Course
            {
                Id = courseEntity.Id,
                ImgUrl = courseEntity.ImgUrl,
                Name = courseEntity.Name,
                Description = courseEntity.Description,
                Cost = courseEntity.Cost
            };

            return View(course);
        }

    }
}
