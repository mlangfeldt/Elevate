using Microsoft.AspNetCore.Mvc;

namespace Elevate.UI.Controllers
{
    public class SearchController : Controller
    {
        private readonly Dictionary<string, string> _pages = new Dictionary<string, string>
        {
            {"Budget", "/Courses/CourseDetails/1" },
            {"Debt Management", "/Courses/CourseDetails/2" },
            {"Investment", "/Courses/CourseDetails/3" },
            {"Retirement", "/Courses/CourseDetails/4" }
        };

        [HttpGet("/search")]
        public IActionResult Search(string search)
        {
            if(string.IsNullOrEmpty(search))
            {
                return RedirectToAction("NotFound");
            }

            search = search.ToLower();

            var findMatch = _pages.Keys.OrderBy(key => Distance(search, key)).FirstOrDefault();
            
            if(findMatch == null || Distance(search, findMatch) > 2)
            {
                return RedirectToAction("NotFound");
            }
            var pageUrl = _pages[findMatch];

            // Redirect to the found page
            return Redirect(pageUrl);
        }

        private int Distance(string s, string t)
        {
            var d = new int[s.Length + 1, t.Length + 1]; 

            for (int i = 0; i <= s.Length; i++) 
            { d[i, 0] = i; }

            for (int j = 0; j <= t.Length; j++) 
            { d[0, j] = j; }

            for (int i = 1; i <= s.Length; i++) 
            { 
                for (int j = 1; j <= t.Length; j++) 
                { 
                    var cost = (t[j - 1] == s[i - 1]) ? 0 : 1; 
                    d[i, j] = new int[] { 
                        d[i - 1, j] + 1, 
                        d[i, j - 1] + 1, 
                        d[i - 1, j - 1] + cost 
                    }.Min(); 
                } 
            }
            return d[s.Length, t.Length];
        }

        [HttpGet("/notfound")]
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
