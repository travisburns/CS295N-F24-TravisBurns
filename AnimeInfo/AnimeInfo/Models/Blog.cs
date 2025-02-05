using System.ComponentModel.DataAnnotations;

namespace AnimeInfo.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; } = string.Empty;
        public string BlogText { get; set; } = string.Empty;
        public DateTime BlogDate { get; set; }  
        public int BlogRating { get; set; }
        public AppUser BlogAuthor { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

