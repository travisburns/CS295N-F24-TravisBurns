using AnimeInfo.Models;

namespace AnimeInfo.Models
{
    public class Blog
    {
     public string BlogTitle { get; set; }
     public string BlogText { get; set;}
     public AppUser BlogAuthor { get; set;}
     public DateTime BlogDate { get; set;}
     public int BlogRating { get; set;  }        
     


    }
}
