using System.ComponentModel.DataAnnotations;

namespace AnimeInfo.Models
{
    public class Blog
    {
        public int BlogId { get; set; }

        [Required(ErrorMessage = "Blog title is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters")]
        public string BlogTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Blog content is required")]
        [MinLength(10, ErrorMessage = "Blog content must be at least 10 characters")]
        public string BlogText { get; set; } = string.Empty;
        public DateTime BlogDate { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int BlogRating { get; set; }
        public AppUser BlogAuthor { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

