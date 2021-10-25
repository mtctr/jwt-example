using jwt_example.Models.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace jwt_example.Services.Token
{
    public class TokenService : ITokenService
    {
        private TimeSpan ExpiryDuration = TimeSpan.FromMinutes(30);
        
        public string BuildToken(string key, UserDTO user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            
            var tokenDescriptor = new JwtSecurityToken(
                    claims: claims, 
                    expires: DateTime.Now.Add(ExpiryDuration), 
                    signingCredentials: credentials);

            
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
