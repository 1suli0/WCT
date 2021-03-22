using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WCT.Core;
using WCT.Infrastructure.DTOs.Output;

namespace WCT.Infrastructure.Utilities
{
    public static class JWT
    {
        public static OutTokenDTO Generate(User user,
           IConfiguration configuration, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var signinKey = Encoding.UTF8.GetBytes(configuration
                .GetValue<string>("JwtSettings:secretKey"));

            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("userId", user.Id.ToString()) };

            if (roles != null)
                claims.AddRange(roles
                    .Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = configuration.GetValue<string>("JwtSettings:validIssuer"),

                Audience = configuration.GetValue<string>("JwtSettings:validAudience"),

                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.Add(configuration
                .GetValue<TimeSpan>("JwtSettings:expires")),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signinKey),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new OutTokenDTO
            {
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}