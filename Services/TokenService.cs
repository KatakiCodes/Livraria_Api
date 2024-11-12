using LivrariaAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace LivrariaAPI.Services
{
    public static class TokenService
    {
        private static JwtSecurityTokenHandler _TokenHandler  = new JwtSecurityTokenHandler();
        private static SecurityTokenDescriptor _TokenDescriptor;
        private static string SecretKey = Settings._JwtSecretKey;

        public static string GerarToken(UserModel user)
        {
            var secreteKey = Encoding.ASCII.GetBytes(Settings._JwtSecretKey);
            _TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secreteKey),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = _TokenHandler.CreateToken(_TokenDescriptor);
            return _TokenHandler.WriteToken(token);
        }
    }
}
