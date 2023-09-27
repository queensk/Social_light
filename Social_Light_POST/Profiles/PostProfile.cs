using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Social_Light_POST.Models;
using Social_Light_POST.Models.DTO;

namespace Social_Light_POST.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostRequestDto, Post>().ReverseMap();
            CreateMap<CommentDto, Comment>().ReverseMap();
            CreateMap<CommentDto, Comment>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<IEnumerable<CommentDto>, IEnumerable<Comment>>();
        }
    }
}