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

    public async Task AddComment(Comment comment)
    {
        context.Comments.Add(comment);
        await context.SaveChangesAsync();
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

    public async Task <List<Blog>> GetReviews()
    {
        return await context.Blogs
            .Include(blog => blog.BlogAuthor)
            .Include(blog => blog.Comments)
                .ThenInclude(comment => comment.CommentAuthor)
            .ToListAsync();
    }

    public async Task <List<Blog>> GetBlogs()
    {
        return await context.Blogs
            .Include(blog => blog.BlogAuthor)
            .Include(blog => blog.Comments)
                .ThenInclude(comment => comment.CommentAuthor)
            .ToListAsync();
    }

    public async Task <Blog?> GetBlogById(int id)
    {
        return await context.Blogs
            .Include(blog => blog.BlogAuthor)
            .Include(blog => blog.Comments)
                .ThenInclude(comment => comment.CommentAuthor)
            .Where(blog => blog.BlogId == id)
            .SingleOrDefaultAsync();
    }

    public async Task <int> StoreReview(Blog model)
    {
        model.BlogDate = DateTime.Now;
        context.Blogs.Add(model);
        return await context.SaveChangesAsync();
    }

    public async Task StoreBlog(Blog model)
    {
        model.BlogDate = DateTime.Now;
        context.Blogs.Add(model);
        await context.SaveChangesAsync();
    }
}