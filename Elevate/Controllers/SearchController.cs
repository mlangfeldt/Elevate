using Elevate.PL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Elevate.BL.Models;

namespace Elevate.UI.Controllers
{
    public class SearchController : Controller
    {
        private readonly Dictionary<string, string> _pages = new Dictionary<string, string>
        {
            {"budget", "/Courses/CourseDetails/1" },
            {"debt management", "/Courses/CourseDetails/2" },
            {"investment", "/Courses/CourseDetails/3" },
            {"retirement", "/Courses/CourseDetails/4" },
            {"debt leverage", "/Courses/CourseDetails/5" },
            {"day trading", "/Courses/CourseDetails/6" }
        };

        [HttpGet("/search")]
        public IActionResult Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return RedirectToAction("NotFound"); 
            }
            
            search = search.ToLower(); 
            
            using (ElevateEntities dc = new ElevateEntities())
            {
                var foundCourse = dc.tblCourses.FirstOrDefault(c => EF.Functions.Like(c.Name.ToLower(), $"%{search}%")); 
                
                if (foundCourse == null) 
                { 
                    return RedirectToAction("NotFound"); 
                }

                var courseName = foundCourse.Name.ToLower(); 
                
                if (_pages.ContainsKey(courseName)) 
                {
                    var pageUrl = _pages[courseName]; 
                    return Redirect(pageUrl); 
                } 
                else 
                { 
                    return RedirectToAction("NotFound"); 
                }
            } 
        }

                [HttpGet("/notfound")]
        public IActionResult NotFound()
        {
            return View();
        }
    }
}