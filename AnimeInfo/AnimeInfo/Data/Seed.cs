using AnimeInfo.Data;
using AnimeInfo.Models;
using Microsoft.AspNetCore.Identity;

public static class SeedData
{
    public static async Task Seed(ApplicationDbContext context, IServiceProvider provider)
    {
        // First seed the admin role and user
        var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = provider.GetRequiredService<UserManager<AppUser>>();

        // Create admin role if it doesn't exist
        string adminRole = "Admin";
        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        // Create admin user if it doesn't exist
        string adminEmail = "admin@animeinfo.com";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                Name = "Admin User",
                SignUpdate = DateTime.Now
            };

            var result = await userManager.CreateAsync(adminUser, "Admin#123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }
        }

        // Only seed other data if Comments table is empty
        if (!context.Comments.Any())
        {
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

            var result1 = await userManager.CreateAsync(travisburns, PASSWORD);
            var result2 = await userManager.CreateAsync(testUser, PASSWORD);

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
            await context.SaveChangesAsync();

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
            await context.SaveChangesAsync();
        }
    }
}