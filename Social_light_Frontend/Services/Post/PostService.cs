using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social_light_Frontend.Models.Post;
using Social_light_Frontend.Models;

namespace Social_light_Frontend.Services.Post
{

    public class PostService: IPostService
    {
        private readonly HttpClient _httpClient;
        private readonly string BASEURL = "https://localhost:7004"; 
        public PostService(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }

        public async Task<List<PostDto>> GetPostsAsync() 
        {
            throw new NotImplementedException();
        }
        public async Task<PostDto> GetPostAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<PostDto> CreatePostAsync(PostDto post)
        {
            throw new NotImplementedException();
        }
        public async Task<PostDto> UpdatePostAsync(PostDto post)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeletePostAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}