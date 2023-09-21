using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Social_light_Frontend.Models.Auth
{
    public class LoginRequestDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}