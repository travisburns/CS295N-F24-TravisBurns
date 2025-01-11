using AnimeInfo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace AnimeInfo.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>

    {

        // constructor just calls the base class constructor

        public ApplicationDbContext(

           DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            Blogs = Set<Blog>();
            Comments = Set<Comment>();
        }



        // one DbSet for each domain model class//

      
        public DbSet<Blog> Blogs { get; set; }
    

        public DbSet<Comment> Comments { get; set; }
    }
}


