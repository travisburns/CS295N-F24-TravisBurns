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

    public IQueryable<Blog> Blogs
    {
        get
        {
            return context.Blogs
                .Include(blog => blog.BlogAuthor);
        }
    }

    public List<Blog> GetReviews()
    {
        return context.Blogs
            .Include(blog => blog.BlogAuthor)
            .ToList();
    }

    public List<Blog> GetBlogs()  // Changed from string? to List<Blog> to match interface
    {
        return context.Blogs
            .Include(blog => blog.BlogAuthor)
            .ToList();
    }

    public Blog GetBlogById(int id)
    {
        return context.Blogs
            .Include(blog => blog.BlogAuthor)
            .Where(blog => blog.Id == id)
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