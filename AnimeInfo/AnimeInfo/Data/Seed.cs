using AnimeInfo.Data;
using AnimeInfo.Models;
using Microsoft.AspNetCore.Identity;

public static class SeedData
{
    public static void Seed(ApplicationDbContext context, IServiceProvider provider)
    {
        if (!context.Comments.Any())
        {
            var userManager = provider.GetRequiredService<UserManager<AppUser>>();

            // Create users with UserManager
            const string PASSWORD = "Secret1123";

            AppUser travisburns = new AppUser
            {
                UserName = "travisburns@gmail.com",
                Name = "Travis Burns",
                SignUpdate = DateTime.Now
            };
            AppUser testUser = new AppUser
            {
                UserName = "testuser@example.com",
                Name = "Test User",
                SignUpdate = DateTime.Now
            };

            var result1 = userManager.CreateAsync(travisburns, PASSWORD).GetAwaiter().GetResult();
            var result2 = userManager.CreateAsync(testUser, PASSWORD).GetAwaiter().GetResult();

            // Create a blog post
            var blog = new Blog
            {
                BlogTitle = "First Blog Post",
                BlogText = "Welcome to our first blog post!",
                BlogAuthor = travisburns, 
                BlogDate = DateTime.Now,
                BlogRating = 5
            };
            context.Blogs.Add(blog);
            context.SaveChanges();

            // Create comments
            var comments = new List<Comment>
            {
                new Comment
                {
                    CommentText = "This is a great article!",
                    CommentDate = DateTime.Parse("2024-03-01"),
                    CommentAuthor = travisburns,
                    Blog = blog
                },
                new Comment
                {
                    CommentText = "I learned a lot from this post.",
                    CommentDate = DateTime.Parse("2024-03-15"),
                    CommentAuthor = testUser,
                    Blog = blog
                },
                new Comment
                {
                    CommentText = "Looking forward to more content.",
                    CommentDate = DateTime.Parse("2024-03-20"),
                    CommentAuthor = travisburns,
                    Blog = blog
                }
            };
            context.Comments.AddRange(comments);
            context.SaveChanges();
        }
    }
}