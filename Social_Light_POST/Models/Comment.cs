using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Light_POST.Models
{
    [NotMapped]
    public class Comment
    {
            public Guid Id { get; set; }
            public string? Content { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public string UserId { get; set; } = string.Empty;
            public User? User { get; set; } 
            public string? PostId { get; set; }
            public Post? Post { get; set; }
    }
}