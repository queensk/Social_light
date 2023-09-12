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
        public async Task<IEnumerable<CommentDto>> GetAllCommentsData(string postId)
        {
            try
            {
                var client = _clientFactory.CreateClient("Comments");
                var response = await client.GetAsync($"/api/Comments/posts/comments/{postId}");
                var content = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);

                if (responseDto.IsSuccess)
                {
                    return JsonConvert.DeserializeObject<List<CommentDto>>(Convert.ToString(responseDto.Result));
                }
                return new List<CommentDto>();
            }
            catch (Exception ex)
            {
                return new List<CommentDto>();
            }

        }
    }
}