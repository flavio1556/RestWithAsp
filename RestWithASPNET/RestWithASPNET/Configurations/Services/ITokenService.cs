using System.Collections.Generic;
using System.Security.Claims;

namespace RestWithASPNET.Configurations.Services
{
    public interface ITokenService
    {
        string GenerateAcessToken(IEnumerable<Claim> claims);
        string GenerateRefrashToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
