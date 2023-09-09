using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Light_COMMENT.Models.DTO
{
    public class UserCommentsDTO
    {
        public string UserId {get; set;}
        public Guid CommentId {get; set;}
        public string postId {get; set;}
    }
}