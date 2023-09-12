using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Light_POST.Models.DTO
{
    public class UserPostsAndCommentsDto
    {
        public Post? Post { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}