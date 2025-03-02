using System.ComponentModel.DataAnnotations;

namespace AnimeInfo.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Comment text cannot be empty")]
        [StringLength(1000, ErrorMessage = "Comment cannot exceed 1000 Characters")]
        public string CommentText { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:yyyy - MM - dd}", ApplyFormatInEditMode = true)]
        public DateTime CommentDate { get; set; }
        public AppUser CommentAuthor { get; set; } = null!;

        public int BlogId { get; set; }
        public Blog Blog { get; set; } = null!;

        public ICollection<Reply> Replies { get; set; } = new List<Reply>();
    }
}