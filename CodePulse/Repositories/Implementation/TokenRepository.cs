using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CodePulse.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CodePulse.Repositories.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJWTAsync(IdentityUser user, List<string> roles)
        {
            //Create Claims 
            var claims = new List<Claim>(){
                new Claim(ClaimTypes.Email,
                user.Email)
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)
            ));

            //JWT security Token paramters
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]));
            var credintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: configuration["Jwt:Issuer"], audience: configuration["Jwt:Audience"],
            claims: claims, expires: DateTime.Now.AddMinutes(50), signingCredentials: credintials);

            //return token
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}