using Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthentication.Auth
{
    public class TokenProvider
    {
        public string CreateToken(AdminModels adminModel)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Iss,Environment.GetEnvironmentVariable("Issuer")),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,DateTime.Now.AddHours(2).ToString()),
                new Claim("FirstName", adminModel.FirstName),
                new Claim("Lastname", adminModel.LastName),
                //bu şekilde claim doldur
                new Claim(ClaimTypes.Role, "admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Key")));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Environment.GetEnvironmentVariable("Issuer"),
                Environment.GetEnvironmentVariable("Audience"),
                claims, DateTime.Now,
                DateTime.Now.AddHours(5),
                signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateToken(UserModels userModel)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Iss,Environment.GetEnvironmentVariable("Issuer")),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,DateTime.Now.AddHours(2).ToString()),
                new Claim("FirstName", userModel.FirstName),
                new Claim("Lastname", userModel.LastName),
                new Claim(ClaimTypes.Role, "user")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Key")));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Environment.GetEnvironmentVariable("Issuer"),
                Environment.GetEnvironmentVariable("Audience"),
                claims, DateTime.Now,
                DateTime.Now.AddHours(5),//5 saat sonra token ölür
                signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
