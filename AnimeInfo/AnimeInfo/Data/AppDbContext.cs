using AnimeInfo.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeInfo.Data
{
    public class ApplicationDbContext : DbContext

    {

        // constructor just calls the base class constructor

        public ApplicationDbContext(

           DbContextOptions<ApplicationDbContext> options) : base(options) { }



        // one DbSet for each domain model class//

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public object Reviews { get; internal set; }

        public DbSet<Comment> Comments { get; set; }
    }
}


