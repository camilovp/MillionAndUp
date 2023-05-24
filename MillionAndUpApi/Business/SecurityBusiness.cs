using Microsoft.IdentityModel.Tokens;
using MillionAndUpApi.DTO_s;
using MillionAndUpApi.Interfaces.Business;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MillionAndUpApi.Business
{
    public class SecurityBusiness : ISecurityBusiness
    {
        private readonly IConfiguration _configuration;

        public SecurityBusiness(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<SecurityTokenDTO> GetToken(CredentialsDTO credentialsDTO)
        {
            var emailAdmin = _configuration["EmailAdmin"];
            if (emailAdmin != credentialsDTO.Email) return null;
            return CreateToken(credentialsDTO);
        }

        private SecurityTokenDTO CreateToken(CredentialsDTO credentialsDTO)
        {
            var claims = new List<Claim>
            {
                new Claim("email", credentialsDTO.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["KeyJwt"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMonths(1);
            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: credentials);
            return new SecurityTokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
            };

        }
    }
}
