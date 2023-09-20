using Newtonsoft.Json;
using Social_light_Frontend.Models;
using Social_light_Frontend.Models.Post;
using Social_light_Frontend.Services.Post;

namespace Social_light_Frontend.Services
{
    public class PostService : IPostInterface
    {
        private readonly HttpClient _httpClient;
        private readonly string BASEURL = "https://localhost:7004";
        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PostDto>> GetPostsAsync()
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Post");
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<IEnumerable<ResponseDto>>(content);

            if (results.IsSuccess && results != null)
            {
                JsonConvert.DeserializeObject<IEnumerable<PostRequestDto>>() results.Results.ToString
            }

        }
    }
}