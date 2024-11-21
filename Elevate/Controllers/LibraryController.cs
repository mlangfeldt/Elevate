using Elevate.BL;
using Elevate.BL.Models;
using Microsoft.AspNetCore.Mvc;

public class LibraryController : Controller
{
    public IActionResult Index()
    {
        try
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var courses = CollectionManager.GetCoursesByUserId(userId.Value);

            return View(courses);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "An error occurred while loading your library: " + ex.Message;
            return View(new List<Course>());
        }
    }
}
