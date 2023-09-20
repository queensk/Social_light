using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_light_Frontend.Models.Post
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? PostId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}