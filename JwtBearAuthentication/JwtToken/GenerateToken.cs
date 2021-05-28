using JwtBearAuthentication.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtBearAuthentication.JwtToken
{
    public static class GenerateToken
    {
        public static Token GetToken(string key, UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>(new Claim[]
            {
                new Claim("user", user.UserName)
            });

            var token = new JwtSecurityToken(null,
              null,
              claims: claims,
              expires: DateTime.UtcNow.AddMinutes(30),
              signingCredentials: credentials);

            return new Token()
            {
                token_type = "Bearer",
                expiration = token.Payload["exp"].ToString(),
                access_token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
