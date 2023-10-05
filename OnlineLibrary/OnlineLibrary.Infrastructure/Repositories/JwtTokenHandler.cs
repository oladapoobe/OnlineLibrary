using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineLibrary.Application.Contracts.Persistence;
using OnlineLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Infrastructure.Repositories
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtTokenHandler(IConfiguration configuration)
        {
            _secretKey = configuration["JwtSecretKey"];
            _issuer = configuration["Issuer"];
            _audience = configuration["Audience"];

        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

            var key = Encoding.UTF8.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return  tokenhandler.WriteToken(token);
        }
    }
}





