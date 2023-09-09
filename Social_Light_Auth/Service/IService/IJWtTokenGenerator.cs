using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social_Light_Auth.Models;

namespace Social_Light_Auth.Service.IService
{
    public interface IJWtTokenGenerator
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}