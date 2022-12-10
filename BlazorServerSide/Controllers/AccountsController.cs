using BlazorServerSide.Services;
using Entities.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorServerSide.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IConfigurationSection _jwtSettings;

        public AccountsController(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
            _jwtSettings = _config.GetSection("jwtSettings");
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserForRegistrationDto userForRegistiration)
        {
            try
            {
                var user = new IdentityUser { UserName = userForRegistiration.Email , Email = userForRegistiration.Email };
                var result = await _userManager.CreateAsync(user,userForRegistiration.Password);

                
                if(!result.Succeeded) 
                {
                    var erros = result.Errors.Select(e => e.Description);
                    return BadRequest(new RegistrationResponseDto { Errors= erros });   
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new ServiceResponse();
                response.Success = false;
                response.Message= ex.Message;   
                return BadRequest(response);    
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserForAuthenticationDto authenticationDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(authenticationDto.Email);
                if(user==null|| !await _userManager.CheckPasswordAsync(user, authenticationDto.Password))
                    return Unauthorized(new AuthResponseDto { ErrorMessage="Invalid Email or Password"});

                var signin = GetSigningCredentials();
                var claims = GetClaims(user);
                var tokenOpt = GenerateTokenOptions(signin, claims);
                var token= new JwtSecurityTokenHandler().WriteToken(tokenOpt);
                
                return Ok(new AuthResponseDto { IsAuthSuccessful=true, Token=token});


            }
            catch (Exception ex)
            {
                var response = new ServiceResponse();
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }


        }//eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcmFAZXhhbXBsZS5jb20iLCJleHAiOjE2NzA2NjcwMTQsImlzcyI6IkJ1cmFrX1RhbWluY2UiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo1MDExLyJ9.btE53T1ddcvmg4E2NQOvQCZ6oirBjJLBFi4ERO69b5g
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings["securityKey"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private  List<Claim> GetClaims(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Email)
            };
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOpt = new JwtSecurityToken(
                issuer: _jwtSettings["validIssuer"],
                audience: _jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
                signingCredentials: signingCredentials
                );
            return tokenOpt;

        }

    }
}
