using AnimeInfo.Models;
namespace AnimeInfo.Data
{
    public static class SeedData
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Comments.Any())
            {
                // First create and save users
                var user1 = new AppUser
                {
                    Name = "John Smith",
                    SignUpdate = DateTime.Now
                };
                var user2 = new AppUser
                {
                    Name = "Jane Doe",
                    SignUpdate = DateTime.Now
                };

                context.AppUsers.AddRange(user1, user2);
                context.SaveChanges();

                // Create a blog post first
                var blog = new Blog
                {
                    BlogTitle = "First Blog Post",
                    BlogText = "Welcome to our first blog post!",
                    BlogAuthor = user1,
                    BlogDate = DateTime.Now,
                    BlogRating = 5
                };

                context.Blogs.Add(blog);
                context.SaveChanges();

                // Now create comments connected to the blog
                var comments = new List<Comment>
                {
                    new Comment
                    {
                        CommentText = "This is a great article!",
                        CommentDate = DateTime.Parse("2024-03-01"),
                        CommentAuthor = user1,
                        Blog = blog
                    },
                    new Comment
                    {
                        CommentText = "I learned a lot from this post.",
                        CommentDate = DateTime.Parse("2024-03-15"),
                        CommentAuthor = user2,
                        Blog = blog
                    },
                    new Comment
                    {
                        CommentText = "Looking forward to more content.",
                        CommentDate = DateTime.Parse("2024-03-20"),
                        CommentAuthor = user1,
                        Blog = blog
                    }
                };

                context.Comments.AddRange(comments);
                context.SaveChanges();
            }
        }
    }
}