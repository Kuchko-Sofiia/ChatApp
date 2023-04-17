using ChatApp.DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChatApp.BLL.Services.Interfaces
{
    public interface IJwtTokenService
    {
        public JwtSecurityToken CreateJwtToken(User user);
        public string CreateToken(User user);
        public  string CreateRefreshToken();
        public List<Claim> GetClaims(User user);
        public SigningCredentials CreateSigningCredentials();
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
        public DateTime GetRefreshTokenExpiryTime();
    }
}
