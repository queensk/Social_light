using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Social_light_Frontend.Models.Post;

namespace Social_light_Frontend.Services.Post
{
    public interface IPostInterface
    {
        Task<IEnumerable<PostDto>> GetPostsAsync();
    }
}