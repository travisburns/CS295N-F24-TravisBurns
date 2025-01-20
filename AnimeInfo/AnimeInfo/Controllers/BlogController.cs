using AnimeInfo.Models;
using AnimeInfo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeInfo.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var blogPosts = await _context.Blogs
                .Include(b => b.BlogAuthor)
                .OrderByDescending(b => b.BlogDate)
                .ToListAsync();
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
        public async Task<IActionResult> Post(Blog model)
        {
            if (ModelState.IsValid)
            {
                model.BlogDate = DateTime.Now;

                // Save the AppUser first
                if (model.BlogAuthor != null)
                {
                    model.BlogAuthor.SignUpdate = DateTime.Now;
                    _context.AppUsers.Add(model.BlogAuthor);
                    await _context.SaveChangesAsync();
                }

                // Then save the blog post
                _context.Blogs.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}