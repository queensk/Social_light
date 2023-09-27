using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Social_light_Frontend.Models.Comment
{
    public class CommentCreateDto
    {
        [Required]
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string PostId { get; set; }  = string.Empty;
    }
}