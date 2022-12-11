using BlazorServerSide.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorServerSide.TokenHelpers
{
    public interface ITokenService
    {
        SigningCredentials SigningCredentials();
        Task<List<Claim>> GetClaims(User user);
        JwtSecurityToken GenerateTokenOpt(SigningCredentials signingCredentials,List<Claim> claims);
        string GenerateRefreshToken();    
        ClaimsPrincipal GetPrinicipalFromExpiredToken(string token);    
    }
}
