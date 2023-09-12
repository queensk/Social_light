using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Light_POST.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? UserId { get; set; } 
        [NotMapped]
        public User? User { get; set; } 
        [NotMapped]
        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
    }
}