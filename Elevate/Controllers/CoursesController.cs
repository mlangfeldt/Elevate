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

            // Hard-code reviews based on the courseId
            List<dynamic> reviews = new List<dynamic>();

            if (id == 1) // Retirement
            {
                reviews.Add(new { Rating = 5, ReviewText = "This course helped me a lot in planning my future. Highly recommend!", ReviewerName = "Kelly Rowan" });
                reviews.Add(new { Rating = 4, ReviewText = "Great course with useful tips, but could use more examples.", ReviewerName = "Jeff Dunham" });
            }
            else if (id == 2) // Debt Management
            {
                reviews.Add(new { Rating = 4, ReviewText = "Informative, but I would have liked more detailed steps on budgeting.", ReviewerName = "Java Script" });
                reviews.Add(new { Rating = 5, ReviewText = "A must-take course for anyone struggling with debt. Fantastic!", ReviewerName = "Joe Smith" });
            }
            else if (id == 3) // Investment
            {
                reviews.Add(new { Rating = 5, ReviewText = "This course was excellent. It provided me with all the tools I need to start investing.", ReviewerName = "Lisa Simon" });
                reviews.Add(new { Rating = 3, ReviewText = "Good course, but the content was a bit basic for my level.", ReviewerName = "James Brown" });
            }
            else if (id == 4) // Budgeting
            {
                reviews.Add(new { Rating = 5, ReviewText = "I learned so much from this course, and it's helped me stick to a budget.", ReviewerName = "Mike White" });
                reviews.Add(new { Rating = 4, ReviewText = "Great introduction to budgeting, but I wish there were more advanced topics.", ReviewerName = "Sarah Adams" });
            }

            ViewData["Reviews"] = reviews;

            return View(course);
        }


    }
}
