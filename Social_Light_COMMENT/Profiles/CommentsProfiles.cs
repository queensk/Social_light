using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Social_Light_COMMENT.Models;
using Social_Light_COMMENT.Models.DTO;

namespace Social_Light_COMMENT.Profiles
{
    public class CommentsProfiles: Profile
    {
        public CommentsProfiles(){
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Comment, CommentCreateDto>().ReverseMap();
        }
    }
}