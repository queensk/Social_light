using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Light_COMMENT.Models.DTO
{
    public class CommentCreateDto
    {
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostId { get; set; }   
    }
}