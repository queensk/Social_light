using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social_light_Frontend.Models.Auth;
using Social_light_Frontend.Models;

namespace Social_light_Frontend.Services.Auth
{
    public interface IAuthInterface
    {
        Task<ResponseDto> Register(RegisterRequestDto registerRequestDto);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}