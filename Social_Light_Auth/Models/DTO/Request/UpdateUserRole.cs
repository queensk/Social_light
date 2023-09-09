using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Light_Auth.Models.DTO.Request
{
    public class UpdateUserRole
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}