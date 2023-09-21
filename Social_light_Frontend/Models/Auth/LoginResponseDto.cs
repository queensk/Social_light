using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_light_Frontend.Models.Auth
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public UserDto User { get; set; } = default!;
    }
}