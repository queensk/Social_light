using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social_Light_Auth.Models;
using Social_Light_Auth.Models.DTO;
using AutoMapper;
using Social_Light_Auth.Models.DTO.Response;

namespace Social_Light_Auth.AuthProfile
{
    public class AuthProfiles : Profile
    {
        public AuthProfiles()
        {
            CreateMap<UserDTO, ApplicationUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(u => u.Email));
            CreateMap<ApplicationUser, UserResponseDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            // CreateMap<ApplicationUser, UserLoginDTO>().ReverseMap();
            // CreateMap<ApplicationUser, UserRegisterDTO>().ReverseMap();
        }
    }
}
