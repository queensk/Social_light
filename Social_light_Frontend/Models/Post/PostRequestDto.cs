using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Social_light_Frontend.Models.Post
{
    public class PostRequestDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string? Content { get; set; }
        [Required]
        [NotMapped]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}