using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Social_Light_POST.Models.DTO;
using Social_Light_POST.Services.IService;

namespace Social_Light_POST.Services
{
    public class CommentsService : ICommentService
    {
        private readonly IHttpClientFactory _clientFactory;
        public CommentsService(IHttpClientFactory clientFactory)
        {

            _clientFactory = clientFactory;

        }
        public async Task<CommentDto> GetPostCommentsData(string postId)
        {
            var client = _clientFactory.CreateClient("Comments");
            var response = await client.GetAsync($"/api/Comments/posts/comments/{postId}");
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (responseDto.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CommentDto>(Convert.ToString(responseDto.Result));
            }
            return new CommentDto();
        }
    }
}