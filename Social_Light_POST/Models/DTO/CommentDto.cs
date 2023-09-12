using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Light_POST.Models.DTO
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public string? PostId { get; set; }
    }
}