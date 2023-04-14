using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.BLL.Services.Interfaces
{
    public interface IJwtTokenService
    {
        public string GenerateToken(Claim[] claims, DateTime? expires = null);
        public bool ValidateToken(string token, out JwtSecurityToken jwtToken);
    }
}
