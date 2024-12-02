using System.ComponentModel.DataAnnotations;

namespace AnimeInfo.Models
{
    public class Blog
    {
      

        [Key]
        public int Id { get; set; }  // Primary key
        public string BlogTitle { get; set; }
        public string BlogText { get; set; }
        public AppUser BlogAuthor { get; set; }
        public DateTime BlogDate { get; set; }
        public int BlogRating { get; set; }
        
    }
}