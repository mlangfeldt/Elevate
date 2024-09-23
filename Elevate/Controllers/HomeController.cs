using Elevate.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Elevate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Courses()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Library()
        {
            return View();
        }
        public IActionResult Retirement()
        {
            return View();
        }

        public IActionResult Budgeting()
        {
            return View();
        }

        public IActionResult DebtManagement()
        {
            return View();
        }

        public IActionResult Investment()
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
