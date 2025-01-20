using AnimeInfo.Models;
using AnimeInfo.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnimeInfo.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _repo;

        public BlogController(IBlogRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var blogPosts = _repo.GetBlogs();
            return View("Index", blogPosts);
        }

        [HttpPost]
        public IActionResult Filter(string commenterName, DateTime? commentDate)
        {
            var blogs = _repo.GetBlogs();

            if (!string.IsNullOrEmpty(commenterName) || commentDate.HasValue)
            {
                foreach (var blog in blogs)
                {
                    if (blog.Comments != null)
                    {
                        blog.Comments = blog.Comments
                            .Where(c =>
                                (string.IsNullOrEmpty(commenterName) || c.CommentAuthor.Name.Contains(commenterName, StringComparison.OrdinalIgnoreCase)) &&
                                (!commentDate.HasValue || c.CommentDate.Date == commentDate.Value.Date))
                            .ToList();
                    }
                }
            }

            return View("Index", blogs);
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
            try
            {
                var blogs = _repo.GetBlogs();
                return View(blogs);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }
        }

        [HttpPost]
        public IActionResult Post(Blog model)
        {
            model.BlogDate = DateTime.Now;
            _repo.StoreBlog(model);
            return RedirectToAction("Index");
        }
    }
}