using System.ComponentModel.DataAnnotations;

namespace AnimeInfo.Models
{
    public class Reply
    {
        [Key]
        public int ReplyId { get; set; }

        [Required(ErrorMessage = "Reply text is required")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Reply must be between 2 and 500 characters")]
        public string ReplyText { get; set; } = string.Empty;
        public DateTime ReplyDate { get; set; }
        public AppUser ReplyAuthor { get; set; } = null!;

        public int CommentId { get; set; }
        public Comment Comment { get; set; } = null!;
    }
}