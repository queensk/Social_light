using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Social_Light_Auth.Models;
using Social_Light_Auth.Models.DTO.JWToptions;
using Social_Light_Auth.Service.IService;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Social_Light_Auth.Service
{
    public class JwtService: IJWtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        public JwtService(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }

        public string GenerateToken(ApplicationUser user, IEnumerable<string> roles)
        {
            var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var credentials = new SigningCredentials(key:SecretKey, algorithm: SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = DateTime.UtcNow.AddHours(3),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescription);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}