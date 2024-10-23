using AnimeInfo.Models;
using Microsoft.AspNetCore.Mvc;


namespace AnimeInfo.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            //TODO: get the blog objects and put them into a list. 
            List<Blog> blogs;

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

        public IActionResult Blogs()
        {
            return View();
        }
    }
}
