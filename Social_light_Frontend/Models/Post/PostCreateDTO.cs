using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_light_Frontend.Models.Post
{
    public class PostCreateDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public string? PostImage { get; set; }
    }
}