using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social_light_Frontend.Models.Post;
using Social_light_Frontend.Models;
using Newtonsoft.Json;
using System.Text;

namespace Social_light_Frontend.Services.Post
{

    public class PostService: IPostService
    {
        private readonly HttpClient _httpClient;
        private readonly string BASEURL = "https://sociallightgateway20230929160142.azurewebsites.net"; 
        public PostService(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }

        public async Task<List<PostDto>> GetPostsAsync() 
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Posts");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                return JsonConvert.DeserializeObject<List<PostDto>>(results.Result.ToString());
            }
            return new List<PostDto>();
        }
        
        public async Task<PostDto> GetPostAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Posts/GetById?Id={id}");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                return JsonConvert.DeserializeObject<PostDto>(results.Result.ToString());
            }
            return new PostDto();
        }
        
        public async Task<ResponseDto> CreatePostAsync(PostCreateDTO post)
        {
            var req = JsonConvert.SerializeObject(post);
            var bodyContent = new StringContent(req, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{BASEURL}/api/Posts", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            return results;
        }
        public async Task<ResponseDto> UpdatePostAsync(PostDto post)
        {
            var req = JsonConvert.SerializeObject(post);
            var bodyContent = new StringContent(req, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BASEURL}/api/Posts", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            return results;
        }
        
        public async Task<bool> DeletePostAsync(Guid id)
        {
            // '/api/Posts?Id=3fa85f64-5717-4562-b3fc-2c963f66afa6
            var response = await _httpClient.DeleteAsync($"{BASEURL}/api/Posts?Id={id}");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            return results.IsSuccess;
        }
        
        public async Task<List<PostDto>> GetPostsByUserIdAsync(Guid id)
        {
            Console.WriteLine("GetPostsByUserIdAsync");
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Posts/GetPostAndComments?userId={id}");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                Console.WriteLine(results.Result.ToString());
                return JsonConvert.DeserializeObject<List<PostDto>>(results.Result.ToString());
            }
            return new List<PostDto>();
        }
    }
}