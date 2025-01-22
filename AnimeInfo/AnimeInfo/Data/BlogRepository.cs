using AnimeInfo.Data;
using AnimeInfo.Models;
using Microsoft.EntityFrameworkCore;

public class BlogRepository : IBlogRepository
{
    private readonly ApplicationDbContext context;
    public BlogRepository(ApplicationDbContext appDbContext)
    {
        context = appDbContext;
    }

    public void AddComment(Comment comment)
    {
        context.Comments.Add(comment);
        context.SaveChanges();
    }

    public IQueryable<Blog> Blogs
    {
        get
        {
            return context.Blogs
                .Include(blog => blog.BlogAuthor)
                .Include(blog => blog.Comments)
                    .ThenInclude(comment => comment.CommentAuthor);
        }
    }

    public List<Blog> GetReviews()
    {
        return context.Blogs
            .Include(blog => blog.BlogAuthor)
            .Include(blog => blog.Comments)
                .ThenInclude(comment => comment.CommentAuthor)
            .ToList();
    }

    public List<Blog> GetBlogs()
    {
        return context.Blogs
            .Include(blog => blog.BlogAuthor)
            .Include(blog => blog.Comments)
                .ThenInclude(comment => comment.CommentAuthor)
            .ToList();
    }

    public Blog? GetBlogById(int id)
    {
        return context.Blogs
            .Include(blog => blog.BlogAuthor)
            .Include(blog => blog.Comments)
                .ThenInclude(comment => comment.CommentAuthor)
            .Where(blog => blog.BlogId == id)
            .SingleOrDefault();
    }

    public int StoreReview(Blog model)
    {
        model.BlogDate = DateTime.Now;
        context.Blogs.Add(model);
        return context.SaveChanges();
    }

    public void StoreBlog(Blog model)
    {
        model.BlogDate = DateTime.Now;
        context.Blogs.Add(model);
        context.SaveChanges();
    }
}