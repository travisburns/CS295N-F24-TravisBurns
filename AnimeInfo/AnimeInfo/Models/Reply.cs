using System.ComponentModel.DataAnnotations;

namespace AnimeInfo.Models
{
    public class Reply
    {
        [Key]
        public int ReplyId { get; set; }
        public string ReplyText { get; set; } = string.Empty;
        public DateTime ReplyDate { get; set; }
        public AppUser ReplyAuthor { get; set; } = null!;

        public int CommentId { get; set; }
        public Comment Comment { get; set; } = null!;
    }
}