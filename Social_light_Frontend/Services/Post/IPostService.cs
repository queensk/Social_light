using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
﻿using Newtonsoft.Json;
using Social_light_Frontend.Models.Post;
using Social_light_Frontend.Models;

namespace Social_light_Frontend.Services.Post
{
    public interface IPostService
    {
        Task<List<PostDto>> GetPostsAsync(); 
        Task<PostDto> GetPostAsync(Guid id);
        Task<ResponseDto> CreatePostAsync(PostCreateDTO post);
        Task<ResponseDto> UpdatePostAsync(PostDto post);
        Task<bool> DeletePostAsync(Guid id);
        Task<List<PostDto>> GetPostsByUserIdAsync(Guid id);
    }
}