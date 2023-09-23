using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social_light_Frontend.Models.Auth;
using Social_light_Frontend.Models.Post;

namespace Social_light_Frontend.Models.Comment
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
        public string PostId { get; set; }
        public PostDto? Post { get; set; }
    }
}