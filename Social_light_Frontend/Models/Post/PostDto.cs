using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social_light_Frontend.Models.Comment;
namespace Social_light_Frontend.Models.Post
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? UserId { get; set; } 
        // public User? User { get; set; } 
        public IEnumerable<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}