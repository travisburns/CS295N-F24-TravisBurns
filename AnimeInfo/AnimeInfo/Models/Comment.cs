﻿using System.ComponentModel.DataAnnotations;

namespace AnimeInfo.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public AppUser CommentAuthor { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}