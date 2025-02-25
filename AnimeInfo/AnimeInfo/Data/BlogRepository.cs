using AnimeInfo.Data;
using AnimeInfo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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
                 .Include(blog => blog.Comments)
            .ThenInclude(comment => comment.Replies)
                .ThenInclude(reply => reply.ReplyAuthor)
            .ToListAsync();
    }

    public async Task <Blog?> GetBlogById(int id)
    {
        return await context.Blogs
            .Include(blog => blog.BlogAuthor)
            .Include(blog => blog.Comments)
                .ThenInclude(comment => comment.CommentAuthor)
                .Include(blog => blog.Comments)
                 .ThenInclude(comment => comment.Replies) 
                .ThenInclude(reply => reply.ReplyAuthor)
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

    public async Task AddReply(Reply reply)
    {
        context.Replies.Add(reply);
        await context.SaveChangesAsync();
    }

    public async Task<Comment?> GetCommentById(int id)
    {
        return await context.Comments
            .Include(c => c.CommentAuthor)
            .Include(c => c.Blog)
            .Include(c => c.Replies)
                .ThenInclude(r => r.ReplyAuthor)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}