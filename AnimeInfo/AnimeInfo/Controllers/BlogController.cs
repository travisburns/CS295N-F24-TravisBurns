using AnimeInfo.Models;
using AnimeInfo.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace AnimeInfo.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _repo;
        private readonly ILogger<BlogController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public BlogController(IBlogRepository repo, UserManager<AppUser> userManager,
            ILogger<BlogController> logger)
        {
            _repo = repo;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var blogPosts = await _repo.GetBlogs();
            return View("Index", blogPosts);
        }

        [HttpPost]
        public async Task<IActionResult> Filter(string commenterName, DateTime? commentDate)
        {
            var blogs = await _repo.GetBlogs();
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

        [Authorize]
        public IActionResult Post()
        {
            Blog model = new Blog();
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

        public async Task<IActionResult> Blogs()
        {
            try
            {
                var blogs = await _repo.GetBlogs();
                return View(blogs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving blogs");
                return View("Error", new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(Blog model)
        {
            // Get the AppUser object for the current user
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                model.BlogDate = DateTime.Now;
                model.BlogAuthor = user;  // Set the current user as the blog author
               await _repo.StoreBlog(model);
                return RedirectToAction("Index");
            }

            // Handle case where user isn't logged in
            return RedirectToAction("Register", "Account");
        }

        public async Task<IActionResult> Details(int id)
        {
            var blog = await _repo.GetBlogById(id);
            if (blog == null)
                return NotFound();
            return View(blog);
        }



        [HttpPost]
        public async Task<IActionResult> AddReply(int commentId, string replyText)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                // Get the comment
                var comment = await _repo.GetCommentById(commentId);

                if (comment != null && user != null)
                {
                    var reply = new Reply
                    {
                        ReplyText = replyText,
                        ReplyDate = DateTime.Now,
                        ReplyAuthor = user,
                        CommentId = commentId,
                        Comment = comment
                    };

                    // Add the new reply
                    await _repo.AddReply(reply);
                    return RedirectToAction("Details", new { id = comment.BlogId });
                }
            }
            return RedirectToAction("Register", "Account");
        }


        [HttpPost]
        public async Task<IActionResult> AddComment(int blogId, string commentText)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var comment = new Comment
                {
                    CommentText = commentText,
                    CommentDate = DateTime.Now,
                    CommentAuthor = user,
                    BlogId = blogId
                };
                // Add method to repo to save comments - not sure if this was part of requirments for lab2 but saw in the videos that saving comments on a post was demonstrated. 
               await _repo.AddComment(comment);
                return RedirectToAction("Details", new { id = blogId });
            }
            return RedirectToAction("Register", "Account");
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _repo.GetCommentById(id);
            if (comment == null)
                return NotFound();

            int blogId = comment.BlogId;
            await _repo.DeleteComment(id);
            return RedirectToAction("Details", new { id = blogId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteReply(int id, int commentId)
        {
            var comment = await _repo.GetCommentById(commentId);
            if (comment == null)
                return NotFound();

            await _repo.DeleteReply(id);
            return RedirectToAction("Details", new { id = comment.BlogId });
        }


    }



}

//