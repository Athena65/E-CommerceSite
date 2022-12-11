using BlazorServerSide.Data;
using BlazorServerSide.Services;
using BlazorServerSide.TokenHelpers;
using Entities.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BlazorServerSide.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public TokenController(UserManager<User> userManager,ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        [HttpPost]
        public async Task<IActionResult> Refresh(RefreshTokenDto refreshToken)
        {
            try
            {
                var principal = _tokenService.GetPrinicipalFromExpiredToken(refreshToken.Token);
                var username = principal.Identity.Name;

                var user = await _userManager.FindByEmailAsync(username);

                var signingCredentials = _tokenService.SigningCredentials();
                var claims = await _tokenService.GetClaims(user);
                var tokenopt=_tokenService.GenerateTokenOpt(signingCredentials, claims);
                var token= new JwtSecurityTokenHandler().WriteToken(tokenopt);
                user.RefreshToken = _tokenService.GenerateRefreshToken();

                await _userManager.UpdateAsync(user);   
                return Ok(new AuthResponseDto { Token = token,RefreshToken=user.RefreshToken,IsAuthSuccessful=true });   

            }
            catch (Exception ex)
            {
                var response = new ServiceResponse();
                response.Success = false;
                response.Message= ex.Message;
                return BadRequest(response);
            }
        }
    }
}
