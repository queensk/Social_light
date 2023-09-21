using System.Text;
using Newtonsoft.Json;
using Social_light_Frontend.Models;
using Social_light_Frontend.Models.Auth;

namespace Social_light_Frontend.Services.Auth
{
    public class AuthService : IAuthInterface
    {
        private readonly HttpClient _httpClient;
        private readonly string BASEURL = "http://localhost:7001";
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var req = JsonConvert.SerializeObject(loginRequestDto);
            var bodyContent = new StringContent(req, Encoding.UTF8, "application/json");

            // communicate with backend ---api
            var response = await _httpClient.PostAsync($"{BASEURL}/api/User/login", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                return JsonConvert.DeserializeObject<LoginResponseDto>(results.Result.ToString());
            }
            return new LoginResponseDto();

        }

        public async Task<ResponseDto> Register(RegisterRequestDto registerRequestDto)
        {
            var req = JsonConvert.SerializeObject(registerRequestDto);
            Console.WriteLine(req);
            var bodyContent = new StringContent(req, Encoding.UTF8, "application/json");
            Console.WriteLine(bodyContent);

            // communicate with backend ---api
            var response = await _httpClient.PostAsync($"{BASEURL}/api/User/register", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            return results;
        }
    }
}