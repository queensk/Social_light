using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Social_light_Frontend.Models;
using Social_light_Frontend.Models.Comment;

namespace Social_light_Frontend.Services.Comment
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;
        private readonly string BASEURL = "https://localhost:5251"; 
        public CommentService(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }
        public async Task<List<CommentDto>> GetPostComments(Guid postId)
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Comments/posts/comments/{postId}");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                return JsonConvert.DeserializeObject<List<CommentDto>>(results.Result.ToString());
            }
            return new List<CommentDto>();
        }

        public async Task<ResponseDto> CreateComment(CommentCreateDto commentCreateDto)
        {
            var req = JsonConvert.SerializeObject(commentCreateDto);
            var bodyContent = new StringContent(req, Encoding.UTF8, "application/json");
            Console.WriteLine(bodyContent);
            var response = await _httpClient.PostAsync($"{BASEURL}/api/Comments", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            return results;
        }
    }
}