using Microsoft.AspNetCore.Mvc;

namespace AnimeInfo.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult TopPosts()
        {
            return View();
        }
    }
}
