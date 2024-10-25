using AnimeInfo.Models;
using Microsoft.AspNetCore.Mvc;



namespace AnimeInfo.Controllers
{
    public class BlogController : Controller
    {

        private static List<Blog> blogPosts = new List<Blog>();

        public IActionResult Index()
        {
            return View(blogPosts);
        }

        public IActionResult Post()
        {
            Blog model = new Blog();
            model.BlogAuthor = new AppUser();
            return View(model);
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

        [HttpPost]
        public IActionResult Post(Blog model)
        {
            if (ModelState.IsValid)
            {
                model.BlogDate = DateTime.Now;

                blogPosts.Add(model);
                return RedirectToAction("Index");  
            }
            return View(model);
        }
    }
}
