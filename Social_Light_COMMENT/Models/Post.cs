using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Light_COMMENT.Models
{
    [NotMapped]
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public User User { get; set; } // Navigation property
        public List<Comment> Comments { get; set; }
    }
}